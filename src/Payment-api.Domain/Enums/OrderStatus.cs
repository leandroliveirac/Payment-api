namespace Payment_api.Domain.Enums
{
    public enum OrderStatus
    {
        PROCESSING = 1,
        SENT,
        DELIVERED,
        CANCELED,
        RETURNED,
        AWAITING_PAYMENT,        
        PAYMENT_ACCEPT,

    }
}