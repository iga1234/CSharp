using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Workers;


namespace WorkersTests
{
    [TestClass]
    public class WorkerTest
    {
        [TestMethod]
        public void OfficeWorkerTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            OfficeWorker worker1 = new OfficeWorker("Iga", "Cieszkowska", 28, 10, address, 100);
            Assert.AreEqual(1000, worker1.CorpoValue);
        }

        [TestMethod]
        public void OfficeWorkerIntellectAboveRangeTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new OfficeWorker("Iga", "Cieszkowska", 28, 10, address, 170));
        }

        [TestMethod]
        public void OfficeWorkerIntellectBelowRangeTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new OfficeWorker("Iga", "Cieszkowska", 28, 10, address, 20));
        }

        [TestMethod]
        public void TraderLowEffectivenessTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            Trader worker1 = new Trader("Iga", "Cieszkowska", 28, 5, address, EffectivenessEnum.LOW, 50);
            Assert.AreEqual(300, worker1.CorpoValue);
        }

        [TestMethod]
        public void TraderMediumEffectivenessTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            Trader worker1 = new Trader("Iga", "Cieszkowska", 28, 7, address, EffectivenessEnum.MEDIUM, 50);
            Assert.AreEqual(630, worker1.CorpoValue);
        }

        [TestMethod]
        public void TraderHighEffectivenessTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            Trader worker1 = new Trader("Iga", "Cieszkowska", 28, 9, address, EffectivenessEnum.HIGH, 50);
            Assert.AreEqual(1080, worker1.CorpoValue);
        }

        [TestMethod]
        public void TraderBonusAboveRangeTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Trader("Iga", "Cieszkowska", 28, 9, address, EffectivenessEnum.HIGH, 120));
        }

        [TestMethod]
        public void TraderBonusBelowRangeTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Trader("Iga", "Cieszkowska", 28, 9, address, EffectivenessEnum.HIGH, -10));
        }

        [TestMethod]
        public void PhysicalWorkerTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            PhysicalWorker worker1 = new PhysicalWorker("Iga", "Cieszkowska", 25, 3, address, 100);
            Assert.AreEqual(100, worker1.Strength);
            Assert.AreEqual(12.0, worker1.CorpoValue);
        }

        [TestMethod]
        public void PhysicalWorkerStrenghtAboveRangeTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new PhysicalWorker("Iga", "Cieszkowska", 25, 3, address, 200));
        }
        [TestMethod]
        public void PhysicalWorkerStrenghtBelowRangeTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new PhysicalWorker("Iga", "Cieszkowska", 25, 3, address, 0));
        }
    }
}
