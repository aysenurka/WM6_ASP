using System.Collections.Generic;
using System.ComponentModel;
using Admin.Models.Abstracts;
using Admin.Models.Enums;

namespace Admin.Models.Entities
{
    public class Order : BaseEntity<long>
    {
        [DisplayName("Sipariş Tipi")]
        public OrderTypes OrderType { get; set; }


        public virtual ICollection<Invoice> Invoices { get; set; }=new HashSet<Invoice>();
    }
}
