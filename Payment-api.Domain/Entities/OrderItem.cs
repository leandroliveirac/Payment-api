using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int Quantity { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid OrderId { get; private set; }
        public virtual ProductEntity? Product { get; set; }
        public virtual OrderEntity? Order { get; set; }

        public OrderItem(int quantity, Guid productId, Guid orderId)
        {
            Id = Guid.NewGuid();
            Validate(quantity);
            Quantity = quantity;
            ProductId = productId;
            OrderId = orderId;
        }
        public void Update(int quantity)
        {
            Validate(quantity);
            Quantity = quantity;
        }

        private void Validate(int quantity)
        {
            DomainExceptionValidation.When(quantity < 1,"Invalid quantity, minimum 1 quantity");
        }
    }
}