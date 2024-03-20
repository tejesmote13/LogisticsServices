using System;
using System.Collections.Generic;

namespace LogisticsServices.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public int CarrierId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? PickUpDate { get; set; }

    public string Status { get; set; }

    public int OriginZipId { get; set; }

    public int DestinationZipId { get; set; }

    public int EquipmentId { get; set; }

    public double? CarrierPrice { get; set; }

    public double? CustomerPrice { get; set; }

    public virtual Carrier Carrier { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual Zip DestinationZip { get; set; }

    public virtual Equipment Equipment { get; set; }

    public virtual Zip OriginZip { get; set; }
}
