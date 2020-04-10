using FT_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Models.PostReplyViewModels
{
    public class PostReplyCreate
    {
        public int PostID { get; set; }
        public virtual Post Post { get; set; }

        public string ReplyContent { get; set; }
    }
}
