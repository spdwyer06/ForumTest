using FT_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Models.ThreadViewModels
{
    public class ThreadDetail
    {
        public int ThreadID { get; set; }

        public Guid ThreadCreator { get; set; }

        public string ThreadTitle { get; set; }

        public string ThreadDescription { get; set; }

        public DateTimeOffset ThreadCreated { get; set; }

        public int NumberOfPost
        {
            get
            {
                if (Posts != null)
                    return Posts.Count();

                return 0;
            }
        }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
