using EntityFrameworkTest.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkTest.Data.Model
{
    public class User
    {
        [Key]
        [StringLength(20)]
        public string AccountNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string AccountName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public double Balance { get; set; } = 0;
        [StringLength(7)]
        public string AdeptReference { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Email> Emails { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Payment> PaymentsMade { get; set; }

        public User(string accountNumber, string accountName, DateTime dateOfBirth, double balance, string adeptReference)
        {
            AccountNumber = accountNumber;
            AccountName = accountName;
            DateOfBirth = dateOfBirth;
            Balance = balance;
            AdeptReference = adeptReference;
        }


        public User(string accountNumber, string accountName, DateTime dateOfBirth, double balance, string adeptReference, Address address, string email, string phone)
        {
            AccountNumber = accountNumber;
            AccountName = accountName;
            DateOfBirth = dateOfBirth;
            Balance = balance;
            AdeptReference = adeptReference;
            Addresses = new List<Address>();
            Addresses.Add(address);
            Emails = new List<Email>();
            Emails.Add(new Email(email, accountNumber));
            Phones = new List<Phone>();
            Phones.Add(new Phone(phone, accountNumber));
        }
    }
}
