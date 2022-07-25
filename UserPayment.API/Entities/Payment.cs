using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserPayment.API.Entities
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("PaymentAdeptReference")]
        [MaxLength(7)]
        public string AdeptReference { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(50)]
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
