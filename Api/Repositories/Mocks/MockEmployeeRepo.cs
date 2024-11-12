using Api.Models;

namespace Api.Repositories.Mocks;

public class MockEmployeeRepo : IMockEmployeeRepo
{
    private ICollection<Employee>? _employees;

    public async Task<Employee?> FindByIdAsync(int id)
    {
        // Load data if not already loaded
        _employees ??= await GetMockDataAsync();
        return _employees?.FirstOrDefault(e => e.Id == id);
    }

    public async Task<ICollection<Employee>?> GetAllAsync()
    {
        // Load Data only if not already loaded
        _employees ??= await GetMockDataAsync();
        return _employees;
    }

    private async Task<ICollection<Employee>> GetMockDataAsync()
    {
        // Short delay of 1 sec to similate async 
        await Task.Delay(1000);

        var employees = new List<Employee>{
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }};

        return employees;
    }

}
