using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public sealed class CategoryEntity : BaseEntity
    {
        public string Description { get; private set; }

        public CategoryEntity(string description)
        {
            Validate(description);
            Description = description;
        }

        public void Update(string description)
        {
            Validate(description.Trim());
            Description = description.Trim();
        }

        private void Validate(string description)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(description),"Invalid description. Description is required");
        }       
    }
}