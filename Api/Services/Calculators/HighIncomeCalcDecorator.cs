using Api.Models;

namespace Api.Services;

public class HighIncomeCalcDecorator : PaycheckCalcDecorator
{
    // Note: This could be stored in config JSON or DB
    private readonly int _salaryThreshold = 80000;
    private readonly decimal _feePct = .02M;

    public HighIncomeCalcDecorator(IPaycheckCalc paycheckCalc) : base(paycheckCalc)
    {

    }

    public override string GetDesc()
    {
        return base.GetDesc() + " HighIncome Deco";
    }

    public override double CalculateCost(Employee employee, int checksPerYear)
    {
        double addOnCost = 0;

        if (employee.Salary >= _salaryThreshold)
        {
            var yearlyCost = employee.Salary * _feePct * 12;
            addOnCost = (double)(yearlyCost / checksPerYear);
        }

        return base.CalculateCost(employee, checksPerYear) + addOnCost;
    }
}
