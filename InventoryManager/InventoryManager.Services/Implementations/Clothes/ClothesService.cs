namespace InventoryManager.Services.Implementations.Clothes
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Clothes;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ClothesService : IClothesService
    {
        private readonly InventoryManagerDbContext db;

        public ClothesService(InventoryManagerDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(
            string name,
            ClothesType type,
            int quantity,
            ClothesSize size,
            double singlePrice,
            string pictureUrl,
            string description)
        {
            var clothes = new Clothes
            {
                Name = name,
                Type = type,
                Quantity = quantity,
                Size = size,
                SinglePrice = singlePrice,
                PictureUrl = pictureUrl,
                Description = description
            };

            this.db.Add(clothes);
            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClothesListingServiceModel>> AllClothesAsync()
            => await this.db.Clothes
                .Select(c => new ClothesListingServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    Quantity = c.Quantity,
                    SinglePrice = c.SinglePrice
                })
                .ToListAsync();
    }
}
