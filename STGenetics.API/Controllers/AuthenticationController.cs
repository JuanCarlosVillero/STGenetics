
namespace STGenetics.API.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using STGenetics.Application.Request;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public AuthenticationController(IConfiguration configuration)
        {
            this.configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(
            AuthenticationBodyRequest authenticationBodyRequest)
        {
            //Validate username/password
            var user = ValidateUserCredentials(
                authenticationBodyRequest.UserName,
                authenticationBodyRequest.Password
                );

            if(user == null)
            {
                return Unauthorized();
            }

            //Create a token
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(this.configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            //The claims that
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));
            claimsForToken.Add(new Claim("city", user.City));

            var jwtSecurityToken = new JwtSecurityToken(
                this.configuration["Authentication:Issuer"],
                this.configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private UserRequest ValidateUserCredentials(string? userName, string? password)
        {
            return new UserRequest(
                1,
                userName ?? "",
                "Kevin",
                "Dockx",
                "Antwerp");
        }
    }
}
