using AuthServiceIN6BM.Domain.Entities;
namespace AuthServiceIN6BM.Domain.Interfaces;

public interface IRoleRepository
{
    /*traer un rol por medio del nombre*/
    Task<Role?> GetByNameAsync(string name);

    /*contar usuraios con un determinado rol*/
    Task<int> CountUsersInRoleAsync(string roleName);

    /*lista de solo lectura de tipo usuarios*/
    Task<IReadOnlyList<User>> GetUsersByRoleAsync(string roleName);

    /*lista de solo lectura buscamos un usurio y nos da todos los roles que el tiene */
    Task<IReadOnlyList<string>> GetUserRoleNamesAsync(string userId);

}