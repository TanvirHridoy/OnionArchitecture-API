using DomainLayer.Models;
using DomainLayer.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ServicesLayer.Data.EKyc;

namespace OnionArchitecture.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    //[ApiVersion("1")]
    public class AccountController : ControllerBase
    {        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IOtpService _otpService;
		private readonly IConfiguration _config;
        public AccountController(
                                    UserManager<IdentityUser> userManager, 
                                    SignInManager<IdentityUser> signInManager,
									IOtpService otpService,
									IConfiguration config
                                )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._otpService = otpService;
			this._config =  config;
        }

        //Manageing OTP
        [HttpPost]
		[Route("ekyc/sendtoken")]
		public async Task<IActionResult> SendOtp(string mobileNo, string emailAddress, int otyTypeId, int otpCarrierId)
        {

            //var result = await _signInManager.PasswordSignInAsync(model.EmailAddress, model.Password, false, false);

            var otp = new Otp();
            otp.MobileNumber = mobileNo;
            otp.EmailAddress = emailAddress;
            otp.OtpTypeId = otyTypeId;
            otp.OtpCarrierId = otpCarrierId;
            
            var result = await _otpService.AddOtp(otp);

            return new OkObjectResult(new { status = "ok", message = "Otp generated successfully." });
		}

		[HttpPost]
		[Route("ekyc/verifytoken")]
		public async Task<IActionResult> VerifyOtp(string mobileNo, string emailAddress, int otyTypeId, string passcode)
		{
			//var result = await _signInManager.PasswordSignInAsync(model.EmailAddress, model.Password, false, false);

			var otp = new Otp();
			otp.MobileNumber = mobileNo;
			otp.EmailAddress = emailAddress;
			otp.OtpTypeId = otyTypeId;
            otp.Passcode = passcode;

			var result = await _otpService.VerifyOtp(otp);

            string status = string.Empty;
            string message = string.Empty;

            if (result == "ok") 
            {
                status = "ok";
                message = "Token Verified Successfully.";
            }
            else
            {
				status = "nok";
				message = "Invalid Token";
			}


			return new OkObjectResult(new { status = status, message = message });
		}


		// POST: AccountController/Create
		[HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new IdentityUser { UserName = model.EmailAddress, Email = model.EmailAddress };
            var result = await _userManager.CreateAsync(user, model.Password);
            if(result.Succeeded)
            {
               await _signInManager.SignInAsync(user, isPersistent: false);
            }
            foreach(var error in result.Errors)
            {
            }

            var response = new Response() { Message = "Successful Login" };
			return new JsonResult(response);
        }

        //Old Code: Commented on 2/7
        //[HttpPost(nameof(Login))]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    var result = await _signInManager.PasswordSignInAsync(model.EmailAddress, model.Password, false, false);
        //    if (result.Succeeded)
        //    {
        //        return Ok("Successful Login");
        //    }
        //    else
        //    {
        //        return Ok("Login Failed");
        //    }
        //}

        [AllowAnonymous]
        [HttpPost]
        //[Route("api/v{version:apiVersion}/user/login")]
        [Route("api/user/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //var reqParam = HttpContext.Request.Query;
            //string loginId = reqParam["loginId"];
            //string loginPassword = reqParam["loginPassword"];

            if (string.IsNullOrEmpty(model.EmailAddress) || string.IsNullOrEmpty(model.Password))
                return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(model.EmailAddress, model.Password, false, false);

            //UserViewModel user = UserDb.Login(loginId, loginPassword);

            if (result.Succeeded)
            {
                //user.AssignedPages = AppInfoDb.GetAssignedPagesByUser(user.ID, user.CompanyID);

                IdentityOptions _options = new IdentityOptions();
                var key = _config["Jwt:Key"];
                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                int.TryParse(_config["Jwt:Expires"], out int days);
                var expires = DateTime.Now.AddDays(days);
                Claim[] claims = {
                //new Claim("UserId", user.ID.ToString()),
                //new Claim("LoginId", user.LoginID),
                //new Claim("UserTypeId", user.UserTypeID.ToString()),
                //new Claim("EmpCode", user.EmpCode.ToString()),
                //new Claim("GradeValue", user.GradeValue.ToString()),
                //new Claim("CompanyId", user.CompanyID.ToString()),
                //new Claim("SalaryType", user.Salarytype.ToString()),
                //new Claim(_options.ClaimsIdentity.RoleClaimType, user.UserTypeName.Replace(" ",""))
            };
                var token = new Token(key, issuer, audience, expires, claims);
                var tokenString = token.BuildToken();

                return Ok("Use bellow Line");
                //return Ok(new { token = tokenString, user });
            }
            else
            {
                return BadRequest();
            }
                      
        }


        [HttpPost(nameof(LogOut))]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok("Successful LogOut");
        }
    }
}
