namespace InventoryManager.Services.Models.Clothes
{
    using Data.Models;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class ClothesFormServiceModel
    {
        [Required]
        [MinLength(ClothesNameMinLength)]
        [MaxLength(ClothesNameMaxLength)]
        public string Name { get; set; }

        public ClothesType Type { get; set; }

        [Range(ClothesQuantityAndPriceMinValue, int.MaxValue)]
        public int Quantity { get; set; }

        public ClothesSize Size { get; set; }

        [Display(Name = "Single Price")]
        [Range(ClothesQuantityAndPriceMinValue, int.MaxValue)]
        public double SinglePrice { get; set; }

        [Required]
        [Display(Name = "Picture Url")]
        [MinLength(ClothesPictureMinLength)]
        [MaxLength(ClothesPictureMaxLength)]
        public string PictureUrl { get; set; }

        [Required]
        [MinLength(ClothesDescriptionMinLength)]
        [MaxLength(ClothesDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
