namespace Mark.Models.ModelViews
{
    public class HomeVM
    {
        public IEnumerable<Toy> toys { get; set; }
        public IEnumerable<Category> categories { get; set; }
    }
}
