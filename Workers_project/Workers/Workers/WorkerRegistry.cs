using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workers;

namespace Workers
{
    public class WorkerRegistry
    {
        public WorkerRegistry()

        {
            uniqueId = 0;
            dictionary = new Dictionary<int, WorkerDTO>();
        }

        public int add(WorkerDTO worker)
        {
            int id = uniqueId;
            dictionary.Add(id, worker);
            uniqueId++;
            return id;
        }

        public List<int> add(List<WorkerDTO > workerList)
        {
            List<int> ids = new List<int>();
            foreach (WorkerDTO  worker in workerList)
            {
                ids.Add(add(worker));
            }
            return ids;
        }


        public WorkerDTO findById(int id) 
        {
            return dictionary[id];
        }

        public bool remove(int id)
        {
            return dictionary.Remove(id);
        }

        private int uniqueId;
        private Dictionary<int, WorkerDTO > dictionary;

        public List<KeyValuePair<int, WorkerDTO >> sortWithComparator(IComparer<KeyValuePair<int, WorkerDTO >> comparer)
        {
            return dictionary.OrderBy(worker => worker, comparer).ToList();
        }

        public List<KeyValuePair<int, WorkerDTO >> getWorkersByCity(string city)
        {
            var result = new List<KeyValuePair<int, WorkerDTO >>();
            foreach (var keyValue in dictionary)
            {
                if (keyValue.Value.Address.City.CompareTo(city) == 0)
                {
                    result.Add(new KeyValuePair<int, WorkerDTO >(keyValue.Key, keyValue.Value));
                }
            }
            return result;
        }

        public void printAllWorkers()
        {
            foreach (var keyValue in dictionary)
            {
                Console.WriteLine(keyValue.Value.ToString());
            }
        }

        public void printCollection(List<KeyValuePair<int, WorkerDTO >> collection)
        {
            foreach (var keyValue in collection)
            {
                Console.WriteLine(keyValue.Value.ToString());
            }
        }
    }
}



public class ExperienceComparer : IComparer<KeyValuePair<int, WorkerDTO >>
{
    public int Compare(KeyValuePair<int, WorkerDTO > x, KeyValuePair<int, WorkerDTO > y)
    {
        if (x.Value.Experience == y.Value.Experience)
        {
            return 0;
        }
        return (x.Value.Experience < y.Value.Experience ? 1 : -1);
    }
}

public class AgeComparer : IComparer<KeyValuePair<int, WorkerDTO >>
{
    public int Compare(KeyValuePair<int, WorkerDTO > x, KeyValuePair<int, WorkerDTO > y)
    {
        if (x.Value.Age == y.Value.Age)
        {
            return 0;
        }
        return (x.Value.Age < y.Value.Age ? -1 : 1);
    }
}

public class SurnameComparer : IComparer<KeyValuePair<int, WorkerDTO >>
{
    public int Compare(KeyValuePair<int, WorkerDTO > x, KeyValuePair<int, WorkerDTO > y)
    {
        return x.Value.Surname.CompareTo(y.Value.Surname);
    }
}

public class CombinedComparer : IComparer<KeyValuePair<int, WorkerDTO >>
{
    public List<IComparer<KeyValuePair<int, WorkerDTO >>> ComparerCollection { get; set; }

    public CombinedComparer(List<IComparer<KeyValuePair<int, WorkerDTO >>> comparerCollection)
    {
        ComparerCollection = comparerCollection;
    }

    public int Compare(KeyValuePair<int, WorkerDTO > x, KeyValuePair<int, WorkerDTO > y)
    {
        foreach (var comparer in ComparerCollection)
        {
            var result = comparer.Compare(x, y);
            if (result != 0)
            {
                return result;
            }
        }
        return 0;
    }
}
