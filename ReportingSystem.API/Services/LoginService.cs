using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ReportingSystem.API.Common;
using ReportingSystem.API.Contants;
using ReportingSystem.API.Data;
using ReportingSystem.API.Dto.Request;
using ReportingSystem.API.DTO.Request;
using ReportingSystem.API.DTO.Response;
using ReportingSystem.API.Exceptions;
using ReportingSystem.API.Extension;
using ReportingSystem.API.Models;
using ReportingSystem.API.Repository.IRepository;
using ReportingSystem.API.Services.Interfaces;
using ReportingSystem.API.Services.IServices;
using StackExchange.Redis;

namespace ReportingSystem.API.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;/**/
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly ReportingSystemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginService(ILoginRepository loginRepository, IMapper mapper, IMailService mailService, IConfiguration configuration, ReportingSystemContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _loginRepository = loginRepository;
            _mapper = mapper;
            _mailService = mailService;
            _configuration = configuration;
            _context = context;
        }

        public async Task<bool> AssignRole(string email, string role)
        {
            return await _loginRepository.AssignRole(email, role);
        }

        public async Task<bool> BlockUser(string email)
        {
            return await _loginRepository.BlockUser(email);
        }

        public async Task<bool> ChangePassword(PasswordChangeRequest request)
        {
            return await _loginRepository.ChangePassword(request);
        }

        public async Task<bool> DeleteUser(string email)
        {
            return await _loginRepository.DeleteUser(email);
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            if (request == null)
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_NoDataSupplied, StaticValues.Error_NoDataSupplied);
            }

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidDataSupplied, StaticValues.Error_InvalidDataSupplied);
            }
            //request.Password = request.Password.DecodeBase64();
            //request.Password = PasswordHasher.GenerateHash(request.Password);

            LoginResponse response = new()
            {
                UserResponse = _mapper.Map<UserResponse>(await _loginRepository.Login(request))
            };

            if (response.UserResponse.Id == 0)
            {
                throw new UnauthorizedException();
            }
            if (response.UserResponse.Role == "User")
            {
                var fileLogoName = await _context.Organizations
                                              .Where(x => x.Email == request.UserName)
                                              .FirstOrDefaultAsync();
                response.UserResponse.OrgLogoFileName = fileLogoName != null ? fileLogoName.LogoFileName : "";
                response.UserResponse.OrgName = fileLogoName != null ? fileLogoName.Name : "";
            }
            response.AccessToken = Utility.Utility.GenerateAccessToken(response.UserResponse.Role);
            return response;
        }
        public async Task<List<OrganizationResponse>> GetOrganizationDetail()
        {
            var result = (from org in _context.Organizations
                          join user in _context.Users on org.Email equals user.Email
                          where org.IsDeleted == false
                          select new OrganizationResponse
                          {
                              City = org.City,
                              Email = org.Email,
                              FirstName = org.FirstName,
                              Id = org.Id,
                              LastName = org.LastName,
                              Mobile = org.Mobile,
                              Name = org.Name,
                              Password = user.Password,
                              PinCode = org.PinCode,
                              State = org.State
                          }).ToList();

            //var res = _mapper.Map<List<OrganizationResponse>>(result);
            return result;
        }
        public async Task<OrganizationResponse> GetProfileDetail()
        {
            int? userId = null;
            if ((bool)_httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("userId"))
            {
                string value = _httpContextAccessor.HttpContext?.Request.Headers["userId"].ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (int.TryParse(value, out int newUserId))
                    {
                        userId = newUserId;
                    }
                }
            }
            if (userId != null)
            {
                //var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "OrganizationLogo");
                var result = (from org in _context.Organizations
                              join user in _context.Users on org.Email equals user.Email
                              where org.IsDeleted == false && user.Id == userId
                              select new OrganizationResponse
                              {
                                  City = org.City,
                                  Email = org.Email,
                                  FirstName = org.FirstName,
                                  Id = org.Id,
                                  LastName = org.LastName,
                                  Mobile = org.Mobile,
                                  Name = org.Name,
                                  Password = user.Password,
                                  PinCode = org.PinCode,
                                  State = org.State,
                                  LogoFileName = org.LogoFileName
                              }).FirstOrDefault();
                return result;
            }
            return new OrganizationResponse();
        }

        public async Task<UserResponse> ChangePassword(ChangePasswordRequest request)
        {
            int? userId = null;
            if ((bool)_httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("userId"))
            {
                string value = _httpContextAccessor.HttpContext?.Request.Headers["userId"].ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (int.TryParse(value, out int newUserId))
                    {
                        userId = newUserId;
                    }
                }
            }
            if (userId != null)
            {
                var result = (from org in _context.Organizations
                              join user in _context.Users on org.Email equals user.Email
                              where org.IsDeleted == false && user.Id == userId && user.Password == request.OldPassword
                              select user).FirstOrDefault();
                if (result == null)
                {
                    throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidCredentials, StaticValues.Error_InvalidOldPassword);
                }
                else if (result.IsDeleted)
                {
                    throw new BusinessRuleViolationException(StaticValues.ErrorType_UserNotFound, StaticValues.Error_UserNotFound);
                }
                if (result != null)
                {
                    result.Password = request.NewPassword;
                    var entity = _context.Attach(result);
                    entity.State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                var userResponse = _mapper.Map<UserResponse>(result);
                return userResponse;
            }
            return new UserResponse();
        }
        public async Task<UserResponse> RegisterUser(UserRequest request)
        {

            if (await _loginRepository.IsUserExist(request.Email))
                throw new BusinessRuleViolationException(StaticValues.ErrorType_AlreadyExist, StaticValues.Error_EmailAlreadyRegistered);

            User user = _mapper.Map<User>(request);
            user.IsEmailVerified = _configuration.GetValue<int>("EnableEmailVerification", 0) == 0;
            user.Password = PasswordHasher.GenerateHash(request.Password.DecodeBase64());
            user.EmailVerificationCode = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
            user.EmailVerificationCodeExpireOn = DateTime.Now.AddHours(48);
            var res = _mapper.Map<UserResponse>(await _loginRepository.RegisterUser(user));
            if (res.Id > 0)
            {
                var emailBody = await _mailService.GetMailTemplete(Constants.EmailTemplateEnum.EmailVerification);

                MailRequest mailRequest = new()
                {
                    ToEmail = request.Email,
                    Body = emailBody,
                    Subject = "Emai verification | IMG PIX"
                };
                _mailService.SendEmailAsync(mailRequest);
            }
            return res;
        }
        public async Task<bool> OrganizationUserEmailSend(OrganizationRequest request)
        {
            try
            {
                var userData = await _loginRepository.GetUserDetail(request.Email);
                if (userData == null)
                    throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.Error_RecordNotFound);

                var emailBody = await _mailService.GetMailTemplete(Constants.EmailTemplateEnum.EmailVerification);
                emailBody = emailBody.Replace("@UserName", request.Email).Replace("@Password", userData.Password);

                MailRequest mailRequest = new()
                {
                    ToEmail = request.Email,
                    Body = emailBody,
                    Subject = "XRay Reporting | Registration Verified"
                };
                _mailService.SendEmailAsync(mailRequest);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return true;
        }
        public async Task<OrganizationResponse> OrganizationUserRegister(OrganizationRequest request)
        {
            if (await _loginRepository.IsUserExist(request.Email))
                throw new BusinessRuleViolationException(StaticValues.ErrorType_AlreadyExist, StaticValues.Error_EmailAlreadyRegistered);

            var org = _mapper.Map<Organization>(request);
            var entity = _context.Organizations.Add(org);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            var savdData = entity.Entity;
            var res = _mapper.Map<OrganizationResponse>(savdData);
            if (res.Id > 0)
            {
                //save entry in user table
                var user = _mapper.Map<User>(savdData);
                user.Id = 0;
                user.UserName = savdData.Email;
                user.Password = request.Password;
                user.IsEmailVerified = true;
                var entity1 = _context.Users.Add(user);
                entity1.State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            return res;
        }

        public async Task<OrganizationResponse> OrganizationUserProfileUpdate(OrganizationRequest request)
        {
            int? userId = null;
            if ((bool)_httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("userId"))
            {
                string value = _httpContextAccessor.HttpContext?.Request.Headers["userId"].ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (int.TryParse(value, out int newUserId))
                    {
                        userId = newUserId;
                    }
                }
            }
            if (userId != null)
            {
                var org = _mapper.Map<Organization>(request);
                var entity = _context.Organizations.Update(org);
                entity.State = EntityState.Modified;
                await _context.SaveChangesAsync();
                var savdData = entity.Entity;

                var res = _mapper.Map<OrganizationResponse>(savdData);
                //if (res.Id > 0)
                //{
                //    //save entry in user table
                //    var user = _mapper.Map<User>(savdData);
                //    user.Id = userId.Value;
                //    user.UserName = savdData.Email;
                //    //user.Password = request.Password;
                //    user.IsEmailVerified = true;
                //    var entity1 = _context.Users.Update(user);
                //    entity1.State = EntityState.Modified;
                //    await _context.SaveChangesAsync();
                //}
                return res;
            }
            return null;
        }

        public async Task<bool> ResetEmailVerificationCode(string email)
        {
            return await _loginRepository.ResetEmailVerificationCode(email);
        }

        public async Task<string> ResetPassword(string userName)
        {
            var result = await _loginRepository.ResetPassword(userName);
            if (result)
            {

            }
            return ValidationMessage.ResetPasswordEmailSentSuccess;
        }

        public async Task<bool> UpdateProfile(UserRequest request)
        {
            User user = _mapper.Map<User>(request);
            return await _loginRepository.UpdateProfile(user);
        }

        public async Task<string> VerifyEmail(string token)
        {
            var result = await _loginRepository.VerifyEmail(token);
            return result ? ValidationMessage.EmailVerificationSuccess : ValidationMessage.EmailVerificationFail;
        }
    }
}
