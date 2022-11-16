using System.Text.RegularExpressions;
using Payment_api.Domain.Enums;
using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public class PhoneEntity : BaseEntity
    {      

        public string Ddd { get; private set; }
        public string Number { get; private set; }
        public PhoneType Type { get; private set; }

        public PhoneEntity(string ddd, string number, PhoneType type)
        {
            Id = Guid.NewGuid();
            Ddd = ddd;
            Number = number;
            Type = type;
        }

        public PhoneEntity(Guid id, string ddd, string number, PhoneType type)
        {
            Id = id;
            Ddd = ddd;
            Number = number;
            Type = type;
        }
        
        public void Validate(string ddd, string number)
        {

            DomainExceptionValidation.When(string.IsNullOrEmpty(ddd),"Invalid ddd. DDD is required");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Number),"Invalid number. Number is required");

            var _number = Regex.Match(number,"^[0-9]+$").ToString();
            DomainExceptionValidation.When(_number.Length < 8,"Invalid phone,phone must have at least 8 digits");

            var _ddd = Regex.Match(ddd,"^[0-9]+$").ToString();
            DomainExceptionValidation.When(_ddd.Length < 2,"Invalid ddd, enter at least 2 digits");   
        }
        
    }
}