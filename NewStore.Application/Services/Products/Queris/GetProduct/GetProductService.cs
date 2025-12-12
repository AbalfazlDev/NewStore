using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common;
using NewStore.Common.Dto;
using System;
using System.Linq;

namespace NewStore.Application.Services.Products.Queris.GetProduct
{
    public class GetProductService : IGetProductService
    {
        private readonly IDataBaseContext _context;
        public GetProductService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetProductDto> Execute(Ordering ordering, string searchKey, UInt16 page, byte pageSize, long? categoryId = null)
        {
            uint rowsCount;
            var productQuery = _context.Products
                .Include(p => p.ProductImages).AsQueryable();

            if (categoryId != null)
                productQuery = productQuery.Where(p => p.CategoryId == categoryId || p.Category.ParentCategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(searchKey))
                productQuery = productQuery.Where(p => p.Name.Contains(searchKey) || p.Brand.Contains(searchKey));

            switch (ordering)
            {
                case Ordering.NotOrder:
                    break;
                case Ordering.MostVisited:
                    productQuery = productQuery.OrderByDescending(p => p.ViewCount);
                    break;
                case Ordering.Bestselling:

                    break;
                case Ordering.MostPopular:
                    break;
                case Ordering.theNewest:
                    productQuery=  productQuery.OrderByDescending(p => p.Id);
                    break;
                case Ordering.Cheapest:
                    productQuery = productQuery.OrderBy(p => p.Price);
                    break;
                case Ordering.theMostExpensive:
                    productQuery = productQuery.OrderByDescending(p => p.Price) ;
                    break;
                default:
                    break;
            }

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
}
