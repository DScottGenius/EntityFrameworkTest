using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkTest.Model.Model
{
    public class Phone
    {
        [Key]
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        [ForeignKey("Fk_phoner_user")]
        public string owner { get; set; }

        public Phone(string phoneNumber, string owner)
        {
            PhoneNumber = phoneNumber;
            this.owner = owner;
        }
    }
}
