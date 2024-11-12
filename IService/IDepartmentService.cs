using CollegeApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollegeApplication.IService
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentModel>> GetAllDepartmentsAsync();
        Task<DepartmentModel> GetDepartmentByIdAsync(long id);
        Task<DepartmentModel> CreateDepartmentAsync(DepartmentModel department);
        Task<bool> UpdateDepartmentAsync(DepartmentModel department);
        Task<bool> DeleteDepartmentAsync(long id);
    }
}