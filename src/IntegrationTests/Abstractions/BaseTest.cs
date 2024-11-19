using IntegrationTests.Services;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace IntegrationTests.Abstractions
{
    public abstract class BaseTest(CustomWebAppFactory factory) :
        IClassFixture<CustomWebAppFactory>,
        IDisposable
    {
        private readonly CustomWebAppFactory _factory = factory;
        private HttpClient _httpClient;
        private IServiceScope _scope = factory.Services.CreateScope();
        private ApplicationDbContext _dbContext;

        protected HttpClient Client => _httpClient ??= _factory.CreateClient();
        protected ApplicationDbContext DbContext => _dbContext ??= _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        protected virtual void TestCleanup() { }

        public void Dispose()
        {
            TestCleanup();
            _httpClient?.Dispose();
            _httpClient = null;
            _dbContext?.Dispose();
            _dbContext = null;
            _scope?.Dispose();
            _scope = null;
            GC.SuppressFinalize(this);
        }
    }
}
