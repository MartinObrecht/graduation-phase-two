using FluentAssertions;
using System.Net.Http.Json;
using TechBlog.NewsManager.API.Application.UseCases.BlogNews.Create;
using TechBlog.NewsManager.API.Application.UseCases.BlogNews.Delete;
using TechBlog.NewsManager.API.Domain.Extensions;
using TechBlog.NewsManager.API.Domain.Responses;
using TechBlog.NewsManager.Tests.IntegrationTests.Fixtures;

namespace TechBlog.NewsManager.Tests.IntegrationTests.BlogNews
{
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    public class DeleteBlogNewTests
    {
        private readonly IntegrationTestsFixture _fixture;
        public DeleteBlogNewTests(IntegrationTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task RequestWithoutAccessKey_ShouldReturnUnauthorized()
        {
            //Arrange
            var id = Guid.NewGuid();

            _fixture.WithoutApiKeyHeader();
            _fixture.WithoutAuthentication();

            //Act
            var response = await _fixture.Client.DeleteAsync(DeleteBlogNewHandler.Route.Replace("{id:guid}", id.ToString()));

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
            var responseBody = await response.Content.ReadFromJsonAsync<BaseResponse>();
            responseBody.Success.Should().BeFalse();
            responseBody.ResponseDetails.Message.Should().Be(ResponseMessage.GenericError.GetDescription());
        }

        [Fact]
        public async Task RequestWithoutAccessToken_ShouldReturnUnauthorized()
        {
            //Arrange
            var id = Guid.NewGuid();

            _fixture.WithApiKeyHeader();
            _fixture.WithoutAuthentication();

            //Act
            var response = await _fixture.Client.DeleteAsync(DeleteBlogNewHandler.Route.Replace("{id:guid}", id.ToString()));

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
            var responseBody = await response.Content.ReadFromJsonAsync<BaseResponse>();
            responseBody.Success.Should().BeFalse();
            responseBody.ResponseDetails.Message.Should().Be(ResponseMessage.UserIsNotAuthenticated.GetDescription());
        }
    }
}
