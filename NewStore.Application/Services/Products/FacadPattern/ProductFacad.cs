using Microsoft.AspNetCore.Hosting;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Products.Commands.AddCategoryService;
using NewStore.Application.Services.Products.Commands.AddNewProduct;
using NewStore.Application.Services.Products.Queris.GetAllCategoris;
using NewStore.Application.Services.Products.Queris.GetCategories;
using NewStore.Application.Services.Products.Queris.GetProductDetailsForAdmin;
using NewStore.Application.Services.Products.Queris.GetProductForAdmin;

namespace NewStore.Application.Services.Products.FacadPattern
{
    public class ProductFacad : IProductFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public ProductFacad(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private IAddCategoryService _addCategory;
        public IAddCategoryService AddCategory
        {
            get
            {
                return _addCategory = _addCategory ?? new AddCategoryService(_context);
            }
        }

        private IGetCategoriesService _getCategories;
        public IGetCategoriesService GetCategories
        {
            get
            {
                return _getCategories = _getCategories ?? new GetCategoriesService(_context);
            }
        }

        private IGetAllCategoriesServise _getAllCategories;
        public IGetAllCategoriesServise GetAllCategories
        {
            get
            {
                return _getAllCategories = _getAllCategories ?? new GetAllCategoriesService(_context);
            }
        }

        private IAddNewProductService _addNewProduct;
        public IAddNewProductService AddNewProduct
        {
            get
            {
                return _addNewProduct = _addNewProduct ?? new AddNewProductService(_context, _environment);
            }
        }

        private IGetProductForAdminService _getProductForAdmin;
        public IGetProductForAdminService GetProductForAdmin
        {
            get
            {
                return _getProductForAdmin = _getProductForAdmin ?? new GetProductForAdminService(_context);
            }
        }

        private IGetProductDetailsForAdminService _getProductDetailsForAdmin;
        public IGetProductDetailsForAdminService GetProductDetailsForAdmin
        {
            get
            {
                return _getProductDetailsForAdmin = _getProductDetailsForAdmin ?? new GetProductDetailsForAdminService(_context);
            }
        }

    }
}
