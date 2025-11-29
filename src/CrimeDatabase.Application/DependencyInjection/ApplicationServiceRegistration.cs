using CrimeDatabase.Application.Interfaces.Services;
using CrimeDatabase.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CrimeDatabase.Application.DependencyInjection
{
    /// <summary>
    /// This extension method registers the crime and audit services from the Application 
    /// layer into the Dependency Injection (DI) container. In effect, this means that 
    /// CrimeDatabase.Web does not need to know about the internal details of the Application layer.
    /// </summary>
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register the Crime and NotesAudit services.
            services.AddScoped<ICrimeService, CrimeService>();
            services.AddScoped<INotesAuditService, NotesAuditService>();

            return services;
        }
    }
}
