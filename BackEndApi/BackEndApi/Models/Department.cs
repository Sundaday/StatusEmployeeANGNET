using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndApi.Models;

public partial class Department
{
    public Department()
    {
        Employees= new HashSet<Employee>();
    }
    [Key]
    public int IdDepartment { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
