using FT_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Models.ThreadViewModels
{
    public class ThreadListItem
    {
        public int ThreadID { get; set; }

        public string ThreadTitle { get; set; }

        public Guid ThreadCreator { get; set; }

        public DateTimeOffset ThreadCreated { get; set; }

        public int NumberOfPost
        {
            get
            {
                if (Posts != null)
                {
                    var postCount = Posts.Count();
                    //return Posts.Count();

                    foreach (var item in Posts)
                    {
                        if (item.Replies != null)
                            postCount += item.Replies.Count();
                    }

                    return postCount;
                }

                return 0;
            }
        }

        public virtual ICollection<Post> Posts { get; set; } //= new List<Post>();
    }
}
