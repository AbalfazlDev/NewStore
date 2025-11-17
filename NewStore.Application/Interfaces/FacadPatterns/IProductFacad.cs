using NewStore.Application.Services.Products.Queris.GetProduct;
using NewStore.Application.Services.Products.Queris.GetProductDetails;

namespace NewStore.Application.Interfaces.FacadPatterns
{
    public interface IProductFacad
    {
        public IGetProductService GetProduct { get; }
        public IGetProductDetailsService GetProductDetails { get; }
    }
}
