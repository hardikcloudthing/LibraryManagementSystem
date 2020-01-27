using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryModels
{
    public class CheckInOutHistory
    {
        [Column("CheckInOutHistoryId")]
        public int Id { get; set; }

        public bool Status { get; set; } //(status = true) means books which are occupied by other.

        public DateTime CheckOutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        [ForeignKey("BorrowerId")]
        public virtual Borrower Borrower { get; set; }
    }
}