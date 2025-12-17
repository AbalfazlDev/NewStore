using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewStore.Application.Services.Products.Commands.RemoveCategory
{
    public interface IRemoveCategoryService
    {
        public ResultDto Execute(long categoryId);
    }

    public class RemoveCategoryService : IRemoveCategoryService
    {
        private readonly IDataBaseContext _context;
        public RemoveCategoryService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long categoryId)
        {
            Category category = _context.Categories.FirstOrDefault(p => p.Id == categoryId) ;
            if (category == null)
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "دسته‌بندی یافت نشد"
                };
            category.IsRemoved = true;
            category.RemoveTime = DateTime.Now;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
            };
        }
    }
}
