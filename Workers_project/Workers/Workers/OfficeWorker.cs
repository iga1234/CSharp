namespace Workers
{
    public class OfficeWorkerDTO : WorkerDTO
    {
        public OfficeWorkerDTO(string name, string surname, int age, int experience, Address address, int intellect)
            : base(name, surname, age, experience, address)
        {
            Intellect = intellect;
        }

        public virtual int Intellect { get; set; }
    }

    public class OfficeWorker : OfficeWorkerDTO, IWorker
    {
        private static int sOfficeCounter = 0;

        public OfficeWorker(string name, string surname, int age, int experience, Address address, int intellect)
            : base(name, surname, age, experience, address, intellect)
        {
            OfficeId = sOfficeCounter;
            sOfficeCounter += 1;
        }

        public int OfficeId { get; set; }

        public override int Intellect
        {
            get { return base.Intellect; }
            set
            {
                if (value >= 70 && value <= 150)
                {
                    base.Intellect = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Intellect));
                }
            }
        }

        public double CorpoValue
        {
            get { return Experience * Intellect; }
        }

        public override string ToString()
        {
            return Name + " " + Surname + ", wiek: " + Age + ", doświadczenie: " + Experience + ", adres: " + Address.ToString() + ", korpo wartość: " + CorpoValue + ", efektywność: " + OfficeId + ", prowizja: " + Intellect;
        }
    }
}
