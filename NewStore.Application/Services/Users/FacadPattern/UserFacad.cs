using NewStore.Application.Interfaces.Contexts;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Users.Commands.ChangeStatusUser;
using NewStore.Application.Services.Users.Commands.EditUser;
using NewStore.Application.Services.Users.Commands.LoginUser;
using NewStore.Application.Services.Users.Commands.RegisterUser;
using NewStore.Application.Services.Users.Commands.RemoveUser;
using NewStore.Application.Services.Users.Queris.GetRole;
using NewStore.Application.Services.Users.Queris.GetUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewStore.Application.Services.Users.FacadPattern
{
    public class UserFacad : IUserFacad
    {
        private readonly IDataBaseContext _context;
        public UserFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IChangeUserStatusService _changeUserStatusService;
        public IChangeUserStatusService ChangeStatus
        {
            get
            {
                return _changeUserStatusService = _changeUserStatusService ?? new ChangeUserStatusService(_context);
            }
        }
        private IEditUserService _editUserService;
        public IEditUserService Edit
        {
            get
            {
                return _editUserService = _editUserService ?? new EditUserService(_context);
            }
        }

        private ILoginUserService _loginUserService;
        public ILoginUserService Login
        {
            get
            {
                return _loginUserService = _loginUserService ?? new LoginUserService(_context);
            }
        }
        private IRegisterUserService _registerUserService;
        public IRegisterUserService Register
        {
            get
            {
                return
                    _registerUserService = _registerUserService ?? new RegisterUserService(_context);
            }
        }
        private IRemoveUserService _removeUserService;
        public IRemoveUserService Remove
        {
            get
            {
                return _removeUserService = _removeUserService ?? new RemoveUserService(_context);
            }
        }
        private IGetRolesService _getRolesService;
        public IGetRolesService GetRoles
        {
            get { return _getRolesService = _getRolesService ?? new GetRolesService(_context); }
        }

        private IGetUserServise _getGetUserServise;
        public IGetUserServise GetUser
        {
            get { return _getGetUserServise = _getGetUserServise ?? new GetUserService(_context); }
        }
    }
}
