using CustomerServiceWebApi.Domain.Models;

namespace CustomerServiceWebApi.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();

        Task<Customer> GetByIdAsync(int id);

        Task AddAsync(Customer customer);

        Task UpdateAsync(Customer customer);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}
