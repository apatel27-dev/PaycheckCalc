namespace Api.Dtos.Paycheck;

public class YearToDateDto
{
    public decimal TotalDeduction { get; set; }
    public decimal GrossPay { get; set; }
    public decimal NetPay { get; set; }
}
