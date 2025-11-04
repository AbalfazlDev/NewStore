using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Products.Queris.GetAllCategoris
{
    public interface IGetAllCategoriesServise
    {
        public ResultDto<List<ResultGetAllCategories>> Execute();
    }

    public class GetAllCategoriesService : IGetAllCategoriesServise
    {
        private readonly IDataBaseContext _context;
        public GetAllCategoriesService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetAllCategories>> Execute()
        {
            List<ResultGetAllCategories> categories = _context.Categories
                .Include(p => p.ParentCategory)
                .Where(p => p.ParentCategory != null)
                .ToList()
                .Select(p => new ResultGetAllCategories()
                {
                    Id = p.Id,
                    Name = p.Name + "<=" + p.ParentCategory.Name,
                }).ToList();

            return new ResultDto<List<ResultGetAllCategories>>()
            {
                Data = categories,
                IsSuccess = true,
                Message = "عملیات با موفقیت انجام شد"
            };
        }
    }


    public class ResultGetAllCategories
    {
        public string Name { get; set; }
        public long Id { get; set; }
    }

}
