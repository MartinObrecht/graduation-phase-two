using FluentAssertions;
using TechBlog.NewsManager.API.Domain.Extensions;
using TechBlog.NewsManager.API.Domain.Responses;

namespace TechBlog.NewsManager.API.Tests.Domain.Responses
{
    public class BaseResponseTests
    {
        [Fact]
        public void AsError_ShouldSetSuccessToFalse()
        {
            // Arrange
            var response = new BaseResponse();

            // Act
            response.AsError();

            // Assert
            response.Success.Should().BeFalse();
        }

        [Fact]
        public void AsError_ShouldSetResponseMessageToGenericError_WhenMessageIsNull()
        {
            // Arrange
            var response = new BaseResponse();

            // Act
            response.AsError();

            // Assert
            response.GetMessage().Should().Be(ResponseMessage.GenericError);
        }

        [Fact]
        public void AsError_ShouldSetResponseMessageToProvidedMessage_WhenMessageIsNotNull()
        {
            // Arrange
            var response = new BaseResponse();
            var message = ResponseMessage.BlogNewNotFound;

            // Act
            response.AsError(message);

            // Assert
            response.GetMessage().Should().Be(message);
        }

        [Fact]
        public void AsError_ShouldSetResponseDetailsMessageToDescriptionOfResponseMessage()
        {
            // Arrange
            var response = new BaseResponse();

            // Act
            response.AsError();

            // Assert
            response.ResponseDetails.Message.Should().Be(ResponseMessage.GenericError.GetDescription());
        }

        [Fact]
        public void AsError_ShouldSetResponseDetailsErrorsToProvidedErrors_WhenErrorsIsNotNull()
        {
            // Arrange
            var response = new BaseResponse();
            var errors = new[] { "Error 1", "Error 2" };

            // Act
            response.AsError(errors: errors);

            // Assert
            response.ResponseDetails.Errors.Should().BeEquivalentTo(errors);
        }

        [Fact]
        public void AsError_ShouldSetResponseDetailsErrorsToEmptyArray_WhenErrorsIsNull()
        {
            // Arrange
            var response = new BaseResponse();

            // Act
            response.AsError();

            // Assert
            response.ResponseDetails.Errors.Should().BeEmpty();
        }

        [Fact]
        public void AsSuccess_ShouldSetSuccessToTrue()
        {
            // Arrange
            var response = new BaseResponse();

            // Act
            response.AsSuccess();

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void AsSuccess_ShouldSetResponseMessageToSuccess()
        {
            // Arrange
            var response = new BaseResponse();

            // Act
            response.AsSuccess();

            // Assert
            response.GetMessage().Should().Be(ResponseMessage.Success);
        }

        [Fact]
        public void AsSuccess_ShouldSetResponseDetailsMessageToDescriptionOfResponseMessage()
        {
            // Arrange
            var response = new BaseResponse();

            // Act
            response.AsSuccess();

            // Assert
            response.ResponseDetails.Message.Should().Be(ResponseMessage.Success.GetDescription());
        }

        [Fact]
        public void AsSuccess_ShouldSetResponseDetailsErrorsToNull()
        {
            // Arrange
            var response = new BaseResponse();

            // Act
            response.AsSuccess();

            // Assert
            response.ResponseDetails.Errors.Should().BeNull();
        }

        [Fact]
        public void ResponseMessageEqual_ShouldReturnTrue_WhenResponseMessageIsEqual()
        {
            // Arrange
            var response = new BaseResponse();
            var message = ResponseMessage.BlogNewNotFound;
            response.AsError(message);

            // Act
            var result = response.ResponseMessageEqual(message);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ResponseMessageEqual_ShouldReturnFalse_WhenResponseMessageIsNotEqual()
        {
            // Arrange
            var response = new BaseResponse();
            var message = ResponseMessage.BlogNewNotFound;
            response.AsError(message);

            // Act
            var result = response.ResponseMessageEqual(ResponseMessage.GenericError);

            // Assert
            result.Should().BeFalse();
        }
    }
}