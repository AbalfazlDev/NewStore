using NewStore.Domain.Entities.Common;
using NewStore.Domain.Entities.Users;
using NewStore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewStore.Domain.Entities.Carts
{
    public class Cart:BaseEntity
    {
        public User User { get; set; }
        public long? UserId { get; set; }

        public Guid BrowserId { get; set; }
    }

    public class Item : BaseEntity
    {
        public virtual NewStore.Domain.Entities.Product.Product Proudct { get; set; }
        public long ProductId { get; set; }

        public int Count { get; set; }
        public int Price { get; set; }
    }
}
