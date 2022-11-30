using BackEndApi.Models;

namespace BackEndApi.Services.Contract
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetList();
        Task<Employee> Get(int IdEmployee);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(Employee employee);

    }
}
