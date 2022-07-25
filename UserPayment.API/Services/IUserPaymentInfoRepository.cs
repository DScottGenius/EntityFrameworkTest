using System.Collections.Generic;
using System.Threading.Tasks;
using UserPayment.API.Entities;

namespace UserPayment.API.Services
{
    public interface IUserPaymentInfoRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(string accountNumber, bool includePaymentsMade);

        Task<IEnumerable<Payment>> GetPaymentsAsync();
        Task<Payment> GetPaymentAsync(string adeptReference);

        Task<bool> UserExistsAsync(string accountNumber);
        void AddUser(User userToAdd);

        void DeleteUser(User userToDelete);

        Task<bool> SaveChangesAsync();
        
    }
}
