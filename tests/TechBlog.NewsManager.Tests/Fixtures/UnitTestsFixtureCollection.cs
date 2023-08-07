namespace TechBlog.NewsManager.Tests.Fixtures
{
    [CollectionDefinition(nameof(UnitTestsFixtureCollection))]
    public class UnitTestsFixtureCollection : ICollectionFixture<UnitTestsFixture> { }

    public class UnitTestsFixture
    {
        public AuthorizationFixtures Authorization { get; set; }
        public HttpContextFixtures HttpContext { get; set; }

        public UnitTestsFixture()
        {
            Authorization = new();
            HttpContext = new();
        }
    }
}
