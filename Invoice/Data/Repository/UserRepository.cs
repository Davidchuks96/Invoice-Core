using Invoice.Core.Entity;
using Invoice.Core.Interfaces.Base;
using Invoice.Core.ViewModel;
using Invoice.Data.AppDataContext;
using Invoice.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Data.Repository
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        public UserRepository(InvoiceDbContext context) : base(context)
        {
        }

        public bool ValidateUser(LoginViewModel user)
        {
            return All().Any(x => x.Email == user.Email && x.Password == user.Password);
        }
    }
}
