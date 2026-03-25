using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Dtos.SalesDto;

public class UpdateSalesRequestDto
{
    public DateTimeOffset DateSigned { get;  set; }
    public CropTypes CropType { get;  set; }
    public decimal PriceForKg { get;  set; }
    public double Quantity { get;  set; }
    public string BuyerName { get;  set; }
}