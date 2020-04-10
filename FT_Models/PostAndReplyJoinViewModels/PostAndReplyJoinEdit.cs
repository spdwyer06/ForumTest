using FT_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Models.PostAndReplyJoinViewModels
{
    public class PostAndReplyJoinEdit
    {
        public int ID { get; set; }

        public int PostID { get; set; }
        public string PostContent { get; set; }
        public virtual Post Post { get; set; }

        public int ReplyID { get; set; }
        public string ReplyContent { get; set; }
        public virtual  PostReply Reply { get; set; }
    }
}
