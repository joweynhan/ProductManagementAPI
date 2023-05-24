using ProductManagementAPI.DTO;
using ProductManagementAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace ProductManagementAPI.Repository
{
    public interface IAccountDBRepository
    {
        Task<IdentityResult> SignUpUserAsync(ApplicationUser user, string password);
        Task<SignInResult> SignInUserAsync(LoginDTO loginDTO);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
    }
}
