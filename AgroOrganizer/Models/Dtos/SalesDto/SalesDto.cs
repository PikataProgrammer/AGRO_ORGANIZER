using AgroOrganizer.Models.Entities.Sales;
using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Dtos.SalesDto;

public class SalesDto
{
    public int SaleId { get; set; }
    public DateTimeOffset DateSigned { get;  set; }
    public CropTypes CropType { get;  set; }
    public decimal PriceForKg { get;  set; }
    public double Quantity { get;  set; }
    public decimal TotalPrice => PriceForKg *  (decimal)Quantity;
    public string BuyerName { get;  set; }
    public int FieldSeasonId { get; set; }

    public SalesDto(SaleEntity entity)
    {
        SaleId = entity.Id;
        DateSigned = entity.DateSigned;
        CropType = entity.CropType;
        PriceForKg = entity.PriceForKg;
        Quantity = entity.Quantity;
        BuyerName = entity.BuyerName;
        FieldSeasonId = entity.FieldSeasonId;
    }
}