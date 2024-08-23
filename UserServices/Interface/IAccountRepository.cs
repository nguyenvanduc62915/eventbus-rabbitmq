using UserServices.Models;

namespace UserServices.Interface
{
    public interface IAccountRepository
    {
        Task<Onwer?> FindByEmail(string email);
    }
}
