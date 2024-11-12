using CollegeApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollegeApplication.IService
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseModel>> GetAllCoursesAsync();
        Task<CourseModel> GetCourseByIdAsync(long id);
        Task<CourseModel> CreateCourseAsync(CourseModel course);
        Task<bool> UpdateCourseAsync(CourseModel course);
        Task<bool> DeleteCourseAsync(long id);
    }
}