using ReportingSystem.API.Enums;

namespace ReportingSystem.API.DTO.Request
{
    public class OrganizationRequest
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Mobile { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int? PinCode { get; set; }
        public string? Password { get; set; } = string.Empty;
        public bool ShowHeader { get; set; }
        public string? LogoFileName { get; set; }
        public string? LogoBase64 { get; set; }
    }
}
