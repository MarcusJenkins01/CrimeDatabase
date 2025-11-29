using CrimeDatabase.Application.Interfaces.Repositories;
using CrimeDatabase.Infrastructure.Data;
using CrimeDatabase.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CrimeDatabase.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Registers all the Infrastructure services, which includes the repositories, Entity Framework,
    /// and the DbContext (with SQL server).
    /// </summary>
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            string connectionString)
        {
            // Register the DbContext with SQL server.
            services.AddDbContext<CrimeDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register our repository implementations.
            services.AddScoped<ICrimeRepository, CrimeRepository>();
            services.AddScoped<INotesAuditRepository, NotesAuditRepository>();

            return services;
        }
    }
}
