using Workers;

namespace WorkersTests
{
    [TestClass]
    public class WorkerRegistryTest
    {
        [TestMethod]
        public void ExperienceComparerTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            OfficeWorker worker1 = new OfficeWorker("Lukasz", "Watka", 29, 5, address, 100);
            OfficeWorker worker2 = new OfficeWorker("Iga", "Cieszkowska", 28, 1, address, 150);
            OfficeWorker worker3 = new OfficeWorker("Jan", "Kowalski", 40, 10, address, 75);

            WorkerRegistry registry = new WorkerRegistry();
            registry.add(worker1);
            registry.add(worker2);
            registry.add(worker3);
            ExperienceComparer comparer = new ExperienceComparer();

            var sortedCol = registry.sortWithComparator(comparer);
            registry.printCollection(sortedCol);

            Assert.AreEqual(10, sortedCol[0].Value.Experience);
            Assert.AreEqual(5, sortedCol[1].Value.Experience);
            Assert.AreEqual(1, sortedCol[2].Value.Experience);
        }

        [TestMethod]
        public void AgeComparerTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            OfficeWorker worker1 = new OfficeWorker("Lukasz", "Watka", 29, 5, address, 100);
            OfficeWorker worker2 = new OfficeWorker("Iga", "Cieszkowska", 28, 1, address, 150);
            OfficeWorker worker3 = new OfficeWorker("Jan", "Kowalski", 40, 10, address, 75);

            WorkerRegistry registry = new WorkerRegistry();
            registry.add(worker1);
            registry.add(worker2);
            registry.add(worker3);
            AgeComparer comparer = new AgeComparer();

            var sortedCol = registry.sortWithComparator(comparer);
            registry.printCollection(sortedCol);

