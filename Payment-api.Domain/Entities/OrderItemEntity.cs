using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public sealed class OrderItemEntity : BaseEntity
    {       
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; } 
        public string ProductDescription { get; private set; }
        public decimal ProductUnitPrice { get; private set; }
        public int Quantity { get; private set; }

        /* Navigation property EF */
        public ProductEntity Product { get; set; }
        public OrderEntity Order { get; set; }
        
        public OrderItemEntity(Guid orderId, Guid productId, string productDescription, decimal productUnitPrice, int quantity)
        {
            Validate(quantity);
            OrderId = orderId;
            ProductId = productId;
            ProductDescription = productDescription;
            ProductUnitPrice = productUnitPrice;
            Quantity = quantity;
        }
        public void Update(Guid orderId, Guid productId, string productDescription, decimal productUnitPrice, int quantity)
        {
            Validate(quantity);
            OrderId = orderId;
            ProductId = productId;
            ProductDescription = productDescription;
            ProductUnitPrice = productUnitPrice;
            Quantity = quantity;
        }

        private void Validate(int quantity)
        {
            DomainExceptionValidation.When(quantity < 1,"Invalid quantity, minimum 1 quantity");
        }
    }
}