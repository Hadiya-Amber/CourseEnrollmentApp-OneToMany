using ApiAssessment.Data;
using ApiAssessment.Interface;
using ApiAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAssessment.Servive
{
    public class ProjectService:IProject
    {
        private readonly EmpDbContext _context;
        public ProjectService(EmpDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Project>> GetAllProj()
        {
            return await _context.Projects
                .Include(e=>e.EmpPros)
                .ToListAsync();
        }
        public async Task<Project> AddProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }
    }
}
