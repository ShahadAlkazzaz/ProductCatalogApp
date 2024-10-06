namespace Resources.Models
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = null!; 
        public decimal Price { get; set; }

        public string DisplayInfo => $"{Name}, {Price:C}";
    }
}
