using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Products.Commands.AddCategoryService
{
    public interface IAddCategoryService
    {
        ResultDto Execute(long? parentId, string name);
    }
    public class AddCategoryService : IAddCategoryService
    {
        private readonly IDataBaseContext _context;
        public AddCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long? parentId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نام دسته بندی را وارد نمایید",
                };
            }

            Category category = new Category()
            {
                Name = name,
                ParentCategoryId = parentId,
                ParentCategory = getParent(parentId)
            };
            _context.Categories.Add(category);
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "دسته بندی با موفقیت اضافه شد",
            };
        }

        private Category getParent(long? parentId)
        {
            return _context.Categories.Find(parentId);
        }

    }
}
