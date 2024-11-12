using CollegeApplication.Entities;
using CollegeApplication.IService;
using CollegeApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeApplication.Service
{
    public class StudentService : IStudentService
    {
        private readonly CollegeContext _context;

        public StudentService(CollegeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentModel>> GetAllStudentsAsync()
        {
            var students = await _context.Students
                .Include(s => s.Department)
                .ToListAsync();

            return students.Select(s => MapEntityToModel(s));
        }

        public async Task<StudentModel?> GetStudentByIdAsync(long id)
        {
            var student = await _context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            return student != null ? MapEntityToModel(student) : null;
        }

        public async Task<StudentModel> CreateStudentAsync(StudentModel studentModel)
        {
            var studentEntity = MapModelToEntity(studentModel);
            _context.Students.Add(studentEntity);
            await _context.SaveChangesAsync();
            return MapEntityToModel(studentEntity);
        }

        public async Task<bool> UpdateStudentAsync(long id, StudentModel studentModel)
        {
            if (id != studentModel.StudentId)
                return false;

            var studentEntity = await _context.Students.FindAsync(id);
            if (studentEntity == null)
                return false;

            // Assign properties directly assuming they are non-nullable
            studentEntity.Name = studentModel.Name;
            studentEntity.DateOfBirth = studentModel.DateOfBirth;
            studentEntity.Email = studentModel.Email;
            studentEntity.Phone = studentModel.Phone;
            studentEntity.DepartmentId = studentModel.DepartmentId;

            _context.Entry(studentEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStudentAsync(long id)
        {
            var studentEntity = await _context.Students.FindAsync(id);
            if (studentEntity == null)
                return false;

            _context.Students.Remove(studentEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        private StudentModel MapEntityToModel(Student student)
        {
            return new StudentModel
            {
                StudentId = student.StudentId,
                Name = student.Name ?? string.Empty, // Provide default value for Name
                DateOfBirth = student.DateOfBirth,
                Email = student.Email ?? string.Empty, // Provide default value for Email
                Phone = student.Phone,
                DepartmentId = student.DepartmentId
            };
        }

        private Student MapModelToEntity(StudentModel studentModel)
        {
            return new Student
            {
                StudentId = studentModel.StudentId,
                Name = studentModel.Name,
                DateOfBirth = studentModel.DateOfBirth,
                Email = studentModel.Email,
                Phone = studentModel.Phone,
                DepartmentId = studentModel.DepartmentId
            };
        }
    }
}