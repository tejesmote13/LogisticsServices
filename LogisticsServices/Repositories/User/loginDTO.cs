using System.ComponentModel.DataAnnotations;

namespace LogisticsServices.Repositories.User
{
    public class loginDTO
    {
        public string UserType { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
