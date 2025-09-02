using ApiAssessment.Models;

namespace ApiAssessment.Interface
{
    public interface IProject
    {
        Task<IEnumerable<Project>> GetAllProj();
        Task<Project?> AddProject(Project proj);
    }
}
