using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using TechBlog.NewsManager.API.Domain.Entities;
using TechBlog.NewsManager.API.Domain.Extensions;
using TechBlog.NewsManager.API.Domain.Logger;
using TechBlog.NewsManager.API.Domain.Responses;

namespace TechBlog.NewsManager.API.Tests.Domain.Extensions
{
    public class BlogNewExtensionsTests
    {
        private readonly ILoggerManager _loggerMock = Substitute.For<ILoggerManager>();
        private readonly BaseResponse _response = new BaseResponse();

        [Fact]
        public void IsEnabled_WithEnabledBlogNew_ReturnsTrue()
        {
            // Arrange
            var blogNew = new BlogNew { Enabled = true };
            IResult result;

            // Act
            var isEnabled = blogNew.IsEnabled(_loggerMock, _response, out result);

            // Assert
            isEnabled.Should().BeTrue();
            result.Should().BeNull();
        }

        [Fact]
        public void IsEnabled_WithDisabledBlogNew_ReturnsFalseAndSetsResult()
        {
            // Arrange
            var blogNew = new BlogNew { Enabled = false };
            IResult result;

            // Act
            var isEnabled = blogNew.IsEnabled(_loggerMock, _response, out result);

            // Assert
            isEnabled.Should().BeFalse();
            result.Should().NotBeNull();
        }

        [Fact]
        public void IsEnabled_WithDisabledBlogNew_LogsDebugMessage()
        {
            // Arrange
            var blogNew = new BlogNew { Enabled = false };
            IResult result;

            // Act
            var isEnabled = blogNew.IsEnabled(_loggerMock, _response, out result);

            // Assert
            _loggerMock.Received(1).Log("Blog new not found", LoggerManagerSeverity.DEBUG, ("Id", blogNew.Id));
        }
    }
}