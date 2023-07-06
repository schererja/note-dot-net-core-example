using Eden.Core.Entities;

namespace Eden.Application.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        //Task<Customer> GetCustomerById(int id);
    }
}
