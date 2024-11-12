using System.Data;
using Api.Dtos.Employee;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    private readonly IEmployeeService _employeeService = employeeService;

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        try
        {
            var employeeDto = await _employeeService.FindByIdAsync(id);

            return new ApiResponse<GetEmployeeDto>
            {
                Data = employeeDto,
                Success = true
            };
        }
        catch (Exception ex)
        {
            // The Service layer throws a few exceptions. Catch these here using the base catch block and determine ex type
            if (ex is NoNullAllowedException) return NotFound();
            return BadRequest("Invalid employee data");
        }
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        try
        {
            var employeeDtos = await _employeeService.GetAllAsync();
            var result = new ApiResponse<List<GetEmployeeDto>>
            {
                Data = employeeDtos?.ToList(),
                Success = true
            };

            return result;
        }
        catch (Exception ex)
        {
            // The Service layer throws a few exceptions. Catch these here using the base catch block and determine ex type
            if (ex is NoNullAllowedException) return NotFound();
            return BadRequest("Invalid employee data");
        }
    }
}
