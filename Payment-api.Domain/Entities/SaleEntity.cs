using Payment_api.Domain.Enums;
using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public class SaleEntity : BaseEntity
    {      

        public DateTime Moment { get; private set; }
        public SaleStatus Status { get; private set; }
        public Guid SellerId { get; private set; }
        public Guid OrderId { get; private set; }

        public virtual OrderEntity? Order { get; set; }
        public virtual SellerEntity? Seller { get; set; }

        public SaleEntity(Guid sellerId, Guid orderId)
        {
            Validate(sellerId, orderId);
            Moment = DateTime.Now;
            Status = SaleStatus.AWAITING_PAYMENT;
            SellerId = sellerId;
            OrderId = orderId;
        }        

        public void UpdateStatus(SaleStatus status)
        {
            switch (Status)
            {
                case SaleStatus.CANCELED:
                    DomainExceptionValidation.When(status != SaleStatus.CANCELED,"Invalid transaction. Generate new sale");
                    break;
                case SaleStatus.AWAITING_PAYMENT:
                    DomainExceptionValidation.When(status != SaleStatus.PAYMENT_ACCEPT || status != SaleStatus.CANCELED,
                                "Invalid transaction, update : awaiting payment for approved payment or awaiting payment for canceled sale");
                    break;
                case SaleStatus.PAYMENT_ACCEPT:
                    DomainExceptionValidation.When(status != SaleStatus.SENT_CARRIER || status != SaleStatus.CANCELED,
                                "Invalid transaction, update: approved payment for Shipped to Carrier or approved payment for canceled sale");
                    break;
                case SaleStatus.SENT_CARRIER:
                    DomainExceptionValidation.When(status != SaleStatus.DELIVERED,
                                "Invalid transaction, Update: Shipped to Carrier for Delivered");
                    break;
                default:
                    break;
            }
                      
            Status = status;
        }

        private void Validate(Guid sellerId, Guid orderId)
        {            
            DomainExceptionValidation.When(sellerId == Guid.Empty, "Invalid sellerId value");
            DomainExceptionValidation.When(orderId == Guid.Empty, "Invalid order value");            
        }

    }
}