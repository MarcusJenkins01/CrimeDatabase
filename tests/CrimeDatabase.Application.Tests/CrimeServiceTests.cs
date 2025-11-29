using CrimeDatabase.Application.Interfaces.Repositories;
using CrimeDatabase.Application.Services;
using CrimeDatabase.Domain.Entities;
using CrimeDatabase.Domain.Enums;
using Moq;

namespace CrimeDatabase.Application.Tests
{
    /// <summary>
    /// This performs the unit testing for the Crime service from the application layer using
    /// a mock repository.
    /// </summary>
    public class CrimeServiceTests
    {
        private readonly Mock<ICrimeRepository> _mockRepo;
        private readonly CrimeService _service;

        public CrimeServiceTests()
        {
            // We use a mock repository since we are unit testing the Application layer.
            _mockRepo = new Mock<ICrimeRepository>();
            _service = new CrimeService(_mockRepo.Object);
        }

        /// <summary>
        /// Test that all crime records are correctly returned with GetAllAsync.
        /// </summary>
        [Fact]
        public async Task Should_Return_All_Crimes()
        {
            // Arrange
            var crimes = new List<Crime>
            {
                new Crime { Id = Guid.NewGuid() },
                new Crime { Id = Guid.NewGuid() }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(crimes);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(crimes[0].Id, result[0].Id);
            Assert.Equal(crimes[1].Id, result[1].Id);
        }

        /// <summary>
        /// Test to check the correct crime is returned by a given Id for GetByIdAsync.
        /// </summary>
        [Fact]
        public async Task Should_Return_Crime_By_Id()
        {
            // Arrange
            var id = Guid.NewGuid();
            var crime = new Crime { Id = id };

            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(crime);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(crime.Id, result!.Id);
        }

        /// <summary>
        /// Test to ensure a Crime entity is successfully created and saved.
        /// </summary>
        [Fact]
        public async Task Should_Save_Crime_When_Creating()
        {
            // Arrange
            var crime = new Crime
            {
                Id = Guid.NewGuid(),
                CrimeDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Location = "Some test street",
                VictimName = "John Smith",
                CrimeType = CrimeType.Burglary,
                Notes = "Some test notes"
            };

            // Act
            await _service.CreateAsync(crime);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(crime), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        /// <summary>
        /// Test to check that the notes field is updated correctly and an audit
        /// is added and saved.
        /// </summary>
        [Fact]
        public async Task Should_Save_Notes_And_Audit_When_Updating()
        {
            // Arrange
            var id = Guid.NewGuid();
            var crime = new Crime
            {
                Id = Guid.NewGuid(),
                CrimeDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Location = "Some test street",
                VictimName = "John Smith",
                CrimeType = CrimeType.Burglary,
                Notes = "Original notes"
            };

            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(crime);

            // Act
            await _service.UpdateNotesAsync(id, "New notes");

            // Assert
            _mockRepo.Verify(r => r.Update(It.Is<Crime>(c => c.Notes == "New notes")), Times.Once);
            _mockRepo.Verify(r => r.AddAuditAsync(It.Is<NotesAudit>(a =>
                a.CrimeId == id &&
                a.OriginalNotes == "Original notes" &&
                a.UpdatedNotes == "New notes"
            )), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        /// <summary>
        /// Test the error handling when we update the notes of a crime that doesn't exist.
        /// </summary>
        [Fact]
        public async Task Should_Do_Nothing_When_Crime_Not_Found_While_Updating()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Crime?)null);

            // Act
            await _service.UpdateNotesAsync(id, "New notes");

            // Assert
            _mockRepo.Verify(r => r.Update(It.IsAny<Crime>()), Times.Never);
            _mockRepo.Verify(r => r.AddAuditAsync(It.IsAny<NotesAudit>()), Times.Never);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
    }
}
