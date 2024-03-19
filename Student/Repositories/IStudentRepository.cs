using Microsoft.AspNetCore.Mvc;
using Student.Models.Domain;
using Student.Models.DTOs;

namespace Student.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Students>> GetAllStudentsAsync();
        Task<Students?> GetStudentAsync(Guid id);
        Task<Students> CreateStudentAsync(Students students);
        Task<Students?> UpdateAsync(Guid id, Students students);
        Task<Students?> RemoveStudentAsync(Guid id);
    }
}
