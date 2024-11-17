using Microsoft.Extensions.Logging;
using Moq;
using Services;
using UnitTests.Services;

namespace UnitTests.Abstractions
{
    public abstract class BaseTest<THandler> : IDisposable where THandler : class
    {
        protected ILogger<THandler> Logger { get; private set; } = new Mock<ILogger<THandler>>().Object;
        protected ApplicationDbContext DbContext { get; private set; } = new InMemoryDbContext(typeof(THandler).Name);

        public void Dispose()
        {
            Logger = null;
            DbContext.Dispose();
            DbContext = null;
            GC.SuppressFinalize(this);
        }
    }
}
