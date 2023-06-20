namespace Payment_api.Application.ViewModels
{
    public class OrderItemViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}