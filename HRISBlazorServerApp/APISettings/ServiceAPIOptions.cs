namespace HRISBlazorServerApp.APISettings
{
    public class ServiceAPIOptions
    {
        public int RequestTimeout { get; set; } = 180;
        public bool DisableServerCertificateValidation { get; set; } = false;
        public string ApiBaseUrl { get; set; }
    }
}
