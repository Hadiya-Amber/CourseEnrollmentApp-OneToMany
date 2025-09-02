using ApiAssessment.Models;

namespace ApiAssessment.Interface
{
    public interface IDepartment
    {
        Task<IEnumerable<Department>> GetAllDept();
        Task<Department?> AddDept(Department dept);
        Task<Department?> UpdateDept(int id, Department dept);
    }
}
