namespace Market
{
    public class RandomDataGenerator
    {
        public RandomDataGenerator(int seed)
        {
            Random = new Random(seed);
        }

        private Random Random { get; set; }

        public int RandomInt(int minimum, int maximum)
        {
            return Random.Next(minimum, maximum + 1);
        }

        public double RandomDouble(double minimum, double maximum)
        {
            return Math.Round(Random.NextDouble() * (maximum - minimum) + minimum, 2);
        }

        public List<Wish> generateClientWishes()
        {
            List<Wish> wishList = new List<Wish>();
            for (int id = 0; id < Product.ProductDataDictionary.Count(); id++)
            {
                int isWanted = RandomInt(0, 1);
                if (isWanted == 1)
                {
                    var p = Product.ProductDataDictionary[id]; 
                    double expectedPrice = RandomDouble(p.MinExpectedPrice, p.MaxExpectedPrice);
                    int priority = RandomInt(p.MinPriority, p.MaxPriority);
                    Wish wish = new Wish(id, expectedPrice, priority, p.MustHave);
                    wishList.Add(wish);
                }
            }
            return wishList;
        }

        public List<Customer> generateCustomers(int amount)
        {
            const double minSalary = 1500.0;
            const double maxSalary = 30000.0;
            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < amount; i++)
            {
                var wishList = generateClientWishes();
                double salary = RandomDouble(minSalary, maxSalary);
                Customer customer = new Customer(wishList, salary);
                customers.Add(customer);
            }
            return customers;
        }

        public List<SellerProduct> generateSellerProducts()
        {
            List<SellerProduct> sellerProducts = new List<SellerProduct>();
            for (int id = 0; id < Product.ProductDataDictionary.Count(); id++)
            {
                int hasProduct = RandomInt(0, 1);
                if (hasProduct == 1)
                {
                    var p = Product.ProductDataDictionary[id];
                    double productionPrice = RandomDouble(p.MinProductionPrice, p.MaxProductionPrice);
                    int amountProductedPerPeriod = RandomInt(p.MinAmountProducted, p.MaxAmountProducted);
                    SellerProduct sellerProduct = new SellerProduct(id, productionPrice, amountProductedPerPeriod);
                    sellerProducts.Add(sellerProduct);
                }
            }
            return sellerProducts;
        }

        public List<Seller> generateSellers(int amount)
        {
            const double minMargin = 0.1;
            const double maxMargin = 0.7;
            List<Seller> sellers = new List<Seller>();
            for (int i = 0; i < amount; i++)
            {
                List<SellerProduct> sellerProducts = generateSellerProducts();
                double margin = RandomDouble(minMargin, maxMargin);
                Seller seller = new Seller(sellerProducts, margin);
                sellers.Add(seller);
            }
            return sellers;
        }
    }
}
