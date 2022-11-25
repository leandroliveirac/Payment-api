namespace Payment_api.Application.InputModels
{
    public record SellerInputModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual IEnumerable<PhoneInputModel> Phones { get; set; }
    }
}