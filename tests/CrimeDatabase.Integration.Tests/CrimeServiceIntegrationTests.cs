using CrimeDatabase.Application.Services;
using CrimeDatabase.Domain.Entities;
using CrimeDatabase.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CrimeDatabase.Integration.Tests;

/// <summary>
/// Integration tests for the Infrastructure layer.
/// </summary>
[Collection("Infrastructure")]
public class CrimeServiceIntegrationTests(InfrastructureFixture fixture)
{
    private readonly IServiceProvider _provider = fixture.Provider;

    /// <summary>
    /// Check that a crime can be retrieved by the Id.
    /// </summary>
    [Fact]
    public async Task CrimeService_Can_Get_Crime_By_Id()
    {
        // Arrange
        using var scope = _provider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<CrimeService>();
        var crime = new Crime
        {
            Id = Guid.NewGuid(),
            CrimeDate = DateOnly.FromDateTime(DateTime.UtcNow),
            Location = "The Street, Your Town",
            VictimName = "M. Jones",
            CrimeType = Domain.Enums.CrimeType.Burglary,
            Notes = "Forensic team currently gathering evidence."
        };

        // Act
        await service.CreateAsync(crime);
        var found = await service.GetByIdAsync(crime.Id);

        // Assert
        Assert.NotNull(found);
        Assert.Equal(crime.Location, found!.Location);
    }

    /// <summary>
    /// Check a Crime can be created and persists.
    /// </summary>
    [Fact]
    public async Task CrimeService_Can_Create_And_Persist_Crime()
    {
        // Arrange
        using var scope = _provider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<CrimeService>();
        var db = scope.ServiceProvider.GetRequiredService<CrimeDbContext>();
        var crime = new Crime
        {
            Id = Guid.NewGuid(),
            CrimeDate = DateOnly.FromDateTime(DateTime.UtcNow),
            Location = "Test location",
            VictimName = "Test victim",
            CrimeType = Domain.Enums.CrimeType.Robbery,
            Notes = "Service test."
        };

        // Act
        await service.CreateAsync(crime);
        var found = await db.Crimes.FindAsync(crime.Id);

        // Assert
        Assert.NotNull(found);
        Assert.Equal("Test location", found!.Location);
    }

    /// <summary>
    /// Check that the Notes field is updated correctly and an Audit is created.
    /// </summary>
    [Fact]
    public async Task CrimeService_UpdateNotes_Should_Create_Audit()
    {
        // Arrange
        using var scope = _provider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<CrimeService>();
        var db = scope.ServiceProvider.GetRequiredService<CrimeDbContext>();
        var crime = new Crime
        {
            Id = Guid.NewGuid(),
            CrimeDate = DateOnly.FromDateTime(DateTime.UtcNow),
            Location = "Test",
            VictimName = "Tester",
            CrimeType = Domain.Enums.CrimeType.Violent,
            Notes = "Original notes"
        };

        // Act
        await service.CreateAsync(crime);
        await service.UpdateNotesAsync(crime.Id, "New notes");
        var saved = await db.Crimes.FindAsync(crime.Id);
        var audit = db.NotesAudits.FirstOrDefault(a => a.CrimeId == crime.Id);

        // Assert
        // Check Notes field is updated on the Crime model.
        Assert.NotNull(saved);
        Assert.Equal("New notes", saved!.Notes);

        // Check audit is saved and has correct values.
        Assert.NotNull(audit);
        Assert.Equal("Original notes", audit!.OriginalNotes);
        Assert.Equal("New notes", audit.UpdatedNotes);
    }
}
