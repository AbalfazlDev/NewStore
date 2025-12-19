using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Carts;
using NewStore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewStore.Application.Services.Carts
{
    public interface ICartService
    {
        public ResultDto RemoveFromCart(long cartItemId, Guid browserId);
        public ResultDto AddToCart(long productId, Guid browserId);
        public ResultDto<GetCartDto> GetCart(Guid browserId);
        public ResultDto IncrementItemFromCart(long productId, Guid browserId);
        public ResultDto DecreaseItemFromCart(long productId, Guid browserId);
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
            Cart cart = _context.Carts.Where(c => c.BrowserId == browserId && c.IsFinished == false).FirstOrDefault();
            if (cart == null)
            {
                Cart newCart = new Cart()
                {
                    IsFinished = false,
                    BrowserId = browserId
                };
                _context.Carts.Add(newCart);
                _context.SaveChanges();
                cart = newCart;
            }

            Product product = _context.Products.Find(productId);

            CartItem cartItem = _context.CartItems.Where(p => p.CartId == cart.Id && p.ProductId == productId).FirstOrDefault();
            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    Cart = cart,
                    Proudct = product,
                    Price = product.Price,
                    Count = 1
                };
                _context.CartItems.Add(cartItem);
            }
            else
                cartItem.Count++;
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = $"محصول{product.Name} با موفقیت به سبد خرید اضافه شد"
            };

        }

        public ResultDto<GetCartDto> GetCart(Guid browserId)
        {
            Cart cart = _context.Carts
                .Include(p => p.CartItems)
                .ThenInclude(p => p.Proudct)
                .ThenInclude(p => p.ProductImages)
                .OrderByDescending(p => p.Id)
                .Where(p => p.BrowserId == browserId && p.IsFinished == false)
                .FirstOrDefault();


            return new ResultDto<GetCartDto>
            {
                IsSuccess = true,
                Data = new GetCartDto
                {

                    TotalPrice = cart.CartItems.Sum(p => p.Price),
                    CartItems = cart.CartItems.Select(p => new CartItemDto
                    {
                        CartItemId = p.Id,
                        ProductName = p.Proudct.Name,
                        ProductScr = p.Proudct.ProductImages.FirstOrDefault().Src,
                        Price = p.Price,
                        Count = p.Count
                    }).ToList(),
                },
                Message = cart.CartItems.Count() > 0 ? "" : "سبدخرید خالی است"
            };
        }

        public ResultDto RemoveFromCart(long cartItemId, Guid browserId)
        {
            // this way to delete items(product in cart) is false :(
            CartItem cartItem = _context.CartItems.Where(p => p.Cart.BrowserId == browserId && p.Id == cartItemId).FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.IsRemoved = true;
                cartItem.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "محصول با موفقیت حذف شد"
                };
            }
            return new ResultDto
            {
                IsSuccess = false,
                Message = "محصولی برای حذف یافت نشد"
            };
        }

        public ResultDto IncrementItemFromCart(long cartItemId, Guid browserId)
        {
            Cart cart = _context.Carts.Include(p => p.CartItems).Where(p => p.BrowserId == browserId && p.IsFinished == false).FirstOrDefault();
            if (cart == null)
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "سبد خرید یافت نشد"
                };
            CartItem item = cart.CartItems?.Where(p => p.Id == cartItemId)?.FirstOrDefault();
            if (item == null)
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "محصول مورد نظر یافت نشد"
                };
            item.Count++;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true
            };
        }

        public ResultDto DecreaseItemFromCart(long cartItemId, Guid browserId)
        {
            Cart cart = _context.Carts.Include(p => p.CartItems).Where(p => p.BrowserId == browserId && p.IsFinished == false).FirstOrDefault();
            if (cart == null)
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "سبد خرید یافت نشد"
                };
            CartItem item = cart.CartItems?.Where(p => p.Id == cartItemId)?.FirstOrDefault();
            if (item == null)
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "محصول مورد نظر یافت نشد"
                };
            item.Count--;
            if (item.Count > 0)
                _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true
            };
        }

    }

    public class GetCartDto
    {
        public List<CartItemDto> CartItems { get; set; }
        public int TotalPrice { get; set; }
    }
    public class CartItemDto
    {
        public long CartItemId { get; set; }
        public string ProductName { get; set; }
        public string ProductScr { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
