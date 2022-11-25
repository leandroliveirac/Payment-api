using Payment_api.Domain.Enums;

namespace Payment_api.Application.InputModels
{
    public record OrderInputModel
    {
        public IEnumerable<OrderItemInputModel> Items { get; set; }
    }
}