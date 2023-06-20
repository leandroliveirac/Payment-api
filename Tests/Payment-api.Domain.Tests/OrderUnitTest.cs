using FluentAssertions;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Enums;
using Payment_api.Domain.Validation;
using Xunit;

namespace Payment_api.Domain.Tests
{
    public class OrderUnitTest
    {

        [Fact]
        public void InsertOrder_WithValidParameters_ResultNotThrowException()
        {
            Action action = () => new OrderEntity();

            action.Should()
                    .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void UpdateStatusOrder_WithValidParameters_ResultNotThowException()
        {
            OrderEntity order = new OrderEntity();

            Action action = () => order.UpdateStatus(OrderStatus.SENT);

            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void UpdateStatusOrder_Transaction_Invalid_Update_Change_Status_Canceled_ResultThrowException()
        {
            OrderEntity order = new OrderEntity();
            
            order.UpdateStatus(OrderStatus.CANCELED);

            Action action = () => order.UpdateStatus(OrderStatus.SENT);

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid transaction. Generate new order");
            
        }

        [Fact]
        public void UpdateStatusOrder_withValidSequenceProcessingToShippedOrCanceled_ResultNotThrowException()
        {
            OrderEntity order = new OrderEntity();

            Action action = () => order.UpdateStatus(OrderStatus.CANCELED);

            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void UpdateStatusOrder_withValidSequenceSentToDeliveredOrCanceled_ResultNotThrowException()
        {
            OrderEntity order = new OrderEntity();

            order.UpdateStatus(OrderStatus.SENT);

            Action action = () => order.UpdateStatus(OrderStatus.DELIVERED);

            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void UpdateStatusOrder_withValidSequenceDeliveredToReturned_ResultNotThrowException()
        {
            OrderEntity order = new OrderEntity();

            order.UpdateStatus(OrderStatus.SENT);
            order.UpdateStatus(OrderStatus.DELIVERED);

            Action action = () => order.UpdateStatus(OrderStatus.RETURNED);

            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void UpdateStatusOrder_withValidSequenceReturnedToSentOrCanceled_ResultNotThrowException()
        {
            OrderEntity order = new OrderEntity();

            order.UpdateStatus(OrderStatus.SENT);
            order.UpdateStatus(OrderStatus.DELIVERED);
            order.UpdateStatus(OrderStatus.RETURNED);

            Action action = () => order.UpdateStatus(OrderStatus.CANCELED);

            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }
    }
}
