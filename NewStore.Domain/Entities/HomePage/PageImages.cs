using NewStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewStore.Domain.Entities.HomePage
{
    public class PageImages : BaseEntity
    {
        public string Name { get; set; }
        public string Src { get; set; }
        public long CategoryId { get; set; }
        public PositionInPage Position { get; set; }
    }
    public enum PositionInPage
    {
        MainPageSlider = 0,
        PopularCategory = 1,
        Banner = 2
    }
}
