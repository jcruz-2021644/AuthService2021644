using System.ComponentModel.DataAnnotations;

namespace AuthServiceIN6BM.Application.DTOs.Email;


public class ForgotPasswordDto
{
    public string Email { get; set; } = string.Empty;
}