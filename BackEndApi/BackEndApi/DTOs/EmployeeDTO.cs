﻿namespace BackEndApi.DTOs
{
    public class EmployeeDTO
    {
        public int IdEmployee { get; set; }
        public string? FullName { get; set; }
        public int? IdDepartment { get; set; }
        public string? DepartmentName { get; set; }
        public string? Salary { get; set; }
        public string? HireDate { get; set; }
    }
}
