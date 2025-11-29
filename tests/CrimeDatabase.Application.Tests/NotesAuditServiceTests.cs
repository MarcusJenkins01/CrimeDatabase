using CrimeDatabase.Application.Interfaces.Repositories;
using CrimeDatabase.Application.Services;
using CrimeDatabase.Domain.Entities;
using Moq;

namespace CrimeDatabase.Application.Tests
{
    /// <summary>
    /// This performs the unit testing for the NotesAudit service from the application layer using
    /// a mock repository.
    /// </summary>
    public class NotesAuditServiceTests
    {
        private readonly Mock<INotesAuditRepository> _mockRepo;
        private readonly NotesAuditService _service;

        public NotesAuditServiceTests()
        {
            _mockRepo = new Mock<INotesAuditRepository>();
            _service = new NotesAuditService(_mockRepo.Object);
        }

        /// <summary>
        /// Test to check note audits are correctly retrieved.
        /// </summary>
        [Fact]
        public async Task Should_Return_All_NotesAudits()
        {
            // Arrange
            var audits = new List<NotesAudit>
            {
                new NotesAudit { Id = Guid.NewGuid(),CrimeId = Guid.NewGuid(), OriginalNotes = "Original notes", UpdatedNotes = "New notes" },
                new NotesAudit { Id = Guid.NewGuid(), CrimeId = Guid.NewGuid(), OriginalNotes = "Before", UpdatedNotes = "After" }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(audits);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(audits[0].Id, result[0].Id);
            Assert.Equal(audits[1].Id, result[1].Id);

            _mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        /// <summary>
        /// Test to check the notes audits are returned for a given CrimeId.
        /// </summary>
        [Fact]
        public async Task Should_Return_Audits_By_CrimeId()
        {
            // Arrange
            var crimeId = Guid.NewGuid();
            var audits = new List<NotesAudit>
            {
                new NotesAudit { Id = Guid.NewGuid(), CrimeId = crimeId, OriginalNotes = "Original notes", UpdatedNotes = "New notes" }
            };

            _mockRepo.Setup(r => r.GetByCrimeIdAsync(crimeId)).ReturnsAsync(audits);

            // Act
            var result = await _service.GetByCrimeIdAsync(crimeId);

            // Assert
            Assert.Single(result);
            Assert.Equal(audits[0].Id, result[0].Id);
            Assert.Equal(crimeId, result[0].CrimeId);

            _mockRepo.Verify(r => r.GetByCrimeIdAsync(crimeId), Times.Once);
        }
    }
}
