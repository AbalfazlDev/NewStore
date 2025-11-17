using NewStore.Application.Interfaces.Contexts;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Products.Queris.GetProduct;
using NewStore.Application.Services.Products.Queris.GetProductDetails;

namespace NewStore.Application.Services.Products.FacadPattern
{
    public class ProductFacad : IProductFacad
    {
        private readonly IDataBaseContext _context;
        public ProductFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IGetProductService _getProduct;
        public IGetProductService GetProduct
        {
            get
            {
                return _getProduct = _getProduct ?? new GetProductService(_context);
            }
        }

        private IGetProductDetailsService _getProductDetails;
        public IGetProductDetailsService GetProductDetails
        {
            get
            {
                return _getProductDetails = _getProductDetails ?? new GetProductDetailsService(_context);
            }
        }
    }
}
