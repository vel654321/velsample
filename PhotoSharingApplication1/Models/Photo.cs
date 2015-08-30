using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PhotoSharingApplication1.Models
{
    public class Photo
    {
        public int photoId { get; set; }
        public string Title { get; set; }

        [DisplayName("Picture")]
        public byte[] PhotoFile { get; set; }

        [DataType(DataType.MultilineText)]
        public string ImageMimeType { get; set; }

        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("CreatedDate")]
        [DisplayFormat(DataFormatString="{0:MM/dd/yy}")]
        public DateTime CreatedDate { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.MultilineText)]
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Photo photo { get; set; }
    }
}