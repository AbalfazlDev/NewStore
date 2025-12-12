using Microsoft.AspNetCore.Hosting;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.HomePage.Commands.AddPageImages;
using NewStore.Application.Services.HomePage.Queries.GetPageImages;

namespace NewStore.Application.Services.HomePage.FacadPattren
{
    public class HomePageFacad : IHomePageFacad
    {
        IDataBaseContext _context;
        IHostingEnvironment _environment;

        public HomePageFacad(IHostingEnvironment environment, IDataBaseContext context)
        {
            _context = context;
            _environment = environment;
        }

        private IAddPageImageService _addImage;

        public IAddPageImageService AddImage
        {
            get
            {
                return _addImage = _addImage ?? new AddPageImageService(_context);
            }
        }

        private IGetPageImagesService _getPageImages;
        public IGetPageImagesService GetImages
        {
            get
            {
                return _getPageImages = _getPageImages ?? new GetPageImagesService(_context);
            }
        }
    }
}
