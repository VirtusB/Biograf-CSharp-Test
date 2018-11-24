using System.Threading.Tasks;

namespace BiografCSharpTest.Services
{
    public interface IUserService
    {
        Task<bool> UpdateLifetimeAmountSaved(int id, float ticketPrice);
    }
}