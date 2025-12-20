using NewStore.Application.Services.HomePage.Queries.GetPageImages;
using NewStore.Domain.Entities.Carts;
using NewStore.Domain.Entities.HomePage;

namespace EndPoint.Site.Models
{
    public class HomePageVM
    {
        public List<PageImages> PageImages { get; set; }
        public List<CountCartItemDto> CountCartItemsDto { get; set; }
    }
    public class CountCartItemDto
    {
        public long CartItemId { get; set; }
        public int Count { get; set; }
    }
}
