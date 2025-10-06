using SempreBella.Services.Interfaces;
using SempreBella.ViewModels;
using System.Security.Claims;

namespace SempreBella.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public async Task<ClaimsPrincipal> AuthenticateUserAsync(LoginInputModel input)
        {
            bool isValidUser = input.Email == "admin@gmail.com" && input.Senha == "123456";

            if (!isValidUser)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, input.Email),
                new Claim(ClaimTypes.Email, input.Email),
                new Claim(ClaimTypes.Role, "Administrador")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
