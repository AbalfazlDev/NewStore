using NewStore.Domain.Entities.Common;
using System.Collections.Generic;

namespace NewStore.Domain.Entities.Product
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }

        public Category Category { get; set; }
        public long CategoryId { get; set; }

        public ICollection<ProductImages> ProductImages { get; set; }
        public ICollection<ProductFeatures> ProductFeatures { get; set; }
    }
}
