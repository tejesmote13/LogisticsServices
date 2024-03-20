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
        private readonly OrdersRepository _ordersRepository;
        public OrderController(OrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        [HttpGet]
        [Route("getOrdersDetails/{userId}")]
        public List<OrderDTO> getOrdersDetails(string userId)
        {
            List <OrderDTO> orderList = new List<OrderDTO> ();
            try
            {
                orderList = _ordersRepository.getOrdersDetailsOfCustomer(userId);
            }
            catch (Exception)
            {
                orderList = null;
            }
            return orderList;
        }

        [HttpGet]
        [Route("getCarrierOrdersDetails/{userId}")]
        public List<OrderDTO> getCarrierOrdersDetails(string userId)
        {
            List<OrderDTO> orderList = new List<OrderDTO>();
            try
            {
                orderList = _ordersRepository.getOrdersDetailsOfCarrier(userId);
            }
            catch (Exception)
            {
                orderList = null;
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
            }
            catch (Exception)
            {
                orderList = null;
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

            }
            catch (Exception)
            {
                quotePrice = -1;
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
                    return Ok(orderReturn.QuoteOrderId);
                }
                else
                {
                    return BadRequest(new { Error = orderReturn.message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("saveOrder")]
        public async Task<IActionResult> saveOrder(OrderDTO orderDetails)
        {
            try
            {
              var orderReturn = await _ordersRepository.saveOrder(orderDetails);
                if (orderReturn.orderId > 0)
                {
                return Ok(orderReturn.orderId);
                }
                else
                {
                    return BadRequest(new { Error = orderReturn.message });
                }
            }
            catch (Exception ex)
            {
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
            catch (Exception)
            {
                return false;
            }
        }
    }
}
