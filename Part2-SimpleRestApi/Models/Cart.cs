namespace Part2_SimpleRestApi.Models
{
    
    public class CartRequest
    {
        public int userId { get; set; }
        public string date { get; set; }
        public List<ProductInCart> products { get; set; }
    }

    public class ProductInCart
    {
        public int productId { get; set; }
        public int quantity { get; set; }
    }

    public class CartResponse
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string date { get; set; }
        public List<ProductInCart> products { get; set; }
    }

}
