using NewStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Products.Queris.GetProduct
{
    public interface IGetProductService
    {
        public ResultDto<ResultGetProductDto> Execute(Ordering ordering, string searchKey, UInt16 page, byte pageSize, long? categoryId);
    }

    public  enum Ordering
    {
        NotOrder = 0,
        /// <summary>
        /// پربازدیدترین
        /// </summary>
        MostVisited = 1,
        /// <summary>
        /// پرفروشترین
        /// </summary>
        Bestselling = 2,
        /// <summary>
        /// محبوبترین
        /// </summary>
        MostPopular = 3,
        /// <summary>
        /// جدیدترین
        /// </summary>
        theNewest = 4,
        /// <summary>
        /// ارزانترین
        /// </summary>
        Cheapest = 5,
        /// <summary>
        /// گرانترین
        /// </summary>
        theMostExpensive = 6
    }

    public class ResultGetProductDto
    {
        public List<GetProductDto> Products { get; set; }
        public uint RowsCount { get; set; }
    }

    public class GetProductDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string ImageSrc { get; set; }
        public byte Star { get; set; }
        public int Price { get; set; }
    }
}
