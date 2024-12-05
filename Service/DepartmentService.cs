using AutoMapper;
using CollegeApplication.Entities;
using CollegeApplication.IService;
using CollegeApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeApplication.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly CollegeContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(CollegeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentModel>> GetAllDepartmentsAsync()
        {
            var departments = await _context.Departments.ToListAsync();
            return _mapper.Map<IEnumerable<DepartmentModel>>(departments);
        }

        public async Task<DepartmentModel> GetDepartmentByIdAsync(long id)
        {
            var department = await _context.Departments.FindAsync(id);
            return _mapper.Map<DepartmentModel>(department);
        }

        public async Task<DepartmentModel> CreateDepartmentAsync(DepartmentModel department)
        {
            var entity = _mapper.Map<Department>(department);
            _context.Departments.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<DepartmentModel>(entity);
        }

        public async Task<bool> UpdateDepartmentAsync(DepartmentModel department)
        {
            var entity = _mapper.Map<Department>(department);
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await DepartmentExists(department.DepartmentId)))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> DeleteDepartmentAsync(long id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return false;
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }

        private Task<bool> DepartmentExists(long id)
        {
            return Task.FromResult(_context.Departments.Any(e => e.DepartmentId == id));
        }
    }
}