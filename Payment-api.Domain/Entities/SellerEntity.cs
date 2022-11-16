using System;
using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public class SellerEntity : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Guid TelephoneId { get; private set; }

        public virtual PhoneEntity? Telephone { get; set; }

        public SellerEntity(string name, string email, Guid telephoneId)
        {
            Id = Guid.NewGuid();
            Validate(name, email);
            Name = name;
            Email = email;
            TelephoneId = telephoneId;
        }

        public SellerEntity(Guid id, string name, string email, string telephone, Guid telephoneId)
        {
            Id = id;
            Validate(name, email);
            Name = name;
            Email = email;
            TelephoneId = telephoneId;
        }
        public void Update(string name, string email, Guid telephoneId)
        {
            Validate(name,email);
            Name = name;
            Email = email;
            TelephoneId = telephoneId;
        }

        private void Validate(string name, string email)
        {
            
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters");
            DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Invalid e-mail. E-mail is required");
        }  
    }
}