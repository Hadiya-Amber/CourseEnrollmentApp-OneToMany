using ApiAssessment.Data;
using ApiAssessment.Interface;
using ApiAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAssessment.Servive
{
    public class EmployeeService:IEmployee
    {
        private readonly EmpDbContext _context;

        public EmployeeService(EmpDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmp()
        {
            return await _context.Employees
           .Include(d => d.Department)
           .ToListAsync();
        }
        public async Task<Employee> GetEmpById(int id)
        {
            return await _context.Employees
                .Include(d => d.Department)
                .FirstOrDefaultAsync();
        }
        public async Task<Employee> AddEmp(Employee emp)
        {
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();
            return emp;
        }
        public async Task<Employee> UpdateEmp(int id, Employee emp)
        {
            var updated = await _context.Employees.FindAsync(id);
            if (updated == null)
            {
                return null;
            }
            updated.Name = emp.Name;
            updated.Email = emp.Email;
            updated.Role = emp.Role;
            updated.DepartmentId = emp.DepartmentId;

            _context.Employees.Update(updated);
            await _context.SaveChangesAsync();
            return updated;
        }
        public async Task<bool> DeleteEmp(int id)
        {
            var del = await _context.Employees.FindAsync(id);
            if (del == null)
            {
                return false;
            }
            _context.Employees.Remove(del);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
