using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoSharingApplication1.Models
{
    public class Comment
    {
        public int CommentID { get; set; }

        public int PhotoID {get; set;}

        [Required]
        [StringLength(250)]
        public string UserName { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }


    }
}