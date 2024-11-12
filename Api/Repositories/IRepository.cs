namespace Api.Repositories;

public interface IRepository<T>
{
    public Task<T?> FindByIdAsync(int id);
    public Task<ICollection<T>?> GetAllAsync();
}
