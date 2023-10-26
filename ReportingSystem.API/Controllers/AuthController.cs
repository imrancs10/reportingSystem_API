using ReportingSystem.API.Contants;
using ReportingSystem.API.DTO.Request;
using ReportingSystem.API.DTO.Response;
using ReportingSystem.API.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Tar;
using ReportingSystem.API.Services;

namespace ReportingSystem.API.Controllers
{
    [Route(StaticValues.APIPrefix)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [HttpPost(StaticValues.LoginPath)]
        public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
        {
            return await _loginService.Login(loginRequest);
        }

        [HttpGet]
        [Route("get/organizationDetail")]
        public async Task<List<OrganizationResponse>> GetOrganizationDetail()
        {
            return await _loginService.GetOrganizationDetail();
        }

        [HttpGet]
        [Route("get/profile")]
        public async Task<OrganizationResponse> GetProfileDetail()
        {
            return await _loginService.GetProfileDetail();
        }

        [ProducesResponseType(typeof(OrganizationResponse), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.OrganizationChangePasswordPath)]
        public async Task<UserResponse> OrganizationChangePassword([FromBody] ChangePasswordRequest request)
        {
            return await _loginService.ChangePassword(request);
        }

        [ProducesResponseType(typeof(OrganizationResponse), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.OrganizationUserEmailSendPath)]
        public async Task<bool> OrganizationUserEmailSendPath([FromBody] OrganizationRequest request)
        {
            return await _loginService.OrganizationUserEmailSend(request);
        }

        [ProducesResponseType(typeof(OrganizationResponse), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.OrganizationUserRegisterPath)]
        public async Task<OrganizationResponse> OrganizationUserRegister([FromBody] OrganizationRequest request)
        {
            if (request.LogoBase64.Contains(","))
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "OrganizationLogo");
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                var filePathName = Path.Combine(basePath, request.Email.Replace("@", "").Replace(".", "") + Path.GetExtension(request.LogoFileName));

                var FileAsBase64 = request.LogoBase64.Substring(request.LogoBase64.IndexOf(",") + 1);
                var FileAsByteArray = Convert.FromBase64String(FileAsBase64);
                // If file found, delete it
                if (System.IO.File.Exists(Path.Combine(basePath, request.Email.Replace("@", "").Replace(".", "") + Path.GetExtension(request.LogoFileName))))
                    System.IO.File.Delete(Path.Combine(basePath, request.Email.Replace("@", "").Replace(".", "") + Path.GetExtension(request.LogoFileName)));

                using (var fs = new FileStream(filePathName, FileMode.CreateNew))
                {
                    fs.Write(FileAsByteArray, 0, FileAsByteArray.Length);
                }
                request.LogoFileName = request.Email.Replace("@", "").Replace(".", "") + Path.GetExtension(request.LogoFileName);
            }
            return await _loginService.OrganizationUserRegister(request);
        }

        [ProducesResponseType(typeof(OrganizationResponse), StatusCodes.Status201Created)]
        [HttpPut(StaticValues.OrganizationUserUpdatePath)]
        public async Task<OrganizationResponse> OrganizationUserProfileUpdate([FromBody] OrganizationRequest request)
        {
            if (request.LogoBase64.Contains(","))
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "OrganizationLogo");
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                var filePathName = Path.Combine(basePath, request.Email.Replace("@", "").Replace(".", "") + Path.GetExtension(request.LogoFileName));

                var FileAsBase64 = request.LogoBase64.Substring(request.LogoBase64.IndexOf(",") + 1);
                var FileAsByteArray = Convert.FromBase64String(FileAsBase64);
                // If file found, delete it
                if (System.IO.File.Exists(Path.Combine(basePath, request.Email.Replace("@", "").Replace(".", "") + Path.GetExtension(request.LogoFileName))))
                    System.IO.File.Delete(Path.Combine(basePath, request.Email.Replace("@", "").Replace(".", "") + Path.GetExtension(request.LogoFileName)));

                using (var fs = new FileStream(filePathName, FileMode.CreateNew))
                {
                    fs.Write(FileAsByteArray, 0, FileAsByteArray.Length);
                }
                request.LogoFileName = request.Email.Replace("@", "").Replace(".", "") + Path.GetExtension(request.LogoFileName);
            }
            return await _loginService.OrganizationUserProfileUpdate(request);
        }

        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [HttpPut(StaticValues.LoginUserRegisterPath)]
        [NonAction]
        public async Task<UserResponse> RegisterUser([FromBody] UserRequest request)
        {
            return await _loginService.RegisterUser(request);
        }

        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.LoginUserChangePasswordPath)]
        [NonAction]
        public async Task<bool> ChangePassword([FromBody] PasswordChangeRequest request)
        {
            return await _loginService.ChangePassword(request);
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpGet(StaticValues.LoginUserVerifyEmailPath)]
        [NonAction]
        public async Task<string> VerifyEmail([FromRoute] string token)
        {
            return await _loginService.VerifyEmail(token);
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpGet(StaticValues.LoginUserResetPasswordPath)]
        [NonAction]
        public async Task<string> ResetPassword([FromHeader] string userName)
        {
            return await _loginService.ResetPassword(userName);
        }

        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.LoginUserUpdateProfilePath)]
        [NonAction]
        public async Task<bool> UpdateProfile([FromBody] UserRequest request)
        {
            return await _loginService.UpdateProfile(request);
        }

        [HttpPost(StaticValues.LoginUserDeleteProfilePath)]
        [NonAction]
        public async Task<bool> DeleteUser([FromRoute] string email)
        {
            return await _loginService.DeleteUser(email);
        }

        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.UserBlockPath)]
        [NonAction]
        public async Task<bool> BlockUser([FromRoute] string email)
        {
            return await _loginService.BlockUser(email);
        }

        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.UserAssignRolePath)]
        [NonAction]
        public async Task<bool> AssignRole([FromRoute] string email, [FromRoute] string role)
        {
            return await _loginService.AssignRole(email, role);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.UserResetEmailVerifyCodePath)]
        [NonAction]
        public async Task<bool> ResetEmailVerificationCode([FromRoute] string email)
        {
            return await _loginService.ResetEmailVerificationCode(email);
        }

    }
}
