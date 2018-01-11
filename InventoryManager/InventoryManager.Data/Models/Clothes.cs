namespace InventoryManager.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Clothes
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ClothesNameMinLength)]
        [MaxLength(ClothesNameMaxLength)]
        public string Name { get; set; }

        public ClothesType Type { get; set; }
        
        [Range(ClothesQuantityAndPriceMinValue, int.MaxValue)]
        public int Quantity { get; set; }

        public ClothesSize Size { get; set; }

        [Range(ClothesQuantityAndPriceMinValue, int.MaxValue)]
        public double SinglePrice { get; set; }

        [Required]
        [MinLength(ClothesPictureMinLength)]
        [MaxLength(ClothesPictureMaxLength)]
        public string PictureUrl { get; set; }

        [Required]
        [MinLength(ClothesDescriptionMinLength)]
        [MaxLength(ClothesDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
