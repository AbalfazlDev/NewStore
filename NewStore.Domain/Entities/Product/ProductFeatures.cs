using NewStore.Domain.Entities.Common;

namespace NewStore.Domain.Entities.Product
{
    public class ProductFeatures:BaseEntity
    {
        public Product Product { get; set; }
        public long ProductId { get; set; }

        public string Feature { get; set; }
        public string Value { get; set; }
    }
}
