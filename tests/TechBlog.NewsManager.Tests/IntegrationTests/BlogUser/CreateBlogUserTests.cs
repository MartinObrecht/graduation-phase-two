using FluentAssertions;
using System.Net.Http.Json;
using TechBlog.NewsManager.API.Application.UseCases.BlogUsers.Create;
using TechBlog.NewsManager.API.Domain.Authentication;
using TechBlog.NewsManager.API.Domain.Responses;
using TechBlog.NewsManager.Tests.IntegrationTests.Fixtures;

namespace TechBlog.NewsManager.Tests.IntegrationTests.BlogUser
{
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    public class CreateBlogUserTests
    {
        private readonly IntegrationTestsFixture _fixture;

        public CreateBlogUserTests(IntegrationTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task RequestWithoutAccessKey_ShouldReturnUnauthorized()
        {
            //Arrange
            var body = new CreateBlogUserRequest
                (
                    email: IntegrationTestsHelper.JournalistEmail,
                    password: IntegrationTestsHelper.FakePassword,
                    name: IntegrationTestsHelper.JournalistName,
                    blogUserType: API.Domain.ValueObjects.BlogUserType.JOURNALIST
                );

            //Act
            var response = await _fixture.Client.PostAsJsonAsync(CreateBlogUserHandler.Route, body);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
            var responseBody = await response.Content.ReadFromJsonAsync<BaseResponse>();
            responseBody.Success.Should().BeFalse();
            responseBody.ResponseMessageEqual(ResponseMessage.GenericError);
        }

        [Fact]
        public async Task ValidRequest_ShouldReturnCreated()
        {
            //Arrange
            var body = new CreateBlogUserRequest
                (
                    email: IntegrationTestsHelper.JournalistEmail,
                    password: IntegrationTestsHelper.FakePassword,
                    name: IntegrationTestsHelper.JournalistName,
                    blogUserType: API.Domain.ValueObjects.BlogUserType.JOURNALIST
                );
            
            _fixture.AddApiKeyHeader();

            //Act
            var response = await _fixture.Client.PostAsJsonAsync(CreateBlogUserHandler.Route, body);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            var responseBody = await response.Content.ReadFromJsonAsync<BaseResponseWithValue<AccessTokenModel>>();
            responseBody.Success.Should().BeTrue();
            responseBody.ResponseMessageEqual(ResponseMessage.Success);
        }
    }
}
