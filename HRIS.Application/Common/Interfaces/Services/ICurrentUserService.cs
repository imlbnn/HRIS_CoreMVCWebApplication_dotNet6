using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string DisplayName { get; }
        bool IsAuthenticated { get; }
        string Username { get; }
        string ClientInfo { get; }
    }
}
