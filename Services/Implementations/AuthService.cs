using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Repositories.Interfaces;
using SempreBella.Services.Interfaces;
using SempreBella.ViewModels;
using System.Security.Claims;

namespace SempreBella.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        
        public async Task<ClaimsPrincipal> AuthenticateUserAsync(LoginInputModel input)
        {
            var user = await _usuarioRepository.GetByEmailAndSenhaAsync(input.Email, input.Senha);

            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, input.Email),
                new Claim(ClaimTypes.Email, input.Email),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Papel)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
