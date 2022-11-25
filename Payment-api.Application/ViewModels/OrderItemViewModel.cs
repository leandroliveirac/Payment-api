namespace Payment_api.Application.ViewModels
{
    public record OrderItemViewModel
    {
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
    }
}