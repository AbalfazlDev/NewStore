using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Carts;
using NewStore.Domain.Entities.Product;
using NewStore.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewStore.Application.Services.Carts
{
    public interface ICartService
    {
        public ResultDto<GetCartDto> GetCart(Guid browserId, long? userId);
        public ResultDto RemoveFromCart(long cartItemId, Guid browserId);
        public ResultDto AddToCart(long productId, Guid browserId, long? userId);
        public ResultDto IncrementItemFromCart(long productId, Guid browserId);
        public ResultDto DecrementItemFromCart(long productId, Guid browserId);
        public ResultDto UpdateCountCartItem(long cartItemId, Guid browserId, int newCount);
    }

    public class CartService : ICartService
    {
        private readonly IDataBaseContext _context;
        public CartService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<GetCartDto> GetCart(Guid browserId, long? userId)
        {
            Cart cart = _context.Carts
                .Include(p => p.CartItems)
                .ThenInclude(p => p.Proudct)
                .ThenInclude(p => p.ProductImages)
                .OrderByDescending(p => p.Id)
                .Where(p => p.BrowserId == browserId && p.IsFinished == false)
                .FirstOrDefault();

            if (cart == null)
                return new ResultDto<GetCartDto>
                {
                    IsSuccess = false,
                    Message = "سبدخرید یافت نشد",
                };

            if (userId != null)
            {
                User user = _context.Users.Find(userId);
                if (user != null)
                {
                    cart.User = user;
                    _context.SaveChanges();
                }

            }

            return new ResultDto<GetCartDto>
            {
                IsSuccess = true,
                Data = new GetCartDto
                {
                    CartItemsCount = cart.CartItems.Count(),
                    TotalPrice = cart.CartItems.Sum(p => p.Price),
                    CartItems = cart.CartItems.Select(p => new CartItemDto
                    {
                        CartItemId = p.Id,
                        ProductName = p.Proudct.Name,
                        ProductSrc = p.Proudct.ProductImages.FirstOrDefault().Src,
                        Price = p.Price,
                        Count = p.Count
                    }).ToList(),
                },
                Message = cart.CartItems.Count() > 0 ? "" : "سبدخرید خالی است"
            };
        }


        public ResultDto AddToCart(long productId, Guid browserId, long? userId)
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
            if (userId != null)
            {
                User user = _context.Users.Find(userId);
                if (user != null)
                {
                    cart.User = user;
                    _context.SaveChanges();
                }
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
            Cart cart;
            CartItem cartItem;
            ResultDto result = identifyCartItem(cartItemId, browserId, out cart, out cartItem);
            if (!result.IsSuccess)
                return result;
            cartItem.Count++;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true
            };
        }


        public ResultDto DecrementItemFromCart(long cartItemId, Guid browserId)
        {
            Cart cart;
            CartItem cartItem;
            ResultDto result = identifyCartItem(cartItemId, browserId, out cart, out cartItem);
            cartItem.Count--;
            if (cartItem.Count > 0)
                _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true
            };
        }

        public ResultDto UpdateCountCartItem(long cartItemId, Guid browserId, int newCount)
        {
            Cart cart;
            CartItem cartItem;
            ResultDto result = identifyCartItem(cartItemId, browserId, out cart, out cartItem);
            if (result.IsSuccess)
            {
                cartItem.Count = newCount;
                _context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                };
            }
            return result;
        }

        private ResultDto identifyCartItem(long cartItemId, Guid browserId, out Cart cart, out CartItem cartItem)
        {
            cart = _context.Carts.Include(p => p.CartItems).Where(p => p.BrowserId == browserId && p.IsFinished == false).FirstOrDefault();
            if (cart == null)
            {
                cartItem = null;
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "سبدخرید یافت نشد"
                };
            }

            cartItem = cart.CartItems.Where(p => p.Id == cartItemId).FirstOrDefault();
            if (cartItem == null)
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کالا مورد نظر یافت نشد"
                };

            return new ResultDto
            {
                IsSuccess = true,
            };
        }
    }

    public class GetCartDto
    {
        public List<CartItemDto> CartItems { get; set; }
        public int CartItemsCount { get; set; }
        public int TotalPrice { get; set; }
    }
    public class CartItemDto
    {
        public long CartItemId { get; set; }
        public string ProductName { get; set; }
        public string ProductSrc { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
