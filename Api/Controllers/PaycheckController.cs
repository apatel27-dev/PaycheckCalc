using System.Data;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaycheckController(IPaycheckService paycheckService) : ControllerBase
    {
        private readonly IPaycheckService _paycheckService = paycheckService;

        [SwaggerOperation(Summary = "Get paycheck by employee id and paycheck number")]
        [HttpGet("{checkNum}/employee/{id}")]
        public async Task<ActionResult<ApiResponse<GetPaycheckDto>>> Get(int id, int checkNum)
        {
            try
            {
                var paycheckDto = await _paycheckService.ComputePaycheck(id, checkNum);

                return new ApiResponse<GetPaycheckDto>
                {
                    Data = paycheckDto,
                    Success = true
                };

            }
            catch (Exception ex)
            {
                // The Service layer throws a few exceptions. Catch these here using the base catch block and determine ex type
                if (ex is NoNullAllowedException) return NotFound();
                return BadRequest("Invalid request data");
            }
        }
    }
}
