namespace LogisticsServices.Models
{
    public class RegistrationDTO
    {
        public string UserId { get; set; }
        public string carrierUserId { get; set; }
        public string customerUserId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public int ZipId { get; set; }

        public string Password { get; set; }
    }
}