using NewStore.Application.Services.Users.Commands.ChangeStatusUser;
using NewStore.Application.Services.Users.Commands.EditUser;
using NewStore.Application.Services.Users.Commands.LoginUser;
using NewStore.Application.Services.Users.Commands.RegisterUser;
using NewStore.Application.Services.Users.Commands.RemoveUser;
using NewStore.Application.Services.Users.Queris.GetCommonUesrDetails;
using NewStore.Application.Services.Users.Queris.GetRole;
using NewStore.Application.Services.Users.Queris.GetUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewStore.Application.Interfaces.FacadPatterns
{
    public interface IUserFacad
    {
        public IChangeUserStatusService ChangeStatus { get; }
        public IEditUserService Edit { get; }
        public ILoginUserService Login { get; }
        public IRegisterUserService Register { get; }
        public IRemoveUserService Remove { get; }
        public IGetRolesService GetRoles { get; }
        public IGetUserServise GetUser { get; }
        public IGetCommonUserDetailsService GetCommonUserDetails { get; }
    }
}
