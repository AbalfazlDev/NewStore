using NewStore.Application.Services.HomePage.Commands.AddPageImages;
using NewStore.Application.Services.HomePage.Queries.GetPageImages;

namespace NewStore.Application.Interfaces.FacadPatterns
{
    public interface IHomePageFacad
    {
        public IAddPageImageService AddImage { get; }
        public IGetPageImagesService GetImages { get;}
    }
}
