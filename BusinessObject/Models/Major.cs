using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Major
    {
        public Major()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Input Major Name!")]
        public string MajorName { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
