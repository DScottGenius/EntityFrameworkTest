using System;
using System.Collections.Generic;

namespace UserPayment.API.Models
{
    public class UserDto
    {
        public string AccountNumber { get; set; }

        public string AccountName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public double Balance { get; set; } = 0;

        public string AdeptReference { get; set; }

        public List<PaymentDto> PaymentsMade { get; set; }

        public string DebtType { get; set; }

    }
}
