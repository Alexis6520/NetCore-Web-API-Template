using Application.Commands.Donuts.Create;
using System.Net;
using UnitTests.Abstractions;

namespace UnitTests.Donuts
{
    [TestClass]
    public class CreateDonutTests : BaseTest<CreateDonutHandler>
    {
        [TestMethod]
        public async Task Create()
        {
            var command = new CreateDonutCommand
            {
                Name = "Frambuesa",
                Description = "Dona rellena de frambuesa",
                Price = 19.99m
            };

            var handler = new CreateDonutHandler(DbContext, Logger);
            var result = await handler.Handle(command, default);
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            var exists = DbContext.Donuts.Any(x => x.Id == result.Value);
            Assert.IsTrue(exists);
        }
    }
}
