using LogisticsServices.Models;
using LogisticsServices.Repositories.User;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger; 
        private readonly UserRepository _userRepository;
        public RegistrationController(UserRepository userRepository, ILogger<RegistrationController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost]
        [Route("addCustomerDetails")]
        public async Task<IActionResult> AddCustomerDetails( Customer customer)
        {
            try
            {
                var status = await _userRepository.AddCustomerDetails(customer);
                if (status.success)
                {
                    _logger.LogInformation("Customer Registration Done succefully!");
                }
                else
                {
                    _logger.LogInformation("Customer Registration failed");
                }

                return Ok(new { Status = status.success, Message = status.message });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(" Error in Registration : " + ex.Message);
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
                if (status.success)
                {
                    _logger.LogInformation("Carrier Registration Done succefully!");
                }
                else
                {
                    _logger.LogInformation("Carrier Registration failed");
                }
                return Ok(new { Status = status.success, Message = status.message });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(" Error in Registration : " + ex.Message);
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
                if (status.success)
                {
                _logger.LogInformation("CarrierRep Registration Done succefully!");
                }
                else
                {
                _logger.LogInformation("CarrierRep Registration failed");
                }
                return Ok(new { Status = status.success, Message = status.message });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(" Error in Registration : " + ex.Message);
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
                if (status.success)
                {
                    _logger.LogInformation("CustomerRep Registration Done succefully!");
                }
                else
                {
                    _logger.LogInformation("CustomerRep Registration failed");
                }
                return Ok(new { Status = status.success, Message = status.message });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(" Error in Registration : " + ex.Message);
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
                    _logger.LogInformation("Login verified succefully!");
                    return Ok(new {token=status.message});
                }
                else
                {
                    _logger.LogInformation("Login verified Failed!");
                    return BadRequest(new { Error = status.message });
                }  
            }
            catch (Exception ex)
            {
                    _logger.LogInformation(" Error in login : "+ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
