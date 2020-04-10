using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Data
{
    public class Thread
    {
        [Key]
        public int ThreadID { get; set; }

        public Guid ThreadCreator { get; set; }

        public string ThreadTitle { get; set; }

        public string ThreadDescription { get; set; }

        public DateTimeOffset ThreadCreated { get; set; }

        //public virtual IEnumerable<Post> Posts { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
