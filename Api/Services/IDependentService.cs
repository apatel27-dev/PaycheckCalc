using Api.Dtos.Dependent;

namespace Api.Services;

public interface IDependentService
{
    Task<GetDependentDto?> FindByIdAsync(int id);
    Task<ICollection<GetDependentDto>?> GetAllAsync();

}
