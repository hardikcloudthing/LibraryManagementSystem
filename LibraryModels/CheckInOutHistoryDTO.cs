﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryModels
{
    public class CheckInOutHistoryDTO
    {
        public bool Status { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int BookId { get; set; }
        public int BorrowerId { get; set; }
    }
}