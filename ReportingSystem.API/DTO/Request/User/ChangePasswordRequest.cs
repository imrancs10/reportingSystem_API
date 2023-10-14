using ReportingSystem.API.Enums;

namespace ReportingSystem.API.DTO.Request
{
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
