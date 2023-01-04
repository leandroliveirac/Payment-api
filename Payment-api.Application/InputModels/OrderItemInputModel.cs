namespace Payment_api.Application.InputModels
{
    public class OrderItemInputModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}