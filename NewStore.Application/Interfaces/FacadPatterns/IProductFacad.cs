using NewStore.Application.Services.Products.Commands.AddCategoryService;
using NewStore.Application.Services.Products.Queris.GetCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Interfaces.FacadPatterns
{
    public interface IProductFacad
    {
        public IAddCategoryService AddCategory { get;}
        public IGetCategoriesService GetCategories {  get;}
    }
}
