using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Interfaces.Services;
using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Services
{

    public sealed class SellerService : ISellerService
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IPhoneRepository _phoneRepository;
        public SellerService(ISellerRepository sellerRepository, IPhoneRepository phoneRepository)
        {
            _sellerRepository = sellerRepository;
            _phoneRepository = phoneRepository;
        }

        public async Task<IEnumerable<SellerEntity>> GetAllAsync()
        {
            var sellers = await _sellerRepository.GetAllAsync();

            if(sellers == null || sellers.Count() <= 0) 
                return Enumerable.Empty<SellerEntity>();

            return sellers;
        }

        public async Task<SellerEntity> GetByIdAsync(Guid id)
        {
            return await _sellerRepository.GetByIdAsync(id);
        }

        public async Task<SellerEntity> CreateAsync(SellerEntity entity)
        {
            Validate(entity, entity.Id);
            await _sellerRepository.CreateAsync(entity);
            return entity;
        }        

        public void Remove(SellerEntity entity)
        {
            _phoneRepository.RemoveRange(entity.Phones);
            _sellerRepository.Remove(entity);
        }

        public SellerEntity Update(SellerEntity currentSeller,SellerEntity newSeller)
        {
            Validate(newSeller, currentSeller.Id);      
            currentSeller.Update(newSeller.Name, newSeller.Email,newSeller.Cpf);
            currentSeller.Phones = UpdatePhones(currentSeller.Phones,newSeller.Phones);
            _sellerRepository.Update(currentSeller);

            return currentSeller;
        }

        private void Validate(SellerEntity entity, Guid idSeller)
        {
            var seller = _sellerRepository.GetByCpf(entity.Cpf).Result;
            if(seller != null)                
                DomainExceptionValidation.When( seller.Cpf == entity.Cpf && seller.Id != idSeller,"There is seller with this CPF!");
            DomainExceptionValidation.When( _sellerRepository.ThereIsEmail(entity.Email,idSeller).Result,"There is seller with this E-mail!");
            DomainExceptionValidation.When( entity.Phones.GroupBy(p => (p.Ddd,p.Number,p.Type)).Count() != entity.Phones.Count(),"No duplicate phone allowed!");
        }

        private IEnumerable<PhoneEntity> UpdatePhones(IEnumerable<PhoneEntity> currentPhones, IEnumerable<PhoneEntity> newPhones)
        {
            var addNewPhones = new List<PhoneEntity>();
            var removePhones = new List<PhoneEntity>();
            var phones = new List<PhoneEntity>();

            newPhones.ToList().ForEach( p => {
            var phone = currentPhones.FirstOrDefault(x => x.Id == p.Id);
            if(phone == null)
            {
                var duplicatePhone = currentPhones.Any(x => x.Ddd == p.Ddd && x.Number == p.Number && x.Type == p.Type);
                if(!duplicatePhone)                 
                    addNewPhones.Add(new PhoneEntity(p.Ddd,p.Number,p.Type,currentPhones.First().Id));
                else
                    DomainExceptionValidation.When(duplicatePhone,$"No duplicate phone allowed!");
            }            
            });

            currentPhones.ToList().ForEach( p => {
            var phone = newPhones.FirstOrDefault(x => x.Id == p.Id);                
            if(phone != null)
            {
                p.Update(phone.Ddd,phone.Number,phone.Type);
            }
            else
            {
                removePhones.Add(p);
            }              
            });           

            phones.AddRange(currentPhones.Except(removePhones));
            phones.AddRange(addNewPhones);

            if(removePhones.Count > 0)
                _phoneRepository.RemoveRange(removePhones);

            return phones;
        }
    }
}