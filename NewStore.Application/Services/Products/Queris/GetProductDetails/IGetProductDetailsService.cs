using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using static NewStore.Application.Services.Products.Queris.GetProductDetails.GetProductDetailsService;

namespace NewStore.Application.Services.Products.Queris.GetProductDetails
{
    public interface IGetProductDetailsService
    {
        public ResultDto<ResultGetProductDetails> Execute(long productId);
    }
    public class GetProductDetailsService : IGetProductDetailsService
    {
        private readonly IDataBaseContext _context;
        public GetProductDetailsService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetProductDetails> Execute(long productId)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductFeatures)
                .Where(p => p.Id == productId).FirstOrDefault();
            if (product == null)
                throw new Exception("Product Not Found ...");

            product.ViewCount++;
            _context.SaveChanges();
            return new ResultDto<ResultGetProductDetails>
            {
                IsSuccess = true,
                Message = "",
                Data = new ResultGetProductDetails
                {
                    title = product.Name,
                    Brand = product.Brand,
                    Description = product.Description,
                    Price = product.Price,
                    Star = product.Star,
                    ViewCount = product.ViewCount,
                    Category = $"{product.Category.Name} / {product.Category.ParentCategory.Name}",
                    ImagesSrc = product.ProductImages.Select(c => c.Src).ToList(),
                    ProductFeatures = product.ProductFeatures.Select(c => new ProductFeaturesDto
                    {
                        DisplayName = c.Feature,
                        value = c.Value,
                    }).ToList(),

                }
            };
        }

        public class ResultGetProductDetails
        {
            public string title { get; set; }
            public string Brand { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public string Category { get; set; }
            public int Star { get; set; }
            public int ViewCount { get; set; }

            public List<string> ImagesSrc { get; set; }
            public List<ProductFeaturesDto> ProductFeatures { get; set; }
        }

        public class ProductFeaturesDto
        {
            public string DisplayName { get; set; }
            public string value { get; set; }
        }
    }
}
