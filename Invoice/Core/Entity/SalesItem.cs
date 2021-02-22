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
    [Table("SalesItems")]
    public class SalesItemsModel : BaseModel
    {
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Amount { get; set; }

        

        [DisplayName("Sale")]
        [ForeignKey("SalesModel")]
        public int SalesId { get; set; }
        public SalesModel SalesModel { get; set; }
    }
}
