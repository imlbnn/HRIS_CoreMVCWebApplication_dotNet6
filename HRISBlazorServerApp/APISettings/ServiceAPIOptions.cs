namespace HRISBlazorServerApp.APISettings
{
    public class ServiceAPIOptions
    {
        public int RequestTimeout { get; set; } = 30;
        public bool DisableServerCertificateValidation { get; set; } = false;
        public string ApiBaseUrl { get; set; }
    }
}
