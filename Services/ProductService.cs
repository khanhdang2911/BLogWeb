namespace CS68_MVC1
{
    public class ProductService
    {
        public List<ProductViewModel> products;
        public ProductService()
        {
            var product1=new ProductViewModel ()
            {
                id=1,
                name="iphone",
                price=353
            };
            var product2=new ProductViewModel ()
            {
                id=2,
                name="samsung",
                price=541
            };
            var product3=new ProductViewModel ()
            {
                id=3,
                name="laptop",
                price=2465
            };
            products=new List<ProductViewModel>();
            products.Add(product1);
            products.Add(product2);
            products.Add(product3);
        }
    }
}