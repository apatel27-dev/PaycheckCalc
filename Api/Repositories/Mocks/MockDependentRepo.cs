using System.Data;
using Api.Models;

namespace Api.Repositories.Mocks;

public class MockDependentRepo : IMockDependentRepo
{
    private ICollection<Dependent>? _dependents;
    public ICollection<Employee>? Employees { set; private get; }
    public async Task<Dependent?> FindByIdAsync(int id)
    {
        // Load data if not already loaded
        _dependents ??= await GetMockDataAsync();
        return _dependents?.FirstOrDefault(e => e.Id == id);
    }

    public async Task<ICollection<Dependent>?> GetAllAsync()
    {
        // Load Data only if not already loaded
        _dependents ??= await GetMockDataAsync();
        return _dependents;
    }

    private async Task<ICollection<Dependent>> GetMockDataAsync()
    {
        // Short delay of 1 sec to similate async 
        await Task.Delay(1000);

        var employees = Employees ?? throw new NoNullAllowedException("DependentProvider must have have a collection of employees");

        var allDependents = new List<Dependent>();
        employees.ToList().ForEach((e) => allDependents.AddRange(e.Dependents));

        return allDependents;
    }
}
