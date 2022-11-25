namespace Payment_api.Application.InputModels
{
    public record OrderItemInputModel
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
    }
}