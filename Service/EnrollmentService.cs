using CollegeApplication.Entities;
using CollegeApplication.IService;
using CollegeApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeApplication.Service
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly CollegeContext _context;

        public EnrollmentService(CollegeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EnrollmentModel>> GetAllEnrollmentsAsync()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();

            return enrollments.Select(e => MapEntityToModel(e));
        }

        public async Task<EnrollmentModel?> GetEnrollmentByIdAsync(long id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.EnrollmentId == id);

            return enrollment != null ? MapEntityToModel(enrollment) : null;
        }

        public async Task<EnrollmentModel> CreateEnrollmentAsync(EnrollmentModel enrollmentModel)
        {
            var enrollmentEntity = MapModelToEntity(enrollmentModel);
            _context.Enrollments.Add(enrollmentEntity);
            await _context.SaveChangesAsync();
            return MapEntityToModel(enrollmentEntity);
        }

        public async Task<bool> UpdateEnrollmentAsync(long id, EnrollmentModel enrollmentModel)
        {
            if (id != enrollmentModel.EnrollmentId)
                return false;

            var enrollmentEntity = await _context.Enrollments.FindAsync(id);
            if (enrollmentEntity == null)
                return false;

            enrollmentEntity.StudentId = enrollmentModel.StudentId;
            enrollmentEntity.CourseId = enrollmentModel.CourseId;
            enrollmentEntity.EnrollmentDate = enrollmentModel.EnrollmentDate;

            if (enrollmentModel.Grade.HasValue)
            {
                enrollmentEntity.Grade = (Entities.Grade?)enrollmentModel.Grade.Value;
            }

            _context.Entry(enrollmentEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEnrollmentAsync(long id)
        {
            var enrollmentEntity = await _context.Enrollments.FindAsync(id);
            if (enrollmentEntity == null)
                return false;

            _context.Enrollments.Remove(enrollmentEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        private EnrollmentModel MapEntityToModel(Enrollment enrollment)
        {
            return new EnrollmentModel
            {
                EnrollmentId = enrollment.EnrollmentId,
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                EnrollmentDate = enrollment.EnrollmentDate,
                Grade = (Models.Grade?)enrollment.Grade
            };
        }

        private Enrollment MapModelToEntity(EnrollmentModel enrollmentModel)
        {
            return new Enrollment
            {
                EnrollmentId = enrollmentModel.EnrollmentId,
                StudentId = enrollmentModel.StudentId,
                CourseId = enrollmentModel.CourseId,
                EnrollmentDate = enrollmentModel.EnrollmentDate,
                Grade = (Entities.Grade?)enrollmentModel.Grade
            };
        }
    }
}