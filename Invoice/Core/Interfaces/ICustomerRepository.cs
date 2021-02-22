using Invoice.Core.Entity;
using Invoice.Core.Interfaces.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<CustomerModel>
    {
        IEnumerable<SelectListItem> GetAllForDropDown();
    }
}
