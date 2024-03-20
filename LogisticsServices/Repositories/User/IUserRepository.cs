using LogisticsServices.Models;

namespace LogisticsServices.Repositories.User
{
    public interface IUserRepository
    {
        public Task<(bool success, string message)> AddCustomerDetails(Customer customer);
        public Task<(bool success, string message)> AddCarrierRegistrationDetails(Carrier carrier);
        public Task<(bool success, string message)> AddCarrierRepRegistrationDetails(RegistrationDTO carrierRep);
        public Task<(bool success, string message)> AddCustomerRepRegistrationDetails(RegistrationDTO customerRep);
        public Task<(bool success, string message)> CheckLoginDetails(loginDTO loginDetails);
        public string HashedPassword(string password);
        public bool VerifyPassword(string password, string hashPassword);
    }
}
