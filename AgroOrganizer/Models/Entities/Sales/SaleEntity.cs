using AgroOrganizer.Models.Dtos.SalesDto;
using AgroOrganizer.Models.Dtos.UserDto;
using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Entities.Sales;

public class SaleEntity
{
    public int Id { get; private set; }
    public DateTimeOffset DateSigned { get; private set; }
    public CropTypes CropType { get; private set; }
    public decimal PriceForKg { get; private set; }
    public double Quantity { get; private set; }
    public decimal TotalPrice => PriceForKg *  (decimal)Quantity;
    public string BuyerName { get; private set; }

    public SaleEntity()
    {
        
    }

    public SaleEntity(int saleId, DateTimeOffset dateSigned, CropTypes cropType, decimal priceForKg,
        double quantity, string buyerName)
    {
        Id = saleId;
        DateSigned = dateSigned;
        CropType = cropType;
        PriceForKg = priceForKg;
        Quantity = quantity;
        BuyerName = buyerName;
    }

    public void Update(UpdateSalesRequestDto dto)
    {
        DateSigned =  dto.DateSigned;
        PriceForKg = dto.PriceForKg;
        Quantity = dto.Quantity;
        BuyerName = dto.BuyerName;
    }
}