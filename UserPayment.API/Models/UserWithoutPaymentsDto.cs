using System;

namespace UserPayment.API.Models
{
    public class UserWithoutPaymentsDto
    {
        public string AccountNumber { get; set; }

        public string AccountName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public double Balance { get; set; } = 0;

        public string AdeptReference { get; set; }

        public string DebtType { get; set; }

    }
}
