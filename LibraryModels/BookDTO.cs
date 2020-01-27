using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryModels
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }

        public string AuthorName { get; set; }
    }
}