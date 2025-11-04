using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Products.Commands.AddNewProduct
{
    public interface IAddNewProductService
    {
        public ResultDto Execute(RequestAddProductDto request);
    }

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
                    UploadDto uploadResult = uploadFile(item);
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

        private UploadDto uploadFile(IFormFile file)
        {
            if (file != null)
            {
                if (file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string folder = @"Images\ProductImages";
                string rootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(rootFolder))
                {
                    Directory.CreateDirectory(rootFolder);
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.Name;
                string path = Path.Combine(rootFolder, fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    Status = true,
                    FileNameAddress = folder + fileName,
                };
            }
            return null;
        }


    }
    public class UploadDto
    {
        public long Id { get; set; }
        public bool Status { get; set; }
        public string FileNameAddress { get; set; }
    }

    public class RequestAddProductDto
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }

        public long CategoryId { get; set; }
        public IList<AddProduct_Feature> Features { get; set; }
        public IList<IFormFile> Images { get; set; }
    }

    public class AddProduct_Feature
    {
        public string Feature { get; set; }
        public string Value { get; set; }
    }

}
