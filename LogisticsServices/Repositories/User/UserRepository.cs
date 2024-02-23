using LogisticsServices.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LogisticsServices.Repositories.User
{
    public class UserRepository:IUserRepository
    {
        private readonly LogisticsDbContext _context;

        public UserRepository(LogisticsDbContext contex)
        {
            this._context = contex;
        }

        public bool AddCustomerDetails(Customer customer)
        {
            bool status = false;
            int noOfRow = 0;
            try
            {
                Customer customerdata = _context.Customers.Where(p => p.UserId == customer.UserId).FirstOrDefault();
                if (customerdata == null)
                {
                    SqlParameter prmUserId = new SqlParameter("@UserId", customer.UserId);
                    SqlParameter prmFirstName = new SqlParameter("@FirstName", customer.FirstName);
                    SqlParameter prmLastName = new SqlParameter("@LastName", customer.LastName);
                    SqlParameter prmPassword = new SqlParameter("@Password", customer.Password);
                    SqlParameter prmEmailId = new SqlParameter("@EmailId", customer.EmailId);
                    SqlParameter prmPhone = new SqlParameter("@Phone", customer.Phone);
                    SqlParameter prmAddress = new SqlParameter("@Address", customer.Address);
                    SqlParameter prmZipId = new SqlParameter("@ZipId", customer.ZipId);

                    noOfRow = _context.Database.ExecuteSqlRaw("EXEC usp_RegisterCustomer @UserId,@FirstName, @LastName, @Password, @EmailId, @Phone, @Address, @ZipId", prmUserId, prmFirstName, prmLastName, prmPassword, prmEmailId, prmPhone, prmAddress, prmZipId);

                    if (noOfRow > 0)
                    {
                        status = true;
                    }
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public bool AddCarrierRegistrationDetails(Carrier carrier)
        {
            bool status = false;
            int noOfRow = 0;
            try
            {
                Carrier carrierdata = _context.Carriers.Where(p => p.UserId == carrier.UserId).FirstOrDefault();
                if (carrierdata == null)
                {
                    SqlParameter prmUserId = new SqlParameter("@UserId", carrier.UserId);
                    SqlParameter prmFirstName = new SqlParameter("@FirstName", carrier.FirstName);
                    SqlParameter prmLastName = new SqlParameter("@LastName", carrier.LastName);
                    SqlParameter prmPassword = new SqlParameter("@Password", carrier.Password);
                    SqlParameter prmEmailId = new SqlParameter("@EmailId", carrier.EmailId);
                    SqlParameter prmPhone = new SqlParameter("@Phone", carrier.Phone);
                    SqlParameter prmAddress = new SqlParameter("@Address", carrier.Address);
                    SqlParameter prmZipId = new SqlParameter("@ZipId", carrier.ZipId);

                    noOfRow = _context.Database.ExecuteSqlRaw("EXEC usp_RegisterCarrier @UserId,@FirstName, @LastName, @Password, @EmailId, @Phone, @Address, @ZipId", prmUserId, prmFirstName, prmLastName, prmPassword, prmEmailId, prmPhone, prmAddress, prmZipId);

                    if (noOfRow > 0)
                    {
                        status = true;
                    }
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }


        public bool AddCarrierRepRegistrationDetails(CarrierRep carrierRep)
        {
            bool status = false;
            int noOfRow = 0;
            try
            {
                CarrierRep carrierRepData = _context.CarrierReps.Where(p => p.UserId == carrierRep.UserId).FirstOrDefault();
                if (carrierRepData == null)
                {
                    SqlParameter prmUserId = new SqlParameter("@UserId", carrierRep.UserId);
                    SqlParameter prmCarrierId = new SqlParameter("@CarrierId", carrierRep.CarrierId);
                    SqlParameter prmFirstName = new SqlParameter("@FirstName", carrierRep.FirstName);
                    SqlParameter prmLastName = new SqlParameter("@LastName", carrierRep.LastName);
                    SqlParameter prmPassword = new SqlParameter("@Password", carrierRep.Password);
                    SqlParameter prmEmailId = new SqlParameter("@EmailId", carrierRep.EmailId);
                    SqlParameter prmPhone = new SqlParameter("@Phone", carrierRep.Phone);
                    SqlParameter prmAddress = new SqlParameter("@Address", carrierRep.Address);
                    SqlParameter prmZipId = new SqlParameter("@ZipId", carrierRep.ZipId);

                    if(carrierRep.CarrierId == null)
                    {
                        prmCarrierId.Value=DBNull.Value;
                    }

                    noOfRow = _context.Database.ExecuteSqlRaw("EXEC usp_RegisterCarrierRep @UserId, @CarrierId, @FirstName, @LastName, @Password, @EmailId, @Phone, @Address, @ZipId", prmUserId, prmCarrierId, prmFirstName, prmLastName, prmPassword, prmEmailId, prmPhone, prmAddress, prmZipId);

                    if (noOfRow > 0)
                    {
                        status = true;
                    }
                }
                else
                {   
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public bool AddCustomerRepRegistrationDetails(CustomerRep customerRep)
        {
            bool status = false;
            int noOfRow = 0;
            try
            {
                CustomerRep customerRepdata = _context.CustomerReps.Where(p => p.UserId == customerRep.UserId).FirstOrDefault();
                if (customerRepdata == null)
                {
                    SqlParameter prmUserId = new SqlParameter("@UserId", customerRep.UserId);
                    SqlParameter prmCustomerId = new SqlParameter("@CustomerId", customerRep.CustomerId);
                    SqlParameter prmFirstName = new SqlParameter("@FirstName", customerRep.FirstName);
                    SqlParameter prmLastName = new SqlParameter("@LastName", customerRep.LastName);
                    SqlParameter prmPassword = new SqlParameter("@Password", customerRep.Password);
                    SqlParameter prmEmailId = new SqlParameter("@EmailId", customerRep.EmailId);
                    SqlParameter prmPhone = new SqlParameter("@Phone", customerRep.Phone);
                    SqlParameter prmAddress = new SqlParameter("@Address", customerRep.Address);
                    SqlParameter prmZipId = new SqlParameter("@ZipId", customerRep.ZipId);

                    if (customerRep.CustomerId == null)
                    {
                        prmCustomerId.Value = DBNull.Value;
                    }

                    noOfRow = _context.Database.ExecuteSqlRaw("EXEC usp_RegisterCustomerRep @UserId, @CustomerId, @FirstName, @LastName, @Password, @EmailId, @Phone, @Address, @ZipId", prmUserId, prmCustomerId, prmFirstName, prmLastName, prmPassword, prmEmailId, prmPhone, prmAddress, prmZipId);

                    if (noOfRow > 0)
                    {
                        status = true;
                    }
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public bool CheckLoginDetails(loginDTO loginDetails)
        {
            bool status = false ;
            if (loginDetails.UserType == "customer")
            {
                Customer customerData = _context.Customers.FirstOrDefault(u => u.UserId == loginDetails.UserId);
               
                if (customerData != null && customerData.Password == loginDetails.Password) 
                {
                    status = true;
                }
                else
                {
                    status = false;
                }

            }
            else if(loginDetails.UserType == "customerRep")
            {
                CustomerRep customerRepData = _context.CustomerReps.FirstOrDefault(u => u.UserId == loginDetails.UserId);

                if (customerRepData != null && customerRepData.Password == loginDetails.Password)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            else if(loginDetails.UserType == "carrier")
            {
                Carrier carrierData = _context.Carriers.FirstOrDefault(u => u.UserId == loginDetails.UserId);

                if (carrierData != null && carrierData.Password == loginDetails.Password)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            else{

                CarrierRep carrierRepData = _context.CarrierReps.FirstOrDefault(u => u.UserId == loginDetails.UserId);
                if (carrierRepData != null && carrierRepData.Password == loginDetails.Password)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            return status;
        }

        
    }
}
