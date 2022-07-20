using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserPayment.API.Models;

namespace UserPayment.API
{
    public class UsersDataStore
    {
        public List<UserDto> Users { get; set; }

        public static UsersDataStore Current { get; } = new UsersDataStore();

        public UsersDataStore()
        {
            Users = new List<UserDto>()
            {
                new UserDto
                {
                    AccountName = "Bob",
                    AdeptReference = "12345",
                    DateOfBirth = DateTime.Now,
                    AccountNumber = "12132452",
                    Balance = 1245
                },
                new UserDto
                {
                    AccountName = "John",
                    AdeptReference = "3asf5",
                    DateOfBirth = DateTime.Now,
                    AccountNumber = "9248762",
                    Balance = 90
                }
            };
        }
    }
}
