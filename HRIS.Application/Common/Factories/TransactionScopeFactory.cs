using HRIS.Application.Common.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace HRIS.Application.Common.Factories
{
    public class TransactionScopeFactory : ITransactionScopeFactory
    {
        public TransactionScope Create()
        {
            return new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted },
                    TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
