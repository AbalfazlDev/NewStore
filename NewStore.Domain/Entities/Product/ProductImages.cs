using NewStore.Domain.Entities.Common;

namespace NewStore.Domain.Entities.Product
{
    public class ProductImages : BaseEntity
    {
        public  Product Product { get; set; }
        public long ProductId { get; set; }
        public string Src { get; set; }
    }
}
