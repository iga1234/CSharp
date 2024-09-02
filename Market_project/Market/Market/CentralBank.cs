using System;

namespace Market
{
    public class InflationChange
    {
        public InflationChange(double previous, double current)
        {
            PreviousValue = previous;
            CurrentValue = current;
        }

        public double PreviousValue { get; set; }
        public double CurrentValue { get; set; }

        public override string ToString()
        {
            return "Inflation, before: " + PreviousValue + ", after:" + CurrentValue;
        }
    };

    public interface ICentralBank
    {
        public double Tax { get; }

        public void RegisterForEvent(IObserver<InflationChange> observer);
    }

    public class InflationUnsubscriber : IDisposable
    {
        public void Dispose()
        {
        }
    }

    public class CentralBank : IObservable<InflationChange>, IObserver<ProductSale>, ICentralBank, IPrepareTimePeriodComponent, INewTimePeriodComponent
    {
        public CentralBank(double tax, double inflation, double expectedIncome)
        {
            Tax = tax;
            _inflation = inflation; // żeby nie wyzwolić zdarzenia zmiany inflacji
            ExpectedIncome = expectedIncome;
            TaxCollected = 0;
            InflationObservers = new List<IObserver<InflationChange>>();

            ServiceLocator.CentralMarketService.RegisterForEvent(this);
        }

        public double Tax { get; set; }

        private double _inflation;

        public double Inflation
        {
            get => _inflation;
            set
            {
                double previousValue = _inflation;
                _inflation = value;
                InflationChange inflationChange = new InflationChange(previousValue, _inflation);
                OnInflationChange(inflationChange);
            }
        }
        public double ExpectedIncome { get; set; }
        public double TaxCollected { get; set; }

        List<IObserver<InflationChange>> InflationObservers { get; set; }

        public IDisposable Subscribe(IObserver<InflationChange> observer)
        {
            if (!InflationObservers.Contains(observer))
            {
                InflationObservers.Add(observer);
            }
            return new InflationUnsubscriber();
        }

        public void RegisterForEvent(IObserver<InflationChange> observer)
        {
            Subscribe(observer);
        }

        // Zdarzenie informujace obserwatorow o zmianie wartości inflacji
        private void OnInflationChange(InflationChange inflationChange)
        {
            foreach (var obs in InflationObservers)
            {
                obs.OnNext(inflationChange);
            }
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
            TaxCollected += (productSale.Product.GrossPrice - productSale.Product.NetPrice);
        }
    }
}
