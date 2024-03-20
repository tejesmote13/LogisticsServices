using LogisticsServices.Models;

namespace LogisticsServices.Repositories.Order
{
    public interface IOrdersRepository
    {
        public List<OrderDTO> getOrdersDetailsOfCarrier(string userId);
        public Task<(int QuoteOrderId, string message)> saveQuoteOrder(OrderDTO OrderDetails);
        public Task<(int orderId, string message)> saveOrder(Models.OrderDTO OrderDetails);
        public List<OrderDTO> getOrdersDetailsOfCustomer(string userId);

    }
}
