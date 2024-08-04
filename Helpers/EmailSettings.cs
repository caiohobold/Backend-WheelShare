namespace WheelShareAPI.Helpers
{
    public class EmailSettings
    {
        public Dictionary<string, SmtpProviderSettings> Providers { get; set; }
        public string DefaultProvider { get; set; }
    }

    public class SmtpProviderSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string SenderPassword { get; set; }
    }
}
