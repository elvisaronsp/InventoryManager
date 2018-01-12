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
            string userId,
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
                Description = description,
                OwnerId = userId
            };

            this.db.Add(clothes);
            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClothesListingServiceModel>> AllClothesAsync(string userId, string sort, string order)
        {
            var result = await this.db.Clothes
                .Select(c => new ClothesListingServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    Quantity = c.Quantity,
                    SinglePrice = c.SinglePrice,
                    OwnerId = c.OwnerId
                })
                .Where(c => c.OwnerId == userId)
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
                    return result.OrderByDescending(c => c.Id);
            }
        }

        public async Task<bool> ProductExistForUserAsync(int id, string userId)
            => await this.db.Clothes.AnyAsync(c => c.Id == id && c.OwnerId == userId);

        public async Task<ClothesFormServiceModel> DetailsAsync(int id)
        {
            var clothes = await this.db.Clothes.FindAsync(id);

            return new ClothesFormServiceModel
            {
                Name = clothes.Name,
                Type = clothes.Type,
                Quantity = clothes.Quantity,
                Size = clothes.Size,
                SinglePrice = clothes.SinglePrice,
                PictureUrl = clothes.PictureUrl,
                Description = clothes.Description
            };
        }

        public async Task DeleteAsync(int id)
        {
            var product = await this.db.Clothes.FindAsync(id);

            this.db.Remove(product);
            await this.db.SaveChangesAsync();
        }

        public async Task<ClothesFormServiceModel> GetProductByIdAsync(int id)
        {
            var product = await this.db.Clothes.FindAsync(id);

            return new ClothesFormServiceModel
            {
                Name = product.Name,
                Type = product.Type,
                Quantity = product.Quantity,
                Size = product.Size,
                SinglePrice = product.SinglePrice,
                PictureUrl = product.PictureUrl,
                Description = product.Description
            };
        }

        public async Task EditAsync(
            int id,
            string name,
            ClothesType type,
            int quantity,
            ClothesSize size,
            double singlePrice,
            string pictureUrl,
            string description)
        {
            var product = await this.db.Clothes.FindAsync(id);

            product.Name = name;
            product.Type = type;
            product.Quantity = quantity;
            product.Size = size;
            product.SinglePrice = singlePrice;
            product.PictureUrl = pictureUrl;
            product.Description = description;

            await this.db.SaveChangesAsync();
        }
    }
}
