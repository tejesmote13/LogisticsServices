using LogisticsServices.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LogisticsServices.Repositories.Order
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly LogisticsDbContext _context;

        public OrdersRepository(LogisticsDbContext contex)
        {
            this._context = contex;
        }

        public Models.Order createOrder(Models.Order OrderDetails)
        {
            throw new NotImplementedException();
        }


        public Models.Order saveOrder(Models.Order OrderDetails)
        {
            throw new NotImplementedException();
        }

        public List<Models.Order> getOrdersDetailsOfCarrier()
        {
            List<Models.Order> ordersList = new List<Models.Order>();

            try
            {
                ordersList = _context.Orders.ToList();
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
                int? customerId = _context.Customers.Where(x => x.UserId == userId).Select(c=>c.CustomerId).FirstOrDefault();

                SqlParameter prmCustomerId = new SqlParameter("@customerId", customerId);
               ordersList = _context.OrderDTO.FromSqlRaw("select * from dbo.fetchDashboardDetails(@customerId)",prmCustomerId).ToList();
            }
            catch (Exception ex)
            {
                ordersList = null;
            }

            return ordersList;
        }

    }
}
