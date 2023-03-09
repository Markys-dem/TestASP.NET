using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mark.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="Порядок оторажения должен быть больше 0")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }

    }
}
