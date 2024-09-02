using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    public class PhysicalWorkerDTO : WorkerDTO
    {
        public PhysicalWorkerDTO(string name, string surname, int age, int experience, Address address, int strength)
            : base(name, surname, age, experience, address)
        {
            Strength = strength;
        }

        public virtual int Strength { get; set; }
    }

    public class PhysicalWorker : PhysicalWorkerDTO, IWorker
    {

        public PhysicalWorker(string name, string surname, int age, int experience, Address address, int strength) 
            : base(name, surname, age, experience, address, strength)
        {
        }
        
        public override int Strength
        {
            get { return base.Strength; }

            set
            {

                if (value >= 1 && value <= 100)
                {
                    base.Strength = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Strength));
                }
            }
        }

        public double CorpoValue
        {
            get { return (double)Experience * (double)Strength / (double)Age; }
        }

        public override string ToString()
        {
            return Name + " " + Surname + ", wiek: " + Age + ", doświadczenie: " + Experience + ", adres: " + Address.ToString() + ", korpo wartość: " + CorpoValue + ", siła: " + Strength;
        }
    }
}


