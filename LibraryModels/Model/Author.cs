using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryModels
{
    /// <summary>
    /// It contain ID and Name.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Author ID.
        /// </summary>
        [Column("AuthorId")]
        public int Id { get; set; }

        /// <summary>
        /// Author Name.
        /// </summary>
        public string Name { get; set; }
    }
}