using LogisticsServices.Models;

namespace LogisticsServices.Repositories.Order
{
    public interface IOrdersRepository
    {
        public List<OrderDTO> getOrdersDetailsOfCarrier();
        public Task<(int QuoteOrderId, string message)> saveQuoteOrder(OrderDTO OrderDetails);
        public Task<(int orderId, string message)> saveOrder(Models.OrderDTO OrderDetails, int pendingOrderId);
        public List<OrderDTO> getOrdersDetailsOfCustomer(string userId);

    }
}
