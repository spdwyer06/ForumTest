using FT_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Models.PostViewModels
{
    public class PostListItem
    {
        public int PostID { get; set; }

        public int ThreadID { get; set; }
        public virtual Thread Thread { get; set; }

        public Guid PostCreator { get; set; }

        public DateTimeOffset PostCreated { get; set; }

        public int NumberOfReplies
        {
            get
            {
                if (Replies != null)
                {
                    return Replies.Count();

                    //var postCount = Posts.Count();
                    ////return Posts.Count();

                    //foreach (var item in Posts)
                    //{
                    //    if (item.Replies != null)
                    //        postCount += item.Replies.Count();
                    //}

                    //return postCount;
                }

                return 0;
            }
        }

        public virtual ICollection<PostReply> Replies { get; set; }
    }
}
