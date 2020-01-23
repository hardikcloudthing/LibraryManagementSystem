using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryModels
{
    public class Author
    {
        [Column("AuthorId")]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}