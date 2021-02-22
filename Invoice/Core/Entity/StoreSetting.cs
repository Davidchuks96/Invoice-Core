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
    [Table("StoreSetting")]
    public class StoreSettingModel : BaseModel
    {

        public string Logo { get; set; }

        [DisplayName("Store Name")]
        [Required]
        public string StoreName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Web { get; set; }

        [Phone]
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Currency { get; set; }
        [Required]
        public string Address { get; set; }

    }
}
