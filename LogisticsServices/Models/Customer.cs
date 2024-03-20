using System;
using System.Collections.Generic;

namespace LogisticsServices.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailId { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public int ZipId { get; set; }

    public string Password { get; set; }

    public virtual ICollection<CustomerRep> CustomerReps { get; set; } = new List<CustomerRep>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<PendingOrder> PendingOrders { get; set; } = new List<PendingOrder>();

    public virtual Zip Zip { get; set; }
}
