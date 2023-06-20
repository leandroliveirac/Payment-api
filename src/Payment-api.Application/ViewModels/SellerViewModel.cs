namespace Payment_api.Application.ViewModels
{
    public class SellerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public IEnumerable<PhoneViewModel> Phones { get; set; }
    }
}