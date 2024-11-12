using System.Collections.ObjectModel;
using System.Data;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Repositories;

namespace Api.Services;

public class EmployeeService(IMockEmployeeRepo mockEmployeeProvider) : IEmployeeService
{
    private readonly IMockEmployeeRepo _mockEmployeeProvider = mockEmployeeProvider;

    public async Task<GetEmployeeDto?> FindByIdAsync(int id)
    {
        var employee = await _mockEmployeeProvider.FindByIdAsync(id) ?? throw new NoNullAllowedException($"employee with {id} was not found.");
        ValidateDependentRules([employee]);

        // Ideally can use AutoMapper or something similar to map Models to Dtos
        return MapModelToDto(employee);
    }

    public async Task<ICollection<GetEmployeeDto>?> GetAllAsync()
    {
        var employees = await _mockEmployeeProvider.GetAllAsync() ?? throw new NoNullAllowedException("No employees found");
        ValidateDependentRules(employees);

        // Ideally can use AutoMapper or something similar to map Models to Dtos
        var dtos = employees?.ToList().Select(MapModelToDto).ToList();

        return dtos;
    }


    // Ideally validation would be done by IRepository Add method. The EmployeeRepository would have implemented an Add method which
    // would receive an Employee model as an arg and add it to the collection. Before adding it would call this method to check the 
    // employee dependents. Can also move the Validtion to an EmployeeValidation class 
    private void ValidateDependentRules(IEnumerable<Employee> employees)
    {
        // Check if any of the employees have more than 1 spouse or domestic partner
        var dependentContraints = new ReadOnlyCollection<Relationship>([Relationship.Spouse, Relationship.DomesticPartner]);
        var invalidDependents = employees.Any((e) => e.Dependents.Count((d) => dependentContraints.Contains(d.Relationship)) > 1);

        if (invalidDependents)
        {
            throw new InvalidDataException("Invalid employee data found. Emloyees can only have 1 spouse or domestic partner");
        }
    }


    // This should use an object to object mapper like AutoMapper
    private GetEmployeeDto MapModelToDto(Employee employee)
    {
        return new GetEmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            DateOfBirth = employee.DateOfBirth,
            Salary = employee.Salary,
            Dependents = employee.Dependents.Select((d) => new GetDependentDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                DateOfBirth = d.DateOfBirth,
                Relationship = d.Relationship
            }).ToList()
        };
    }
}

