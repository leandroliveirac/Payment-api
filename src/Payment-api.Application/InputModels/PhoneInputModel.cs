using Payment_api.Domain.Enums;

namespace Payment_api.Application.InputModels
{
    public class PhoneInputModel
    {
        public Guid Id { get; set; }
        public string Ddd { get; set; }
        public string Number { get; set; }
        public PhoneType Type { get; set; }
        public Guid SellerId { get; set; }
    }
}