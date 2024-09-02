using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Workers.Trader;

namespace Workers
{

    public enum EffectivenessEnum
    {
        LOW = 60,
        MEDIUM = 90,
        HIGH = 120
    }

    public class TraderDTO : WorkerDTO
    {
        public TraderDTO(string name, string surname, int age, int experience, Address address, EffectivenessEnum effectiveness, int bonus)
        : base(name, surname, age, experience, address)
        {
            Effectiveness = effectiveness;
            Bonus = bonus;
        }
        public virtual EffectivenessEnum Effectiveness{ get; set; }
        public virtual int Bonus { get; set; }

    }

    public class Trader : TraderDTO, IWorker
    {
        public Trader(string name, string surname, int age, int experience, Address address, EffectivenessEnum effectiveness, int bonus)
            : base(name, surname, age, experience, address, effectiveness, bonus)
        {
        }


        public override int Bonus
        {
            get { return base.Bonus; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    base.Bonus = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Bonus));
                }
            }
        }

        
        public double CorpoValue
        {
            get { return Experience * (int)Effectiveness; }
        }

        public override string ToString()
        {
            return Name + " " + Surname + ", wiek: " + Age + ", doświadczenie: " + Experience + ", adres: " + Address.ToString() + ", korpo wartość: " + CorpoValue + ", efektywność: " + Effectiveness + ", prowizja: " + Bonus + "%";
        }
    }
}
