namespace Payment_api.Application.DTOs
{
    public record CategoryDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
