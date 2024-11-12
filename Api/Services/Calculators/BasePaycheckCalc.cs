using Api.Models;

namespace Api.Services;

public class BasePaycheckCalc : IPaycheckCalc
{
    private readonly double _costPerMonth = 1000;

    public string GetDesc() => "BasePaycheckCalc";

    public double CalculateCost(Employee employee, int checksPerYear)
    {
        var costPerYear = _costPerMonth * 12;
        var costPerCheck = costPerYear / checksPerYear;

        return costPerCheck;
    }
}
