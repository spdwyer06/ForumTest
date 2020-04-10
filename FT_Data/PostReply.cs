
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Data
{
    public class PostReply
    {
        [Key]
        public int ReplyID { get; set; }

        public int PostID { get; set; }
        public virtual Post Post { get; set; }

        public Guid ReplyCreator { get; set; }

        public string ReplyContent { get; set; }

        public DateTimeOffset ReplyCreated { get; set; }

    }
}
