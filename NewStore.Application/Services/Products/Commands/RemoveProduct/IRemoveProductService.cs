using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Product;
using System;

namespace NewStore.Application.Services.Products.Commands.RemoveProduct
{
    public interface IRemoveProductService
    {
        ResultDto Execute(long productId);
    }
    public class RemoveProductService : IRemoveProductService
    {
        private readonly IDataBaseContext _context;
        public RemoveProductService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long productId)
        {
            try
            {
                Product product = _context.Products.Find(productId);
                if (product == null)
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "شناسه محصول انتخاب نشده"
                    };
                product.IsRemoved = true;
                product.RemoveTime = DateTime.Now;
                _context.SaveChangesAsync();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "با موفیت حذف شد"
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
