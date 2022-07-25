using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserPayment.API.DbContexts;
using UserPayment.API.Entities;

namespace UserPayment.API.Services
{
    public class UserPaymentInfoRepository : IUserPaymentInfoRepository
    {
        private readonly UserPaymentDbContext context;

        public UserPaymentInfoRepository(UserPaymentDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Payment> GetPaymentAsync(string adeptReference)
        {
            return await context.Payments.Where(p => p.AdeptReference == adeptReference).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsAsync()
        {
            return await context.Payments.ToListAsync();
        }

        public async Task<User> GetUserAsync(string accountNumber, bool includePaymentsMade)
        {
            if (includePaymentsMade)
            {
                return await context.Users.Include(u => u.PaymentsMade).Where(u => u.AccountNumber == accountNumber).FirstOrDefaultAsync();
            }
            else
            {
                return await context.Users.Where(u => u.AccountNumber == accountNumber).FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {

            return await context.Users.OrderBy(u => u.AccountNumber).ToListAsync();

        }

        public async Task<bool> UserExistsAsync(string accountNumber)
        {
            return await context.Users.AnyAsync(u => u.AccountNumber == accountNumber);
        }

        public void AddUser(User userToAdd)
        {
            if (userToAdd != null)
            {
                context.Users.Add(userToAdd);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() >= 0;
        }

        public void DeleteUser(User userToDelete)
        {
            context.Users.Remove(userToDelete);
        }
    }
}
