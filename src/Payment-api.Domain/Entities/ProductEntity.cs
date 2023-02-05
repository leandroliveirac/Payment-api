using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public sealed class ProductEntity : BaseEntity
    {
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Guid CategoryId { get; private set; }
        public bool Active { get; private set; }
        /* Navigation property EF */
        public CategoryEntity Category { get; set; }
        public IEnumerable<OrderItemEntity> OrderItems { get; set; }

        public ProductEntity(string description, decimal price, Guid categoryId, bool active)
        {
            Validate(description, price, categoryId);
            Description = description;
            Price = price;
            CategoryId = categoryId;
            Active = active;
        }

        public void Update(string description, decimal price, Guid categoryId,bool active)
        {
            Validate(description.Trim(), price, categoryId);
            Description = description.Trim();
            Price = price;
            CategoryId = categoryId;
            Active = active;
        }

        public void Inactivate()
        {
            this.Active = false;
        }

        private void Validate(string description, decimal price, Guid categoryId)
        {
            DomainExceptionValidation.When(price < 0.00m,"Invalid price value.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(description),"Invalid description. Description is required");
            DomainExceptionValidation.When(categoryId == Guid.Empty,"Invalid categoryId. CategoryId is required");
        }
    }
}