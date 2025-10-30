using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Users.Queris.GetUser
{
    public interface IGetUserServise
    {
        public ResultGetUserDto Execute(RequestGetUserDto request);
    }
}
