using System;

namespace NewStore.Application.Services.Users.Queris.GetUser
{
    public class GetUserDto
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string? Lastname { get; set; }
        public string Email { get; set; }
        public short? Age { get; set; }
    }
}
