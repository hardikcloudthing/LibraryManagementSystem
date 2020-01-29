using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryModels
{
    /// <summary>
    /// It contains status, CheckOutDate, DueDate, ReturnDate , Book Detail and Borrower Detail.
    /// </summary>
    public class CheckInOutHistoryDTOForGet
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
        /// Book Detail.
        /// </summary>
        public BookDTO Book { get; set; }

        /// <summary>
        /// Borrower Detail.
        /// </summary>
        public Borrower Borrower { get; set; }
    }
}