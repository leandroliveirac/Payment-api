using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public class CategoryEntity : BaseEntity
    {

        public string Description { get; private set; }

         public CategoryEntity(string description)
        {
            Validate(description);
            Id = Guid.NewGuid();
            Description = description;
        }

        public void Update(string description)
        {
            Validate(description);
            Description = description;
        }

        public void Validate(string description)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(description),"Invalid description. Description is required");
        }       
    }
}