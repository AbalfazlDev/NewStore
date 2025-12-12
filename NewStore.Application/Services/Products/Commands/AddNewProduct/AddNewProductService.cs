using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Application.Tools;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.IO;

namespace NewStore.Application.Services.Products.Commands.AddNewProduct
{
    public class AddNewProductService : IAddNewProductService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public AddNewProductService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public ResultDto Execute(RequestAddProductDto request)
        {
            try
            {
                Category category = _context.Categories.Find(request.CategoryId);

                Product product = new Product()
                {
                    Name = request.Name,
                    Brand = request.Brand,
                    Description = request.Description,
                    Price = request.Price,
                    Inventory = request.Inventory,
                    Displayed = request.Displayed,
                    Category = category,
                };
                _context.Products.Add(product);

                List<ProductImages> productImages = new List<ProductImages>();
                foreach (var item in request.Images)
                {
                    UploadDto uploadResult = CommonFunctions.UploadFile(_environment,item,"products");
                    productImages.Add(new ProductImages()
                    {
                        Product = product,
                        Src = uploadResult.FileNameAddress,
                    });
                }
                _context.ProductImages.AddRange(productImages);

                List<ProductFeatures> productFeatures = new List<ProductFeatures>();
                foreach (var feature in request.Features)
                {
                    productFeatures.Add(new ProductFeatures()
                    {
                        Product = product,
                        Feature = feature.Feature,
                        Value = feature.Value
                    });
                }
                _context.ProductFeatures.AddRange(productFeatures);
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "عملیات با موفقیت انجام شد"
                };
            }
            catch
            {
                return new ResultDto() { IsSuccess = false, Message = "خطا رخ داده است" };
            }
        }

        


    }

}
