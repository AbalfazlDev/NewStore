using NewStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Products.Queris.GetProductForAdmin
{
    public interface IGetProductForAdminService
    {
        ResultDto<ProductsForAdmin> Execute(int page, int pageSize);
    }

    public class ProductsForAdmin
    {
        public UInt16 PageSize { get; set; }
        public UInt16 CurrentPage { get; set; }
        public UInt16 RowCount { get; set; }
        public List<ProductsFormAdminList_Dto> Products { get; set; }
    }
    public class ProductsFormAdminList_Dto
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
