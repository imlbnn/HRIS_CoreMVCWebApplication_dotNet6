using HRIS.Application.Common.Interfaces;
using HRIS.Application.Common.Interfaces.Services;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace HRIS.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest>
    : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public LoggingBehaviour(ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;
        string? userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }

    }
}
