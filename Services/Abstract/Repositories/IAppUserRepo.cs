using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JWTAuthorizationASPNETCoreDemo.Models.Abstract.DbModels;

namespace JWTAuthorizationASPNETCoreDemo.Services.Abstract.Repositories
{
    public interface IAppUserRepo
    {
        List<IAppUser> GetListByFilter(Expression<Func<IAppUser, bool>> filter);
        IAppUser GetByFilter(Expression<Func<IAppUser, bool>> filter);
        void Add(IAppUser user);
        void Update(IAppUser user);
    }
}