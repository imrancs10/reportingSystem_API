using ReportingSystem.API.Enums;

namespace ReportingSystem.API.Models
{
    public class Organization : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string LogoFileName { get; set; }
    }
}
