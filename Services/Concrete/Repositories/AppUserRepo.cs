using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JWTAuthorizationASPNETCoreDemo.Models.Abstract.DbModels;
using JWTAuthorizationASPNETCoreDemo.Models.Concrete.DbModels;
using JWTAuthorizationASPNETCoreDemo.Services.Abstract.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthorizationASPNETCoreDemo.Services.Concrete.Repositories
{
    public class AppUserRepo : IAppUserRepo
    {
        private readonly AppUserDbContext _context;

        public AppUserRepo(AppUserDbContext context)
        {
            _context = context;
        }

        private IQueryable<IAppUser> AppUsers => _context.AppUsers;

        public List<IAppUser> GetListByFilter(Expression<Func<IAppUser, bool>> filter)
            => filter == null ? AppUsers.ToList() : AppUsers.Where(filter).ToList();

        public IAppUser GetByFilter(Expression<Func<IAppUser, bool>> filter)
            => filter == null ? null : AppUsers.FirstOrDefault(filter);

        public void Update(IAppUser user)
        {
            var updatedEntity = _context.Entry(user);
            updatedEntity.State = EntityState.Modified;

            _context.SaveChanges();
        }

        public void Add(IAppUser user)
        {
            var addedEntity = _context.Entry(user);
            addedEntity.State = EntityState.Added;

            _context.SaveChanges();
        }
    }
}