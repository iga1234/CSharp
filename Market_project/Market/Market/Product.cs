namespace Market
{
    public class GeneratorProduct
    {
        public GeneratorProduct(string name, bool mustHave, double minProductionPrice, double maxProductionPrice, double minExpectedPrice, double maxExpectedPrice, int minAmountProducted, int maxAmountProducted, int minPriority, int maxPriority)
        {
            Name = name;
            MustHave = mustHave;
            MinProductionPrice = minProductionPrice;
            MaxProductionPrice = maxProductionPrice;
            MinExpectedPrice = minExpectedPrice;
            MaxExpectedPrice = maxExpectedPrice;
            MinAmountProducted = minAmountProducted;
            MaxAmountProducted = maxAmountProducted;
            MinPriority = minPriority;
            MaxPriority = maxPriority;
        }

        public string Name { get; set; }
        public bool MustHave { get; set; }
        public double MinProductionPrice { get; set; }
        public double MaxProductionPrice { get; set; }
        public double MinExpectedPrice { get; set; }
        public double MaxExpectedPrice { get; set; }
        public int MinAmountProducted { get; set; }
        public int MaxAmountProducted { get; set; }
        public int MinPriority { get; set; }
        public int MaxPriority { get; set; }
    }

    public class Product
    {
        public Product(int id, int sellerId, double netprice)
        {
            Id = id;
            NetPrice = netprice;
        }
        public int Id { get; set; }
        public double NetPrice { get; set; }
        public double GrossPrice
        {
            get
            {
                return Math.Round(NetPrice + ServiceLocator.CentralBankService.Tax * NetPrice, 2);
            }
        }

        public static Dictionary<int, GeneratorProduct> ProductDataDictionary = new Dictionary<int, GeneratorProduct>()
        {
            { 0, new GeneratorProduct("Bread", true, 2.0, 5.0, 3.5, 6.0, 10, 100, 90, 100) },
            { 1, new GeneratorProduct("Butter", true, 3.0, 6.0, 4.0, 8.0, 10, 100, 90, 100) },
            { 2, new GeneratorProduct("Cheese", true, 5.0, 8.0, 6.0, 10.0, 10, 100, 90, 100) },
            { 3, new GeneratorProduct("ToiletPaper", true, 10.0, 15.0, 10.0, 20.0, 10, 100, 90, 100) },
            { 4, new GeneratorProduct("Iphone", false, 1000.0, 4000.0, 3000.0, 8000.0, 1, 25, 1, 100) },
            { 5, new GeneratorProduct("TV", false, 500.0, 2000.0, 1500.0, 4000.0, 1, 25, 1, 100) },
            { 6, new GeneratorProduct("Computer", false, 2000.0, 5000.0, 3000.0, 8000.0, 1, 25, 1, 100) },
            { 7, new GeneratorProduct("Car", false, 10000.0, 50000.0, 20000.0, 100000.0, 1, 5, 1, 100) }
        };

        public override string ToString()
        {
            return ProductDataDictionary[Id].Name + ", NetPrice:" + NetPrice + ", GrossPrice: " + GrossPrice;
        }
    }
}
