using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndApi.Models;

public partial class Employee
{
    [Key]
    public int IdEmployee { get; set; }
    public string? FullName { get; set; }
    public int? Salary { get; set; }
    public DateTime? HireDate { get; set; }
    [ForeignKey("Department")]
    public int? IdDepartment { get; set; }
    public virtual Department? Department { get; set; }
}