            Assert.AreEqual(28, sortedCol[0].Value.Age);
            Assert.AreEqual(29, sortedCol[1].Value.Age);
            Assert.AreEqual(40, sortedCol[2].Value.Age);
        }

        [TestMethod]
        public void SurnameComparerTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            OfficeWorker worker1 = new OfficeWorker("Lukasz", "Watka", 29, 5, address, 100);
            OfficeWorker worker2 = new OfficeWorker("Iga", "Cieszkowska", 28, 1, address, 150);
            OfficeWorker worker3 = new OfficeWorker("Jan", "Kowalski", 40, 10, address, 75);

            WorkerRegistry registry = new WorkerRegistry();
            registry.add(worker1);
            registry.add(worker2);
            registry.add(worker3);
            SurnameComparer comparer = new SurnameComparer();

            var sortedCol = registry.sortWithComparator(comparer);
            registry.printCollection(sortedCol);

            Assert.AreEqual("Cieszkowska", sortedCol[0].Value.Surname);
            Assert.AreEqual("Kowalski", sortedCol[1].Value.Surname);
            Assert.AreEqual("Watka", sortedCol[2].Value.Surname);
        }

        [TestMethod]
        public void ExperienceAgeSurnameComparerResultByExperienceTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            OfficeWorker worker1 = new OfficeWorker("Lukasz", "Watka", 29, 5, address, 100);
            OfficeWorker worker2 = new OfficeWorker("Iga", "Cieszkowska", 28, 1, address, 150);
            OfficeWorker worker3 = new OfficeWorker("Jan", "Kowalski", 40, 10, address, 75);

            WorkerRegistry registry = new WorkerRegistry();
            registry.add(worker1);
            registry.add(worker2);
            registry.add(worker3);

            var comparerList = new List<IComparer<KeyValuePair<int, WorkerDTO>>>();
            comparerList.Add(new ExperienceComparer());
            comparerList.Add(new AgeComparer());
            comparerList.Add(new SurnameComparer());
            CombinedComparer combinedComparer = new CombinedComparer(comparerList);

            var sortedCol = registry.sortWithComparator(combinedComparer);
            registry.printCollection(sortedCol);

            Assert.AreEqual(10, sortedCol[0].Value.Experience);
            Assert.AreEqual(5, sortedCol[1].Value.Experience);
            Assert.AreEqual(1, sortedCol[2].Value.Experience);
        }

        [TestMethod]
        public void ExperienceAgeSurnameComparerResultByAgeTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            OfficeWorker worker1 = new OfficeWorker("Lukasz", "Watka", 29, 10, address, 100);
            OfficeWorker worker2 = new OfficeWorker("Iga", "Cieszkowska", 28, 10, address, 150);
            OfficeWorker worker3 = new OfficeWorker("Jan", "Kowalski", 40, 10, address, 75);

            WorkerRegistry registry = new WorkerRegistry();
            registry.add(worker1);
            registry.add(worker2);
            registry.add(worker3);

            var comparerList = new List<IComparer<KeyValuePair<int, WorkerDTO>>>();
            comparerList.Add(new ExperienceComparer());
            comparerList.Add(new AgeComparer());
            comparerList.Add(new SurnameComparer());
            CombinedComparer combinedComparer = new CombinedComparer(comparerList);

            var sortedCol = registry.sortWithComparator(combinedComparer);
            registry.printCollection(sortedCol);

            Assert.AreEqual(28, sortedCol[0].Value.Age);
            Assert.AreEqual(29, sortedCol[1].Value.Age);
            Assert.AreEqual(40, sortedCol[2].Value.Age);
        }

        [TestMethod]
        public void ExperienceAgeSurnameComparerResultBySurnameTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            OfficeWorker worker1 = new OfficeWorker("Lukasz", "Watka", 29, 10, address, 100);
            OfficeWorker worker2 = new OfficeWorker("Iga", "Cieszkowska", 29, 10, address, 150);
            OfficeWorker worker3 = new OfficeWorker("Jan", "Kowalski", 29, 10, address, 75);

            WorkerRegistry registry = new WorkerRegistry();
            registry.add(worker1);
            registry.add(worker2);
            registry.add(worker3);

            var comparerList = new List<IComparer<KeyValuePair<int, WorkerDTO>>>();
            comparerList.Add(new ExperienceComparer());
            comparerList.Add(new AgeComparer());
            comparerList.Add(new SurnameComparer());
            CombinedComparer combinedComparer = new CombinedComparer(comparerList);

            var sortedCol = registry.sortWithComparator(combinedComparer);
            registry.printCollection(sortedCol);

            Assert.AreEqual("Cieszkowska", sortedCol[0].Value.Surname);
            Assert.AreEqual("Kowalski", sortedCol[1].Value.Surname);
            Assert.AreEqual("Watka", sortedCol[2].Value.Surname);
        }

        [TestMethod]
        public void GetWorkersByCityTest()
        {
            Address address1 = new Address("Suwalska", 40, 1, "Elblag");
            Address address2 = new Address("Bartoszcze", 11, 2, "Inowroclaw");
            OfficeWorker worker1 = new OfficeWorker("Lukasz", "Watka", 29, 10, address1, 100);
            OfficeWorker worker2 = new OfficeWorker("Iga", "Cieszkowska", 29, 10, address2, 150);
            OfficeWorker worker3 = new OfficeWorker("Jan", "Kowalski", 29, 15, address1, 75);
            OfficeWorker worker4 = new OfficeWorker("Adam", "Adamiak", 35, 5, address2, 75);

            WorkerRegistry registry = new WorkerRegistry();
            registry.add(worker1);
            registry.add(worker2);
            registry.add(worker3);
            registry.add(worker4);

            var elblagWorkers = registry.getWorkersByCity("Elblag");
            registry.printCollection(elblagWorkers);

            Assert.AreEqual("Elblag", elblagWorkers[0].Value.Address.City);
            Assert.AreEqual("Elblag", elblagWorkers[1].Value.Address.City);
            Assert.AreEqual(2, elblagWorkers.Count());
        }

        [TestMethod]
        public void RemoveWorkerTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            OfficeWorker worker1 = new OfficeWorker("Lukasz", "Watka", 29, 5, address, 100);
            OfficeWorker worker2 = new OfficeWorker("Iga", "Cieszkowska", 28, 1, address, 150);
            OfficeWorker worker3 = new OfficeWorker("Jan", "Kowalski", 40, 10, address, 75);

            WorkerRegistry registry = new WorkerRegistry();
            var id = registry.add(worker1);
            var id2 = registry.add(worker2);
            var id3 = registry.add(worker3);

            Assert.IsTrue(registry.remove(id));
            Assert.IsFalse(registry.remove(id));
            
        }

        [TestMethod]
        public void RemoveWorkerFromEmptyRegistryTest()
        {
            WorkerRegistry registry = new WorkerRegistry();
            Assert.IsFalse(registry.remove(5));

        }

        [TestMethod]
        public void IdUniquenessTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            OfficeWorker worker1 = new OfficeWorker("Lukasz", "Watka", 29, 5, address, 100);
            OfficeWorker worker2 = new OfficeWorker("Iga", "Cieszkowska", 28, 1, address, 150);
            OfficeWorker worker3 = new OfficeWorker("Jan", "Kowalski", 40, 10, address, 75);

            WorkerRegistry registry = new WorkerRegistry();
            var id = registry.add(worker1);
            var id2 = registry.add(worker2);
            var id3 = registry.add(worker3);

            Assert.AreNotEqual(id, id2);
            Assert.AreNotEqual(id2, id3);
        }

        [TestMethod]
        public void FindIdExceptionTest()
        {
            WorkerRegistry registry = new WorkerRegistry();
            Assert.ThrowsException<KeyNotFoundException>(() => registry.findById(5));

        }

        [TestMethod]
        public void FindWorkerByIdTest()
        {
            Address address = new Address("Suwalska", 40, 1, "Elblag");
            OfficeWorker worker1 = new OfficeWorker("Lukasz", "Watka", 29, 5, address, 100);


            WorkerRegistry registry = new WorkerRegistry();
            var id = registry.add(worker1);

            var worker = registry.findById(id);
            Assert.AreEqual(worker, worker1);

        }
    }
}
