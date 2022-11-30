using BackEndApi.Models;
using BackEndApi.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace BackEndApi.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DbemployeeContext _context;
        public EmployeeService(DbemployeeContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetList()
        {
            try
            {
                List<Employee> employees = new List<Employee>();
                employees = await _context.Employees.Include(c => c.Department).ToListAsync();
                return employees;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Employee> Get(int idEmployee)
        {
            try
            {
                Employee? employee = new Employee();
                employee = await _context.Employees
                    .Include(c => c.Department)
                    .Where(i => i.IdEmployee == idEmployee)
                    .FirstOrDefaultAsync();
                return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                return employee;                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
