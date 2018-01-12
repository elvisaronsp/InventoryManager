namespace InventoryManager.Test.Services
{
    using Data;
    using FluentAssertions;
    using InventoryManager.Data.Models;
    using InventoryManager.Services.Implementations.Clothes;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ClothesServiceTest
    {
        /*
        [Fact]
        public async Task AddAsyncShouldAddItemInDatabase()
        {
            // Arrange
            var db = GetDatabase();

            var clothesService = new ClothesService(db);

            // Act
            await clothesService.AddAsync(
                "userId",
                "Test Name",
                ClothesType.Jacket,
                1,
                ClothesSize.S,
                1,
                "picture",
                "description");

            var result = db.Clothes.Find(1);

            // Assert
            result.Id.Should().Be(1);
            result.Name.Should().Be("Test Name");
        }
        */

        [Fact]
        public async Task AllClothesAsyncShouldReturnUserItemsSortedWithFilter()
        {
            // Arrange
            var db = GetDatabase();

            var item1 = new Clothes { OwnerId = "owner1", Name = "Item 1" };
            var item2 = new Clothes { OwnerId = "owner2", Name = "Item 2" };
            var item3 = new Clothes { OwnerId = "owner1", Name = "Another" };
            var item4 = new Clothes { OwnerId = "owner1", Name = "Item 3" };

            await db.AddRangeAsync(item1, item2, item3, item4);
            await db.SaveChangesAsync();

            var clothesService = new ClothesService(db);

            // Act
            var result = await clothesService.AllClothesAsync("owner1", "name", "descending", "item");

            // Assert
            result
                .Should()
                .Match(c => c.ElementAt(0).OwnerId == "owner1"
                    && c.ElementAt(0).Name == "Item 3")
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task ProductExistForUserAsyncShouldReturnTrueIfUserHaveItemWithPassedId()
        {
            // Arrange
            var db = GetDatabase();

            var item = new Clothes { Id = 1, OwnerId = "testOwner" };

            await db.AddAsync(item);
            await db.SaveChangesAsync();

            var clothesService = new ClothesService(db);

            // Act
            var result = await clothesService.ProductExistForUserAsync(1, "testOwner");

            // Assert
            result
                .Should()
                .Be(true);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteItemWithPassedId()
        {
            // Arrange
            var db = GetDatabase();

            var item1 = new Clothes { Id = 1 };
            var item2 = new Clothes { Id = 2 };

            await db.AddRangeAsync(item1, item2);
            await db.SaveChangesAsync();

            var clothesService = new ClothesService(db);

            // Act
            await clothesService.DeleteAsync(1);

            var result = await db.Clothes.ToListAsync();

            // Assert
            result
                .Should()
                .Match(c => c.ElementAt(0).Id == 2)
                .And
                .HaveCount(1);
        }

        [Fact]
        public async Task EditAsyncShouldEditItemWithPassedId()
        {
            // Arrange
            var db = GetDatabase();

            var item = new Clothes
            {
                Id = 1,
                Description = "Test Description",
                Name = "Test Name",
                PictureUrl = "Test Picture",
                Quantity = 1,
                SinglePrice = 1,
                Size = ClothesSize.L,
                Type = ClothesType.Jacket
            };

            await db.AddAsync(item);
            await db.SaveChangesAsync();

            var clothesService = new ClothesService(db);

            // Act
            await clothesService.EditAsync(
                1,
                "Name Changed",
                ClothesType.Jacket,
                1,
                ClothesSize.L,
                1,
                "Test Picture",
                "Test Description");

            var result = await db.Clothes.FindAsync(1);

            // Assert
            result
                .Name
                .Should()
                .Be("Name Changed");
        }

        private static InventoryManagerDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<InventoryManagerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new InventoryManagerDbContext(dbOptions);
        }
    }
}
