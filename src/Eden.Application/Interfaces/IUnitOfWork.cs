using Eden.Application.Interfaces.Notes;

namespace Eden.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        INotesCategoriesRepository NotesCategories { get; }
        INotesRepository Notes { get; }
    }
}
