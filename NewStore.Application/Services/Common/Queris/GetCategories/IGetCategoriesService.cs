using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace NewStore.Application.Services.Common.Queris.GetCategories
{
    public interface IGetCategoriesService
    {
        ResultDto<List<ResultGetCategories>> Execute();
    }
    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly IDataBaseContext _context;
        public GetCategoriesService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetCategories>> Execute()
        {
            List<ResultGetCategories> result = _context.Categories
                .Where(p => p.ParentCategoryId == null)
                .Select(p => new ResultGetCategories
                {
                    CategoryId = p.Id,
                    Name = p.Name,
                }).ToList();

            return new ResultDto<List<ResultGetCategories>>
            {
                Data = result,
                IsSuccess = true,
                Message = ""
            };
        }
    }

    public class ResultGetCategories
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
    }
}
