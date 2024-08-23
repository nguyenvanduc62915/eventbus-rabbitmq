using Microsoft.EntityFrameworkCore;
using UserServices.Data;
using UserServices.Interface;
using UserServices.Models;

namespace UserServices.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _appDbContext;
        public AccountRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Onwer?> FindByEmail(string email)
        {
            return await _appDbContext.Onwers.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}





