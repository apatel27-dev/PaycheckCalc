using System.Data;
using Api.Dtos.Dependent;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController(IDependentService dependentService) : ControllerBase
{
    private readonly IDependentService _dependentService = dependentService;

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        try
        {
            var dependentDto = await _dependentService.FindByIdAsync(id);

            return new ApiResponse<GetDependentDto>
            {
                Data = dependentDto,
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

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        try
        {
            var dependentDtos = await _dependentService.GetAllAsync();
            var result = new ApiResponse<List<GetDependentDto>>
            {
                Data = dependentDtos?.ToList(),
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
