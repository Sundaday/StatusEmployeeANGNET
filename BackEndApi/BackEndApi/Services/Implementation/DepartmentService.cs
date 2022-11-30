using BackEndApi.Models;
using BackEndApi.Services.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BackEndApi.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DbemployeeContext _context;

        public DepartmentService(DbemployeeContext context)
        {
            _context = context;
        }
        public async Task<List<Department>> GetList()
        {
            return await _context.Departments.ToListAsync();
        }
    }
}
