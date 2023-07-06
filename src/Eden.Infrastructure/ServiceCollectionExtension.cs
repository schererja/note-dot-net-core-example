using Eden.Application.Interfaces;
using Eden.Application.Interfaces.Notes;
using Eden.Infrastructure.Repositories;
using Eden.Infrastructure.Repositories.Notes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace Eden.Infrastructure
{
    public static class ServiceCollectionExtension
    {


        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnectionString = configuration.GetConnectionString("DBConnection");
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(dbConnectionString));
            services.AddTransient<INotesCategoriesRepository, NotesCategoriesRepository>();
            services.AddTransient<INotesRepository, NotesRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
