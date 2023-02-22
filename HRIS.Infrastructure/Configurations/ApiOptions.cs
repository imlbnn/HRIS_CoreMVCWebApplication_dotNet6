using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Infrastructure.Configurations
{
    public class ApiOptions
    {
        public string IdentityServerBaseUrl { get; set; }
        public string OidcApiName { get; set; }
        public bool RequireHttpsMetadata { get; set; }

        public int RequestTimeout { get; set; } = 180;
        public bool DisableServerCertificateValidation { get; set; } = false;
        public string InventoryServiceUrl { get; set; }
    }
}
