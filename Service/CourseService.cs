using AutoMapper;
using CollegeApplication.Entities;
using CollegeApplication.IService;
using CollegeApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollegeApplication.Service
{
    public class CourseService : ICourseService
    {
        private readonly CollegeContext _context;
        private readonly IMapper _mapper;

        public CourseService(CollegeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseModel>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            return _mapper.Map<IEnumerable<CourseModel>>(courses);
        }

        public async Task<CourseModel> GetCourseByIdAsync(long id)
        {
            var course = await _context.Courses.FindAsync(id);
            return _mapper.Map<CourseModel>(course);
        }

        public async Task<CourseModel> CreateCourseAsync(CourseModel course)
        {
            var entity = _mapper.Map<Course>(course);
            _context.Courses.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CourseModel>(entity);
        }

        public async Task<bool> UpdateCourseAsync(CourseModel course)
        {
            var entity = _mapper.Map<Course>(course);
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await CourseExists(course.CourseId)))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteCourseAsync(long id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return false;
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        private Task<bool> CourseExists(long id)
        {
            return Task.FromResult(_context.Courses.Any(e => e.CourseId == id));
        }
    }
}