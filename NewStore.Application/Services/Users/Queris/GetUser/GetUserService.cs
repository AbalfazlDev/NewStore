using System;
using System.Linq;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common;

namespace NewStore.Application.Services.Users.Queris.GetUser
{
    public class GetUserService : IGetUserServise
    {
        private readonly IDataBaseContext _context;
        public GetUserService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultGetUserDto Execute(RequestGetUserDto request)
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                users = users.Where(p => p.Name.Contains(request.SearchKey) & p.Lastname.Contains(request.SearchKey) & p.Email.Contains(request.SearchKey));
            }

            uint rowsCount = 0;
            ResultGetUserDto result = new ResultGetUserDto();
            result.Users = users.ToPage(request.Page, 20, out rowsCount).Select(n => new GetUserDto
            {
                Id = n.Id,
                Name = n.Name,
                Lastname = n.Lastname,
                IsActive = n.IsActive,
                Age = n.Age,
                Email = n.Email,
            }).ToList();
            result.Rows = rowsCount;
            return result;

        }
    }
}
