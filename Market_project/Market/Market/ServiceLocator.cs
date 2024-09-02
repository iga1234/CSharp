namespace Market
{
    public static class ServiceLocator
    {
        private static ICentralMarket? _centralMarketService;

        private static ICentralBank? _centralBank;

        public static ICentralMarket CentralMarketService
        {
            get
            {
                if (_centralMarketService == null)
                {
                    throw new InvalidOperationException("CentralMarketService not initialized");
                }
                return _centralMarketService;
            }
            set => _centralMarketService = value;
        }

        public static ICentralBank CentralBankService
        {
            get
            {
                if (_centralBank == null)
                {
                    throw new InvalidOperationException("CentralBankService not initialized");
                }
                return _centralBank;
            }
            set => _centralBank = value;
        }
    }
}
