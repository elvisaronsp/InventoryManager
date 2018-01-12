namespace InventoryManager.Services.Models.Clothes
{
    using Data.Models;

    public class ClothesListingServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ClothesType Type { get; set; }

        public int Quantity { get; set; }

        public double SinglePrice { get; set; }

        public string OwnerId { get; set; }
    }
}
