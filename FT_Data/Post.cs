using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Data
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        public int ThreadID { get; set; }
        public virtual  Thread Thread { get; set; }

        public Guid PostCreator { get; set; }

        public string PostContent { get; set; }

        public DateTimeOffset PostCreated { get; set; }

        // Left this one as IEnumerable just to see what happens // Doesn't seem like LINQ likes IEnumerable
        // If it doesn't work switch to ICollection
        public virtual ICollection<PostReply> Replies { get; set; }
    }
}
