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
        public async Task<IActionResult> AddCustomerDetails( Customer customer)
        {
            try
            {
               var status = await _userRepository.AddCustomerDetails(customer);
                    return Ok(new { Status = status.success, Message = status.message });
            }
            catch (Exception ex)
            {
                return BadRequest(new {Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("addCarrierRegistrationDetails")]
        public async Task<IActionResult> AddCarrierRegistrationDetails(Carrier carrier)
        {
            try
            {
                var status = await _userRepository.AddCarrierRegistrationDetails(carrier);
                return Ok(new { Status = status.success, Message = status.message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("addCarrierRepRegistrationDetails")]
        public async Task<IActionResult> AddCarrierRepRegistrationDetails(RegistrationDTO carrierRep)
        {
            try
            {
                var status = await _userRepository.AddCarrierRepRegistrationDetails(carrierRep);
                return Ok(new { Status = status.success, Message = status.message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("addCustomerRepRegistrationDetails")]
        public async Task<IActionResult> AddCustomerRepRegistrationDetails(RegistrationDTO customerRep)
        {
            try
            {
              var  status = await _userRepository.AddCustomerRepRegistrationDetails(customerRep);
                return Ok(new { Status = status.success, Message = status.message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("checkLoginDetails")]
        public async Task<IActionResult> CheckLoginDetails(loginDTO loginDetails)
        {
            try
            {
               var status = await _userRepository.CheckLoginDetails(loginDetails);
                if (status.success)
                {
                    return Ok(new {token=status.message});
                }
                else
                {
                    return BadRequest(new { Error = status.message });
                }  
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
