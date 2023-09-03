namespace Mark.Models.ModelViews
{
    public class DetailsVM
    {
        public Toy Toy { get; set; }
        public bool ExistsInCart { get; set; }
        public DetailsVM() 
        {
            Toy = new Toy();  
        }
    }
}
