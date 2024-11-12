using Api.Models;

namespace Api.Services;

public class DependentsCalcDecorator : PaycheckCalcDecorator
{
    private readonly int _costPerDependent = 600;

    public DependentsCalcDecorator(IPaycheckCalc paycheckCalc) : base(paycheckCalc)
    {

    }

    public override string GetDesc()
    {
        return base.GetDesc() + " DependendsPaycheckCalcDecorator";
    }

    public override double CalculateCost(Employee employee, int checksPerYear)
    {
        var dependentCount = employee.Dependents.Count;
        var yearlyCost = _costPerDependent * dependentCount * 12;
        var addOnCost = yearlyCost / checksPerYear;

        return base.CalculateCost(employee, checksPerYear) + addOnCost;
    }
}
