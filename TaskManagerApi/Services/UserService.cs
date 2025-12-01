using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using TaskManagerApi.Models;
using TaskManagerApi.Services.Interfaces;
using static TaskManagerApi.Models.Authorization;

namespace TaskManagerApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly ITokenService _tokenService;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWT> jwt,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _tokenService = tokenService;
        }

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Add user to database
                    await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                    return $"User Registered with username {user.UserName} and email {user.Email}";
                }
                else
                {
                    return $"Error: {string.Join(", ", result.Errors.Select(e => e.Description))}";
                }
            }
            else
            {
                return $"Email {user.Email} is already registered.";
            }
        }

        public async Task<AuthenticationModel> LoginAsync(LoginModel model)
        {
            var authenticationModel = new AuthenticationModel();
            ApplicationUser? user = null;

            // Check if the user exists by email  
            if (!string.IsNullOrEmpty(model.EmailOrUserName))
            {
                user = await _userManager.FindByEmailAsync(model.EmailOrUserName);
            }

            // If not found by email, check by username  
            if (user == null && !string.IsNullOrEmpty(model.EmailOrUserName))
            {
                user = _userManager.Users.FirstOrDefault(u => u.UserName == model.EmailOrUserName);
            }

            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = "Invalid email or username.";
                return authenticationModel;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = "Invalid password.";
                return authenticationModel;
            }

            var token = await _tokenService.GenerateJwtToken(user);

            authenticationModel.IsAuthenticated = true;
            authenticationModel.Message = "Login successful.";
            authenticationModel.Token = token;
            authenticationModel.Email = user.Email;
            authenticationModel.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user);
            authenticationModel.Roles = rolesList.ToList();

            return authenticationModel;

        }
    }
}
