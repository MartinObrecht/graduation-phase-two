using TechBlog.NewsManager.API.Domain.Exceptions;
using FluentAssertions;

namespace TechBlog.NewsManager.API.Tests.Domain.Exceptions
{
    public class InfrastructureExceptionTests
    {
        [Fact]
        public void Constructor_WithMessage_SetsMessageProperty()
        {
            // Arrange
            string message = "Test message";

            // Act
            var exception = new InfrastructureException(message);

            // Assert
            exception.Message.Should().Be(message);
        }
    }
}