namespace Market
{
    public class ProductSale
    {
        public ProductSale(Product product, int sellerId)
        {
            Product = product;
            SellerId = sellerId;
        }

        public Product Product { get; set; }

        public int SellerId { get; set; }
    };

    public class ProductSaleUnsubscriber : IDisposable
    {
        public void Dispose()
        {
        }
    }

    public class ProductOffer
    {
        public ProductOffer(Product product, int sellerId)
        {
            Product = product;
            SellerId = sellerId;
        }

        public Product Product { get; set; }

        public int SellerId { get; set; }
    }

    public class ProductUnsubscriber : IDisposable
    {
        public void Dispose()
        {
        }
    }

    public interface ICentralMarket
    {
        public void RegisterForEvent(IObserver<ProductOffer> observer);

        public void RegisterForEvent(IObserver<ProductSale> observer);

        // funkcja wywolywana przez sprzedawce ktory oferuje nowy produkt
        public void OfferProduct(Product product, int sellerId);

        // funkcja wywolywana przez klienta gdy kupuje produkt
        public void BuyProduct(Product product, int sellerId);
    }

    public class CentralMarket : IObservable<ProductOffer>, IObservable<ProductSale>, ICentralMarket
    {
        public CentralMarket()
        {
            OfferObservers = new List<IObserver<ProductOffer>>();
            SaleObservers = new List<IObserver<ProductSale>>();
        }

        List<IObserver<ProductOffer>> OfferObservers { get; set; }

        List<IObserver<ProductSale>> SaleObservers { get; set; }

        public IDisposable Subscribe(IObserver<ProductOffer> observer)
        {
            if (!OfferObservers.Contains(observer))
            {
                OfferObservers.Add(observer);
            }
            return new ProductUnsubscriber();
        }

        public IDisposable Subscribe(IObserver<ProductSale> observer)
        {
            if (!SaleObservers.Contains(observer))
            {
                SaleObservers.Add(observer);
            }
            return new ProductSaleUnsubscriber();
        }

        // Zdarzenie informujace obserwatorow o nowym produkcie
        private void OnProductOffered(ProductOffer productOffer)
        {
            foreach (var obs in OfferObservers)
            {
                obs.OnNext(productOffer);
            }
        }

        // Zdarzenie informujace obserwatorow o sprzedazy produktu
        private void OnProductSelled(ProductSale productSale)
        {
            foreach (var obs in SaleObservers)
            {
                obs.OnNext(productSale);
            }
        }

        public void RegisterForEvent(IObserver<ProductOffer> observer)
        {
            Subscribe(observer);
        }

        public void RegisterForEvent(IObserver<ProductSale> observer)
        {
            Subscribe(observer);
        }

        public void OfferProduct(Product product, int sellerId)
        {
            ProductOffer productOffer = new ProductOffer(product, sellerId);
            OnProductOffered(productOffer);
        }

        public void BuyProduct(Product product, int sellerId)
        {
            ProductSale productSale = new ProductSale(product, sellerId);
            OnProductSelled(productSale);
        }
    }
}
