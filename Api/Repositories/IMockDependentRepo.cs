using Api.Models;

namespace Api.Repositories;

public interface IMockDependentRepo : IRepository<Dependent>
{
    public ICollection<Employee> Employees { set; }
}