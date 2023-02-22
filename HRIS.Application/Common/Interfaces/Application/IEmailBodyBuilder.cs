using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces.Application
{
    public interface IEmailBodyBuilder
    {
        Task<string> GenerateEmail(int systemId, string requestCode, string baseURL, string status);
    }
}
