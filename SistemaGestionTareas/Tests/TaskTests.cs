using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class TaskTests
    {
        [Fact]
        public void Task_ShouldThrowException_WhenTitleIsNullOrEmpty()
        {
            // Arrange
            var task = new Domain.Entities.Task();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => task.Title = null);
            Assert.Equal("Title cannot be null or empty.", exception.Message);

            exception = Assert.Throws<Exception>(() => task.Title = "");
            Assert.Equal("Title cannot be null or empty.", exception.Message);
        }

        [Fact]
        public void Task_ShouldSetTitle_WhenValidTitle()
        {
            // Arrange
            var task = new Domain.Entities.Task();

            // Act
            task.Title = "Valid Task Title";

            // Assert
            Assert.Equal("Valid Task Title", task.Title);  // Verifica que el título se establece correctamente
        }

        [Fact]
        public void Task_ShouldThrowException_WhenDescriptionIsNullOrEmpty()
        {
            // Arrange
            var task = new Domain.Entities.Task();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => task.Description = null);
            Assert.Equal("Description cannot be null or empty.", exception.Message);

            exception = Assert.Throws<Exception>(() => task.Description = "");
            Assert.Equal("Description cannot be null or empty.", exception.Message);
        }

        [Fact]
        public void Task_ShouldSetDescription_WhenValidDescription()
        {
            // Arrange
            var task = new Domain.Entities.Task();

            // Act
            task.Description = "Valid Task Description";

            // Assert
            Assert.Equal("Valid Task Description", task.Description);  // Verifica que la descripción se establece correctamente
        }

        [Fact]
        public void Task_ShouldThrowException_WhenExpirationDateIsInThePast()
        {
            // Arrange
            var task = new Domain.Entities.Task();
            var pastDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => task.ExpirationDate = pastDate);
            Assert.Equal("Expiration date cannot be in the past.", exception.Message);
        }

        [Fact]
        public void Task_ShouldSetExpirationDate_WhenValidExpirationDate()
        {
            // Arrange
            var task = new Domain.Entities.Task();
            var futureDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5));

            // Act
            task.ExpirationDate = futureDate;

            // Assert
            Assert.Equal(futureDate, task.ExpirationDate);  // Verifica que la fecha de expiración se establece correctamente
        }

        [Fact]
        public void Task_ShouldThrowException_WhenInvalidStatus()
        {
            // Arrange
            var task = new Domain.Entities.Task();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => task.Status = (Domain.Enums.TaskStatus)999);
            Assert.Equal("Invalid status.", exception.Message);
        }

        [Fact]
        public void Task_ShouldSetStatus_WhenValidStatus()
        {
            // Arrange
            var task = new Domain.Entities.Task();

            // Act
            task.Status = Domain.Enums.TaskStatus.InProgress;

            // Assert
            Assert.Equal(Domain.Enums.TaskStatus.InProgress, task.Status);  // Verifica que el estado se establece correctamente
        }

        [Fact]
        public void Task_ShouldSetDefaultStatus_WhenCreated()
        {
            // Arrange
            var task = new Domain.Entities.Task();

            // Act & Assert
            Assert.Equal(Domain.Enums.TaskStatus.Created, task.Status); // Verifica que el valor predeterminado de Status es "Created"
        }
    }
}
