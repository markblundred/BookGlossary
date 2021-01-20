using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTestMvc.Models
{
    public class GenreModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public IList<BookModel> Books { get; set; }
    }
}
