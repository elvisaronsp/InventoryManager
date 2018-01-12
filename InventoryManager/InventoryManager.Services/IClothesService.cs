namespace InventoryManager.Services
{
    using InventoryManager.Data.Models;
    using Models.Clothes;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClothesService
    {
        Task AddAsync(
            string name,
            ClothesType type,
            int quantity,
            ClothesSize size,
            double singlePrice,
            string pictureUrl,
            string description);

        Task<IEnumerable<ClothesListingServiceModel>> AllClothesAsync(string sort, string order);

        Task<ClothesFormServiceModel> DetailsAsync(int id);

        Task<bool> ProductExistAsync(int id);

        Task DeleteAsync(int id);

        Task<ClothesFormServiceModel> GetProductByIdAsync(int id);

        Task EditAsync(
            int id,
            string name,
            ClothesType type,
            int quantity,
            ClothesSize size,
            double singlePrice,
            string pictureUrl,
            string description);
    }
}
