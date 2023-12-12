// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disable = true;
var post1 = document.getElementById("postID").value;
var userLogin = document.getElementById("userInput").value;

console.log(userLogin);




connection.start().then(function () {
    connection.invoke("GetCommmentListByPostID", post1);
   
                    }).catch(function (err) {
                        return console.error(err.toString());
                    });

connection.on("ReceiveMessage", function (ListComment) {
    console.log("Receive Message", ListComment);

    for (var i = 0; i < ListComment.length; i++) {
        var comment = ListComment[i];
        console.log(comment.userId.toString());
        var userID = comment.userId.toString();

        // Check if comment already exists in data div
        var existingCommentDiv = document.querySelector(`[data-comment-id="${comment.id}"]`);
        if (existingCommentDiv) {
            continue; // Comment already exists, move on to next comment
        }

        var html = `<div class="card p-3" data-comment-id="${comment.id}">
        <div class="d-flex justify-content-between align-items-center">
            <div class="user d-flex flex-row align-items-center">
                <img src="https://i.imgur.com/hczKIze.jpg" width="30" class="user-img rounded-circle mr-2">
                    <span>
                        <small class="font-weight-bold text-primary">${comment.user.fullName}</small>
                        <small class="font-weight-bold">${comment.content}</small>
                    </span>
                    </div>
                <small>${comment.createdAt}</small>
            </div>
            <div class="action d-flex justify-content-between mt-2 align-items-center">
                <div class="reply px-4">
                    ${userID === userLogin ? `<span class='delete-car-btn' data-car-id=${comment.id}><i class="bi bi-trash"></i></span>` : ''}
                </div>
            </div>
        </div>`;
        var div = document.createElement("div");
        div.innerHTML = html;
        document.getElementById("data").appendChild(div);
    }
});

$(document).on("click", ".delete-car-btn", function () {
    var id = $(this).data("car-id");
    console.log(id)
    connection.invoke("DeleteComment", id)
        .then(function () {
            console.log("Comment deleted successfully.");
        })
        .catch(function (err) {
            console.error("Error deleting comment:", err.toString());
        });
});

connection.on("ReceiveDeletedComment", function (id) {
    console.log("Car deleted:", id);
    var comment = document.querySelector(`[data-comment-id="${id}"]`);
    if (comment) {
        comment.remove();
    }
    
});

connection.on("ReceiveNewMessage", function (comment) {
    console.log("New Comment Received: ", comment);
    var userID = comment.userId.toString();

    // Add the new comment to the UI
    var html = `<div class="card p-3" data-comment-id="${comment.id}">
        <div class="d-flex justify-content-between align-items-center">
            <div class="user d-flex flex-row align-items-center">
                <img src="https://i.imgur.com/hczKIze.jpg" width="30" class="user-img rounded-circle mr-2">
                    <span>
                        <small class="font-weight-bold text-primary">${comment.user.fullName}</small>
                        <small class="font-weight-bold">${comment.content}</small>
                    </span>
                    </div>
                <small>${comment.createdAt}</small>
            </div>
            <div class="action d-flex justify-content-between mt-2 align-items-center">
                <div class="reply px-4">
                    ${userID === userLogin ? `<span class='delete-car-btn' data-car-id=${comment.id}><i class="bi bi-trash"></i></span>` : ''}
                </div>
            </div>
        </div>`;
    var div = document.createElement("div");
    div.innerHTML = html;
    document.getElementById("data").appendChild(div);
    $("#messageInput").val('');
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var post = document.getElementById("postID").value;
    connection.invoke("SendMessage", user, post, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
