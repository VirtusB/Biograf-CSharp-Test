using System.Linq;
using System.Threading.Tasks;
using BiografCSharpTest.Data;
using BiografCSharpTest.Models;
using Microsoft.EntityFrameworkCore;


namespace BiografCSharpTest.Services
{
    public class UserService : IUserService
    {
        private readonly IDiscountRepository _discountRepo;
        private readonly IUserRepository _userRepo;
        private readonly BioContext _context;

        public UserService(IDiscountRepository discountRepo, IUserRepository userRepo, BioContext context) {
            this._discountRepo = discountRepo;
            this._userRepo = userRepo;
            this._context = context;
        }
        
        public async Task<bool> UpdateLifetimeAmountSaved(User user, float ticketPrice, int ticketCount)
        {
            var discountStep = await _discountRepo.GetCurrentDiscountStep(user.Id);
            float rate = 1.00f * (discountStep.Amount / 100);
            float amountSaved = ticketCount * ticketPrice * rate;

            user.LifetimeSavedAmount = user.LifetimeSavedAmount + amountSaved;

            
            _userRepo.Update(user);
            
            return await _userRepo.SaveAll();
        }
    }
}