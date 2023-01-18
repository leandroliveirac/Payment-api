using System;
using System.Text.RegularExpressions;
using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public sealed class SellerEntity : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Cpf { get; private set; } = string.Empty;

        /* Navigation property EF */
        public IEnumerable<PhoneEntity> Phones { get; set; } = new List<PhoneEntity>();

        public SellerEntity(string name, string email, string cpf)
        {
            Validate(name, email,cpf);
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public void Update(string name, string email,string cpf)
        {
            Validate(name,email,cpf);
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        private void Validate(string name, string email, string cpf)
        {
            
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters");
            DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Invalid e-mail. E-mail is required");            
            DomainExceptionValidation.When(string.IsNullOrEmpty(cpf), "Invalid CPF. CPF is required");

            var _cpf = Regex.Match(cpf,"^[0-9]+$").ToString();
            DomainExceptionValidation.When(_cpf.Length != 11,"Invalid CPF, enter 11 digits");
        }  
    }
}