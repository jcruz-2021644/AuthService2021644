

using AuthServiceIN6BM.Domain.Entities;

namespace AuthServiceIN6BM.Domain.Interfaces;

public interface IUserRepository
{
/*asincrono*/
Task<User> CreateAsync(User user);

/*manda a traer al usuario con el id*/
Task<User> GetByIdAsync(string id);
/*devuelve el usuario segun el email o lo devuelve nulo "?"*/
Task<User?> GetByEmailAsync(string email);

Task<User?> GetByUsernameAsync(string username);

Task<User?> GetByEmailVerificationTokenAsync(string token);

Task<User?>  GetByPasswordResetTokenAsync(string email);
/*devuelvce si existe el email o no*/
Task<bool>  ExistsByEmailAsync(string email);
Task<bool>  ExistsByUserNameAsync(string username);


/*nos da el usuraio ya modificado*/
Task<User> UpdateAsync(User user);
Task<bool> DeleteAsync(string id);

/*a tal usurio le asignamos el rol que esta en tal id*/
Task UpdateUserRoleAsync(string userId, string roleId);


}