using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserPayment.API.Entities
{
    public class User
    {

        [Key]
        [MaxLength(10)]
        public string AccountNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string DebtType { get; set; }

        [Required]
        [MaxLength(50)]
        public string AccountName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public double Balance { get; set; } = 0;
        [MaxLength(7)]
        public string AdeptReference { get; set; }

        public ICollection<Payment> PaymentsMade { get; set; } = new List<Payment>();

    }
}
