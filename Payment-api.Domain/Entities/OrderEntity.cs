using Payment_api.Domain.Enums;
using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public class OrderEntity : BaseEntity
    {
       
        public DateTime Date { get; private set; }
        public OrderStatus Status { get; private set; }

         public OrderEntity()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
            Status = OrderStatus.PROCESSING;
        }
        public void Update(OrderStatus status)
        {
            Validate(status);
            Status = status;

        }
        public void Validate(OrderStatus status)
        {
            DomainExceptionValidation.When(Status.Equals(OrderStatus.CANCELED),"Invalid transaction. Generate new order");
        }
    }
}