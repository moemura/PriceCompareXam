namespace PriceCompareXam.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
        }

        public object ImageUrl { get; set; }
        public string PageUrl { get; set; }
        public string WebSiteName { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public int Price { get; set; }
    }
}