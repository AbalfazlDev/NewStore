using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Users.Queris.GetRole
{
    public interface IGetRolesService
    {
        public ResultDto<List<RolesDto>> Execute();
    }
    public class GetRolesService : IGetRolesService
    {
        private readonly IDataBaseContext _context;
        public GetRolesService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<RolesDto>> Execute()
        {
            /// ToList
            List<RolesDto> roles = _context.Roles.Select(p => new RolesDto
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();
            return new ResultDto<List<RolesDto>>()
            {
                Data = roles,
                IsSuccess = true,
                Message = "عمللیات با موفقیت انجام شد"
            };
        }
    }

    public class RolesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

}
