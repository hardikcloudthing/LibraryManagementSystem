using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryModels
{
    public class CheckInOutHistory
    {
        [Column("CheckInOutHistoryId")]
        public Guid Id { get; set; }

        public bool Status { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CheckedInDate { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        [ForeignKey("BorrowerId")]
        public Borrower Borrower { get; set; }
    }
}