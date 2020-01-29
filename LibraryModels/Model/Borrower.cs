using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryModels
{
    /// <summary>
    /// It contain Id, CloudthingId, Name, Phone, Email.
    /// </summary>
    public class Borrower
    {
        /// <summary>
        /// Borrower ID.
        /// </summary>
        [Column("BorrowerId")]
        public int Id { get; set; }

        /// <summary>
        /// Borrower Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Borrower Phone Number.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Borrower Email.
        /// </summary>
        public string Email { get; set; }
    }
}