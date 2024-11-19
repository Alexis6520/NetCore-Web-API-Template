using Application.Commands.Donuts.Create;
using Application.Commands.Donuts.Update;
using Application.DTOs.Donuts;
using Application.Wrappers;
using Domain.Entities;
using IntegrationTests.Abstractions;
using IntegrationTests.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace IntegrationTests
{
    public class DonutTests(CustomWebAppFactory factory) : BaseTest(factory)
    {
        private const string BaseUrl = "donuts";
        private readonly List<int> _toDelete = [];

        [Fact]
        public async Task Create()
        {
            var command = new CreateDonutCommand
            {
                Name = "Frambuesa",
                Description = "Dona rellena de frambuesa",
                Price = 19.99m
            };

            var response = await Client.PostAsJsonAsync(BaseUrl, command);
            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<Result<int>>();

            var donut = await DbContext.Donuts
                .FindAsync([result.Value]);

            Assert.NotNull(donut);
            _toDelete.Add(donut.Id);
            Assert.Equal(command.Name, donut.Name);
            Assert.Equal(command.Description, donut.Description);
            Assert.Equal(command.Price, donut.Price);
        }

        [Fact]
        public async Task GetAll()
        {
            var donuts = new List<Donut>();

            for (int i = 1; i <= 3; i++)
            {
                var donut = new Donut
                {
                    Name = $"Dona {i}",
                    Description = "Una dona",
                    Price = 1000m
                };

                donuts.Add(donut);

                await DbContext.Donuts
                    .AddAsync(donut);
            }

            await DbContext.SaveChangesAsync();
            _toDelete.AddRange(donuts.Select(x => x.Id));
            var result = await Client.GetFromJsonAsync<Result<List<DonutItemDTO>>>(BaseUrl);
            var missing = donuts.Any(x => !result.Value.Any(y => y.Id == x.Id));
            Assert.False(missing);
        }

        [Fact]
        public async Task GetById()
        {
            var donut = new Donut
            {
                Name = $"Dona de Santa",
                Description = "Una dona de Santa",
                Price = 1000m
            };

            await DbContext.Donuts
                .AddAsync(donut);

            await DbContext.SaveChangesAsync();
            _toDelete.Add(donut.Id);
            var result = await Client.GetFromJsonAsync<Result<DonutDTO>>($"{BaseUrl}/{donut.Id}");
            var value = result.Value;
            Assert.Equal(donut.Id, value.Id);
            Assert.Equal(donut.Name, value.Name);
            Assert.Equal(donut.Description, value.Description);
            Assert.Equal(donut.Price, value.Price);
        }

        [Fact]
        public async Task Update()
        {
            var donut = new Donut
            {
                Name = $"Dona de Grinch",
                Description = "Una dona de Grinch",
                Price = 1000m
            };

            await DbContext.Donuts
                .AddAsync(donut);

            await DbContext.SaveChangesAsync();
            _toDelete.Add(donut.Id);

            var command = new UpdateDonutCommand
            {
                Name = "Optimus Prime",
                Description = "El otimu praim",
                Price = 30
            };

            var response = await Client.PutAsJsonAsync(
                $"{BaseUrl}/{donut.Id}",
                command);

            response.EnsureSuccessStatusCode();

            donut = await DbContext.Donuts
                .Where(x => x.Id == donut.Id)
                .AsNoTracking()
                .FirstAsync();

            Assert.Equal(donut.Name, command.Name);
            Assert.Equal(donut.Description, command.Description);
            Assert.Equal(donut.Price, command.Price);
        }

        [Fact]
        public async Task Delete()
        {
            var donut = new Donut
            {
                Name = $"Dona de Grinch",
                Description = "Una dona de Grinch",
                Price = 1000m
            };

            await DbContext.Donuts
                .AddAsync(donut);

            await DbContext.SaveChangesAsync();
            _toDelete.Add(donut.Id);
            var response = await Client.DeleteAsync($"{BaseUrl}/{donut.Id}");
            response.EnsureSuccessStatusCode();

            var exists = await DbContext.Donuts
                .AnyAsync(x => x.Id == donut.Id);

            Assert.False(exists);
        }

        protected override void TestCleanup()
        {
            DbContext.Donuts
                .RemoveRange(DbContext.Donuts.Where(x => _toDelete.Contains(x.Id)));

            DbContext.SaveChanges();
        }
    }
}