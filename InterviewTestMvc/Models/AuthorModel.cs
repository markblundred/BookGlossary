
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTestMvc.Models
{
    public class AuthorModel
    {
        public long Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Initials { get; set; }
    }
}
