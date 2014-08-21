using BaseApp.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace BaseApp.Data.Contracts
{
    public interface IAuthRepository : IRepository<ApplicationUser>, IDisposable
    {
        Task<IdentityResult> RegisterUser(string userName, string password);
        Task<ApplicationUser> FindUser(string userName, string password);
    }
}
