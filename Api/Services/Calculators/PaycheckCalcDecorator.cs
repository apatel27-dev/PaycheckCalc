using Api.Models;

namespace Api.Services;

public abstract class PaycheckCalcDecorator : IPaycheckCalc
{
    protected IPaycheckCalc _paycheckCalc;
    public PaycheckCalcDecorator(IPaycheckCalc paycheckCalc)
    {
        _paycheckCalc = paycheckCalc;
    }

    public virtual double CalculateCost(Employee employee, int checksPerYear)
    {
        return _paycheckCalc.CalculateCost(employee, checksPerYear);
    }

    public virtual string GetDesc()
    {
        return _paycheckCalc.GetDesc();
    }
}
