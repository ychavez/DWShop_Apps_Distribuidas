namespace DWShop.Application.Responses.Catalog
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public decimal Price { get; set; }
        public string? PhotoURL { get; set; }

    }
}
