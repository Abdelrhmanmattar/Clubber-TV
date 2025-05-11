using code_quests.Core.DTOs;
using code_quests.Core.entities;
using code_quests.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Services.Services
{
    public class UserService : IUserApp
    {
        private readonly UserManager<UserApp> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public UserService(UserManager<UserApp> _userManager,
            RoleManager<IdentityRole> _roleManager,
            IConfiguration _configuration
            )
        {
            userManager = _userManager;
            roleManager = _roleManager;
            configuration = _configuration;
        }
        public async Task<ResponseDTO> Regisiter_User(Regisiter_DTO _DTO)
        {
            UserApp? user = await userManager.FindByNameAsync(_DTO.UserName);
            if (user != null)
                return new ResponseDTO(HttpStatusCode.BadRequest, "UserName unavaliable");

            UserApp? user2 = await userManager.FindByEmailAsync(_DTO.Email);
            if (user2 != null)
                return new ResponseDTO(HttpStatusCode.BadRequest, "Email unavaliable");

            UserApp newUser = new UserApp()
            { UserName = _DTO.UserName, Email = _DTO.Email, PhoneNumber = _DTO.PhoneNumber };

            IdentityResult result = await userManager.CreateAsync(newUser, _DTO.Password);
            if (!result.Succeeded)
                return new ResponseDTO(HttpStatusCode.InternalServerError, "User registration failed.", result.Errors);


            return new ResponseDTO(HttpStatusCode.Created
                , "User registered successfully."
                , new { UserName = _DTO.UserName, Email = _DTO.Email }
                );
        }
        public async Task<ResponseDTO> LogIn_User(LogIn_DTO _DTO)
        {
            var user = await userManager.FindByEmailAsync(_DTO.Email);
            if (user == null)
                return new ResponseDTO(HttpStatusCode.BadRequest, "The email or password you entered is incorrect");

            var check = await userManager.CheckPasswordAsync(user, _DTO.Password);
            if (!check)
                return new ResponseDTO(HttpStatusCode.BadRequest, "The email or password you entered is incorrect");

            string token = await GenerateToken(user);

            return new ResponseDTO(HttpStatusCode.OK, "Log in Succeed", new
            {
                Name = user.UserName,
                Email = user.Email,
                Token = token
            }
            );
        }

        private async Task<string> GenerateToken(UserApp user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));


            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            SigningCredentials credentials = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserApp> GetCurrentUser(ClaimsPrincipal claims)
        {
            var id = claims.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(id);
            return user;
        }
    }

}
