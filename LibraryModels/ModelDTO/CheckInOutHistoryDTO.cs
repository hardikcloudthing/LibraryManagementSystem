using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryModels
{
    /// <summary>
    /// It contain Id, Status, CheckOutDate, DueDate, ReturnDate, Borrower ID and Book ID.
    /// </summary>
    public class CheckInOutHistoryDTO
    {
        /// <summary>
        /// Status of Book. (status = true) means books which are occupied by other.
        /// </summary>
        public bool Status { get; set; }

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
        /// Book ID.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Borrower ID.
        /// </summary>
        public int BorrowerId { get; set; }
    }
}