using FluentAssertions;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Validation;
using Xunit;

namespace Payment_api.Domain.Tests
{
    public class SellerUnitTest
    {
        [Fact]
        public void InsertSeller_WthValidParameters_ResultNotThrowException()
        {
 
            Action action = () => new SellerEntity("Joao", "joao@test.com");

            action.Should()
                    .NotThrow<DomainExceptionValidation>();
        }
    }
}