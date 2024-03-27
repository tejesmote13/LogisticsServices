using LogisticsServices.Models;
using LogisticsServices.Repositories.Order;
using LogisticsServices.Repositories.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LogisticsServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrdersRepository _ordersRepository;
        public OrderController(OrdersRepository ordersRepository, ILogger<OrderController> logger)
        {
            _ordersRepository = ordersRepository;
            _logger = logger;   
        }

        [HttpGet]
        [Route("getOrdersDetails/{userId}")]
        public List<OrderDTO> getOrdersDetails(string userId)
        {
            List <OrderDTO> orderList = new List<OrderDTO> ();
            try
            {
                orderList = _ordersRepository.getOrdersDetailsOfCustomer(userId);
                _logger.LogInformation("Customer order details fetched succefully!");
            }
            catch (Exception)
            {
                orderList = null;
                _logger.LogInformation("Not able fetch Order list");
            }
            return orderList;
        }

        [HttpGet]
        [Route("getCarrierOrdersDetails")]
        public List<OrderDTO> getCarrierOrdersDetails( )
        {
            List<OrderDTO> orderList = new List<OrderDTO>();
            try
            {
                orderList = _ordersRepository.getOrdersDetailsOfCarrier();
                _logger.LogInformation("Carrier order details fetched succefully!");
            }
            catch (Exception)
            {
                orderList = null;
                _logger.LogInformation("Not able fetch Order list");
            }
            return orderList;
        }

        [HttpGet]
        [Route("getQuoteOrders/{userId}")]
        public List<PendingOrderDTO> getQuoteOrders(string userId)
        {
            List<PendingOrderDTO> orderList = new List<PendingOrderDTO>();
            try
            {
                orderList = _ordersRepository.getQuoteOrders(userId);
                _logger.LogInformation("Quote order details fetched succefully!");

            }
            catch (Exception)
            {
                orderList = null;
                _logger.LogInformation("Not able fetch Order list");
            }
            return orderList;
        }

        [HttpGet]
        [Route("getQuotePrice/{equipmentType}/{pickUpDate}/{distance}")]
        public double getQuotePrice(string equipmentType, DateOnly pickUpDate, double distance) {

            double quotePrice;
            try
            {
                quotePrice = _ordersRepository.getQuotePrice(equipmentType, pickUpDate, distance);
                _logger.LogInformation("Quote price calculated successfully!");
            }
            catch (Exception)
            {
                quotePrice = -1;
                _logger.LogInformation("Quote price calculation failed.");

            }
            return quotePrice;

        }

        [HttpPost]
        [Route("saveQuoteOrder")]
        public async Task<IActionResult> saveQuoteOrder(OrderDTO quoteOrderDetails) {
            
            try
            {
               var orderReturn = await _ordersRepository.saveQuoteOrder(quoteOrderDetails);
                if (orderReturn.QuoteOrderId > 0)
                {
                    _logger.LogInformation("Quote order  saved successfully!");
                    return Ok(orderReturn.QuoteOrderId);
                }
                else
                {
                    _logger.LogInformation("Can't save Quote order.");
                    return BadRequest(new { Error = orderReturn.message });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error : " + ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("saveOrder")]
        public async Task<IActionResult> saveOrder(OrderDTO orderDetails, int pendingOrderId)
        {
            try
            {
              var orderReturn = await _ordersRepository.saveOrder(orderDetails, pendingOrderId);
                if (orderReturn.orderId > 0)
                {
                    _logger.LogInformation(" Order  saved successfully!");
                    return Ok(orderReturn.orderId);
                }
                else
                {
                    _logger.LogInformation("Can't save Order.");
                    return BadRequest(new { Error = orderReturn.message });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error : "+ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("changeOrderStatus")]
        public bool changeOrderStatus(int orderId)
        {
            try
            {
                return _ordersRepository.changeOrderStatus(orderId);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error : " + ex.Message);
                return false;
            }
        }

        [HttpGet]
        [Route("getAddressList")]
        public List<Zip> GetAddressList()
        {
            List<Zip> addressList = new List<Zip>();
            try
            {
                addressList = _ordersRepository.GetAddressList();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error : " + ex.Message);
                addressList = null;
            }
            return addressList;
        }

        [HttpGet]
        [Route("getEquipmentList")]
        public List<Equipment> GetEquipmentList()
        {
            List<Equipment> EquipmentList = new List<Equipment>();
            try
            {
                EquipmentList = _ordersRepository.GetEquipmentList();
            }
            catch (Exception ex)
            {
                EquipmentList = null;
                _logger.LogInformation("Error : " + ex.Message);
            }
            return EquipmentList;
        }

        [HttpGet]
        [Route("updateQuoteOrder")]
        public async Task<IActionResult> updateQuoteOrder(int pendingOrderId, double customerPrice)
        {
            try
            {
                var orderReturn = await _ordersRepository.updateQuoteOrder( pendingOrderId, customerPrice);
                if (orderReturn.pendingOrderId > 0)
                {
                    _logger.LogInformation("Updated quote order successfully!");
                    return Ok(orderReturn.pendingOrderId);
                }
                else
                {
                    _logger.LogInformation("Can't update quote order.");
                    return BadRequest(new { Error = orderReturn.message });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error : " + ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
