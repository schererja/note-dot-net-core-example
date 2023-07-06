using Eden.Application.Interfaces.Notes;
using Eden.Core.Entities.Notes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eden.Infrastructure.Repositories.Notes
{
    public class NotesRepository : INotesRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public NotesRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public async Task<int> AddAsync(Note entity)
        {
            await _repositoryContext.AddAsync<Note>(entity);
            return await _repositoryContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(long id)
        {
            var found = await _repositoryContext.FindAsync<Note>(id);
            if (found != null)
            {
                _repositoryContext.Remove<Note>(found);
                return await _repositoryContext.SaveChangesAsync();
            }
            return 0;

        }

        public async Task<IReadOnlyList<Note>> GetAllAsync()
        {
            return await _repositoryContext.Notes.ToListAsync();
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            var entityFound = await _repositoryContext.Notes.FindAsync(id);
            if (entityFound != null)
            {
                return entityFound;
            }
            return new Note();
        }

        public async Task<Note> GetByUniqueIdAsync(string uniqueId)
        {
            var entityFound = await _repositoryContext.Notes.Where(noteCategory => noteCategory.UniqueIdentifier.ToString() == uniqueId).FirstAsync();
            if (entityFound != null)
            {
                return entityFound;
            }
            return new Note();
        }

        public async Task<int> UpdateAsync(Note entity)
        {
            _repositoryContext.Notes.Update(entity);
            return await _repositoryContext.SaveChangesAsync();
        }
    }
}
