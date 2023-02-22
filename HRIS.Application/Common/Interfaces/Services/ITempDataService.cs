using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Application.Common.Interfaces.Services
{
    public interface ITempDataService
    {
        void Put<T>(string key, T value);
        T Get<T>(string key);
        void Remove(string key);
    }
}
