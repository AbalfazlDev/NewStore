using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.HomePage;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewStore.Application.Services.HomePage.Commands.RemovePageImages
{
    public interface IRemovePageImageService
    {
        public ResultDto Execute(long pageImageId);
    }

    public class RemovePageImageService : IRemovePageImageService
    {
        private readonly IDataBaseContext _context;
        public RemovePageImageService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long pageImageId)
        {
            PageImages pageImage = _context.PageImages.Find(pageImageId);
            if (pageImage == null)
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "تصویر یافت نشد"
                };
            pageImage.IsRemoved = true;
            pageImage.RemoveTime = DateTime.Now;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,

            };
        }
    }
}
