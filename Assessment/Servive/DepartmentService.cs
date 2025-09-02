using ApiAssessment.Interface;
using ApiAssessment.Models;
using ApiAssessment.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiAssessment.Servive
{
    public class DepartmentService:IDepartment
    {
        private readonly EmpDbContext _context;
        public DepartmentService(EmpDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Department>> GetAllDept()
        {
            return await _context.Departments
                .Include(d=>d.Employees)
                .ToListAsync();
        }

        public async Task<Department> AddDept(Department dept)
        {
            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();
            return dept;
        }
        public async Task<Department> UpdateDept(int id, Department dept)
        {
            var updated = await _context.Departments.FindAsync(id);
            if (updated == null)
            {
                return null;
            }
            updated.Name = dept.Name;
            _context.Departments.Update(updated);
            await _context.SaveChangesAsync();
            return updated;
        }
    }
}
