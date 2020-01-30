using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryModels
{
    /// <summary>
    /// It contain ID, ISBN, Title and Author Detail.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Book ID.
        /// </summary>
        [Column("BookId")]
        public int Id { get; set; }

        /// <summary>
        /// ISBN Number of Book.
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Title of Book.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Status of Book. ('True' means book is removed)
        /// </summary>
        public bool BookIsRemoved { get; set; } = false;

        /// <summary>
        /// Author Detail.
        /// </summary>
        [ForeignKey("AuthoId")]
        public virtual Author Author { get; set; }
    }
}