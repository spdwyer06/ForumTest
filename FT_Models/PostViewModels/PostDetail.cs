using FT_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Models.PostViewModels
{
    public class PostDetail
    {
        public int PostID { get; set; }

        public int ThreadID { get; set; }
        public virtual Thread Thread { get; set; }

        public Guid PostCreator { get; set; }

        public string PostContent { get; set; }

        public DateTimeOffset PostCreated { get; set; }

        public int NumberOfReplies
        {
            get
            {
                if (Replies != null)
                    return Replies.Count();

                return 0;
            }
        }


        public virtual IEnumerable<PostReply> Replies { get; set; }
    }
}
