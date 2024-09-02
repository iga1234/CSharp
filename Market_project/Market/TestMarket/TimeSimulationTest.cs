using Market;

namespace TestMarket
{
    [TestClass]
    public class TimeSimulationTest
    {
        [TestMethod]
        public void Simulation1()
        {
            const int seed = 24041994;
            const int customerAmount = 5;
            const int sellerAmount = 5;
            const int rounds = 20;

            ServiceLocator.CentralMarketService = new CentralMarket();
            ServiceLocator.CentralBankService = new CentralBank(0.23, 0.02, 200.0);

            RandomDataGenerator generator = new RandomDataGenerator(seed);
            var customers = generator.generateCustomers(customerAmount);
            var sellers = generator.generateSellers(sellerAmount);

            TimeSimulation simulation = new TimeSimulation(customers, sellers);
            simulation.Run(rounds);
        }

        [TestMethod]

        public void Simulation2()
        {
            const int seed = 34524;
            const int customerAmount = 15;
            const int sellerAmount = 5;
            const int rounds = 50;

            ServiceLocator.CentralMarketService = new CentralMarket();
            ServiceLocator.CentralBankService = new CentralBank(0.23, 0.50, 2000.0);

            RandomDataGenerator generator = new RandomDataGenerator(seed);
            var customers = generator.generateCustomers(customerAmount);
            var sellers = generator.generateSellers(sellerAmount);

            TimeSimulation simulation = new TimeSimulation(customers, sellers);
            simulation.Run(rounds);
        }

        [TestMethod]

        public void Simulation3()
        {
            const int seed = 1837363;
            const int customerAmount = 15;
            const int sellerAmount = 10;
            const int rounds = 50;

            ServiceLocator.CentralMarketService = new CentralMarket();
            ServiceLocator.CentralBankService = new CentralBank(0.23, -0.10, 800.0);

            RandomDataGenerator generator = new RandomDataGenerator(seed);
            var customers = generator.generateCustomers(customerAmount);
            var sellers = generator.generateSellers(sellerAmount);

            TimeSimulation simulation = new TimeSimulation(customers, sellers);
            simulation.Run(rounds);
        }

        [TestMethod]

        public void Simulation4()
        {
            const int seed = 273621;
            const int customerAmount = 5;
            const int sellerAmount = 15;
            const int rounds = 50;

            ServiceLocator.CentralMarketService = new CentralMarket();
            ServiceLocator.CentralBankService = new CentralBank(0.23, 0.20, 100.0);

            RandomDataGenerator generator = new RandomDataGenerator(seed);
            var customers = generator.generateCustomers(customerAmount);
            var sellers = generator.generateSellers(sellerAmount);

            TimeSimulation simulation = new TimeSimulation(customers, sellers);
            simulation.Run(rounds);
        }
    }
}
