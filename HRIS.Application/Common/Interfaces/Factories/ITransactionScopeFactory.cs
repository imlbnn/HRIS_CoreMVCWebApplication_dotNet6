using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace HRIS.Application.Common.Interfaces.Factories
{
    public interface ITransactionScopeFactory
    {
        TransactionScope Create();
    }
}
