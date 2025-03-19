using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Budgetify.Models.DTOs;
using Budgetify.Models.Response;
using Microsoft.IdentityModel.Tokens;

namespace Budgetify.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public JwtService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _secret = _configuration["Jwt:Secret"];
            _issuer = _configuration["Jwt:Issuer"];
            _audience = _configuration["Jwt:Audience"];
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateToken(LoginResponse response)
        {
            var claims = new[]
            {
                new Claim("id", response.UserId.ToString()),
                new Claim("name", response.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secret);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }
        
        public UserDto GetUserToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return new UserDto();
            }

            var token = httpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            if (string.IsNullOrWhiteSpace(token))
            {
                return new UserDto();
            }

            var principal = ValidateToken(token);
            if (principal == null)
            {
                return new UserDto();
            }

            var username = principal.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var userId = principal.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userId))
            {
                return new UserDto();
            }

            return new UserDto
            {
                Username = username,
                UserId = int.TryParse(userId, out var parsedUserId) ? parsedUserId : 0
            };
        }
    }
}