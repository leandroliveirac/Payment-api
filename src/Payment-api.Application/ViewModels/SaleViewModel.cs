using Payment_api.Domain.Enums;

namespace Payment_api.Application.ViewModels
{
    public class SaleViewModel
    {
        public Guid Id { get; set; }
        public DateTime Moment { get; set; }
        public SaleStatus Status { get; set; }
        public decimal Total { get => Order.Items.Sum(x => x.ProductUnitPrice * x.Quantity); }                                
        public SellerViewModel Seller { get; set; }
        public OrderViewModel Order { get; set; }

    }
}