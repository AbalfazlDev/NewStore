using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace NewStore.Application.Services.Products.Commands.AddNewProduct
{
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

}
