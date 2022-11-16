using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public ProductEntity(string description, decimal price)
        {
            Validate(description, price);
            Id = Guid.NewGuid();
            Description = description;
            Price = price;
        }     

        public void Update(string description, decimal price)
        {
            Description = description;
            Price = price;
        }

        private void Validate(string description, decimal price)
        {
            DomainExceptionValidation.When(price < 0.00m,"Invalid price value.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(description),"Invalid description. Description is required");
        }


    }
}