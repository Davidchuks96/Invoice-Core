using Invoice.Core.Entity;
using Invoice.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.Interfaces.Base
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        bool ValidateUser(LoginViewModel user);
    }
}
