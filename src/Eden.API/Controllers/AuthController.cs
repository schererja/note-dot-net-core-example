using Eden.API.Models;
using Eden.API.Services;
using Eden.Core.Entities.Auth;
using Eden.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eden.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RepositoryContext _repositoryContext;
        private readonly TokenService _tokenService;
        public AuthController(UserManager<ApplicationUser> userManager, RepositoryContext repositoryContext, TokenService tokenService)
        {
            _userManager = userManager;
            _repositoryContext = repositoryContext;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userManager.CreateAsync(
                new ApplicationUser { UserName = request.Username, Email = request.Email },
                request.Password
            );
            if (result.Succeeded)
            {
                request.Password = "";
                return CreatedAtAction(nameof(Register), new { email = request.Email }, request);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.Email != null)
            {
                var managedUser = await _userManager.FindByEmailAsync(request.Email);
                if (managedUser == null)
                {
                    return BadRequest("Bad Credentials");
                }
                var isPasswordValid = request.Password != null && await _userManager.CheckPasswordAsync(managedUser, request.Password);
                if (!isPasswordValid)
                {
                    return BadRequest("Bad Credentials");
                }
            }

            var userInDb = _repositoryContext.Users.FirstOrDefault(user => user.Email == request.Email);
            if (userInDb is null)
            {
                return Unauthorized();
            }

            var accessToken = _tokenService.CreateToken(userInDb);
            await _repositoryContext.SaveChangesAsync();
            return Ok(new AuthResponse
            {
                Username = userInDb.UserName,
                Email = userInDb.Email,
                Token = accessToken,
            });
        }
    }
}
