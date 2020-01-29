using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryModels
{
    /// <summary>
    /// It contain Id, Status, CheckOutDate, DueDate, ReturnDate, Book Detail and Borrower Detail.
    /// </summary>
    public class CheckInOutHistory
    {
        /// <summary>
        /// History ID.
        /// </summary>
        [Column("CheckInOutHistoryId")]
        public int Id { get; set; }

        /// <summary>
        /// Status of Book. (status = true) means books which are occupied by other.
        /// </summary>
        public bool Status { get; set; } //(status = true) means books which are occupied by other.

        /// <summary>
        /// Date on which book is issue.
        /// </summary>
        public DateTime CheckOutDate { get; set; }

        /// <summary>
        /// Date When book should return.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Date When book returned.
        /// </summary>
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// Book Detail.
        /// </summary>
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        /// <summary>
        /// Borrower Detail.
        /// </summary>
        [ForeignKey("BorrowerId")]
        public virtual Borrower Borrower { get; set; }
    }
}