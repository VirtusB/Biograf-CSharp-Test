using System.Threading.Tasks;
using BiografCSharpTest.Models;

namespace BiografCSharpTest.Services
{
    public interface IUserService
    {
        Task<bool> UpdateLifetimeAmountSaved(User user, float ticketPrice, int ticketCount);
    }
}