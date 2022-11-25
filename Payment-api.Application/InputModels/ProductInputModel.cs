namespace Payment_api.Application.InputModels
{
    public record ProductInputModel
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}