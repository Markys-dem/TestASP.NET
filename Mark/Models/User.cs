using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Mark.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Display (Name = "Login")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<Basket> baskets { get; set; }
    }
}
