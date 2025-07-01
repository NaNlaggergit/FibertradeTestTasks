namespace Task1
{
    public class Home
    {
        private const int ApartmentsPerFloor = 4;
        private int ApartmentsPerEntrance { get; }
        private int ApartmentsInHouse { get; }
        public int Floors { get; }
        public int Entrances { get; }

        private readonly string[] positionDescriptions =
        {
            "",
            "ближняя слева",
            "дальняя слева",
            "дальняя справа",
            "ближняя справа"
        };

        public Home(int floors, int entrances)
        {
            Floors = floors;
            Entrances = entrances;
            ApartmentsPerEntrance = Floors * ApartmentsPerFloor;
            ApartmentsInHouse = Entrances * ApartmentsPerEntrance;
        }

        public void FindLocateApartment(int apartmentNumber)
        {
            if (apartmentNumber < 1 || apartmentNumber > ApartmentsInHouse)
            {
                Console.WriteLine("Квартиры с таким номером нет в доме.");
                return;
            }

            int entrance = (apartmentNumber - 1) / ApartmentsPerEntrance + 1;
            int localApartment = apartmentNumber - (entrance - 1) * ApartmentsPerEntrance;
            int floor = (localApartment - 1) / ApartmentsPerFloor + 1;
            int position = (localApartment - 1) % ApartmentsPerFloor + 1;

            string positionName = position switch
            {
                1 => "ближняя слева",
                2 => "дальняя слева",
                3 => "дальняя справа",
                4 => "ближняя справа",
                _ => "неизвестно"
            };
            Console.WriteLine($"Квартира находится в подъезде № {entrance}, на этаже № {floor}, {positionName}.");
        }

        public void FindLocateApartmentWithoutSwitch(int apartmentNumber) //без switch
        {
            if (apartmentNumber < 1 || apartmentNumber > ApartmentsInHouse)
            {
                Console.WriteLine("Квартиры с таким номером нет в доме.");
                return;
            }

            int entrance = (apartmentNumber - 1) / ApartmentsPerEntrance + 1;
            int localApartment = apartmentNumber - (entrance - 1) * ApartmentsPerEntrance;
            int floor = (localApartment - 1) / ApartmentsPerFloor + 1;
            int position = (localApartment - 1) % ApartmentsPerFloor + 1;
            Console.WriteLine($"Квартира находится в подъезде № {entrance}, на этаже № {floor}, {positionDescriptions[position]}.");
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите число этажей: ");
            int floors;
            while (!int.TryParse(Console.ReadLine(), out floors))
            {
                Console.Write("Ошибка! Введите корректное число: ");
            }

            Console.Write("Введите количество подъездов: ");
            int entrances;
            while (!int.TryParse(Console.ReadLine(), out entrances))
            {
                Console.Write("Ошибка! Введите корректное число: ");
            }

            Console.Write("Введите номер квартиры: ");
            int apartmentNumber;
            while (!int.TryParse(Console.ReadLine(), out apartmentNumber))
            {
                Console.Write("Ошибка! Введите корректное число: ");
            }

            Home home = new Home(floors, entrances);
            home.FindLocateApartmentWithoutSwitch(apartmentNumber);
        }
    }
}