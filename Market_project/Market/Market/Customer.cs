using System;

namespace Market
{
    public class Wish
    {
        public Wish(int id, double expectedPrice, int priority, bool mustHave)
        {
            Id = id;
            ExpectedPrice = expectedPrice;
            Priority = priority;
            IsMustHave = mustHave;
        }

        public int Id { get; set; }

        public double ExpectedPrice { get; set; }

        public int Priority { get; set; }

        public bool IsMustHave { get; set; }

        public int CompareTo(Wish otherWish)
        {
            return Priority.CompareTo(otherWish.Priority);
        }
    }

    public class Customer : IObserver<ProductOffer>, IObserver<InflationChange>, IPrepareTimePeriodComponent, INewTimePeriodComponent
    {
        public Customer(List<Wish> productWishlist, double salary)
        {
            ProductWishlist = productWishlist;
            OriginalWishlist = productWishlist.ConvertAll(wish => new Wish(wish.Id,
                    wish.ExpectedPrice,
                    wish.Priority,
                    wish.IsMustHave));
            DesiredOffers = new Dictionary<int, Tuple<ProductOffer, Wish>>();
            Money = 0;
            Salary = salary;
            InflationModifier = 1.0;
            Id = Counter;
            Counter++;

            ServiceLocator.CentralMarketService.RegisterForEvent(this);
        }

        public List<Wish> ProductWishlist { get; set; }

        public List<Wish> OriginalWishlist { get; set; }

        public Dictionary<int, Tuple<ProductOffer, Wish>> DesiredOffers { get; set; }

        public double Money { get; set; }

        public double Salary { get; set; }

        public double InflationModifier { get; set; }

        public int Id { get; set; }

        private static int Counter { get; set; }

        public void GiveSalary()
        {
            Money += Salary;
        }

        public void BuyProduct(ProductOffer productOffer)
        {
            Console.WriteLine("Client " + Id + " - BuyProduct: " + productOffer.Product);
            Money -= productOffer.Product.GrossPrice;
            ServiceLocator.CentralMarketService.BuyProduct(productOffer.Product, productOffer.SellerId);
        }

        public void Accept(IPrepareTimePeriodVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void Accept(INewTimePeriodVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void WishProduct(Wish wish)
        {
            ProductWishlist.Add(wish);
        }

        public bool UnwishProduct(Wish wish)
        {
            return (ProductWishlist.RemoveAll(w => w.Id == wish.Id) != 0);
        }

        public bool UnwishProduct(int id)
        {
            return (ProductWishlist.RemoveAll(w => w.Id == id) != 0);
        }

        public Wish? FindWish(Product product)
        {
            return ProductWishlist.Find(w => w.Id == product.Id);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(ProductOffer productOffer)
        {
            Wish? wish = FindWish(productOffer.Product);
            if (wish != null)
            {
                if ((wish.IsMustHave || productOffer.Product.GrossPrice * InflationModifier <= wish.ExpectedPrice) && productOffer.Product.GrossPrice <= Money)
                {
                    if (!DesiredOffers.ContainsKey(productOffer.Product.Id) || productOffer.Product.GrossPrice < DesiredOffers[productOffer.Product.Id].Item1.Product.GrossPrice)
                    {
                        DesiredOffers[productOffer.Product.Id] = new Tuple<ProductOffer, Wish>(productOffer, wish);
                    }
                }
            }
        }

        public void OnNext(InflationChange inflationChange)
        {
            if (inflationChange.PreviousValue < inflationChange.CurrentValue)
            {
                // inflacja rosnie
                if (InflationModifier < 2.0)
                {
                    InflationModifier += 0.1;
                }
            }
            else
            {
                // inflacja maleje
                if (InflationModifier > 0.0)
                {
                    InflationModifier -= 0.1;
                }
            }
        }
    }
}
