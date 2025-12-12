using NewStore.Common.Dto;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Products.Commands.AddNewProduct
{
    public interface IAddNewProductService
    {
        public ResultDto Execute(RequestAddProductDto request);
    }

}
