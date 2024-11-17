using Application.Commands;
using Application.Commands.Donuts;
using Domain.Entities;
using System.Net;
using UnitTests.Abstractions;

namespace UnitTests.Donuts
{
    [TestClass]
    public class DeleteDonutTests : BaseTest<DeleteDonutHandler>
    {
        [TestMethod]
        public async Task Delete()
        {
            var donut = new Donut
            {
                Name = "Glaseada original",
                Description = "La original",
                Price = 16.99m
            };

            DbContext.Donuts.Add(donut);
            DbContext.SaveChanges();
            var command = new DeleteCommand<int, Donut>(donut.Id);
            var handler = new DeleteDonutHandler(DbContext);
            var result = await handler.Handle(command, default);
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            var exists = DbContext.Donuts.Any(x => x.Id == donut.Id);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public async Task DeleteNonExistingDonut()
        {
            var command = new DeleteCommand<int, Donut>(-1);
            var handler = new DeleteDonutHandler(DbContext);
            var result = await handler.Handle(command, default);
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
