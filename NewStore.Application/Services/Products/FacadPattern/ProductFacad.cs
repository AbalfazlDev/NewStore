using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Products.Commands.AddCategoryService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Products.FacadPattern
{
    public class ProductFacad : IProductFacad
    {
        private readonly IDataBaseContext _context;
        public ProductFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IAddCategoryService _addCategory; 
        public IAddCategoryService AddCategory
        {
            get
            {
                return _addCategory = _addCategory?? new AddCategoryService(_context);
            }
        }
    }
}
