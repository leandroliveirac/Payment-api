using Payment_api.Domain.Enums;

namespace Payment_api.Application.InputModels
{
    public record PhoneInputModel
    {
        public string Ddd { get; set; }
        public string Number { get; set; }
        public PhoneType Type { get; set; }
    }
}