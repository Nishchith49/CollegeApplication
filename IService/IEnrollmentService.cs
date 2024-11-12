using CollegeApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollegeApplication.IService
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentModel>> GetAllEnrollmentsAsync();
        Task<EnrollmentModel?> GetEnrollmentByIdAsync(long id); // Changed to long
        Task<EnrollmentModel> CreateEnrollmentAsync(EnrollmentModel enrollmentModel);
        Task<bool> UpdateEnrollmentAsync(long id, EnrollmentModel enrollmentModel); // Changed to long
        Task<bool> DeleteEnrollmentAsync(long id); // Changed to long
    }
}