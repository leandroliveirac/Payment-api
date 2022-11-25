using Payment_api.Application.ViewModels;
using Payment_api.Domain.Enums;

namespace Payment_api.Application.InputModels
{
    public record SaleInputModel
    {
        public SaleStatus Status { get; set; }
        public SellerViewModel Seller { get; set; }
        public OrderViewModel Order { get; set; }
    }
}