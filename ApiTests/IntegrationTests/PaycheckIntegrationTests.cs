using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Dtos.Paycheck;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ApiTests.IntegrationTests;

public class PaycheckIntegrationTests : IntegrationTest
{
    private HttpClient _httpClient;
    public PaycheckIntegrationTests()
    {
        var webAppFactory = new WebApplicationFactory<Api.Main.Program>();
        _httpClient = webAppFactory.CreateDefaultClient();
    }

    [Fact]
    public async Task WhenAskedForPaycheck_10_Of_Employee3_ShouldReturnPaycheck()
    {
        var response = await _httpClient.GetAsync("/api/v1/paycheck/10/employee/3");
        var paycheckDto = new GetPaycheckDto
        {
            Id = "EMP-3.10",
            PaycheckNum = 10,
            TotalDeduction = 2151.79,
            GrossPay = 5508.12,
            NetPay = 3356.33,
            FirstName = "Michael",
            LastName = "Jordan",
            Salary = 143211.12,
            TotalDependents = 1,
            YearToDate = new()
            {
                TotalDeduction = 21517.95M,
                GrossPay = 55081.2M,
                NetPay = 33563.25M
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, paycheckDto);
    }

    [Fact]
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await _httpClient.GetAsync($"/api/v1/paycheck/10/employee/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task WhenAskedForInvalidPaycheckNum_ShouldReturn400()
    {
        var response = await _httpClient.GetAsync($"/api/v1/paycheck/100/employee/1");
        await response.ShouldReturn(HttpStatusCode.BadRequest);
    }
}

