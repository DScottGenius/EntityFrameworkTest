using System;

namespace UserPayment.API.Models
{
    public class PaymentDto
    {

        public int Id { get; set; }
        public string AdeptReference { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string PaymentSource { get; set; }

    }
}
