using System;
using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public sealed class SellerEntity : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        /*EF Relation*/
        public IEnumerable<PhoneEntity>? Phones { get; set; }

        private SellerEntity()
        {            
        }

        public SellerEntity(string name, string email)
        {
            Validate(name, email);
            Name = name;
            Email = email;
        }

        // private SellerEntity(string name, string email, IEnumerable<PhoneEntity>? phones)
        // {
        //     Name = name;
        //     Email = email;
        //     Phones = phones;
        // }

        public void Update(string name, string email)
        {
            Validate(name,email);
            Name = name;
            Email = email;
        }

        private void Validate(string name, string email)
        {
            
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters");
            DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Invalid e-mail. E-mail is required");            
        }  
    }
}