using LogisticsServices.DbContex;
using LogisticsServices.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace LogisticsServices.Repositories.User
{
    public class UserRepository:IUserRepository
    {
        private readonly DbContex.LogisticsDbContext _context;
        private const string pattern = @"(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$";

        public UserRepository(DbContex.LogisticsDbContext contex)
        {
            this._context = contex;
        }

        public async Task<(bool success,string message)> AddCustomerDetails(Customer customer)
        {
            int noOfRow = 0;
            try
            {
                if(string.IsNullOrEmpty(customer.UserId) || string.IsNullOrEmpty(customer.Password) || string.IsNullOrEmpty(customer.EmailId)) 
                {
                    return (false, "All fields are required.");
                }

                if(!Regex.IsMatch(customer.Password,pattern))
                {
                    return (false, "Invalid Password. Password should be 10 characters, can be alphanumeric, no space, need one special character. ");
                }
                string hashedPassword = HashedPassword(customer.Password);

                Customer customerdata = _context.Customers.Where(p => p.UserId == customer.UserId).FirstOrDefault();
                if (customerdata == null)
                {
                    SqlParameter prmUserId = new SqlParameter("@UserId", customer.UserId);
                    SqlParameter prmFirstName = new SqlParameter("@FirstName", customer.FirstName);
                    SqlParameter prmLastName = new SqlParameter("@LastName", customer.LastName);
                    SqlParameter prmPassword = new SqlParameter("@Password", hashedPassword);
                    SqlParameter prmEmailId = new SqlParameter("@EmailId", customer.EmailId);
                    SqlParameter prmPhone = new SqlParameter("@Phone", customer.Phone);
                    SqlParameter prmAddress = new SqlParameter("@Address", customer.Address);
                    SqlParameter prmZipId = new SqlParameter("@ZipId", customer.ZipId);

                    noOfRow = await _context.Database.ExecuteSqlRawAsync("EXEC usp_RegisterCustomer @UserId,@FirstName, @LastName, @Password, @EmailId, @Phone, @Address, @ZipId", prmUserId, prmFirstName, prmLastName, prmPassword, prmEmailId, prmPhone, prmAddress, prmZipId);

                    if (noOfRow > 0)
                    {
                        return (true, "Registration Successful");
                    }
                    else
                    {
                        return (false, "Registration Failed");
                    }
                }
                else
                {                  
                    return (false, "UserId Or EmailId already Registered.");
                }
            }
            catch (Exception ex)
            {
                return (false, "Error: "+ ex.Message);
            }
        }

        public async Task <(bool success, string message)> AddCarrierRegistrationDetails(Carrier carrier)
        {
            int noOfRow = 0;
            try
            {
                if (string.IsNullOrEmpty(carrier.UserId) || string.IsNullOrEmpty(carrier.Password) || string.IsNullOrEmpty(carrier.EmailId))
                {
                    return (false, "All fields are required.");
                }
                if (!Regex.IsMatch(carrier.Password, pattern))
                {
                    return (false, "Invalid Password. Password should be 10 characters, can be alphanumeric, no space, need one special character. ");
                }

                string hashedPassword = HashedPassword(carrier.Password);

                Carrier carrierdata = _context.Carriers.Where(p => p.UserId == carrier.UserId).FirstOrDefault();

                if (carrierdata == null)
                {
                    SqlParameter prmUserId = new SqlParameter("@UserId", carrier.UserId);
                    SqlParameter prmFirstName = new SqlParameter("@FirstName", carrier.FirstName);
                    SqlParameter prmLastName = new SqlParameter("@LastName", carrier.LastName);
                    SqlParameter prmPassword = new SqlParameter("@Password", hashedPassword);
                    SqlParameter prmEmailId = new SqlParameter("@EmailId", carrier.EmailId);
                    SqlParameter prmPhone = new SqlParameter("@Phone", carrier.Phone);
                    SqlParameter prmAddress = new SqlParameter("@Address", carrier.Address);
                    SqlParameter prmZipId = new SqlParameter("@ZipId", carrier.ZipId);

                    noOfRow = await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp_RegisterCarrier] @UserId, @FirstName, @LastName, @Password, @EmailId, @Phone, @Address, @ZipId", prmUserId, prmFirstName, prmLastName, prmPassword, prmEmailId, prmPhone, prmAddress, prmZipId);

                    if (noOfRow > 0)
                    {
                        return (true, "Registration Successful");
                    }
                    else
                    {
                        return (false, "Registration Failed");
                    }
                }
                     else
                    {
                        return (false, "UserId Or EmailId already Registered.");
                    }
            }
            catch (Exception ex)
            {
                return (false, "Error: " + ex.Message);
            }
        }

        public async Task<(bool success, string message)> AddCarrierRepRegistrationDetails(RegistrationDTO carrierRep)
        {
            int noOfRow = 0;
            try
            {
                if (string.IsNullOrEmpty(carrierRep.UserId) || string.IsNullOrEmpty(carrierRep.Password) || string.IsNullOrEmpty(carrierRep.EmailId))
                {
                    return (false, "All fields are required.");
                }
                if (!Regex.IsMatch(carrierRep.Password, pattern))
                {
                    return (false, "Invalid Password. Password should be 10 characters, can be alphanumeric, no space, need one special character. ");
                }

                string hashedPassword = HashedPassword(carrierRep.Password);

                CarrierRep carrierRepData = _context.CarrierReps.Where(p => p.UserId == carrierRep.UserId).FirstOrDefault();
                int carrierId = _context.Carriers.Where(p => p.UserId == carrierRep.carrierUserId).Select(s => s.CarrierId).FirstOrDefault();
                if (carrierRepData == null)
                {
                    SqlParameter prmUserId = new SqlParameter("@UserId", carrierRep.UserId);
                    SqlParameter prmCarrierId = new SqlParameter("@CarrierId", carrierId);
                    SqlParameter prmFirstName = new SqlParameter("@FirstName", carrierRep.FirstName);
                    SqlParameter prmLastName = new SqlParameter("@LastName", carrierRep.LastName);
                    SqlParameter prmPassword = new SqlParameter("@Password", hashedPassword);
                    SqlParameter prmEmailId = new SqlParameter("@EmailId", carrierRep.EmailId);
                    SqlParameter prmPhone = new SqlParameter("@Phone", carrierRep.Phone);
                    SqlParameter prmAddress = new SqlParameter("@Address", carrierRep.Address);
                    SqlParameter prmZipId = new SqlParameter("@ZipId", carrierRep.ZipId);

                    if(carrierRep.carrierUserId == null)
                    {
                        prmCarrierId.Value=DBNull.Value;
                    }

                    noOfRow = await _context.Database.ExecuteSqlRawAsync("EXEC usp_RegisterCarrierRep @UserId, @CarrierId, @FirstName, @LastName, @Password, @EmailId, @Phone, @Address, @ZipId", prmUserId, prmCarrierId, prmFirstName, prmLastName, prmPassword, prmEmailId, prmPhone, prmAddress, prmZipId);

                    if (noOfRow > 0)
                    {
                        return (true, "Registration Successful");
                    }
                    else
                    {
                        return (false, "Registration Failed");
                    }
                }
                else
                {
                    return (false, "UserId Or EmailId already Registered.");
                }
            }
            catch (Exception ex)
            {
                return (false, "Error: " + ex.Message);
            }
        }

        public async Task<(bool success, string message)> AddCustomerRepRegistrationDetails(RegistrationDTO customerRep)
        {
            int noOfRow = 0;
            try
            {
                if (string.IsNullOrEmpty(customerRep.UserId) || string.IsNullOrEmpty(customerRep.Password) || string.IsNullOrEmpty(customerRep.EmailId))
                {
                    return (false, "All fields are required.");
                }
                if (!Regex.IsMatch(customerRep.Password, pattern))
                {
                    return (false, "Invalid Password. Password should be 10 characters, can be alphanumeric, no space, need one special character. ");
                }

                string hashedPassword = HashedPassword(customerRep.Password);

                CustomerRep customerRepdata = _context.CustomerReps.Where(p => p.UserId == customerRep.UserId).FirstOrDefault();
                int customerId = _context.Customers.Where(p => p.UserId == customerRep.customerUserId).Select(s => s.CustomerId).FirstOrDefault();

                if (customerRepdata == null)
                {
                    SqlParameter prmUserId = new SqlParameter("@UserId", customerRep.UserId);
                    SqlParameter prmCustomerId = new SqlParameter("@CustomerId", customerId);
                    SqlParameter prmFirstName = new SqlParameter("@FirstName", customerRep.FirstName);
                    SqlParameter prmLastName = new SqlParameter("@LastName", customerRep.LastName);
                    SqlParameter prmPassword = new SqlParameter("@Password", hashedPassword);
                    SqlParameter prmEmailId = new SqlParameter("@EmailId", customerRep.EmailId);
                    SqlParameter prmPhone = new SqlParameter("@Phone", customerRep.Phone);
                    SqlParameter prmAddress = new SqlParameter("@Address", customerRep.Address);
                    SqlParameter prmZipId = new SqlParameter("@ZipId", customerRep.ZipId);

                    if (customerRep.carrierUserId == null)
                    {
                        prmCustomerId.Value = DBNull.Value;
                    }

                    noOfRow =await _context.Database.ExecuteSqlRawAsync("EXEC usp_RegisterCustomerRep @UserId, @CustomerId, @FirstName, @LastName, @Password, @EmailId, @Phone, @Address, @ZipId", prmUserId, prmCustomerId, prmFirstName, prmLastName, prmPassword, prmEmailId, prmPhone, prmAddress, prmZipId);


                    if (noOfRow > 0)
                    {
                        return (true, "Registration Successful");
                    }
                    else
                    {
                        return (false, "Registration Failed");
                    }
                }
                else
                {
                    return (false, "UserId Or EmailId already Registered.");
                }
            }
            catch (Exception ex)
            {
                return (false, "Error: " + ex.Message);
            }
        }

        public async Task<(bool success, string message)> CheckLoginDetails(loginDTO loginDetails)
        {
            if (string.IsNullOrEmpty(loginDetails.UserId) || string.IsNullOrEmpty(loginDetails.Password))
            {
                return (false, "All fields are required.");
            }

            if (loginDetails.UserType == "customer")
            {
                Customer customerData = await _context.Customers.FirstOrDefaultAsync(u => u.UserId == loginDetails.UserId);
                bool IsPasswordMatched = VerifyPassword(loginDetails.Password, customerData.Password);
               
                if (customerData == null)
                {
                    return (false, "User Not Found");
                }

                if (customerData != null && IsPasswordMatched) 
                {
                    var token = GenerateToken(loginDetails.UserId,loginDetails.UserType);
                    return (true, token);
                }
                else
                {
                    return (false, "Login failed, Please enter correct password");
                }

            }
            else if(loginDetails.UserType == "customerRep")
            {
                CustomerRep customerRepData = await _context.CustomerReps.FirstOrDefaultAsync(u => u.UserId == loginDetails.UserId);
                string customerUserId=  _context.Customers.Where(c=>c.CustomerId==customerRepData.CustomerId).Select(s=>s.UserId).FirstOrDefault();
                bool IsPasswordMatched = VerifyPassword(loginDetails.Password, customerRepData.Password);

                if (customerRepData == null)
                {
                    return (false, "User Not Found");
                }

                if (customerRepData != null && IsPasswordMatched)
                {
                    var token = GenerateToken(loginDetails.UserId, loginDetails.UserType, customerUserId);
                    return (true, token);
                }
                else
                {
                    return (false, "Login failed");
                }
            }
            else if(loginDetails.UserType == "carrier")
            {
                Carrier carrierData = await _context.Carriers.FirstOrDefaultAsync(u => u.UserId == loginDetails.UserId);
                bool IsPasswordMatched = VerifyPassword(loginDetails.Password, carrierData.Password);

                if (carrierData == null)
                {
                    return (false, "User Not Found");
                }

                if (carrierData != null && IsPasswordMatched)
                {
                    var token = GenerateToken(loginDetails.UserId, loginDetails.UserType);
                    return (true, token);
                }
                else
                {
                    return (false, "Login failed");
                }
            }
            else{

                CarrierRep carrierRepData = await _context.CarrierReps.FirstOrDefaultAsync(u => u.UserId == loginDetails.UserId);
                bool IsPasswordMatched = VerifyPassword(loginDetails.Password, carrierRepData.Password);
                string customerUserId = _context.Carriers.Where(c => c.CarrierId == carrierRepData.CarrierId).Select(s => s.UserId).FirstOrDefault();

                if (carrierRepData == null)
                {
                    return (false, "User Not Found");
                }
                if (carrierRepData != null && IsPasswordMatched)
                {
                    var token = GenerateToken(loginDetails.UserId, loginDetails.UserType, customerUserId);
                    return (true, token);
                }
                else
                {
                    return (false, "Login failed");
                }
            }
        }

        public string HashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password,hashPassword);
        }

        public string GenerateToken (string UserId, string userType, string custOrCarrUserId="")   
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("supersupersupersupersupersupersupersupersupersupersupersecretkey................");
            var identity = new ClaimsIdentity(new Claim[]
            {
               new Claim(ClaimTypes.Name,UserId),
               new Claim(ClaimTypes.Name,userType),
               new Claim(ClaimTypes.Name,custOrCarrUserId)
            }); ;
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials

            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

    }
}
