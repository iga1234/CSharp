using System.Runtime.CompilerServices;

namespace Workers
{
    public abstract class WorkerDTO
    {
        public WorkerDTO(string name, string surname, int age, int experience, Address address)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Experience = experience;
            Address = address;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public Address Address { get; set; }
    }

    public interface IWorker
    {
        public double CorpoValue { get; }

        public string ToString();
    }
}


