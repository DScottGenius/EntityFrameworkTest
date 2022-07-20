using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkTest.Data.Model
{
    public class Address
    {
        [ForeignKey("Address_Owner")]
        public string Owner { get; set; }
        [StringLength(50)]
        public string Line1 { get; set; }
        [StringLength(50)]
        public string Line2 { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(10)]
        public string Postcode { get; set; }


        public Address(string owner, string line1, string line2, string city, string postcode)
        {
            Owner = owner;
            Line1 = line1;
            Line2 = line2;
            City = city;
            Postcode = postcode;
        }
    }
}
