using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common;
using NewStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewStore.Application.Services.Products.Queris.GetProductForAdmin.GetProductForAdminService;

namespace NewStore.Application.Services.Products.Queris.GetProductForAdmin
{
    public interface IGetProductForAdminService
    {
        ResultDto<ProductsForAdmin> Execute(UInt16 page, byte pageSize);
    }


    public class GetProductForAdminService : IGetProductForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetProductForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ProductsForAdmin> Execute(UInt16 page, byte pageSize)
        {
            try
            {
                uint rowsCount = 0;
                List<ProductsForAdminList_Dto> product = _context.Products
                    .Include(p => p.Category)
                    .ToPage(page, pageSize, out rowsCount)
                    .Select(p => new ProductsForAdminList_Dto()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Category = p.Category.Name,
                        Brand = p.Brand,
                        Description = p.Description,
                        Price = p.Price,
                        Inventory = p.Inventory,
                        Displayed = p.Displayed,
                    }).ToList();

                return new ResultDto<ProductsForAdmin>
                {
                    Data = new ProductsForAdmin()
                    {
                        Products = product,
                        PageSize = pageSize,
                        CurrentPage = page,
                        RowCount = (UInt32)rowsCount,
                    },
                    IsSuccess = true,
                    Message = "عملیات با موفقیت انجام شد"
                };
            }
            catch (Exception ex)
            {
                return new ResultDto<ProductsForAdmin>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public class ProductsForAdmin: PageModel
        {
            public UInt16 PageSize { get; set; }
            public UInt16 CurrentPage { get; set; }
            public UInt32 RowCount { get; set; }
            public List<ProductsForAdminList_Dto> Products { get; set; }
        }
        public class ProductsForAdminList_Dto
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string Brand { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public int Inventory { get; set; }
            public bool Displayed { get; set; }
        }
    }
}
