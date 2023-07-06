using Eden.Application.Interfaces.Notes;
using Eden.Core.Entities.Notes;
using Microsoft.EntityFrameworkCore;

namespace Eden.Infrastructure.Repositories.Notes
{
    public class NotesCategoriesRepository : INotesCategoriesRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public NotesCategoriesRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public async Task<int> AddAsync(NotesCategory entity)
        {
            await _repositoryContext.AddAsync<NotesCategory>(entity);
            return await _repositoryContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(long id)
        {
            var found = await _repositoryContext.FindAsync<NotesCategory>(id);
            if(found != null)
            {
                _repositoryContext.Remove<NotesCategory>(found);
                return await _repositoryContext.SaveChangesAsync();
            }
            return 0;
            
        }

        public async Task<IReadOnlyList<NotesCategory>> GetAllAsync()
        {
            return await _repositoryContext.NotesCategories.ToListAsync();
        }

        public async Task<NotesCategory> GetByIdAsync(int id)
        {
            var entityFound = await _repositoryContext.NotesCategories.FindAsync(id);
            if(entityFound != null)
            {
                return entityFound;
            }
            return new NotesCategory();
        }

        public async Task<NotesCategory> GetByUniqueIdAsync(string uniqueId)
        {
            var entityFound = await _repositoryContext.NotesCategories.Where(noteCategory => noteCategory.UniqueIdentifier.ToString() == uniqueId).FirstAsync();
            if (entityFound != null)
            {
                return entityFound;
            }
            return new NotesCategory();
        }

        public async Task<int> UpdateAsync(NotesCategory entity)
        {
            _repositoryContext.NotesCategories.Update(entity);
            return await _repositoryContext.SaveChangesAsync();
        }
    }
}
