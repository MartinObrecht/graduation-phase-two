using FluentAssertions;
using System.Net.Http.Json;
using System.Text;
using TechBlog.NewsManager.API.Application.UseCases.BlogNews.GetByStrategy;
using TechBlog.NewsManager.API.Application.UseCases.BlogNews.Update;
using TechBlog.NewsManager.API.Domain.Extensions;
using TechBlog.NewsManager.API.Domain.Responses;
using TechBlog.NewsManager.API.Domain.Strategies.GetBlogNews;
using TechBlog.NewsManager.Tests.IntegrationTests.Fixtures;

namespace TechBlog.NewsManager.Tests.IntegrationTests.BlogNews
{
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    public class GetByStrategyTests
    {
        private readonly IntegrationTestsFixture _fixture;
        public GetByStrategyTests(IntegrationTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData(GetBlogNewsStrategy.GET_BY_ID, "id=909080d1-c3c2-4c5c-a849-6deae16f2619")]
        [InlineData(GetBlogNewsStrategy.GET_BY_CREATE_DATE, "from=2022-01-01T00:00:00:000Z&to=from=2022-01-01T00:00:00:000Z")]
        [InlineData(GetBlogNewsStrategy.GET_BY_CREATE_OR_UPDATE_DATE, "from=2022-01-01T00:00:00:000Z&to=from=2022-01-01T00:00:00:000Z")]
        [InlineData(GetBlogNewsStrategy.GET_BY_NAME, "name=fakeName")]
        [InlineData(GetBlogNewsStrategy.GET_BY_TAGS, "tags=fake&tags=tag")]
        public async Task RequestWithoutAccessKey_ShouldReturnUnauthorized(GetBlogNewsStrategy strategy, string param)
        {
            //Arrange
            _fixture.WithoutApiKeyHeader();
            _fixture.WithoutAuthentication();

            //Act
            var response = await _fixture.Client.GetAsync(GetByStrategyHandler.Route + "?strategy=" + strategy.ToString() + "&" + param);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
            var responseBody = await response.Content.ReadFromJsonAsync<BaseResponse>();
            responseBody.Success.Should().BeFalse();
            responseBody.ResponseDetails.Message.Should().Be(ResponseMessage.GenericError.GetDescription());
        }
    }
}
