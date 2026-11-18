using System.ComponentModel.DataAnnotations;
namespace DriveHub.Models;

public class CartItem
{
    public string Id { get; set; } = "";
    [Required]
    public string BuyerUid{ get; set;} = "";

    [Required]
    [Range(1, 99, ErrorMessage = "Quantity must be between 1 and 99.")]
    public int Quantity { get; set; } = 1;

    [Required]
    [Range(0.01, 1000000, ErrorMessage = "Price must be between 0.01 and 1,000,000.")]
    public decimal Price { get; set; } = 0.01m;

    [Required]
    [StringLength(120)]
    public string partName { get; set; } = "";

    public string? ImageUrl { get; set; } = "";

    public string SellerId { get; set; } = "";

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;



    
}
