using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.Models.Entities
{
    public class Order:BaseEntity
    {
        public string ShippedAddress { get; set; }
        public int EmployeeID { get; set; }
        //Relational Properties
        public virtual Employee Employee { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }


    }
}
