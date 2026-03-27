using AgroOrganizer.Models.Dtos.SalesDto;
using AgroOrganizer.Models.Enums.CropTypes;
using AgroOrganizer.Models.Entities.FieldSeason;

namespace AgroOrganizer.Models.Entities.Sales;

public class SaleEntity
{
    public int Id { get; private set; }
    public DateTimeOffset DateSigned { get; private set; }
    public CropTypes CropType { get; private set; }
    public decimal PriceForKg { get; private set; }
    public double Quantity { get; private set; }
    public decimal TotalPrice => PriceForKg * (decimal)Quantity;
    public string BuyerName { get; private set; }

    public int FieldSeasonId { get; private set; }
    public FieldSeasonEntity FieldSeason { get; private set; }

    public SaleEntity() { }

    public SaleEntity(int id, DateTimeOffset dateSigned, CropTypes cropType, decimal priceForKg, double quantity, string buyerName, FieldSeasonEntity fieldSeason)
    {
        Id = id;
        DateSigned = dateSigned;
        CropType = cropType;
        PriceForKg = priceForKg;
        Quantity = quantity;
        BuyerName = buyerName;
        FieldSeason = fieldSeason;
        FieldSeasonId = fieldSeason.Id;
    }

    public void Update(UpdateSalesRequestDto dto)
    {
        DateSigned = dto.DateSigned;
        PriceForKg = dto.PriceForKg;
        Quantity = dto.Quantity;
        BuyerName = dto.BuyerName;
    }
}