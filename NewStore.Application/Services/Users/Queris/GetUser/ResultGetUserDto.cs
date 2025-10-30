using System.Collections.Generic;

namespace NewStore.Application.Services.Users.Queris.GetUser
{
    public class ResultGetUserDto
    {
        public List<GetUserDto> Users { get; set; }
        public int Rows { get; set; }
    }
}
