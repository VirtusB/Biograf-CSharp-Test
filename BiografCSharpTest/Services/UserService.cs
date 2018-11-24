using System.Threading.Tasks;
using BiografCSharpTest.Data;

namespace BiografCSharpTest.Services
{
    public class UserService : IUserService
    {
        private readonly IDiscountRepository _discountRepo;
        private readonly IUserRepository _userRepo;

        public UserService(IDiscountRepository discountRepo, IUserRepository userRepo) {
            this._discountRepo = discountRepo;
            this._userRepo = userRepo;
        }
        
        public async Task<bool> UpdateLifetimeAmountSaved(int id, float ticketPrice) //TODO: virker ikke
        {
            var discountStep = await _discountRepo.GetCurrentDiscountStep(id);
            float rate = 1.00f * (discountStep.Amount / 100);
            float amountSaved = ticketPrice * rate;

            var user = await _userRepo.GetUser(id);

            user.LifetimeSavedAmount = user.LifetimeSavedAmount + amountSaved;


            _userRepo.Update(user);
            return await _userRepo.SaveAll();
        }
    }
}