namespace Payment_api.Application.InputModels
{
    public class ProductInputModel
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public bool  Active { get; set; }
    }
}