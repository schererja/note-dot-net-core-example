

using Eden.Application.Interfaces;
using Eden.Application.Interfaces.Notes;

namespace Eden.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(ICustomerRepository customerRepository, INotesCategoriesRepository notesCategoriesRepository, INotesRepository notesRepository)
        {
            Customers = customerRepository;
            NotesCategories = notesCategoriesRepository;
            Notes = notesRepository;
        }
        public ICustomerRepository Customers { get; set; }
        public INotesCategoriesRepository NotesCategories { get; set; }
        public INotesRepository Notes { get; set; }

    }
}
