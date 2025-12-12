using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.HomePage;
using NewStore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewStore.Application.Services.HomePage.Commands.AddPageImages
{
    public interface IAddPageImageService
    {
        public ResultDto Execute(string name, string filePath, long categoryId, PositionInPage position);
    }
    public class AddPageImageService : IAddPageImageService
    {
        private readonly IDataBaseContext _context;

        public AddPageImageService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(string name, string filePath, long categoryId, PositionInPage position)
        {
            PageImages pageImages = new PageImages()
            {
                Name = name,
                CategoryId = categoryId,
                Position = position,
                Src = filePath
            };

            _context.PageImages.Add(pageImages);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,

            };
        }
    }
}
