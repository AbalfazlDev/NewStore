using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common;
using NewStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Products.Queris.GetProduct
{
    public interface IGetProductService
    {
        public ResultDto<ResultGetProductDto> Execute(string searchKey, UInt16 page, byte pageSize, long? categoryId);
    }
    public class GetProductService : IGetProductService
    {
        private readonly IDataBaseContext _context;
        public GetProductService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetProductDto> Execute(string searchKey, UInt16 page, byte pageSize, long? categoryId = null)
        {
            uint rowsCount;
            var productQuery = _context.Products
                .Include(p => p.ProductImages).AsQueryable();

            if (categoryId != null)
                productQuery = productQuery.Where(p => p.CategoryId == categoryId || p.Category.ParentCategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(searchKey))
                productQuery = productQuery.Where(p => p.Name.Contains(searchKey) || p.Brand.Contains(searchKey));

            var products = productQuery.ToPage(page, pageSize, out rowsCount);

            Random random = new Random();
            return new ResultDto<ResultGetProductDto>()
            {
                Data = new ResultGetProductDto()
                {
                    RowsCount = rowsCount,
                    Products = products.Select(p => new GetProductDto
                    {
                        Id = p.Id,
                        Title = p.Name,
                        Brand = p.Brand,
                        ImageSrc = p.ProductImages.FirstOrDefault().Src,
                        Star = (byte)random.Next(1, 5),
                        Price = p.Price,
                    }).ToList(),
                },
                IsSuccess = true,
                Message = ""
            };
        }
    }

    public class ResultGetProductDto
    {
        public List<GetProductDto> Products { get; set; }
        public uint RowsCount { get; set; }
    }

    public class GetProductDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string ImageSrc { get; set; }
        public byte Star { get; set; }
        public int Price { get; set; }
    }
}
