using AutoMapper;
using CollegeApplication.Entities;
using CollegeApplication.Models;

namespace CollegeApplication
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Create a mapping between College and CollegeModel
            CreateMap<College, CollegeModel>();
            CreateMap<Course,  CourseModel>();
            CreateMap<Department, DepartmentModel>();
        }
    }
}
