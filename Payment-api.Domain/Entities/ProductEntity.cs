using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Guid CategoryId { get; private set; }
        public virtual CategoryEntity? Category { get; set; }
        public virtual IEnumerable<OrderItemEntity>? OrderItems { get; set; }

        public ProductEntity(string description, decimal price, Guid categoryId)
        {
            Validate(description, price, categoryId);
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }

        public void Update(string description, decimal price, Guid categoryId)
        {
            Validate(description.Trim(), price, categoryId);
            Description = description.Trim();
            Price = price;
            CategoryId = categoryId;
        }

        private void Validate(string description, decimal price, Guid categoryId)
        {
            DomainExceptionValidation.When(price < 0.00m,"Invalid price value.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(description),"Invalid description. Description is required");
            DomainExceptionValidation.When(categoryId == Guid.Empty,"Invalid categoryId. CategoryId is required");
        }
    }
}