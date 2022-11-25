using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public abstract class BaseEntity
    {
        private Guid _id;
        public Guid Id 
        {   
            get => _id; 
            protected set
            {                
                DomainExceptionValidation.When(value.Equals(Guid.Empty), "Invalid id value");
                _id = value;              
            }
        }
    }
}