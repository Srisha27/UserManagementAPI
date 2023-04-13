using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Service.Models;
using UserManagement.Service.Services;
using UserManagementAPI.Entity;
using UserManagementAPI.Models;
using UserManagementAPI.Models.Authentication.SignUp;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
      
            private readonly UserManager<AddUserDetails> _userManager;
            private readonly IConfiguration _configuration;
            private readonly IEmailService _emailService;
        //private readonly RoleManager<IdentityRole> _roleManager;
        public AuthenticationController(UserManager<AddUserDetails> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
        }
        [HttpPost("UserRegister")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            //var UserExit = await _userManager.FindByEmailAsync(registerUser.Email);


            //if (UserExit != null)
            //{
            //    return StatusCode(StatusCodes.Status403Forbidden,
            //    new Response { Status = "Error", Message = "User already exits!" });
            //}


            //var user = new AddUserDetails()
            //{
            //    Email = registerUser.Email,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    FirstName = registerUser.FirstName,
            //    LastName = registerUser.LastName,   
            //    PhoneNumber = registerUser.PhoneNumber,
            //    Address = registerUser.Address,
            //    DOB = (DateTime)registerUser.DOB,


            //};



            //var result = await _userManager.CreateAsync(user, registerUser.Password);

            //return result.Succeeded

            //? StatusCode(StatusCodes.Status201Created,
            //new Response { Status = "Success", Message = "User Created Successfully" })
            //: StatusCode(StatusCodes.Status500InternalServerError,
            //new Response { Status = "Error", Message = "User Failed to  Create " });

            var user = new AddUserDetails
            {
                Email = registerUser.Email,
                UserName=registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                PhoneNumber = registerUser.PhoneNumber,
                Address = registerUser.Address,
                DOB = (DateTime)registerUser.DOB,


            };


            var IsExists = await _userManager.FindByEmailAsync(registerUser.Email);

            if(IsExists!=null && IsExists.Email==registerUser.Email)

            {
                return Problem(statusCode: 403, detail: $"User already exist :{ registerUser.Email}");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var ConfirmationLink = Url.Action(nameof(ConfirmEmail),"Authentication", new {token, email=user.Email}, Request.Scheme);
            var message = new Message(new string[] { user.Email! }, "Confirmation email Link", ConfirmationLink!);
            _emailService.SendEmail(message);
             var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                return Ok($"User Created Successfully");
            }
            else
            {
                return Problem(statusCode: 400, detail: $"Password is Incorrect");
            }
            return StatusCode(500);
        }

        //[HttpGet("Get")]

        //public IActionResult TestEmail()
        //{
        //  var message = new Message(new string[]
        //    {
        //        "srisakthi9085@gmail.com",
        //        "umadevip1227@gmail.com",
        //        "gangadharanstr@gmail.com",
        //        "gangadharanmaddy2@gmail.com",
        //        "sathish020501@gmail.com",
        //        "jgangadharan25@gmail.com",
        //        "gangadharan2199@gmail.com",
        //        //"sri.sakthi@abstract-tech.com",
        //        //"gangadharan.jeyaseelan@abstract-tech.com",
        //    },
        //    "test", "hello");
        //    _emailService.SendEmail(message);
        //    return StatusCode(StatusCodes.Status200OK,
        //    new Response
        //    {
        //        Status = "Sucesss",
        //        Message = "Email Sent SuccessFully"
        //    });

        //}

        [HttpGet("ConfirmEmail")]

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Email Verified Successfully" });
                }

            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "This User Doesnot exist!" });
        }

    }

}

