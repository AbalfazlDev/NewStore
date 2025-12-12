using NewStore.Application.Services.HomePage.Queries.GetPageImages;
using NewStore.Domain.Entities.HomePage;

namespace EndPoint.Site.Models
{
    public class HomePageVM
    {
        public List<PageImages> PageImages { get; set; }
    }
}
