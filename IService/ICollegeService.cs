using CollegeApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollegeApplication.IService
{
    public interface ICollegeService
    {
        Task<IEnumerable<CollegeModel>> GetAllCollegesAsync();
        Task<CollegeModel> GetCollegeByIdAsync(long id);
        Task<CollegeModel> CreateCollegeAsync(CollegeModel college);
        Task<bool> UpdateCollegeAsync(CollegeModel college);
        Task<bool> DeleteCollegeAsync(long id);
    }
}