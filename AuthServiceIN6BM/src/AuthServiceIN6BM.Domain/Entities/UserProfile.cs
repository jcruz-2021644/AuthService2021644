using System.ComponentModel.DataAnnotations;

namespace AuthServiceIN6BM.Domain.Entities;

public class UserProfile
{
    [Key]
    [MaxLength(16)]
    public string Id { get; set; } = string.Empty;

    [Required]
    [MaxLength(16)]
    public string UserId { get; set; } = string.Empty;


    [MaxLength(512)]
    public string ProfilePicture { get; set; } = string.Empty;


    [Required]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "El numero de telefono tiene que tener solo 8 digitos exactamente")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "El telefono solo debe tener numeros")]
    public string Phone { get; set; } = string.Empty;

    [Required]
    public User User { get; set; } = null!;

}