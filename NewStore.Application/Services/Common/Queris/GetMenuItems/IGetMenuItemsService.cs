using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace NewStore.Application.Services.Common.Queris.GetMenuItems
{
    public interface IGetMenuItemsService
    {
        public ResultDto<List<MenuItemsDto>> Execute();
    }
    public class GetMenuItemsService : IGetMenuItemsService
    {
        private readonly IDataBaseContext _context;
        public GetMenuItemsService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<MenuItemsDto>> Execute()
        {
            List<MenuItemsDto> items = _context.Categories
                .Include(p => p.ChildCategories)
                .Where(p => p.ParentCategoryId == null)
                .Select(p => new MenuItemsDto
                {
                    CategoryId = p.Id,
                    Name = p.Name,
                    Childs = p.ChildCategories.Select(c => new MenuItemsDto
                    {
                        CategoryId = c.Id,
                        Name = c.Name,
                    }).ToList(),
                }).ToList();
            return new ResultDto<List<MenuItemsDto>>
            {
                Data = items,
                IsSuccess = true,
                Message = ""
            };
        }
    }

    public class MenuItemsDto
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public List<MenuItemsDto> Childs { get; set; }

    }

}
