using LogisticsServices.Models;

namespace LogisticsServices.Repositories.User
{
    public interface IUserRepository
    {
        public bool AddCustomerDetails(Customer customer);
        public bool AddCarrierRegistrationDetails(Carrier carrier);
        public bool AddCarrierRepRegistrationDetails(CarrierRep carrierRep);
        public bool AddCustomerRepRegistrationDetails(CustomerRep customerRep);

        }
}
