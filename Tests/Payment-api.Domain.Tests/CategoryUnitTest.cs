using FluentAssertions;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Validation;
using Xunit;

namespace Payment_api.Domain.Tests
{
    public class CategoryUnitTest
    {
        [Fact(DisplayName = "Insert category with valid state")]
        public void InsertCategory_WithValidParameters_ResultNotThrowException()
        {
            Action action = () => new CategoryEntity("Category name");
            
            action.Should()
                    .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Insert category with invalid state, throw exception")]
        public void InsertCategory_WithInvalidParameters_ResultThrowException()
        {

            Action action = () => new CategoryEntity(string.Empty);

            action.Should()
                    .Throw<DomainExceptionValidation>()
                    .WithMessage("Invalid description. Description is required");
        }

        [Fact(DisplayName ="Update category with valid state")]
        public void UpdateCategory_WithValidParameters_ResultNotThrowException()
        {
            var category = new CategoryEntity("Category name");
            Action action = () => category.Update("Update category name");

            action.Should()
                    .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName ="Upadate category white invalid state, throw exception")]
        public void UpdateCategory_WithInvalidParemeters_ResultThrowException()
        {
            var category = new CategoryEntity("Category name");
            Action action = () => category.Update(string.Empty);

            action.Should()
                    .Throw<DomainExceptionValidation>()
                    .WithMessage("Invalid description. Description is required");
        }
    }
}
