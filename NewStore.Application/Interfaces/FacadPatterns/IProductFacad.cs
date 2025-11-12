using NewStore.Application.Services.Products.Commands.AddCategoryService;
using NewStore.Application.Services.Products.Commands.AddNewProduct;
using NewStore.Application.Services.Products.Queris.GetAllCategoris;
using NewStore.Application.Services.Products.Queris.GetCategories;
using NewStore.Application.Services.Products.Queris.GetProductDetailsForAdmin;
using NewStore.Application.Services.Products.Queris.GetProductForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Interfaces.FacadPatterns
{
    public interface IProductFacad
    {
        public IAddCategoryService AddCategory { get;}
        public IGetCategoriesService GetCategories {  get;}
        public IAddNewProductService AddNewProduct {  get;}
        public IGetAllCategoriesServise  GetAllCategories { get;}
        public IGetProductForAdminService GetProductForAdmin {  get;}
        public IGetProductDetailsForAdminService GetProductDetailsForAdmin {  get;}
    }
}
