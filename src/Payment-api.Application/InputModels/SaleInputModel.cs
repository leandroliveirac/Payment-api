namespace Payment_api.Application.InputModels
{
    public class SaleInputModel
    {
        public Guid SellerId { get; set; }
        public Guid OrderId { get; set; }
    }
}