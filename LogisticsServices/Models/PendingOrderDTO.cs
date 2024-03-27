using System.ComponentModel.DataAnnotations;

namespace LogisticsServices.Models
{
    public class PendingOrderDTO
    {
        [Key]
        public int PendingOrderId { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime OrderDate { get; set; }
        public int OriginZipId { get; set; }
        public int DestinationZipId { get; set; }
        public string OriginAddress { get; set; }
        public string DestinationAddress { get; set; }
        public string EquipmentName { get; set; }
        public double CarrierPrice { get; set; }
        public double CustomerPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
    }
}
