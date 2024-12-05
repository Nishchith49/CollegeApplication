using AutoMapper;
using CollegeApplication.Entities;
using CollegeApplication.IService;
using CollegeApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeApplication.Service
{
    public class CollegeService : ICollegeService
    {
        private readonly CollegeContext _context;
        private readonly IMapper _mapper;

        public CollegeService(CollegeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CollegeModel>> GetAllCollegesAsync()
        {
            var colleges = await _context.Colleges.ToListAsync();
            return _mapper.Map<IEnumerable<CollegeModel>>(colleges);
        }

        public async Task<CollegeModel> GetCollegeByIdAsync(long id)
        {
            var college = await _context.Colleges.FindAsync(id);
            return _mapper.Map<CollegeModel>(college);
        }

        public async Task<CollegeModel> CreateCollegeAsync(CollegeModel college)
        {
            var entity = _mapper.Map<College>(college);
            _context.Colleges.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CollegeModel>(entity);
        }

        public async Task<bool> UpdateCollegeAsync(CollegeModel college)
        {
            var entity = _mapper.Map<College>(college);
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await CollegeExists(college.CollegeId)))
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

        public async Task<bool> DeleteCollegeAsync(long id)
        {
            var college = await _context.Colleges.FindAsync(id);
            if (college == null)
            {
                return false;
            }

            _context.Colleges.Remove(college);
            await _context.SaveChangesAsync();
            return true;
        }

        private Task<bool> CollegeExists(long id)
        {
            return Task.FromResult(_context.Colleges.Any(e => e.CollegeId == id));
        }
    }
}