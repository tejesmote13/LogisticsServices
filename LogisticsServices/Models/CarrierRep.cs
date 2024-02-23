using System;
using System.Collections.Generic;

namespace LogisticsServices.Models;

public partial class CarrierRep
{
    public int CarrierRepId { get; set; }

    public string UserId { get; set; }

    public int? CarrierId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Password { get; set; }

    public string EmailId { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public int ZipId { get; set; }

    public virtual Carrier Carrier { get; set; }

    public virtual Zip Zip { get; set; }
}
