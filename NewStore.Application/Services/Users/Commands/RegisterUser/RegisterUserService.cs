using NewStore.Application.Interfaces.Contexts;
using NewStore.Common;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NewStore.Application.Services.Users.Commands.RegisterUser
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IDataBaseContext _context;
        public RegisterUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultRegisterUserDto> Execute(RequestRegisterUserDto request)
        {
            User user = new User();
            ResultDto<ResultRegisterUserDto> result = new ResultDto<ResultRegisterUserDto>();
            List<UserInRole> roles = new List<UserInRole>();
            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "پست الکترونیک را وارد نمایید"
                    };
                }
                var match = Regex.Match(request.Email, emailRegex, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "پست الکترونیک معتبر نمی باشد"
                    };
                }
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "نام را وارد نمایید"
                    };
                }

                if (request.Roles == null)
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "سطح دسترسی را مشخص کنید"
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "رمز عبور را وارد نمایید"
                    };
                }

                if (string.IsNullOrWhiteSpace(request.RePassword))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "رمز عبور را وارد نمایید"
                    };
                }

                if (request.Password != request.RePassword)
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "رمز عبور و تکرار آن برابر نیست"
                    };
                }

                foreach (var item in request.Roles)
                {
                    Role role = _context.Roles.Find(item.RoleId);

                    roles.Add(new UserInRole()
                    {
                        Role = role,
                        RoleId = item.RoleId,
                        User = user,
                        UserId = user.Id
                    });
                }
                PasswordHasher hasher = new PasswordHasher();
                user.Name = request.Name;
                user.Email = request.Email;
                user.UserInRoles = roles;
                user.IsActive = true;
                user.Password = hasher.HashPassword(request.Password);

                _context.Users.Add(user);
                _context.SaveChanges();
                return new ResultDto<ResultRegisterUserDto>()
                {
                    Data = new ResultRegisterUserDto()
                    {
                        UserId = user.Id,
                    },
                    IsSuccess = true,
                    Message = "عملیات با موفقیت انجام شد"
                };

            }
            catch (Exception msg)
            {
                result.IsSuccess = false;
                result.Message = "عملیات با شکست مواجه شد :(";
                result.Data = null;
            }

            return result;
        }
    }
}
