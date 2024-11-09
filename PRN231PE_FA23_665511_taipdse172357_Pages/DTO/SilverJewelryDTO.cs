using System.ComponentModel.DataAnnotations;

namespace PRN231PE_FA23_665511_taipdse172357_Pages.DTO
{
    public class SilverJewelryDTO
    {
        public string SilverJewelryId { get; set; } = "0";

        [Required(ErrorMessage = "SilverJewelryName is required")]
        //[RegularExpression(@"^(?:[A-Z][a-zA-Z0-9@#]*\s?)+$", ErrorMessage = "Each word in FullName must begin with a capital letter and can contain letters, digits, space, @, and #.")]
        public string SilverJewelryName { get; set; } = null!;

        [Required(ErrorMessage = "SilverJewelryDescription is required")]
        //[StringLength(100, MinimumLength = 9, ErrorMessage = "Achievements must be between 9 and 100 characters.")]
        public string? SilverJewelryDescription { get; set; }

        [Required(ErrorMessage = "MetalWeight is required")]
        public decimal? MetalWeight { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "ProductionYear is required")]
        public int? ProductionYear { get; set; }

        [Required(ErrorMessage = "CreatedDate is required")]
        public DateTime? CreatedDate { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string? CategoryId { get; set; }


        public virtual CategoryDTO? Category { get; set; }
    }
}
