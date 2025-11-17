using System;

namespace NewStore.Application.Services.Users.Queris.GetUser
{
    public class RequestGetUserDto
    {
        public string SearchKey { get; set; }
        public UInt16 Page { get; set; }
    }
}
