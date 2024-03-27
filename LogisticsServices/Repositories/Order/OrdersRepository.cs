using LogisticsServices.DbContex;
using LogisticsServices.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LogisticsServices.Repositories.Order
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DbContex.LogisticsDbContext _context;
        public double customerPrice { get; set; }
        public OrdersRepository(DbContex.LogisticsDbContext contex)
        {
            this._context = contex;
        }

        public async Task<(int QuoteOrderId, string message)> saveQuoteOrder(OrderDTO quoteOrderDetails)
        {
            try
            {
                int equipmentId = _context.Equipments
                            .Where(e => e.EquipmentName == quoteOrderDetails.EquipmentName)
                            .Select(e => e.EquipmentId)
                            .FirstOrDefault();

                int customerId = _context.Customers
                            .Where(c => c.UserId == quoteOrderDetails.UserId)
                            .Select(c => c.CustomerId)
                            .FirstOrDefault();

                int carrierId = 1;
                quoteOrderDetails.CarrierPrice = 1.5 * quoteOrderDetails.CustomerPrice;

                if (equipmentId == 0)
                {
                    return (-1, "Please Enter Correct Equipment Id.");
                }
                SqlParameter prmCustomerId = new SqlParameter("@CustomerId", customerId);
                SqlParameter prmCarrierId = new SqlParameter("@CarrierId", carrierId);
                SqlParameter prmQuoteOrderDate = new SqlParameter("@QuoteOrderDate", DateTime.Today);
                SqlParameter prmPickUpDate = new SqlParameter("@PickUpDate", quoteOrderDetails.PickUpDate);
                SqlParameter prmOriginZipId = new SqlParameter("@OriginZipId", quoteOrderDetails.OriginZipId);
                SqlParameter prmDestinationZipId = new SqlParameter("@DestinationZipId", quoteOrderDetails.DestinationZipId);
                SqlParameter prmEquipmentId = new SqlParameter("@EquipmentId", equipmentId);
                SqlParameter prmCarrierPrice = new SqlParameter("@CarrierPrice", quoteOrderDetails.CarrierPrice);
                SqlParameter prmCustomerPrice = new SqlParameter("@CustomerPrice", quoteOrderDetails.CustomerPrice);

                SqlParameter prmQuoteOrderId = new SqlParameter("@PendingOrderId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC [dbo].[usp_InsertQuoteOrder] @PendingOrderId OUTPUT, @CustomerId, @CarrierId, @QuoteOrderDate, @PickUpDate, @OriginZipId, @DestinationZipId, @EquipmentId, @CarrierPrice, @CustomerPrice",
                    prmQuoteOrderId, prmCustomerId, prmCarrierId, prmQuoteOrderDate, prmPickUpDate, prmOriginZipId, prmDestinationZipId, prmEquipmentId, prmCarrierPrice, prmCustomerPrice
                );
             
                int QuoteOrderId = (int)prmQuoteOrderId.Value;
                return (QuoteOrderId, "Order has created successfuly.");
            }
            catch (Exception ex)
            {
                return (-1, $"Error saving order: {ex.Message}");
            }
        }
        public async Task<(int orderId, string message)> saveOrder(OrderDTO orderDetails,int pendingOrderId)
        {
            try
            {
                string status = "New";
                int equipmentId = _context.Equipments
                            .Where(e => e.EquipmentName == orderDetails.EquipmentName)
                            .Select(e => e.EquipmentId)
                            .FirstOrDefault();

                int customerId= _context.Customers
                            .Where(c => c.UserId == orderDetails.UserId)
                            .Select(c => c.CustomerId)
                            .FirstOrDefault();

                int carrierId = 1;
                orderDetails.CarrierPrice = 1.5 * orderDetails.CustomerPrice;

                if (equipmentId == 0)
                {
                    return (-1,"Please Enter Correct Equipment Id.");
                }
                SqlParameter prmCustomerId = new SqlParameter("@CustomerId", customerId);
                SqlParameter prmCarrierId = new SqlParameter("@CarrierId", carrierId);
                SqlParameter prmOrderDate = new SqlParameter("@OrderDate", DateTime.Today);
                SqlParameter prmPickUpDate = new SqlParameter("@PickUpDate", orderDetails.PickUpDate);
                SqlParameter prmStatus = new SqlParameter("@Status", status);
                SqlParameter prmOriginZipId = new SqlParameter("@OriginZipId", orderDetails.OriginZipId);
                SqlParameter prmDestinationZipId = new SqlParameter("@DestinationZipId", orderDetails.DestinationZipId);
                SqlParameter prmEquipmentId = new SqlParameter("@EquipmentId", equipmentId);
                SqlParameter prmCarrierPrice = new SqlParameter("@CarrierPrice", orderDetails.CarrierPrice);
                SqlParameter prmCustomerPrice = new SqlParameter("@CustomerPrice", orderDetails.CustomerPrice);

                
                SqlParameter prmOrderId = new SqlParameter("@OrderId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

               await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp_InsertOrder] @OrderId OUTPUT, @CustomerId, @CarrierId, @OrderDate, @PickUpDate, @Status, @OriginZipId, @DestinationZipId, @EquipmentId, @CarrierPrice, @CustomerPrice",
                    prmOrderId, prmCustomerId, prmCarrierId, prmOrderDate, prmPickUpDate, prmStatus, prmOriginZipId, prmDestinationZipId, prmEquipmentId, prmCarrierPrice, prmCustomerPrice);

                int orderId = (int)prmOrderId.Value;

                if (orderId > 0)
                {
                    var pendingOrderData = _context.PendingOrders.Where(p=>p.PendingOrderId==pendingOrderId).FirstOrDefault();
                    if(pendingOrderData != null)
                    {
                         _context.PendingOrders.Remove(pendingOrderData);
                        _context.SaveChanges();
                    }
                }

                return (orderId,"Order has created successfuly.");
            }
            catch (Exception ex)
            {
                return (-1, $"Error saving order: {ex.Message}");
            }
            }
        public double getQuotePrice(string equipmentType, DateOnly pickUpDate, double distance)
        {
            try
            {
                // perhour= mile/hour * rateperequipmenttype

                double ratePerMile = 10;
                double equipmentCost = _context.Equipments.Where(e => e.EquipmentName == equipmentType).Select(s => s.EquipmentPrice).FirstOrDefault();
                double equimentCostPerHour = (distance/60)* equipmentCost;

                customerPrice = (distance * ratePerMile) + equimentCostPerHour;

                DateOnly today = DateOnly.FromDateTime(DateTime.Now);

                if (DateOnly.Equals(pickUpDate,today))
                {
                    customerPrice += customerPrice * 0.07;

                }
                else if (DateOnly.Equals(pickUpDate, today.AddDays(1)))
                {
                    customerPrice += customerPrice * 0.03;
                }
                else if (pickUpDate >= today.AddDays(5))
                {
                    customerPrice -= customerPrice * 0.05;
                }
                else
                {
                    return customerPrice;
                }

            }
            catch (Exception)
            {
                customerPrice = -1;
            }

            return customerPrice;
        }
        public List<PendingOrderDTO> getQuoteOrders(string userId)
        {
            List<PendingOrderDTO> ordersList = new List<PendingOrderDTO>();

            try
            {
                int customerId = _context.Customers.Where(x => x.UserId == userId).Select(c => c.CustomerId).FirstOrDefault();

                SqlParameter prmCustomerId = new SqlParameter("@customerId", customerId);
                ordersList = _context.PendingOrderDTO.FromSqlRaw("select * from [dbo].[fetchQuoteDashboardDetails] (@customerId)", prmCustomerId).ToList();
            }
            catch (Exception)
            {
                ordersList = null;
            }
            return ordersList;
        }
        public List<OrderDTO> getOrdersDetailsOfCarrier()
        {
            List<OrderDTO> ordersList = new List<OrderDTO>();

            try
            {
                ordersList = _context.OrderDTO.FromSqlRaw("select * from dbo.fetchCarrierDashboardDetails()").ToList();
            }
            catch (Exception)
            {
                ordersList = null;
            }
            return ordersList;
        }
        public List<OrderDTO> getOrdersDetailsOfCustomer(string userId)
        {
            List<OrderDTO> ordersList = new List<OrderDTO>();
            try
            {
                int customerId = _context.Customers.Where(x => x.UserId == userId).Select(c=>c.CustomerId).FirstOrDefault();
                SqlParameter prmCustomerId = new SqlParameter("@customerId", customerId);
               ordersList = _context.OrderDTO.FromSqlRaw("select * from dbo.fetchDashboardDetails(@customerId)",prmCustomerId).ToList();
            }
            catch (Exception)
            {
                ordersList = null;
            }

            return ordersList;
        }
        public bool changeOrderStatus(int orderId)
        {
            var orderExits=_context.Orders.Find(orderId);
            if (orderExits != null)
            {
                if (orderExits.Status == "New")
                {
                orderExits.Status = "InProgress";
                }
                else
                { 
                    orderExits.Status = "Completed";
                }
                _context.SaveChanges();
                return true;
            }
            else {  return false; }
        }

        public List<Zip> GetAddressList()
        {
            List<Zip> addressList = new List<Zip>();

            try
            {
                addressList = _context.Zips.ToList();
            }
            catch (Exception)
            {
                addressList = null;
            }
            return addressList;
        }

        public List<Equipment> GetEquipmentList()
        {
            List<Equipment> EquipmentList = new List<Equipment>();

            try
            {
                EquipmentList = _context.Equipments.ToList();
            }
            catch (Exception)
            {
                EquipmentList = null;
            }
            return EquipmentList;
        }

        public async Task<(int pendingOrderId, string message)> updateQuoteOrder(int pendingOrderId, double customerPrice)
        {

            try
            {
                var quoteorder = _context.PendingOrders.Find(pendingOrderId);
                double carrierPrice = 1.5 * customerPrice;
                if(quoteorder == null)
                {
                    return (-1, "Order Not Found");
                }
                quoteorder.CustomerPrice=customerPrice;
                quoteorder.CarrierPrice = carrierPrice;

               await _context.SaveChangesAsync();
               return (quoteorder.PendingOrderId, "Order has updated successfully.");
            }
            catch (Exception)
            {
                return (-1, "There is an Error. Please try again.");
            }
        }
    }
}
