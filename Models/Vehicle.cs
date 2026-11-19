using System.ComponentModel.DataAnnotations;

namespace DriveHub.Models;

// The vehicle a buyer saved for the My Vehicle screen and catalog filtering.
// BuyerId is the Firebase uid of that buyer (ApplicationUser.Id).
public class Vehicle
{
    public string Id { get; set; } = "";

    [Required(ErrorMessage = "Buyer is required")]
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Buyer id cannot be longer than 128 characters")]
    public string BuyerId { get; set; } = "";

    [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100")]
    [Display(Name = "Year")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Make is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Make must be between 2 and 100 characters")]
    [Display(Name = "Make")]
    public string MakeName { get; set; } = "";

    [Required(ErrorMessage = "Model is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Model must be between 1 and 100 characters")]
    [Display(Name = "Model")]
    public string ModelName { get; set; } = "";
}
