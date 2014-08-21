using BaseApp.Data.Contracts;
using BaseApp.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BaseApp.Data.Repositories
{
    public class AuthRepository : EFRepository<ApplicationUser>, IAuthRepository
    {
        private UserManager<ApplicationUser> _userManager;

        public AuthRepository(DbContext context)
            : base(context)
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        public async Task<IdentityResult> RegisterUser(string userName, string password)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = userName
            };

            var result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            DbContext.Dispose();
            _userManager.Dispose();

        }
    }
}
