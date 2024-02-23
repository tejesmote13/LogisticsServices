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
        [Route("getCarrierOrdersDetails")]
        public List<Models.Order> getCarrierOrdersDetails()
        {
            List<Models.Order> orderList = new List<Models.Order>();
            try
            {
                orderList = _ordersRepository.getOrdersDetailsOfCarrier();
            }
            catch (Exception)
            {
                orderList = null;
            }
            return orderList;
        }
    }
}
