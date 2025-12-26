using NewStore.Application.Interfaces.Contexts;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Finances.Commands.AddRequest;
using NewStore.Application.Services.Finances.Queries.GetRequestPay;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewStore.Application.Services.Finances.FacadPattern
{
    public class FinancesFacad : IFinancesFacad
    {
        private readonly IDataBaseContext _context;
        public FinancesFacad(IDataBaseContext context)
        {
            _context = context;
        }
        private IAddRequestPayService _addRequestPay;
        public IAddRequestPayService AddRequestPay
        {
            get
            {
                return _addRequestPay = _addRequestPay ?? new AddRequestPayService(_context);
            }
        }
        private IGetRequestPayService _getRequestPay;
        public IGetRequestPayService GetRequestPay
        {
            get
            {
                return _getRequestPay = _getRequestPay ?? new GetRequestPayService(_context);
            }
        }
    }
}
