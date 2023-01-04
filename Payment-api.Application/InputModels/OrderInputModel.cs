namespace Payment_api.Application.InputModels
{
    public class OrderInputModel
    {
        public IEnumerable<OrderItemInputModel> Items { get; set; }
    }
}