using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data.Common;
using System.Net.Http.Json;
using TechBlog.NewsManager.API;
using TechBlog.NewsManager.API.Application.UseCases.Authentication.Login;
using TechBlog.NewsManager.API.Domain.Authentication;
using TechBlog.NewsManager.API.Domain.ValueObjects;
using Dapper;
using System.Text.Json.Serialization;
using System.Text.Json;
using TechBlog.NewsManager.API.Application.UseCases.BlogUsers.Create;

namespace TechBlog.NewsManager.Tests.IntegrationTests.Fixtures
{
    [CollectionDefinition(nameof(IntegrationTestsFixtureCollection))]
    public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture> { }

    public class IntegrationTestsFixture : IDisposable
    {
        private readonly DbConnection _connection;
        public HttpClient Client { get; }

        public IntegrationTestsFixture()
        {
            _connection = new SqliteConnection("DataSource=:memory:");

            var webApplicationFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var jsonOptions = new JsonSerializerOptions();
                        jsonOptions.Converters.Add(new JsonStringEnumConverter());
                        jsonOptions.PropertyNameCaseInsensitive = true;

                        services.AddSingleton(jsonOptions);

                        services.RemoveAll(typeof(DbConnection));
                        services.AddScoped(o => _connection);
                        _connection.Open();
                        _connection.Execute(CreateDatabase.Script);
                        services.AddDatabase(_connection);
                        services.AddIdentity(_connection);
                    });

                    builder.Configure(app =>
                    {
                        app.UseIntegrationTestsConfiguration();
                    });

                    builder.UseEnvironment("Testing");
                });

            Client = webApplicationFactory.CreateClient();
        }

        ~IntegrationTestsFixture()
        {
            Dispose(false);
        }

        public void WithApiKeyHeader()
        {
            if (!Client.DefaultRequestHeaders.TryGetValues(IntegrationTestsHelper.ApiKeyName, out _))
                Client.DefaultRequestHeaders.Add(IntegrationTestsHelper.ApiKeyName, IntegrationTestsHelper.ApiKeyValue);
        }

        public void WithoutApiKeyHeader()
        {
            if (Client.DefaultRequestHeaders.TryGetValues(IntegrationTestsHelper.ApiKeyName, out _))
                Client.DefaultRequestHeaders.Remove(IntegrationTestsHelper.ApiKeyName);
        }

        public async Task CreateJournalist()
        {
            var body = new CreateBlogUserRequest
                (
                    email: IntegrationTestsHelper.JournalistEmail,
                    password: IntegrationTestsHelper.FakePassword,
                    name: IntegrationTestsHelper.JournalistName,
                    blogUserType: BlogUserType.JOURNALIST
                );

            WithApiKeyHeader();

            await Client.PostAsJsonAsync(CreateBlogUserHandler.Route, body);

            WithoutApiKeyHeader();
        }

        public async Task CreateReader()
        {
            var body = new CreateBlogUserRequest
                (
                    email: IntegrationTestsHelper.ReaderEmail,
                    password: IntegrationTestsHelper.FakePassword,
                    name: IntegrationTestsHelper.ReaderName,
                    blogUserType: BlogUserType.READER
                );

            WithApiKeyHeader();

            await Client.PostAsJsonAsync(CreateBlogUserHandler.Route, body);

            WithoutApiKeyHeader();
        }

        public async Task AuthenticateAsync(BlogUserType userType)
        {
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await GetJwtAsync(userType));
        }

        private async Task<string> GetJwtAsync(BlogUserType userType)
        {
            var accessTokenResponse = await Client.PostAsJsonAsync(LoginHandler.Route, new LoginRequest
            (
                Username: userType == BlogUserType.JOURNALIST ?
                                 IntegrationTestsHelper.JournalistEmail :
                                 IntegrationTestsHelper.ReaderEmail,
                Password: IntegrationTestsHelper.FakePassword
            ));

            var accessToken = await accessTokenResponse.Content.ReadFromJsonAsync<AccessTokenModel>();

            return accessToken.AccessToken;
        }

        public async Task ClearDb()
        {
            await Task.WhenAll
            (
                _connection.ExecuteAsync("DELETE FROM AspNetRoleClaims"),
                _connection.ExecuteAsync("DELETE FROM AspNetRoles"),
                _connection.ExecuteAsync("DELETE FROM AspNetUserClaims"),
                _connection.ExecuteAsync("DELETE FROM AspNetUserLogins"),
                _connection.ExecuteAsync("DELETE FROM AspNetUserRoles"),
                _connection.ExecuteAsync("DELETE FROM AspNetUsers"),
                _connection.ExecuteAsync("DELETE FROM AspNetUserTokens"),
                _connection.ExecuteAsync("DELETE FROM BlogNew")
            );
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClearDb().Wait();
                _connection.Close();
                _connection.Dispose();
                Client.Dispose();
            }
        }
    }
}
