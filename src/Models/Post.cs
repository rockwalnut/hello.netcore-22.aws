using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hello.netcore_22.aws.Models
{
    public class Post
    {
        public string Id { get; set; }

         public string BlogId { get; set; }
        //public Clan Clan { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }
        public string Status { get; set; }

         public string Url { get; set; }


        public DateTime Published { get; set; }
        
    }
}