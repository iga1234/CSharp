namespace Market
{
    public interface IPrepareTimePeriodVisitor
    {
        public void Visit(Seller seller);
        public void Visit(Customer customer);
        public void Visit(CentralBank centralBank);
    }

    public class PrepareTimePeriodVisitor : IPrepareTimePeriodVisitor
    {
        public void Visit(Seller seller)
        {
            foreach (var p in seller.ProductCatalog)
            {
                p.Value.CurrentAmount += p.Value.AmountProductedPerPeriod;
            }
        }

        public void Visit(Customer customer)
        {
            customer.GiveSalary();
            customer.ProductWishlist = customer.OriginalWishlist.ConvertAll(wish => new Wish(wish.Id, wish.ExpectedPrice, wish.Priority, wish.IsMustHave));
            customer.DesiredOffers = new Dictionary<int, Tuple<ProductOffer, Wish>>();
        }

        public void Visit(CentralBank centralBank)
        {
            centralBank.TaxCollected = 0;
        }
    }

    public interface IPrepareTimePeriodComponent
    {
        void Accept(IPrepareTimePeriodVisitor visitor);
    }

    public interface INewTimePeriodVisitor
    {
        public void Visit(Seller seller);
        public void Visit(Customer customer);

        public void Visit(CentralBank centralBank);
    }

    public interface INewTimePeriodComponent
    {
        void Accept(INewTimePeriodVisitor visitor);
    }

    public class NewTimePeriodVisitor : INewTimePeriodVisitor
    {
        public void Visit(Seller seller)
        {
            foreach (var p in seller.ProductCatalog)
            {
                if (p.Value.CurrentAmount > 0)
                {
                    seller.OfferProduct(p.Key);
                }
            }
        }

        public void Visit(Customer customer)
        {
            // produkty Must Have przed pozostalymi
            // sortowanie malejace po priorytecie - im wyzszy priorytet tym wazniejszy produkt
            List<Tuple<ProductOffer, Wish>> listOfOffers = new List<Tuple<ProductOffer, Wish>>();
            foreach (var e in customer.DesiredOffers)
            {
                listOfOffers.Add(e.Value);
            }
            listOfOffers.Sort((first, second) =>
            {
                if (first.Item2.IsMustHave && !second.Item2.IsMustHave)
                {
                    return -1;
                }
                else if (!first.Item2.IsMustHave && second.Item2.IsMustHave)
                {
                    return 1;
                }
                return second.Item2.Priority.CompareTo(first.Item2.Priority);
            });

            foreach (var e in listOfOffers)
            {
                if (e.Item1.Product.GrossPrice <= customer.Money)
                {
                    customer.BuyProduct(e.Item1);
                    customer.UnwishProduct(e.Item2);
                }
                else
                {
                    // nie moze kupic wiecej - nie ma pieniedzy
                    break;
                }
            }
        }

        public void Visit(CentralBank centralBank)
        {
            Console.WriteLine("CentralBank - TaxCollected: " + centralBank.TaxCollected);
            Console.WriteLine("CentralBank - Old Inflation: " + centralBank.Inflation * 100 + "%");
            Console.WriteLine("CentralBank - TaxCollected * Inflation: " + centralBank.TaxCollected * centralBank.Inflation);
            Console.WriteLine("CentralBank - ExpectedIncome: " + centralBank.ExpectedIncome);
            if (centralBank.TaxCollected * centralBank.Inflation < centralBank.ExpectedIncome)
            {
                centralBank.Inflation += 0.01;
            }
            else
            {
                centralBank.Inflation -= 0.01;
            }
            Console.WriteLine("CentralBank - New Inflation: " + centralBank.Inflation * 100 + "%");
        }
    }

    public class TimeSimulation
    {
        public List<Customer> Customers { get; set; }

        public List<Seller> Sellers { get; set; }

        public TimeSimulation(List<Customer> customers, List<Seller> sellers)
        {
            Customers = customers;
            Sellers = sellers;
        }

        private void PrepareNextCustomers()
        {
            PrepareTimePeriodVisitor visitor = new PrepareTimePeriodVisitor();

            foreach (Customer c in Customers)
            {
                c.Accept(visitor);
            }
        }

        private void OnNewCustomers()
        {
            NewTimePeriodVisitor visitor = new NewTimePeriodVisitor();

            foreach (Customer c in Customers)
            {
                c.Accept(visitor);
            }
        }

        private void PrepareNextSellers()
        {
            PrepareTimePeriodVisitor visitor = new PrepareTimePeriodVisitor();

            foreach (Seller s in Sellers)
            {
                s.Accept(visitor);
            }
        }

        private void OnNewSellers()
        {
            NewTimePeriodVisitor visitor = new NewTimePeriodVisitor();

            foreach (Seller s in Sellers)
            {
                s.Accept(visitor);
            }
        }

        private void PrepareNextCentralBank()
        {
            PrepareTimePeriodVisitor visitor = new PrepareTimePeriodVisitor();

            var centralBank = ServiceLocator.CentralBankService as CentralBank;
            if (centralBank != null)
            {
                centralBank.Accept(visitor);
            }
        }

        private void OnNewCentralBank()
        {
            NewTimePeriodVisitor visitor = new NewTimePeriodVisitor();

            var centralBank = ServiceLocator.CentralBankService as CentralBank;
            if (centralBank != null)
            {
                centralBank.Accept(visitor);
            }
        }

        public void Run(int rounds)
        {
            PrepareNextSellers();
            PrepareNextCustomers();
            PrepareNextCentralBank();

            for (int i = 0; i < rounds; i++)
            {
                Console.WriteLine("Round " + i + " - START");
                OnNewSellers();
                OnNewCustomers();
                OnNewCentralBank();
                Console.WriteLine("Round " + i + " - END");

                PrepareNextSellers();
                PrepareNextCustomers();
                PrepareNextCentralBank();
            }
        }
    }
}
