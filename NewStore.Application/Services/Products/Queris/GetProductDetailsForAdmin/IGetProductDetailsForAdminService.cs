using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewStore.Application.Services.Products.Queris.GetProductDetailsForAdmin
{
    public interface IGetProductDetailsForAdminService
    {
        public ResultDto<ProductDetailsForAdminDto> Execute(long productId);
    }

    public class GetProductDetailsForAdminService : IGetProductDetailsForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetProductDetailsForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ProductDetailsForAdminDto> Execute(long productId)
        {
            try
            {
                ProductDetailsForAdminDto product = _context.Products
                    .Include(p => p.Category)
                    .ThenInclude(p => p.ParentCategory)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductFeatures)
                    .Where(p => p.Id == productId)
                    .ToList()
                    .Select(p => new ProductDetailsForAdminDto
                    {
                        Name = p.Name,
                        Category = getCategoryName(p.Category),
                        Brand = p.Brand,
                        Description = p.Description,
                        Price = p.Price,
                        Inventory = p.Inventory,
                        Displayed = p.Displayed,
                        Images = p.ProductImages.ToList().Select(c => new ProductDetailsImages
                        {
                            Id = c.ProductId,
                            Src = c.Src,
                        }).ToList(),
                        Features = p.ProductFeatures.ToList().Select(c => new ProductDetailsFeatures
                        {
                            Id = c.ProductId,
                            DisplayName = c.Feature,
                            Value = c.Value,
                        }).ToList(),

                    }).FirstOrDefault();
                return new ResultDto<ProductDetailsForAdminDto>
                {
                    IsSuccess = true,
                    Message = "",
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new ResultDto<ProductDetailsForAdminDto>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        private string getCategoryName(Category category) => category != null ? category.ParentCategory.Name : "";

    }


    public class ProductDetailsForAdminDto
    {
        public string Name { get; set; }
        public string Category { get; set; }

        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
        public List<ProductDetailsImages> Images { get; set; }
        public List<ProductDetailsFeatures> Features { get; set; }

    }

    public class ProductDetailsImages
    {
        public long Id { get; set; }
        public string Src { get; set; }
    }
    public class ProductDetailsFeatures
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
