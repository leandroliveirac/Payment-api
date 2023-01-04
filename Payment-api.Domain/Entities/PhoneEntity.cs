using System.Text.RegularExpressions;
using Payment_api.Domain.Enums;
using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Entities
{
    public sealed class PhoneEntity : BaseEntity
    {      

        public string Ddd { get; private set; } = string.Empty;
        public string Number { get; private set; } = string.Empty;
        public PhoneType Type { get; private set; }
        public Guid SellerId { get; private set; }

        /* EF Relation*/
        public SellerEntity? SellerEntity { get; }

        private PhoneEntity()
        {            
        }

        public PhoneEntity(string ddd, string number, PhoneType type, Guid sellerId)
        {
            Validate(ddd, number,sellerId,type);
            Ddd = ddd;
            Number = number;
            Type = type;
            SellerId = sellerId;
        }

        public void Update(string ddd, string number, PhoneType type)
        {
            Validate(ddd, number, SellerId,type);
            Ddd = ddd;
            Number = number;
            Type = type;
        }

        private void Validate(string ddd, string number, Guid sellerId, PhoneType type)
        {
            DomainExceptionValidation.When(!Enum.IsDefined(typeof(PhoneType),type),"Invalid phone type. phone type is required");
            DomainExceptionValidation.When(string.IsNullOrEmpty(ddd),"Invalid ddd. DDD is required");
            DomainExceptionValidation.When(string.IsNullOrEmpty(number),"Invalid number. Number is required");
            DomainExceptionValidation.When(sellerId == Guid.Empty, "Invalid sellerId. Seller is required");

            var _number = Regex.Match(number,"^[0-9]+$").ToString();
            DomainExceptionValidation.When(_number.Length != 8,"Invalid phone, enter 8 digits");

            var _ddd = Regex.Match(ddd,"^[0-9]+$").ToString();
            DomainExceptionValidation.When(_ddd.Length != 2,"Invalid DDD, enter 2 digits");   
        }
        
    }
}