using CoreMVCIntro.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.VMClasses
{
    public class EmployeeVM
    {
        public List<Employee> Employees { get; set; }
        public Employee Employee { get; set; }
    }
}
