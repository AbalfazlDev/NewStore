using NewStore.Common.Dto;

namespace NewStore.Application.Services.Users.Commands.LoginUser
{
    public class ResultLoginUserDto:ResultDto
    {
        public string Name { get; set; }
        public long UserId { get; set; }
        public int Roles { get; set; }
    }
}
