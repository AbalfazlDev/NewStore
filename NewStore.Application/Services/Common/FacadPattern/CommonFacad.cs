using NewStore.Application.Interfaces.Contexts;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Common.Queris.GetMenuItems;

namespace NewStore.Application.Services.Common.FacadPattern
{
    public class CommonFacad : ICommonFacad
    {
        private readonly IDataBaseContext _context;
        public CommonFacad(IDataBaseContext context)
        {
            _context = context;
        }
        private IGetMenuItemsService _getMenuItems;
        public IGetMenuItemsService GetMenuItems
        {
            get
            {
                return _getMenuItems = _getMenuItems ?? new GetMenuItemsService(_context);
            }
        }
    }
}
