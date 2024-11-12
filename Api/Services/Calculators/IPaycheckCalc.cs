using Api.Models;

namespace Api.Services;

public interface IPaycheckCalc
{
    string GetDesc();
    double CalculateCost(Employee employee, int checksPerYear);
}
