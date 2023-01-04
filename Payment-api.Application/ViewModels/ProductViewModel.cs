namespace Payment_api.Application.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}