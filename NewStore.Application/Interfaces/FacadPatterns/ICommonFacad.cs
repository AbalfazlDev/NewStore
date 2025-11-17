using NewStore.Application.Services.Common.Queris.GetMenuItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Interfaces.FacadPatterns
{
    public interface ICommonFacad
    {
        public IGetMenuItemsService  GetMenuItems { get; }
    }
}
