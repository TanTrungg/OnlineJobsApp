using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Post
    {
        public Post()
        {
            Candidates = new HashSet<Candidate>();
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public int? ComId { get; set; }
        public int? MajorId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public int? Status { get; set; }

        [Required]
        public string Skill { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsReported { get; set; }
        public virtual Company Com { get; set; }
        public virtual Major Major { get; set; }
        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
