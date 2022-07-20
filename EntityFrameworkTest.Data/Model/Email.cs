using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkTest.Model.Model
{
    public class Email
    {
        [Key]
        [StringLength(100)]
        public string EmailAddress { get; set; }

        [ForeignKey("Fk_email_user")]
        public string owner { get; set; }

        public Email(string emailAddress, string owner)
        {
            EmailAddress = emailAddress;
            this.owner = owner;
        }
    }
}
