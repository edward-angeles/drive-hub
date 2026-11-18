using System.ComponentModel.DataAnnotations;

namespace DriveHub.Models;

public enum UserRole
{
    Buyer,
    Seller
}

public class ShippingAddress
{
    [Required(ErrorMessage = "Street is required")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 120 characters")]
    [Display(Name = "Street")]
    public string Street { get; set; } = "";

    [Required(ErrorMessage = "City is required")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "City must be between 2 and 80 characters")]
    [Display(Name = "City")]
    public string City { get; set; } = "";

    [Required(ErrorMessage = "Province is required")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "Province must be between 2 and 30 characters")]
    [Display(Name = "Province")]
    public string Province { get; set; } = "";

    [Required(ErrorMessage = "Postal code is required")]
    [StringLength(7, MinimumLength = 6, ErrorMessage = "Postal code must be 6 or 7 characters")]
    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; } = "";
}

// Profile stored for each Firebase account.
// The Firestore document id is the Firebase uid. CarPart.SellerId,
// cart ownership, and order ownership all store this same uid.
public class ApplicationUser
{
    public string Id { get; set; } = "";

    [Required(ErrorMessage = "Full name is required")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 60 characters")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [StringLength(254, ErrorMessage = "Email cannot be longer than 254 characters")]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = "";

    // Unset until registration. The account has no role before the user chooses Buyer or Seller.
    [Required(ErrorMessage = "Choose Buyer or Seller")]
    [Display(Name = "Role")]
    public UserRole? Role { get; set; }

    public ShippingAddress? DefaultShippingAddress { get; set; }
}
