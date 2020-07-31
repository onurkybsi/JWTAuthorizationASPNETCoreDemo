using System.Linq;
using JWTAuthorizationASPNETCoreDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthorizationASPNETCoreDemo.Services.Repositories
{
    public class AppUserRepo : IAppUserRepo
    {
        private readonly AppUserDbContext _context;

        public AppUserRepo(AppUserDbContext context)
        {
            _context = context;
        }

        public IQueryable<AppUser> AppUsers => _context.AppUsers;

        public AppUser GetByUserName(string username) => AppUsers.SingleOrDefault(u => u.Username == username);

        public void Update(AppUser updatedUser)
        {
            var addedEntity = _context.Entry(updatedUser);
            addedEntity.State = EntityState.Modified;
            
            _context.SaveChanges();
        }
    }
}