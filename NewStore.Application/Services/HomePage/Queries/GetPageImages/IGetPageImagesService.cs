using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewStore.Application.Services.HomePage.Queries.GetPageImages
{
    public interface IGetPageImagesService
    {
        public ResultDto<List<PageImages>> Execute();
    }

    public class GetPageImagesService : IGetPageImagesService
    {
        IDataBaseContext _context;

        public GetPageImagesService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<PageImages>> Execute()
        {
            List<PageImages> PageImages =_context.PageImages.Select(p =>new PageImages
            {
                Src = p.Src,
                CategoryId = p.CategoryId,
                Position = p.Position,
                Name = p.Name,
            }).ToList();

            return new ResultDto<List<PageImages>>
            {
                IsSuccess = true,
                Data = PageImages
            };
        }
    }

    
}
