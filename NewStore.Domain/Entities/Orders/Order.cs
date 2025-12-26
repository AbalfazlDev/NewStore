using NewStore.Domain.Entities.Common;
using NewStore.Domain.Entities.Finances;
using NewStore.Domain.Entities.Product;
using NewStore.Domain.Entities.Users;
using System.Collections.Generic;


namespace NewStore.Domain.Entities.Order
{
    public class Order : BaseEntity
    {
        public virtual User User { get; set; }
        public long UserId { get; set; }

        public virtual RequestPay RequestPay { get; set; }
        public long RequestId { get; set; }

        public OrderState OrderState { get; set; }
        public ICollection<OrderDetails> OrdersDetails { get; set; }
        public string Address { get; set; }
        public int FinalPrice { get; set; }

    }

    public class OrderDetails : BaseEntity
    {
        public virtual Order Order { get; set; }
        public long OrderId { get; set; }

        public virtual NewStore.Domain.Entities.Product.Product Product { get; set; }
        public long ProductId { get; set; }

        public int Price { get; set; }
        public int Count { get; set; }
    }

    public enum OrderState
    {
        Processing = 0,
        Canceled = 1,
        Delivered = 2,
    }
}
