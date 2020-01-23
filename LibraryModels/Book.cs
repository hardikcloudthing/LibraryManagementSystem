using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryModels
{
    public class Book
    {
        [Column("BookId")]
        public Guid Id { get; set; }

        public string ISBN { get; set; }
        public string Title { get; set; }

        [ForeignKey("AuthoId")]
        public Author Author { get; set; }
    }
}