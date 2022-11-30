using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackEndApi.Models;

public partial class DbemployeeContext : DbContext
{
    public DbemployeeContext(DbContextOptions<DbemployeeContext> options) : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

}
