using System.ComponentModel.DataAnnotations;
namespace AuthServiceIN6BM.Domain.Entities;

public class User
{
    [Key]
    [MaxLength(16)]
    public string Id { get; set; } = string.Empty;


    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(25, ErrorMessage = "El nombre no pude tener mas de 25 caracteres ")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [MaxLength(25, ErrorMessage = "El apellido no pude tener mas de 25 caracteres ")]
    public string SurName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    [MaxLength(25, ErrorMessage = "El nombre de usuario no pude tener mas de 25 caracteres ")]
    public string UserName { get; set; } = string.Empty;


    [Required(ErrorMessage = "El correo electronico es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato valido ")]
    [MaxLength(150, ErrorMessage = "El correo no pude tener mas de 150 caracteres ")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contrasña es obligatoria")]
    [EmailAddress(ErrorMessage = "La contraseña tiene que tener 8 caracteres")]
    [MaxLength(255, ErrorMessage = "El correo no pude tener mas de 255 caracteres ")]
    public string Password { get; set; } = string.Empty;


    public bool Status {get;set;} = false;
    
    public DateTime CreatedAt {get; set;}

    public DateTime UpdateAt{get; set;}

    /*controles de navegacion*/
    public UserProfile UserProfile{get;set;} = null!;
    

    /*para traer todos los roles*/
    public ICollection< UserRole> UserRoles {get;set;} = [];

    public UserEmail UserEmail {get;set;} = null!;

    public UserPasswordReset UserPasswordReset{get;set;} = null!;

}