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
        public ResultDto<List<GetPageImagesResult>> Execute();
    }

    public class GetPageImagesService : IGetPageImagesService
    {
        IDataBaseContext _context;

        public GetPageImagesService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<GetPageImagesResult>> Execute()
        {
            List<GetPageImagesResult> PageImages =_context.PageImages.Select(p =>new GetPageImagesResult
            {
                Src = p.Src,
                CategoryId = p.CategoryId,
                Position = p.Position,
            }).ToList();

            return new ResultDto<List<GetPageImagesResult>>
            {
                IsSuccess = true,
                Data = PageImages
            };
        }
    }

    public class GetPageImagesResult
    {
        public PositionInPage Position { get; set; }
        public string Src { get; set; }
        public long CategoryId { get; set; }
    }
}
