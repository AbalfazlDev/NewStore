using NewStore.Application.Services.Finances.Commands.AddRequest;
using NewStore.Application.Services.Finances.Commands.SetRequestAuthority;
using NewStore.Application.Services.Finances.Queries.GetRequestPay;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewStore.Application.Interfaces.FacadPatterns
{
    public interface IFinancesFacad
    {
        public IAddRequestPayService  AddRequestPay { get;}
        public IGetRequestPayService GetRequestPay { get;}
        public ISetRequestAuthorityService SetRequestAuthority {  get;}
    }
}
