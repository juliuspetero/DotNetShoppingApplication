using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ShoppingApplication.Models;
using ShoppingApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        // AspNet Core dependency injector create the instance of these classes and inject them into our constructor at runtime
        public AccountsController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;

            // This retrieves keys coming from appsettings.json
            this.configuration = configuration;
        }


        #region Register a user in the /api/account/register
        [Route("Register")] // /Register [FromBody] RegisterViewModel model
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterViewModel model)
        {
                //Create an instance of application user
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");


                // Generate a token to use for the next request
                // This the secret key for token generation
                var signingKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:Signingkey"]));

                int expiryInMinutes = Convert.ToInt32(configuration["Jwt:ExpiryInMinutes"]);

                // The token is created with the following part
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Site"],
                    audience: configuration["Jwt:Site"],
                    expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        phoneNumber = user.PhoneNumber,
                        email = user.Email,
                        role = await userManager.GetRolesAsync(user),
                        expiration = token.ValidTo
                    });
                 }
            
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("error", error.Description);
                }

                return BadRequest(ModelState);

        }
        #endregion


        #region Login to get token by navigating to /api/account/login
        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(model.Email);

            // The user is given token when the he is present in the db and the password is also validated correct
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                // Instantiate a new claim based on the user name
                var claim = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
                };

                // This the secret key for token generation
                var signingKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:Signingkey"]));

                int expiryInMinutes = Convert.ToInt32(configuration["Jwt:ExpiryInMinutes"]);

                // The token is created with the following part
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Site"],
                    audience: configuration["Jwt:Site"],
                    expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        phoneNumber = user.PhoneNumber,
                        email = user.Email,
                        role = await userManager.GetRolesAsync(user),
                        expiration = token.ValidTo
                    });
            }

            // The user has provided incorrect authentication credentials
            return Unauthorized(
                new
                {
                    status = "Incorrect login credentials"
                });
        }
        #endregion
    }
}
