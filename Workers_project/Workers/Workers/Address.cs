namespace Workers
{

    public class Address
    {
        public Address(string street, int homeNumber, int apartmentNumber, string city)
        {
            Street = street;
            HomeNumber = homeNumber;
            ApartmentNumber = apartmentNumber;
            City = city;
        }

        public string Street { get; set; }
        public int HomeNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return Street + " " + HomeNumber.ToString() + "/" + ApartmentNumber.ToString() + " " + City;
        }
    }
}
