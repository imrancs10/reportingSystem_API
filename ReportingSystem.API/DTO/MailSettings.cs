namespace ReportingSystem.API.Dto
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string GmailHost { get; set; }
        public int GmailPort { get; set; }
        public string GoDaddyHost { get; set; }
        public int GoDaddyPort { get; set; }
    }
}
