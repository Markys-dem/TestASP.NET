using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mark.Models.ModelViews
{
    public class ToyVM
    {
        public Toy toy { get; set; }
        public IEnumerable<SelectListItem> categoryList { get; set; }
    }
}
