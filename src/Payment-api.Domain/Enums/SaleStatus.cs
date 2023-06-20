namespace Payment_api.Domain.Enums
{
    public enum SaleStatus
    {        
        AWAITING_PAYMENT = 1,        
        PAYMENT_ACCEPT,
        SENT_CARRIER,
        DELIVERED,
        CANCELED,
    }
}