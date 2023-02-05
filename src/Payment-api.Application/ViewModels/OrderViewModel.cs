using Payment_api.Domain.Enums;

namespace Payment_api.Application.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public IEnumerable<OrderItemViewModel> Items { get; set; }
    }
}