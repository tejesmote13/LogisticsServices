namespace LogisticsServices.Repositories.Order
{
    public interface IOrdersRepository
    {
        public List<Models.Order> getOrdersDetailsOfCarrier();
        public Models.Order createOrder(Models.Order OrderDetails);
        public Models.Order saveOrder(Models.Order OrderDetails);
        public List<OrderDTO> getOrdersDetailsOfCustomer(string userId);

    }
}
