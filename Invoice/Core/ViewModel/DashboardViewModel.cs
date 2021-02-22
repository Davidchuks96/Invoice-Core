using Invoice.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.ViewModel
{
    public class DashboardViewModel
    {
        public int Customers { get; set; }
        public int Products { get; set; }
        public double TotalSaleValue { get; set; }
        public int TotalSales { get; set; }
        public List<SalesModel> LastFiveSales { get; set; }
    }
}
