using SempreBella.ViewModels;
using System.Security.Claims;

namespace SempreBella.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ClaimsPrincipal> AuthenticateUserAsync(LoginInputModel input);
    }
}
