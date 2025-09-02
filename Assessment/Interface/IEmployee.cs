using ApiAssessment.Models;

namespace ApiAssessment.Interface
{
    public interface IEmployee
    {
        Task<IEnumerable<Employee>> GetAllEmp();
        Task<Employee?> GetEmpById(int id);

        Task<Employee> AddEmp(Employee emp);
        Task<Employee?> UpdateEmp(int id, Employee emp);
        Task<bool> DeleteEmp(int id);

    }
}
