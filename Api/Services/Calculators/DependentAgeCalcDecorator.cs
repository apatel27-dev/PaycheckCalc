using Api.Models;

namespace Api.Services;

public class DependentAgeCalcDecorator : PaycheckCalcDecorator
{
    // Note: This could be stored in config JSON or DB
    private readonly int _ageLimit = 50;
    private readonly decimal _addOnFee = 200;

    public DependentAgeCalcDecorator(IPaycheckCalc paycheckCalc) : base(paycheckCalc)
    {

    }

    public override double CalculateCost(Employee employee, int checksPerYear)
    {
        var dependentsOverAgeLimit = employee.Dependents.Where((d) => getAge(d.DateOfBirth) >= _ageLimit).ToList().Count;
        var yearlyCost = dependentsOverAgeLimit * _addOnFee * 12;
        double addOnCost = (double)(yearlyCost / checksPerYear);

        return base.CalculateCost(employee, checksPerYear) + addOnCost;
    }

    public override string GetDesc()
    {
        return base.GetDesc() + " DependentAgeCalcDeco";
    }

    private int getAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth.Date > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
