using System.ComponentModel.DataAnnotations.Schema;

namespace Mark.Models
{
    public class Basket
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User user { get; set; }
        [ForeignKey("ToyId")]
        public int ToyId { get; set; }
        public Toy toy { get; set; }
    }
}
