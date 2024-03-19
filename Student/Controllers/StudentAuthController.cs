using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Student.Models.DTOs;
using Student.Repositories;

namespace Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public StudentAuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]StudentRegisterDto studentRegisterDto)
        {
            var identityUser = new IdentityUser()
            {
                UserName=studentRegisterDto.Username,
                Email=studentRegisterDto.Username,
            };
           var identityResult=await userManager.CreateAsync(identityUser, studentRegisterDto.Password);
            if (identityResult.Succeeded)
            {

                if (studentRegisterDto.Roles!=null&& studentRegisterDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, studentRegisterDto.Roles); 

                    if (identityResult.Succeeded)
                    {
                        return Ok("User Registered Successfully! Please Login");
                    }
                }
            }
            return BadRequest("Something Went Wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]StudentLoginDto studentLoginDto)
        {
            var user=await userManager.FindByEmailAsync(studentLoginDto.Username);

            if (user != null)
            {
                var userPasswordResult=await userManager.CheckPasswordAsync(user, studentLoginDto.Password);

                if (userPasswordResult)
                {
                    //Get the roles from userManager class
                    var roles=await userManager.GetRolesAsync(user);
                    if (roles!=null)
                    {
                        //create jwt token
                        var jwtToken= tokenRepository.CreateJwtToken(user, roles.ToList());
                        var response = new LoginJwtTokenDto()
                        {
                            JwtToken = jwtToken,
                        };

                        return Ok(response); 
                    }
                }
            }
            return BadRequest("Something went wrong,Please Check your Username and Password once");
        }
    }
}
