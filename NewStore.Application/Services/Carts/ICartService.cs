using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewStore.Application.Services.Carts
{
    public interface ICartService
    {
        ResultDto RemoveFromCart(long productId,Guid browserId);
        ResultDto AddToCart(long productId,Guid browserId);
        ResultDto GetCart(Guid browserId);
    }

    public class CartService : ICartService
    {
        private readonly IDataBaseContext _context;
        public CartService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto AddToCart(long productId, Guid browserId)
        {
            throw new NotImplementedException();
        }

        public ResultDto GetCart(Guid browserId)
        {
            throw new NotImplementedException();
        }

        public ResultDto RemoveFromCart(long productId, Guid browserId)
        {
            throw new NotImplementedException();
        }
    }
}
