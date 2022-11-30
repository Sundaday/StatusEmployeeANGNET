using BackEndApi.Models;

namespace BackEndApi.Services.Contract
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetList();
    }
}
