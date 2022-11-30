using Payment_api.Application.ViewModels;
using Payment_api.Domain.Enums;

namespace Payment_api.Application.InputModels
{
    public record SaleInputModel
    {
        public Guid SellerId { get; set; }
        public Guid OrderId { get; set; }
    }
}