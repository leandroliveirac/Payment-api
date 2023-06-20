namespace Payment_api.Application.InputModels
{
    public class SellerInputModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public IEnumerable<PhoneInputModel> Phones { get; set; }
    }
}