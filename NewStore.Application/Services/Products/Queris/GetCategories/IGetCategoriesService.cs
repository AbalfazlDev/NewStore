using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace NewStore.Application.Services.Products.Queris.GetCategories
{
    public interface IGetCategoriesService
    {
        ResultDto<List<ResultGetCategories>> Execute(long? parentId);
    }

    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly IDataBaseContext _context;
        public GetCategoriesService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultGetCategories>> Execute(long? parentId)
        {
            List<ResultGetCategories> result = _context.Categories
                .Include(p => p.ParentCategory)
                .Include(p => p.ChildCategories)
                .Where(p => p.ParentCategoryId == parentId)
                .ToList()
                .Select(p => new ResultGetCategories
                {
                    Name = p.Name,
                    Id = p.Id,
                    Parent = p.ParentCategory != null ?
                    new ParentCategoryDto()
                    {
                        Id = p.ParentCategory.Id,
                        Name = p.ParentCategory.Name,
                    } : null,
                    HasChild = p.ChildCategories.Count() > 0

                }).ToList();
            return new ResultDto<List<ResultGetCategories>>()
            {
                Data = result,
                IsSuccess = true,
                Message = "عملیات با موفقیت انجام شد",
            };
        }
    }

    public class ResultGetCategories
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public bool HasChild { get; set; }
        public ParentCategoryDto Parent { get; set; }
    }
    public class ParentCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
