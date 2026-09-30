using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AIS_Practica_One_Model
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string dataDirectory = "C:\\Users\\eleinar\\source\\repos\\AIS_Practica_One";

            Logic logic = new Logic(dataDirectory);


            while (true)
            {
                Console.Clear();
                ShowMenu();

                Console.Write("\nВыберите действие: ");
                string? choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1": ShowAllCars(logic); break;
                    case "2": FindCar(logic); break;
                    case "3": AddCar(logic); break;
                    case "4": UpdateCar(logic); break;
                    case "5": DeleteCar(logic); break;
                    case "6": ShowAllBrands(logic); break;
                    case "7": AddBrand(logic); break;
                    case "8": UpdateBrand(logic); break;
                    case "9": DeleteBrand(logic); break;
                    case "10": ShowCarsByBrand(logic); break;
                    case "11": ShowCarsByFuelConsumption(logic); break;
                    case "12": logic.SaveAll(); Console.WriteLine("Данные сохранены."); break;
                    case "0": logic.SaveAll(); return;
                    default: Console.WriteLine("Неизвестная команда."); break;
                }
            }
        }

       
        private static void ShowMenu()
        {
            Console.WriteLine("АВТОПАРК\n");
            Console.WriteLine("1. Показать все автомобили");
            Console.WriteLine("2. Найти автомобиль по госномеру");
            Console.WriteLine("3. Добавить автомобиль");
            Console.WriteLine("4. Изменить автомобиль");
            Console.WriteLine("5. Удалить автомобиль\n");
            Console.WriteLine("6. Показать все бренды");
            Console.WriteLine("7. Добавить бренд");
            Console.WriteLine("8. Изменить бренд");
            Console.WriteLine("9. Удалить бренд\n");
            Console.WriteLine("10. Автомобили выбранного бренда");
            Console.WriteLine("11. Автомобили с заданным расходом топлива\n");
            Console.WriteLine("12. Сохранить данные");
            Console.WriteLine("0. Выход");
        }

       
        private static void PrintCar(Logic logic, Car car)
        {
            Console.WriteLine($"Госномер: {car.LicensePlate}");
            Console.WriteLine($"Бренд: {logic.GetBrandName(car.BrandID)}");
            Console.WriteLine($"Модель: {car.ModelName}");
            Console.WriteLine($"Год: {car.ProductionYear}");
            Console.WriteLine($"Мощность: {car.HorsePower} л.с.");
            Console.WriteLine($"Объём бака: {car.FuelTankVolume} л");
            Console.WriteLine($"Расход: {car.FuelConsumption} л/100 км");
            Console.WriteLine($"Цвет: {car.Color}\n");
        }

        
        private static void PrintCars(Logic logic, List<Car> cars)
        {
            if (cars.Count == 0) Console.WriteLine("Автомобили не найдены.");
            foreach (Car car in cars)
            {
                PrintCar(logic, car);
            }

            Console.WriteLine("\nПродолжить...");
            Console.ReadLine();
        }

        
        private static void PrintBrands(Logic logic)
        {
            foreach (Brand brand in logic.GetAllBrands())
                Console.WriteLine($"{brand.ID}: {brand.Name} — {brand.Country}");

            Console.WriteLine("\nПродолжить...");
            Console.ReadLine();
        }

        
        private static void ShowAllCars(Logic logic)
        {
            Console.WriteLine("АВТОМОБИЛИ\n");
            PrintCars(logic, logic.GetAllCars());

        }

        
        private static void FindCar(Logic logic)
        {
            Console.Write("Госномер или его начало: ");
            PrintCars(logic, logic.SearchCarsByLicensePlate(ReadString()));
        }

        
        private static void AddCar(Logic logic)
        {
            Console.WriteLine("ДОБАВЛЕНИЕ АВТОМОБИЛЯ\n");
            PrintBrands(logic);
            Console.Write("Госномер: ");
            string plate = ReadString();
            int brandId = ReadInt("ID бренда: ");
            Car car = ReadCarDetails(plate, brandId);
            Console.WriteLine(logic.AddCar(car) ? "Автомобиль добавлен." : "Не удалось добавить автомобиль.");

        }

        
        private static void UpdateCar(Logic logic)
        {
            Console.Write("Госномер автомобиля: ");
            string plate = ReadString();
            Car? car = logic.GetCarById(plate);
            if (car == null)
            {
                Console.WriteLine("Автомобиль не найден.");
                return;
            }

            PrintCar(logic, car);
            PrintBrands(logic);
            int brandId = ReadInt("Новый ID бренда: ");
            Car updated = ReadCarDetails(car.LicensePlate, brandId);
            Console.WriteLine(logic.UpdateCar(updated) ? "Автомобиль изменён." : "Не удалось изменить автомобиль.");

            Console.WriteLine("\nПродолжить...");
            Console.ReadLine();
        }

        
        private static void DeleteCar(Logic logic)
        {
            Console.Write("Госномер автомобиля: ");
            string plate = ReadString();
            Car? car = logic.GetCarById(plate);
            if (car == null)
            {
                Console.WriteLine("Автомобиль не найден.");
                return;
            }

            PrintCar(logic, car);
            Console.Write("Удалить? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLowerInvariant() != "y") return;
            Console.WriteLine(logic.DeleteCar(plate) ? "Автомобиль удалён." : "Не удалось удалить автомобиль.");

            Console.WriteLine("\nПродолжить...");
            Console.ReadLine();
        }

        
        private static Car ReadCarDetails(string plate, int brandId)
        {
            Console.Write("Модель: ");
            string model = ReadString();
            int year = ReadInt("Год выпуска: ");
            float power = ReadFloat("Мощность (л.с.): ");
            float tank = ReadFloat("Объём бака (л): ");
            float consumption = ReadFloat("Расход (л/100 км): ");
            Console.Write("Цвет: ");
            string color = ReadString();

            Console.WriteLine("\nПродолжить...");
            Console.ReadLine();

            return new Car
            {
                LicensePlate = plate,
                BrandID = brandId,
                ModelName = model,
                ProductionYear = year,
                HorsePower = power,
                FuelTankVolume = tank,
                FuelConsumption = consumption,
                Color = color
            };
            

        }

        
        private static void ShowAllBrands(Logic logic)
        {
            Console.WriteLine("БРЕНДЫ\n");
            PrintBrands(logic);
        }

        
        private static void AddBrand(Logic logic)
        {
            Console.Write("Название: ");
            string name = ReadString();
            Console.Write("Страна: ");
            string country = ReadString();
            Brand brand = new() { Name = name, Country = country };
            Console.WriteLine(logic.AddBrand(brand) ? $"Бренд добавлен. ID: {brand.ID}" : "Не удалось добавить бренд.");

            Console.WriteLine("\nПродолжить...");
            Console.ReadLine();
        }

        private static void UpdateBrand(Logic logic)
        {
            PrintBrands(logic);
            int id = ReadInt("ID бренда: ");
            Console.Write("Новое название: ");
            string name = ReadString();
            Console.Write("Новая страна: ");
            string country = ReadString();
            Brand brand = new() { ID = id, Name = name, Country = country };
            Console.WriteLine(logic.UpdateBrand(brand) ? "Бренд изменён." : "Не удалось изменить бренд.");

            Console.WriteLine("\nПродолжить...");
            Console.ReadLine();
        }

           private static void DeleteBrand(Logic logic)
        {
            PrintBrands(logic);
            int id = ReadInt("ID бренда: ");
            Console.Write("Удалить? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLowerInvariant() != "y") return;
            Console.WriteLine(logic.DeleteBrand(id) ? "Бренд удалён." : "Не удалось удалить бренд.");

            Console.WriteLine("\nПродолжить...");
            Console.ReadLine();
        }

        private static void ShowCarsByBrand(Logic logic)
        {
            PrintBrands(logic);
            int id = ReadInt("ID бренда: ");
            PrintCars(logic, logic.GetCarsByBrand(id));
        }

        private static void ShowCarsByFuelConsumption(Logic logic)
        {
            float max = ReadFloat("Максимальный расход (л/100 км): ");
            PrintCars(logic, logic.GetCarsByFuelConsuption(max));
        }

        private static string ReadString() => Console.ReadLine()?.Trim() ?? string.Empty;

        private static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value)) return value;
                Console.WriteLine("Введите целое число.");
            }
        }

      
        private static float ReadFloat(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = (Console.ReadLine() ?? "").Replace(',', '.');
                if (float.TryParse(input, out float value)) return value;
                Console.WriteLine("Введите число.");
            }
        }

    }
}