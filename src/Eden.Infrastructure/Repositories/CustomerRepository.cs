using Eden.Application.Interfaces;
using Eden.Core.Entities;
using Eden.Sql.Queries;
using Dapper;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Eden.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly RepositoryContext _repositoryContext;
        #region Constructor

        public CustomerRepository(IDbConnection dbConnection, RepositoryContext repositoryContext)
        {
            _dbConnection = dbConnection;
            _repositoryContext = repositoryContext;
        }

        public Task<int> AddAsync(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        #endregion
        public async Task<IReadOnlyList<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
            //    var result = await _repositoryContext.Customers.ToListAsync();
            //    return result;
            //}
            //public async Task<Customer> GetCustomerById(int Id)
            //{
            //    var result = await _repositoryContext.Customers.FirstAsync(customer => customer.Id == Id);
            //    return result;
            //}
            //public async Task<Customer> AddAsync(Customer entity)
            //{
            //    var result = await _repositoryContext.Customers.AddAsync(entity);
            //    await _repositoryContext.SaveChangesAsync();
            //    return result.Entity;
        }

        public Task<Customer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByUniqueIdAsync(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
