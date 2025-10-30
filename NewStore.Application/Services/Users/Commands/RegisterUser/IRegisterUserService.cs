using NewStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Users.Commands.RegisterUser
{
    public interface IRegisterUserService
    {
        ResultDto<ResultRegisterUserDto> Execute(RequestRegisteUserDto request);
    }

    public class RequestRegisteUserDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public Int16 Age { get; set; }
        public List<RoleInRegisterUserDto> Roles { get; set; }
    }

    public class RoleInRegisterUserDto
    {
        public long RoleId { get; set; }
    }

    public class ResultRegisterUserDto
    {
        public long UserId { get; set; }

    }
}
