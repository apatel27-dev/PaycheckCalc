using Api.Dtos.Employee;

namespace Api.Services;

public interface IEmployeeService
{
    Task<GetEmployeeDto?> FindByIdAsync(int id);
    Task<ICollection<GetEmployeeDto>?> GetAllAsync();
}
