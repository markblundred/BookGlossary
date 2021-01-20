using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTestMvc.Models
{
    public class ReviewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public DateTime ReviewedOn { get; set; }
    }
}
