using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineJobsApp
{
    public class ChatHub : Hub
    {
        private OnlineJobSeekingManagementContext _Context = new OnlineJobSeekingManagementContext();

        ICommentRepository commentRepository = new CommentRepository();

        public async Task GetCommmentListByPostID(string postID)
        {
            List<Comment> ListComment = await _Context.Comments.Where(c => c.PostId == int.Parse(postID)).ToListAsync();
            foreach (Comment comment in ListComment)
            {
                var userRepo = new UserRepository();
                User userObj = userRepo.getUserByID((int)comment.UserId);
                comment.User = userObj;
            }
            await Clients.All.SendAsync("ReceiveMessage", ListComment);
        }

        public async Task SendMessage (string user, string post, string message)
        {

            Comment comment = new Comment();
            comment.UserId = int.Parse(user);
            comment.PostId = int.Parse(post);
            comment.Content = message;

            commentRepository.AddComment(comment);

            var userRepo = new UserRepository();
            User userObj = userRepo.getUserByID(int.Parse(user));
            comment.User = userObj;

            await Clients.All.SendAsync("ReceiveNewMessage", comment);
        }
        public async Task DeleteComment(int id)
        {
            var car = await _Context.Comments.FindAsync(id);
            if (car != null)
            {
                _Context.Comments.Remove(car);
                await _Context.SaveChangesAsync();
                await Clients.All.SendAsync("ReceiveDeletedComment", id);
            }
        }

        

        


    }
}
