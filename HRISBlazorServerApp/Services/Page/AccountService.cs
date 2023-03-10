using AutoMapper;
using HRISBlazorServerApp.Interfaces.Services;
using MediatR;

namespace HRISBlazorServerApp.Services.Page
{
    public class AccountService : IAccountService
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public AccountService(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }





    }
}
