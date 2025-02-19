using System.ComponentModel.DataAnnotations;

namespace Shopon.WebAPI.Models
{
    public class ProductVM
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Product name Cannot be Null")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage ="Price cannot be null")]
        public double Price { get; set; }
        public bool AvailableStatus { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public CompanyVM Company { get; set; }
    }
}
