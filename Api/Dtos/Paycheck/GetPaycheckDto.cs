using Api.Dtos.Employee;

namespace Api.Dtos.Paycheck;

public class GetPaycheckDto
{
    public string? Id { get; set; }
    public int PaycheckNum { get; set; }
    public double? TotalDeduction { get; set; }
    public double? GrossPay { get; set; }
    public double? NetPay { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public double? Salary { get; set; }
    public YearToDateDto? YearToDate { get; set; }
    public int TotalDependents { get; set; }
}
