using CrimeDatabase.Application.DependencyInjection;
using CrimeDatabase.Application.Services;
using CrimeDatabase.Infrastructure.Data;
using CrimeDatabase.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace CrimeDatabase.Integration.Tests;

public class InfrastructureFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer;
    public IServiceProvider Provider { get; private set; } = default!;

    public InfrastructureFixture()
    {
        // I use a docker container for a throw-away MSSQL server. This enables
        // automated testing in CI/CD.
        _dbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("Password123!ForTestOnly")
            .Build();
    }

    public async Task InitializeAsync()
    {
        // Start the container.
        await _dbContainer.StartAsync();

        var services = new ServiceCollection();

        // Register the infrastructure services.
        services.AddInfrastructure(_dbContainer.GetConnectionString());

        // Register the services from the application layer.
        services.AddApplication();
        
        Provider = services.BuildServiceProvider();
        using var scope = Provider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CrimeDbContext>();

        // Run the Entity Framework migrations.
        await db.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}
