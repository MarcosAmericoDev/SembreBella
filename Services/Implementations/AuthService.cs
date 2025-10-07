using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Model;
using SempreBella.Repositories.Interfaces;
using SempreBella.Services.Interfaces;
using SempreBella.Utilities;
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
            string senhaHash = PasswordHasher.HashPassword(input.Senha);
            var user = await _usuarioRepository.GetByEmailAndSenhaAsync(input.Email, senhaHash);

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

        public async Task<Usuario?> RegisterUserAsync(RegisterInputModel input)
        {
            var existingUser = await _usuarioRepository.GetByEmailAsync(input.Email);
            if (existingUser != null)
            {
                return null;
            }

            var senhaHash = PasswordHasher.HashPassword(input.Senha);

            var newUser = new Usuario
            {
                Email = input.Email,
                SenhaHash = senhaHash,
                Papel = input.Papel
            };

            await _usuarioRepository.AddAsync(newUser);
            await _usuarioRepository.SaveChangesAsync();

            return newUser;
        }
    }
}
