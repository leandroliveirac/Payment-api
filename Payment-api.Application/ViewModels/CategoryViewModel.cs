namespace Payment_api.Application.ViewModels
{
    public record CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}