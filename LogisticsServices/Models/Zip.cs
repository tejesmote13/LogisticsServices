using System;
using System.Collections.Generic;

namespace LogisticsServices.Models;

public partial class Zip
{
    public int ZipId { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public virtual ICollection<CarrierRep> CarrierReps { get; set; } = new List<CarrierRep>();

    public virtual ICollection<Carrier> Carriers { get; set; } = new List<Carrier>();

    public virtual ICollection<CustomerRep> CustomerReps { get; set; } = new List<CustomerRep>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Order> OrderDestinationZips { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderOriginZips { get; set; } = new List<Order>();

    public virtual ICollection<PendingOrder> PendingOrderDestinationZips { get; set; } = new List<PendingOrder>();

    public virtual ICollection<PendingOrder> PendingOrderOriginZips { get; set; } = new List<PendingOrder>();
}
