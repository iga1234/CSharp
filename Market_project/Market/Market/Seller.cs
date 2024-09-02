namespace Market
{
    public class SellerProduct
    {
        public SellerProduct(int id, double productionPrice, int amountProductedPerPeriod)
        {
            Id = id;
            ProductionPrice = productionPrice;
            AmountProductedPerPeriod = amountProductedPerPeriod;
            CurrentAmount = 0;
        }

        public int Id { get; set; }

        public double ProductionPrice { get; set; }

        public int AmountProductedPerPeriod { get; set; }

        public int CurrentAmount { get; set; }
    }

    public class Seller : IObserver<ProductSale>, IObserver<InflationChange>, IPrepareTimePeriodComponent, INewTimePeriodComponent
    {
        public Dictionary<int, SellerProduct> ProductCatalog { get; set; }

        public double Margin { get; set; }

        public double Money { get; set; }

        public int Id { get; set; }

        private static int Counter { get; set; }

        public Seller(List<SellerProduct> sellerProducts, double margin)
        {
            ProductCatalog = new Dictionary<int, SellerProduct>();
            foreach (SellerProduct sellerProduct in sellerProducts)
            {
                AddToCatalog(sellerProduct);
            }
            Margin = margin;
            Money = 0;
            Id = Counter;
            Counter++;

            ServiceLocator.CentralMarketService.RegisterForEvent(this);
            ServiceLocator.CentralBankService.RegisterForEvent(this);
        }

        public void AddToCatalog(SellerProduct sellerProduct)
        {
            ProductCatalog.Add(sellerProduct.Id, sellerProduct);
        }

        public void OfferProduct(int productId)
        {
            Product product = new Product(productId, Id, GetSellPrice(productId));
            Console.WriteLine("Seller " + Id + " - OfferProduct: " + product);
            ServiceLocator.CentralMarketService.OfferProduct(product, Id);
        }

        public bool HasProduct(int productId)
        {
            return ProductCatalog.ContainsKey(productId);
        }

        public double GetSellPrice(int productId)
        {
            return Math.Round((1 + Margin) * ProductCatalog[productId].ProductionPrice, 2);
        }

        public void Accept(IPrepareTimePeriodVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void Accept(INewTimePeriodVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(ProductSale productSale)
        {
            if (productSale.SellerId == Id)
            {
                if (ProductCatalog[productSale.Product.Id].CurrentAmount > 0)
                {
                    Console.WriteLine("Seller " + Id + " - ProductSelled: " + productSale.Product);
                    ProductCatalog[productSale.Product.Id].CurrentAmount--;
                    Money += (Margin * ProductCatalog[productSale.Product.Id].ProductionPrice);
                }
            }
        }

        public void OnNext(InflationChange inflationChange)
        {
            if (inflationChange.PreviousValue < inflationChange.CurrentValue)
            {
                // inflacja rosnie, podnosimy ceny szybko
                Margin += (inflationChange.CurrentValue - inflationChange.PreviousValue) * 2.0;
                if (Margin > 0.4)
                {
                    Margin = 0.4;
                }
            }
            else
            {
                // inflacja spada, odnizamy ceny wolno
                Margin -= (inflationChange.CurrentValue - inflationChange.PreviousValue) * 0.5;
                if (Margin < 0.05)
                {
                    Margin = 0.05;
                }
            }
        }
    }
}
