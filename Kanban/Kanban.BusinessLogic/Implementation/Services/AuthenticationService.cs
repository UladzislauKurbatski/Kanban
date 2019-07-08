using Kanban.BusinessLogic.Authentication;
using Kanban.BusinessLogic.Interfaces.Sarvices;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kanban.BusinessLogic.Implementation.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly TokenManagement _tokenManagement;

        public AuthenticationService(IUserService userService, TokenManagement tokenManagement)
        {
            _userService = userService;
            _tokenManagement = tokenManagement;
        }

        public string GenerateToken(string login, string password)
        {
            var user = _userService.AuthenticateUser(login, password);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return $"Bearer {token}";
        }
    }
}
