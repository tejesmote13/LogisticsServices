using System;
using System.Collections.Generic;

namespace LogisticsServices.Models;

public partial class Equipment
{
    public int EquipmentId { get; set; }

    public string EquipmentName { get; set; }

    public string EquipmentCategory { get; set; }

    public bool IsHazmat { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<PendingOrder> PendingOrders { get; set; } = new List<PendingOrder>();
}
