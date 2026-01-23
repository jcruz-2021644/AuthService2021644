using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using AuthServiceIN6BM.Application.Interfaces;

namespace AuthServiceIN6BM.Application.DTOs;

public class RegisterDto
{


    [Required]
    [MaxLength(25)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(25)]
    public string Surname { get; set; } = string.Empty;

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "El formato del email tiene que ser valido")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [StringLength(8, MinimumLength = 8)]
    public string Phone { get; set; } = string.Empty;

    public IFieldData? ProfilePicture {get;set;}
}
