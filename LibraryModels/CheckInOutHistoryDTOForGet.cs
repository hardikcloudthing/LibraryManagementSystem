using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryModels
{
    public class CheckInOutHistoryDTOForGet
    {
        public bool Status { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public BookDTO Book { get; set; }
        public Borrower Borrower { get; set; }
    }
}