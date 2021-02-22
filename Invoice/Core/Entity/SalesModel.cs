using Invoice.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.Entity
{
    [Table("Sales")]
    public class SalesModel : BaseModel
    {
        [Required]
        [DisplayName("Sales Date")]
        public DateTime SalesDate { get; set; }
        public string SaleCode { get; set; }
        public string Notes { get; set; }
        [Required]
        public Double Total { get; set; }
        public string Status { get; set; }
        public double? Discount { get; set; }
        public double GrandTotal { get; set; }
        [DisplayName("Payment Method")]
        public string PaymentMethod { get; set; }

        [DisplayName("CustomerId")]
        [ForeignKey("CustomerModel")]
        public int CustomerId { get; set; }
        public CustomerModel CustomerModel { get; set; }

        public ICollection<SalesItemsModel> Items { get; set; }
        public SalesModel()
        {
            Items = new List<SalesItemsModel>();
        }
    }
 }
