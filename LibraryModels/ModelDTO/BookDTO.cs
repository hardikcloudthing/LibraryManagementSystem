using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryModels
{
    /// <summary>
    /// It contain ID, ISBN, Title and AuthorName.
    /// </summary>
    public class BookDTO
    {
        /// <summary>
        /// BookDTO ID.
        /// </summary>
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
        public bool BookIsRemoved { get; set; }

        /// <summary>
        /// Name of Author.
        /// </summary>
        public string AuthorName { get; set; }
    }
}