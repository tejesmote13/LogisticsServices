using LogisticsServices.Models;
using LogisticsServices.Repositories.User;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private readonly UserRepository _userRepository;
        public RegistrationController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("addCustomerDetails")]
        public bool AddCustomerDetails( Customer customer)
        {
            bool status=false;
            try
            {
                status = _userRepository.AddCustomerDetails(customer);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        [HttpPost]
        [Route("addCarrierRegistrationDetails")]
        public bool AddCarrierRegistrationDetails(Carrier carrier)
        {
            bool status;
            try
            {
                status = _userRepository.AddCarrierRegistrationDetails(carrier);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        [HttpPost]
        [Route("addCarrierRepRegistrationDetails")]
        public bool AddCarrierRepRegistrationDetails(CarrierRep carrierRep)
        {
            bool status;
            try
            {
                status = _userRepository.AddCarrierRepRegistrationDetails(carrierRep);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        [HttpPost]
        [Route("addCustomerRepRegistrationDetails")]
        public bool AddCustomerRepRegistrationDetails(CustomerRep customerRep)
        {
            bool status;
            try
            {
                status = _userRepository.AddCustomerRepRegistrationDetails(customerRep);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        [HttpPost]
        [Route("checkLoginDetails")]
        public bool CheckLoginDetails(loginDTO loginDetails)
        {
            bool status;
            try
            {
                status = _userRepository.CheckLoginDetails(loginDetails);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }


    }
}
