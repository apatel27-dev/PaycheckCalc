using System.Data;
using Api.Dtos.Dependent;
using Api.Models;
using Api.Repositories;

namespace Api.Services;

public class DependentService(IMockDependentRepo mockDependentRepo, IMockEmployeeRepo mockEmployeeRepo) : IDependentService
{
    private readonly IMockDependentRepo _mockDependentRepo = mockDependentRepo;
    private readonly IMockEmployeeRepo _mockEmployeeProvider = mockEmployeeRepo;

    public async Task<GetDependentDto?> FindByIdAsync(int id)
    {
        _mockDependentRepo.Employees = await _mockEmployeeProvider.GetAllAsync() ?? throw new NoNullAllowedException("No employees found.");

        var dependent = await _mockDependentRepo.FindByIdAsync(id) ?? throw new NoNullAllowedException($"Dependent with {id} was not found.");

        // Ideally can use AutoMapper or something similar to map Models to Dtos
        return MapModelToDto(dependent);
    }

    public async Task<ICollection<GetDependentDto>?> GetAllAsync()
    {
        _mockDependentRepo.Employees = await _mockEmployeeProvider.GetAllAsync() ?? throw new NoNullAllowedException("No employees found.");
        var dependents = await _mockDependentRepo.GetAllAsync() ?? throw new NoNullAllowedException("No dependents found.");

        // Ideally can use AutoMapper or something similar to map Models to Dtos
        var dtos = dependents?.ToList().Select(MapModelToDto).ToList();

        return dtos;
    }

    // This should use an object to object mapper like AutoMapper
    private GetDependentDto MapModelToDto(Dependent dependent)
    {
        return new GetDependentDto
        {
            Id = dependent.Id,
            FirstName = dependent.FirstName,
            LastName = dependent.LastName,
            DateOfBirth = dependent.DateOfBirth,
            Relationship = dependent.Relationship
        };
    }
}

