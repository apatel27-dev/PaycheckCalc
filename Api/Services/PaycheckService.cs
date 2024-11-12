using System.Data;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Repositories;

namespace Api.Services;

public class PaycheckService(IMockEmployeeRepo employeeRepo) : IPaycheckService
{
    // This could be config/db based
    private const int _paychecksPerYear = 26;
    private readonly IMockEmployeeRepo _employeeRepo = employeeRepo;

    public async Task<GetPaycheckDto> ComputePaycheck(int empId, int paycheckNum)
    {
        // Do validation
        if (paycheckNum <= 0 || paycheckNum > _paychecksPerYear)
        {
            throw new BadHttpRequestException("Invalid paycheck number.");
        }

        var employee = await _employeeRepo.FindByIdAsync(empId) ?? throw new NoNullAllowedException("Employee not found");

        // We have a valid request and found the employee so perform calculations
        // This uses a decorator design pattern where we build upon each pay check calculation
        IPaycheckCalc paycheckCalc = new BasePaycheckCalc();
        paycheckCalc.CalculateCost(employee, _paychecksPerYear);

        paycheckCalc = new HighIncomeCalcDecorator(paycheckCalc);
        paycheckCalc.CalculateCost(employee, _paychecksPerYear);

        paycheckCalc = new DependentAgeCalcDecorator(paycheckCalc);
        paycheckCalc.CalculateCost(employee, _paychecksPerYear);

        paycheckCalc = new DependentsCalcDecorator(paycheckCalc);
        double totalDeductions = paycheckCalc.CalculateCost(employee, _paychecksPerYear);

        var paycheckDto = createPaycheckDto(employee, totalDeductions, paycheckNum);

        return paycheckDto;
    }

    private GetPaycheckDto createPaycheckDto(Employee employee, double totalDeductions, int paycheckNum)
    {
        var grossPay = (double)employee.Salary / _paychecksPerYear;
        var netPay = grossPay - totalDeductions;

        var ytdGrossPay = (decimal)Math.Round(grossPay * paycheckNum, 2);
        var ytdTotalDeductions = (decimal)Math.Round(totalDeductions * paycheckNum, 2);
        var ytdNetPay = ytdGrossPay - ytdTotalDeductions;

        var paycheckDto = new GetPaycheckDto
        {
            Id = $"EMP-{employee.Id}.{paycheckNum}",
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Salary = (double?)Math.Round(employee.Salary, 2),
            PaycheckNum = paycheckNum,
            TotalDependents = employee.Dependents.Count,
            TotalDeduction = Math.Round(totalDeductions, 2),
            GrossPay = Math.Round(grossPay, 2),
            NetPay = Math.Round(netPay, 2),
            YearToDate = new()
            {
                GrossPay = ytdGrossPay,
                TotalDeduction = ytdTotalDeductions,
                NetPay = ytdNetPay
            }
        };

        return paycheckDto;
    }
}




