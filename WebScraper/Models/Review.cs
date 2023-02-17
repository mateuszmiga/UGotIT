using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.Models
{
    public class Review
    {
        public string UserName { get; set; }
        public string Rating { get; set; }
        public string ReviewContent { get; set; }
        public string SourcePage { get; set; }
    }
}
