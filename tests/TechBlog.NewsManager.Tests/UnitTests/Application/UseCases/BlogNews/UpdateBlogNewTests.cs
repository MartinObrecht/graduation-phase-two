using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TechBlog.NewsManager.API.Application.UseCases.BlogNews.Update;
using TechBlog.NewsManager.API.Domain.Database;
using TechBlog.NewsManager.API.Domain.Entities;
using TechBlog.NewsManager.API.Domain.Exceptions;
using TechBlog.NewsManager.API.Domain.Logger;
using TechBlog.NewsManager.API.Domain.Responses;
using TechBlog.NewsManager.Tests.AcceptanceTests.Fixtures;

namespace TechBlog.NewsManager.Tests.UnitTests.Application.UseCases.BlogNews
{
    [Collection(nameof(UnitTestsFixtureCollection))]
    public class UpdateBlogNewTests
    {
        private readonly ILoggerManager _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CancellationToken _cancellationToken;
        private readonly UpdateBlogNewRequest _request;

        public UpdateBlogNewTests()
        {
            _logger = Substitute.For<ILoggerManager>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _cancellationToken = CancellationToken.None;
            _request = new UpdateBlogNewRequest
            {
                Title = "New Title",
                Description = "New Description",
                Body = "New Body"
            };
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        public async Task Action_WhenBlogNewIsNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "author-id")
            }));
            _unitOfWork.BlogNew.GetByIdAsync(id, _cancellationToken).Returns(new BlogNew { Enabled = false });

            // Act
            var result = await UpdateBlogNewHandler.Action(_logger, _unitOfWork, user, id, _request, _cancellationToken);

            // Assert
            result.Should().BeOfType<NotFound<BaseResponse>>();

            await _unitOfWork.BlogNew.DidNotReceive().UpdateAsync(Arg.Any<BlogNew>(), _cancellationToken);
            await _unitOfWork.DidNotReceiveWithAnyArgs().SaveChangesAsync(default);
        }

        [Fact]
        public async Task Action_WhenBlogNewIsDisabled_ReturnsNotFoundResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var blogNew = new BlogNew
            {
                Id = id,
                AuthorId = "author-id",
                Enabled = false
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "author-id")
            }));
            _unitOfWork.BlogNew.GetByIdAsync(id, _cancellationToken).Returns(blogNew);

            // Act
            var result = await UpdateBlogNewHandler.Action(_logger, _unitOfWork, user, id, _request, _cancellationToken);

            // Assert
            result.Should().BeOfType<NotFound<BaseResponse>>();
            await _unitOfWork.BlogNew.DidNotReceive().UpdateAsync(Arg.Any<BlogNew>(), _cancellationToken);
            await _unitOfWork.DidNotReceiveWithAnyArgs().SaveChangesAsync(default);
        }

        [Fact]
        public async Task Action_WhenUserIsNotTheOwner_ReturnsForbidResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var blogNew = new BlogNew
            {
                Id = id,
                AuthorId = "author-id",
                Enabled = true
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "not-author-id")
            }));

            _unitOfWork.BlogNew.GetByIdAsync(id, _cancellationToken).Returns(blogNew);

            // Act
            var result = await UpdateBlogNewHandler.Action(_logger, _unitOfWork, user, id, _request, _cancellationToken);

            // Assert
            result.Should().BeOfType<ForbidHttpResult>();
            await _unitOfWork.BlogNew.DidNotReceive().UpdateAsync(Arg.Any<BlogNew>(), _cancellationToken);
            await _unitOfWork.DidNotReceiveWithAnyArgs().SaveChangesAsync(default);
        }

        [Fact]
        public async Task Action_WhenRequestIsInvalid_ReturnsBadRequestResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var blogNew = new BlogNew
            {
                Id = id,
                AuthorId = "author-id",
                Enabled = true
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "author-id")
            }));

            _unitOfWork.BlogNew.GetByIdAsync(id, _cancellationToken).Returns(blogNew);
            _unitOfWork.BlogNew.UpdateAsync(blogNew, _cancellationToken).Throws(new ValidationException("Invalid request"));
            
            // Act
            var result = await UpdateBlogNewHandler.Action(_logger, _unitOfWork, user, id, _request, _cancellationToken);
            // Assert
            result.Should().BeOfType<BadRequest<BaseResponse>>();
            await _unitOfWork.BlogNew.Received(1).UpdateAsync(Arg.Any<BlogNew>(), _cancellationToken);
        }

        [Fact]
        public async Task Action_WhenUnexpectedErrorOccurs_ThrowsInfrastructureException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var blogNew = new BlogNew
            {
                Id = id,
                AuthorId = "author-id",
                Enabled = true
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "author-id")
            }));

            _unitOfWork.BlogNew.GetByIdAsync(id, _cancellationToken).Returns(blogNew);
            _unitOfWork.BlogNew.UpdateAsync(blogNew, _cancellationToken).Throws(new InfrastructureException("Unexpected error"));

            // Act
            Func<Task> action = async () => await UpdateBlogNewHandler.Action(_logger, _unitOfWork, user, id, _request, _cancellationToken);
            // Assert
            await action.Should().ThrowAsync<InfrastructureException>();
            await _unitOfWork.BlogNew.Received(1).UpdateAsync(blogNew, _cancellationToken);
        }

        [Fact]
        public async Task Action_WhenBlogNewIsValid_UpdatesBlogNewAndReturnsOkResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var blogNew = new BlogNew
            {
                Id = id,
                AuthorId = "author-id",
                Enabled = true
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "author-id")
            }));

            _unitOfWork.BlogNew.GetByIdAsync(id, _cancellationToken).Returns(blogNew);          
            
            // Act
            var result = await UpdateBlogNewHandler.Action(_logger, _unitOfWork, user, id, _request, _cancellationToken);
            // Assert
            result.Should().BeOfType<Ok<BaseResponse>>();
            blogNew.Title.Should().Be(_request.Title);
            blogNew.Description.Should().Be(_request.Description);
            blogNew.Body.Should().Be(_request.Body);
        }
    }
}