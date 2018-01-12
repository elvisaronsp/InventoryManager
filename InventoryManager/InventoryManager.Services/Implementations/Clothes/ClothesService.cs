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

        public async Task<IEnumerable<ClothesListingServiceModel>> AllClothesAsync(string sort, string order)
        {
            var result = await this.db.Clothes
                .Select(c => new ClothesListingServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    Quantity = c.Quantity,
                    SinglePrice = c.SinglePrice
                })
                .ToListAsync();

            var sortType = sort + "_" +order;

            switch (sortType)
            {
                case "name_ascending":
                    return result.OrderBy(c => c.Name);
                case "name_descending":
                    return result.OrderByDescending(c => c.Name);
                case "type_ascending":
                    return result.OrderBy(c => c.Type);
                case "type_descending":
                    return result.OrderByDescending(c => c.Type);
                case "quantity_ascending":
                    return result.OrderBy(c => c.Quantity);
                case "quantity_descending":
                    return result.OrderByDescending(c => c.Quantity);
                case "singlePrice_ascending":
                    return result.OrderBy(c => c.SinglePrice);
                case "singlePrice_descending":
                    return result.OrderByDescending(c => c.SinglePrice);
                default:
                    return result;
            }
        }
    }
}
