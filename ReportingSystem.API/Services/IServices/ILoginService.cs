using ReportingSystem.API.DTO.Request;
using ReportingSystem.API.DTO.Response;
using ReportingSystem.API.Models;

namespace ReportingSystem.API.Services.IServices
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<List<OrganizationResponse>> GetOrganizationDetail();
        Task<UserResponse> RegisterUser(UserRequest request);
        Task<OrganizationResponse> OrganizationUserRegister(OrganizationRequest request);
        Task<bool> OrganizationUserEmailSend(OrganizationRequest request);
        Task<bool> ChangePassword(PasswordChangeRequest request);
        Task<string> ResetPassword(string userName);
        Task<string> VerifyEmail(string token);
        Task<bool> UpdateProfile(UserRequest request);
        Task<bool> AssignRole(string email, string Role);
        Task<bool> ResetEmailVerificationCode(string email);
        Task<bool> DeleteUser(string email);
        Task<bool> BlockUser(string email);
    }
}
