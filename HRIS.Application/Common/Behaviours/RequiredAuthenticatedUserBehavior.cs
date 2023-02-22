using HRIS.Application.Common.Attributes;
using HRIS.Application.Common.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Behaviours
{
    public class RequiredAuthenticatedUserBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUserService _currentUserService;

        public RequiredAuthenticatedUserBehavior(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            IEnumerable<AllowAnonymous> authorizeAttributes = request.GetType().GetCustomAttributes<AllowAnonymous>();
            if (!authorizeAttributes.Any() && _currentUserService.UserId == null)
            {
                throw new UnauthorizedAccessException();
            }

            return await next();
        }
    }
}
