using System.ComponentModel.DataAnnotations;

namespace AuthServiceIN6BM.Domain.Entities;

public class UserEmail
{
    [Key]
    [MaxLength(16)]
    public string Id { get; set; } = string.Empty;

    [Required]
    [MaxLength]
    public string UserId { get; set; } = string.Empty;
    [Required]
    public bool EmailVerified { get; set; } = false;


    /*signo ? porque indica que ese valor puede ser nulo*/
    [MaxLength(256)]
    public string? EmailVerificationToken { get; set; }


    public DateTime? EmailVerificationTokenExpiry { get; set; }


    public User User { get; set; } = null!;
}