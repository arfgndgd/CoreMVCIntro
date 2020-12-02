using CoreMVCIntro.Models.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.Models.Entities
{
    public class Employee:BaseEntity
    {
        public string FirstName { get; set; }

        public UserRole UserRole { get; set; }
        //Relational Properties

        public virtual IList<Order> Orders { get; set; }


    }
}
