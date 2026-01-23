using AuthServiceIN6BM.Application.DTOs;

namespace AuthServiceIN6BM.Application.Interfaces;

public interface IUserManagementService
{
    Task<UserReposnseDto> UpdateUserRolAsync(string userId, string roleName);
    Task<IReadOnlyList<string>> GetUserRolesAsync(string userId);

    Task<IReadOnlyList<UserReposnseDto>> GetUserByRoleAsync(string roleName);

}