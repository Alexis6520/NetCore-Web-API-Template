using Application.Commands.Donuts.Update;
using Domain.Entities;
using System.Net;
using UnitTests.Abstractions;

namespace UnitTests.Donuts
{
    [TestClass]
    public class UpdateDonutTests : BaseTest<UpdateDonutHandler>
    {
        [TestMethod]
        public async Task Update()
        {
            var donut = new Donut
            {
                Name = "Frambuesa",
                Description = "Dona rellena de frambuesa",
                Price = 19.99m
            };

            DbContext.Donuts.Add(donut);
            DbContext.SaveChanges();

            var command = new UpdateDonutCommand
            {
                Id = donut.Id,
                Name = "Nuevo nombre",
                Description = "Nueva descripción",
                Price = 17000m
            };

            var handler = new UpdateDonutHandler(DbContext, Logger);
            var result = await handler.Handle(command, default);
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            donut = DbContext.Donuts.Find(donut.Id);
            Assert.AreEqual(donut.Name, command.Name);
            Assert.AreEqual(donut.Description, command.Description);
            Assert.AreEqual(donut.Price, command.Price);
        }

        [TestMethod]
        public async Task UpdateNonExistingDonut()
        {
            var command = new UpdateDonutCommand
            {
                Id = -1
            };

            var handler = new UpdateDonutHandler(DbContext, Logger);
            var result = await handler.Handle(command, default);
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
