using AutoMapper;
using HRIS.Application.Common.Exceptions;
using HRIS.Application.Common.Interfaces.Factories;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Employees.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Application.Employees.Handlers.Commands
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Tuple<bool,string>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionScopeFactory _transactionScopeFactory;
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(IMapper mapper, ITransactionScopeFactory transactionScopeFactory, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _transactionScopeFactory = transactionScopeFactory;
            _employeeRepository = employeeRepository;
        }

        private async Task<bool> ValidateFields(DeleteEmployeeCommand request)
        {
            if (string.IsNullOrEmpty(request.EmpID))
                throw new ValidationException("Employee ID is required");

            return await Task.FromResult(true);
        }


        public async Task<Tuple<bool, string>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await ValidateFields(request);

            var _entity = await _employeeRepository.GetAllAsync(x => x.EmpID == request.EmpID);

            if (!_entity.Any())
                return Tuple.Create(false, "Employee does not exist");

            var entity = _entity.FirstOrDefault();

            await _employeeRepository.SoftDeleteAsync(entity);

            //Full Delete
            //await _departmentRepository.DeleteAsync(entity);

            return Tuple.Create(true, "Successfully Deleted");
        }
    }
}
