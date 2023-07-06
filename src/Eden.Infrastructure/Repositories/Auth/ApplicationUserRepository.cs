using Eden.Application.Interfaces.Auth;
using Eden.Core.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eden.Infrastructure.Repositories.Auth
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public ApplicationUserRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public async Task<int> AddAsync(ApplicationUser entity)
        {
            await _repositoryContext.Users.AddAsync(entity);
            var didSave = await _repositoryContext.SaveChangesAsync();
            if(didSave == 1)
            {
                return 1;
            }
            return 0;
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ApplicationUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetByUniqueIdAsync(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
