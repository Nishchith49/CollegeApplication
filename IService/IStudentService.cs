using CollegeApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollegeApplication.IService
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentModel>> GetAllStudentsAsync();
        Task<StudentModel?> GetStudentByIdAsync(long id); // Changed to long
        Task<StudentModel> CreateStudentAsync(StudentModel studentModel);
        Task<bool> UpdateStudentAsync(long id, StudentModel studentModel); // Changed to long
        Task<bool> DeleteStudentAsync(long id); // Changed to long
    }
}