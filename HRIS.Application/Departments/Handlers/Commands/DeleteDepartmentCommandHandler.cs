using AutoMapper;
using HRIS.Application.Common.Exceptions;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Departments.Commands;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Departments.Handlers.Commands
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Tuple<bool,string>>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        public DeleteDepartmentCommandHandler(IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public async Task<Tuple<bool,string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _departmentRepository.GetAllAsync(x => x.Code == request.Code);

            if (!isExist.Any()) 
                return Tuple.Create(false,"Department does not exist");
  
            var entity = isExist.FirstOrDefault();

            await _departmentRepository.SoftDeleteAsync(entity);

            //Full Delete
            //await _departmentRepository.DeleteAsync(entity);

            return Tuple.Create(true,"Successfully Deleted");
        }
    }
}
