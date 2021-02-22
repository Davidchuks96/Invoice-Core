using Invoice.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.ViewModel
{
    public class SaleReportViewModel
    {
        public StoreSettingModel company { get; set; }
        public SalesModel Sales { get; set; }
    }
}
