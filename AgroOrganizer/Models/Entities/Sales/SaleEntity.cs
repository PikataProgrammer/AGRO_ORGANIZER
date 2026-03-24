using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Entities.Sales;

public class SaleEntity
{
    public int SaleId { get; private set; }
    public DateTimeOffset DateSigned { get; private set; }
    public CropTypes CropType { get; private set; }
    public decimal PriceForKg { get; private set; }
    public double Quantity { get; private set; }
    public decimal TotalPrice { get; private set; }
    public string BuyerName { get; private set; }

    public SaleEntity()
    {
        
    }

    public SaleEntity(int saleId, DateTimeOffset dateSigned, CropTypes cropType, decimal priceForKg,
        double quantity, decimal totalPrice,  string buyerName)
    {
        SaleId = saleId;
        DateSigned = dateSigned;
        CropType = cropType;
        PriceForKg = priceForKg;
        Quantity = quantity;
        TotalPrice = totalPrice;
        BuyerName = buyerName;
    }
}