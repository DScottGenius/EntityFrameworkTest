using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkTest.Data.Model
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("PaymentAdeptReference")]
        [StringLength(7)]
        public string AdeptReference { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        [StringLength(50)]
        public string PaymentSource { get; set; }
        public User AssociatedUser { get; set; }

        public Payment(int id, decimal amount, DateTime date, string paymentSource)
        {
            Id = id;
            Amount = amount;
            Date = date;
            PaymentSource = paymentSource;
        }

        public Payment(int id, string adeptReference, decimal amount, DateTime date, string paymentSource)
        {
            Id = id;
            AdeptReference = adeptReference;
            Amount = amount;
            Date = date;
            PaymentSource = paymentSource;
        }
    }
}
