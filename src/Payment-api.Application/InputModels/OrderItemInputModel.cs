namespace Payment_api.Application.InputModels
{
    public class OrderItemInputModel
    {
       
        public Guid OrderId { get; set; }
         public Guid ProductId { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}