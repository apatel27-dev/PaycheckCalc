using Api.Dtos.Paycheck;

namespace Api.Services;

public interface IPaycheckService
{
    public Task<GetPaycheckDto> ComputePaycheck(int empId, int paycheckNum);
}




