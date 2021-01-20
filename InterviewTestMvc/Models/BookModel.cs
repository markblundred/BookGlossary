using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTestMvc.Models
{
    public class BookModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string FirstPublished { get; set; }
        public AuthorModel Author { get; set; }
        public IList<GenreModel> Genres { get; set; }
        public int NumberOfReviews { get; set; }
    }
}
