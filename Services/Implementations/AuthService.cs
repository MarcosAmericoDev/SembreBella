using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Services.Interfaces;
using SempreBella.ViewModels;
using System.Security.Claims;

namespace SempreBella.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<ClaimsPrincipal> AuthenticateUserAsync(LoginInputModel input)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == input.Email
            && x.SenhaHash == input.Senha);

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
