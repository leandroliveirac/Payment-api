using Payment_api.Domain.Enums;
using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public class OrderEntity : BaseEntity
    {
       
        public DateTime Date { get; private set; }
        public OrderStatus Status { get; private set; }

        public IEnumerable<OrderItemEntity>? Items { get; set; }

        public OrderEntity()
        {
            Date = DateTime.Now;
            Status = OrderStatus.PROCESSING;
        }
        public void UpdateStatus(OrderStatus status)
        {

            switch (Status)
            {
                case OrderStatus.PROCESSING:
                    DomainExceptionValidation.When(status != OrderStatus.PROCESSING && status != OrderStatus.AWAITING_PAYMENT && status != OrderStatus.CANCELED, "Invalid transaction, valid sequence AWAITING PAYMENT or CANCELED");
                    break;
                case OrderStatus.AWAITING_PAYMENT:
                    DomainExceptionValidation.When(status != OrderStatus.AWAITING_PAYMENT && status != OrderStatus.PAYMENT_ACCEPT && status != OrderStatus.CANCELED,"Invalid transaction, valid sequence PAYMENT_ACCEPT or CANCELED");
                    break;
                case OrderStatus.PAYMENT_ACCEPT:
                    DomainExceptionValidation.When(status != OrderStatus.PAYMENT_ACCEPT && status != OrderStatus.SENT && status != OrderStatus.CANCELED,"Invalid transaction, valid sequence SENT or CANCELED");
                    break;                 
                case OrderStatus.SENT:
                    DomainExceptionValidation.When(status != OrderStatus.SENT && status != OrderStatus.DELIVERED && status != OrderStatus.CANCELED, "Invalid transaction, valid sequence DELIVERED or CANCELED");
                    break;
                case OrderStatus.DELIVERED:
                    DomainExceptionValidation.When(status != OrderStatus.DELIVERED && status != OrderStatus.RETURNED, "Invalid transaction, valid sequence RETURNED");
                    break;
                case OrderStatus.CANCELED:
                    DomainExceptionValidation.When(status != OrderStatus.CANCELED, "Invalid transaction. Generate new order");
                    break;
                case OrderStatus.RETURNED:
                    DomainExceptionValidation.When(status != OrderStatus.RETURNED && status != OrderStatus.SENT && status != OrderStatus.CANCELED, "Invalid transaction. valid sequence SENT or CANCELED.");
                    break;
                default:
                    break;
            }

            Status = status;
        }
    }
}