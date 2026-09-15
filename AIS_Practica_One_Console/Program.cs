using System;
using System.ComponentModel.DataAnnotations;

namespace AIS_Practica_One_Model
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string dataDirectory = "Data";

            Logic logic = new Logic(dataDirectory);

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                ShowMenu();

                Console.Write("\nВыберите действие: ");
                string? choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1":
                        ShowAllCars(logic);
                        break;

                    case "2":
                        FindCar(logic);
                        break;

                    case "3":
                        AddCar(logic);
                        break;

                    case "4":
                        UpdateCar(logic);
                        break;

                    case "5":
                        DeleteCar(logic);
                        break;

                    case "6":
                        ShowAllBrands(logic);
                        break;

                    case "7":
                        AddBrand(logic);
                        break;

                    case "8":
                        UpdateBrand(logic);
                        break;

                    case "9":
                        DeleteBrand(logic);
                        break;

                    case "10":
                        ShowCarsByBrand(logic);
                        break;

                    case "11":
                        ShowCarsByFuelConsumption(logic);
                        break;

                    case "12":
                        SaveData(logic);
                        break;

                    case "0":
                        logic.SaveAll();
                        isRunning = false;
                        Console.WriteLine("Данные сохранены.");
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда.");
                        Console.WriteLine("\nНажмите Enter для продолжения...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("================================");
            Console.WriteLine("     УПРАВЛЕНИЕ МАФЫНКАМИ");
            Console.WriteLine("================================");

            Console.WriteLine("1. Показать все автомобили");
            Console.WriteLine("2. Найти автомобиль по ID");
            Console.WriteLine("3. Добавить автомобиль");
            Console.WriteLine("4. Изменить автомобиль");
            Console.WriteLine("5. Удалить автомобиль");

            Console.WriteLine();

            Console.WriteLine("6. Показать все бренды");
            Console.WriteLine("7. Добавить бренд");
            Console.WriteLine("8. Изменить бренд");
            Console.WriteLine("9. Удалить бренд");

            Console.WriteLine();

            Console.WriteLine("10. Автомобили выбранного бренда");
            Console.WriteLine("11. Автомобили с заданным расходом топлива");

            Console.WriteLine();

            Console.WriteLine("12. Сохранить данные");
            Console.WriteLine("0. Выход");
            Console.WriteLine("================================");
        }

        static void PrintCar(Logic logic, Car car)
        {
            string brandName = logic.GetBrandName(car.BrandID);

            Console.WriteLine($"ID: {car.ID}");
            Console.WriteLine($"Бренд: {brandName}");
            Console.WriteLine($"Модель: {car.ModelName}");
            Console.WriteLine($"Год выпуска: {car.ProductionYear}");
            Console.WriteLine($"Мощность: {car.HorsePower} л.с.");
            Console.WriteLine($"Объем топливного бака: {car.FuelTankVolume} л.");
            Console.WriteLine($"Расход топлива: {car.FuelConsumption} л/100км");
            Console.WriteLine($"Цвет: {car.Color}");
            Console.WriteLine();
        }

        static void ShowAllCars(Logic logic)
        {
            List<Car> cars = logic.GetAllCars();

            Console.WriteLine("АВТОМОБИЛИ\n");

            if(cars.Count == 0)
            {
                Console.WriteLine("Автомобилей нет");
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
                return;
            }

            foreach (Car car in cars)
            {
                PrintCar(logic, car);
            }

            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }

        static void FindCar(Logic logic)
        {
            Console.WriteLine("========== ПОИСК АВТОМОБИЛЯ ==========\n");

            int id = ReadInt("Введите ID автомобиля: ");
            Car? car = logic.GetCarById(id);

            if(car == null)
            {
                Console.WriteLine("Автомобиль не найдей.");
            }
            else
            {
                PrintCar(logic, car);
            }

            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();

        }

        static void AddCar(Logic logic)
        {
            Console.WriteLine("========== ДОБАВЛЕНИЕ АВТОМОБИЛЯ ==========\n");

            ShowAllBrands(logic);

            Console.WriteLine();

            int brandID = ReadInt("Введите ID бренда: ");

            if (logic.GetCarById(brandID) == null)
            {
                Console.WriteLine("Бренд с таким ID не найден.");
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
                return;
            }

            Console.Write("Модель: ");
            string modelName = ReadString();

            int productionYear = ReadInt("Год выпуска: ");

            float horsePower = ReadFloat("Мощность (л.с.): ");

            float fuelTankVolume = ReadFloat("Объем топливного бака (л): ");

            float fuelConsumption = ReadFloat("Расход топлива (л/100км): ");

            Console.Write("Цвет: ");
            string color = ReadString();

            Car car = new Car
            {
                BrandID = brandID,
                ModelName = modelName,
                ProductionYear = productionYear,
                HorsePower = horsePower,
                FuelTankVolume = fuelTankVolume,
                FuelConsumption = fuelConsumption,
                Color = color
            };

            bool result = logic.AddCar(car);

            if (result)
            {
                Console.WriteLine("\nАвтомобиль успешно добавлен.");
                Console.WriteLine($"Присвоенный ID: {car.ID}");
            }
            else
            {
                Console.WriteLine("\nНе удалось добавить автомобиль.");
            }

            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }

        static void ShowAllBrands(Logic logic)
        {
            Console.WriteLine("========== БРЕНДЫ ==========\n");

            List<Brand> brands = logic.GetAllBrands();

            if(brands.Count == 0)
            {
                Console.WriteLine("Брендов нет");
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
                return;
            }

            foreach (Brand brand in brands)
            {
                Console.WriteLine($"ID: {brand.ID} | Название: {brand.Name} | Страна: {brand.Country}");
            }
        }

        static void UpdateCar(Logic logic)
        {
            Console.WriteLine("========== ИЗМЕНЕНИЕ АВТОМОБИЛЯ ==========\n");

            int id = ReadInt("Введите ID автомобиля для изменения: ");
            Car? car = logic.GetCarById(id);

            if(car == null)
            {
                Console.WriteLine("Автомобиль с таким ID не найден.");
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nТекущие данные автомобиля:");
            PrintCar(logic, car);
        }

        static void DeleteCar(Logic logic)
        {
            Console.WriteLine("========== УДАЛЕНИЕ АВТОМОБИЛЯ ==========\n");

            int id = ReadInt("Введите ID автомобиля для удаления: ");

            Car? car = logic.GetCarById(id);

            if(car==null)
            {
                Console.WriteLine("Автомобиль с таким ID не найден.");
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("\nУдаляемый автомобиль:");
            PrintCar(logic, car);
            Console.WriteLine("\nВы уверены, что хотите удалить этот автомобиль? (y/n): ");
            string? answer = Console.ReadLine();

            if(answer?.ToLower() != "y")
            {
                Console.WriteLine("Удаление отменено.");
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
                return;
            }
            bool result = logic.DeleteCar(id);
            if (result)
            {
                Console.WriteLine("\nАвтомобиль успешно удален.");
            }
            else
            {
                Console.WriteLine("\nНе удалось удалить автомобиль."); 
            }

            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();


        }

        static int ReadInt(string value)
        {
            while (true)
            {
                Console.Write(value);

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите целое число.");
                }
            }
        }

        static float ReadFloat(string value)
        {
            while (true)
            {
                Console.Write(value);

                string? input = Console.ReadLine();

                input = input?.Replace(',', '.');

                if (float.TryParse(
                    input,
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out float result))
                {
                    return result;
                }

                Console.WriteLine("Ошибка: введите число.");
            }
        }

        static string ReadString()
        {
            while (true)
            {
                string? value = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value.Trim();
                }

                Console.WriteLine(
                    "Значение не может быть пустым.");
                Console.Write("Повторите ввод: ");
            }
        }

        static void AddBrand(Logic logic)
        {
            Console.WriteLine("========== ДОБАВЛЕНИЕ БРЕНДА ==========\n");
            Console.Write("Название бренда: ");
            string name = ReadString();
            Console.Write("Страна бренда: ");
            string country = ReadString();
            Brand brand = new Brand
            {
                Name = name,
                Country = country
            };
            bool result = logic.AddBrand(brand);
            if (result)
            {
                Console.WriteLine("\nБренд успешно добавлен.");
                Console.WriteLine($"Присвоенный ID: {brand.ID}");
            }
            else
            {
                Console.WriteLine("\nНе удалось добавить бренд.");
            }
            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }

        static void UpdateBrand(Logic logic)
        {
            Console.WriteLine("========== ИЗМЕНЕНИЕ БРЕНДА ==========\n");

            ShowAllBrands(logic);
            Console.WriteLine();

            int id = ReadInt("Введите ID бренда для изменения: ");
            Brand? brand = logic.GetBrandById(id);

            if(brand == null)
            {
                Console.WriteLine("Бренд с таким ID не найден.");
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"\nTекущие данные бренда: {brand.Name}, {brand.Country}");
            Console.Write($"\nНовое название: ");
            string name = ReadString();

            Console.Write($"Новая страна: ");
            string country = ReadString();

            Brand updateBrand = new Brand
            {
                ID = id,
                Name = name,
                Country = country
            };

            bool result = logic.UpdateBrand(updateBrand);
            if (result)
            {
                Console.WriteLine("\nБренд успешно обновлен.");
            }
            else
            {
                Console.WriteLine("\nНе удалось обновить бренд.");
            }
            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();


        }

        static void DeleteBrand(Logic logic)
        {
            Console.WriteLine("========== УДАЛЕНИЕ БРЕНДА ==========\n");

            ShowAllBrands(logic);
            Console.WriteLine();

            int id = ReadInt("Введите ID бренда: ");

            Brand? brand = logic.GetBrandById(id);

            if (brand == null)
            {
                Console.WriteLine("Бренд не найден.");
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine(
                $"\nВы собираетесь удалить: {brand.Name}");

            Console.WriteLine(
                "Если бренд используется автомобилями, " +
                "удаление невозможно.");

            Console.Write("\nВы уверены? (y/n): ");
            string? answer = Console.ReadLine();

            if (answer?.ToLower() != "y")
            {
                Console.WriteLine("Удаление отменено.");
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
                return;
            }

            bool result = logic.DeleteBrand(id);
            if (result)
            {
                Console.WriteLine("\nБренд успешно удален.");
            }
            else
            {
                Console.WriteLine("\nНе удалось удалить бренд. " +
                    "Возможно, он используется автомобилями.");
            }
            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();

        }



        static void ShowCarsByBrand(Logic logic)
        {
            Console.WriteLine(
                "========== АВТОМОБИЛИ ПО БРЕНДУ ==========\n");

            ShowAllBrands(logic);

            Console.WriteLine();

            int brandID =
                ReadInt("Введите ID бренда: ");

            Brand? brand = logic.GetBrandById(brandID);

            if (brand == null)
            {
                Console.WriteLine("Бренд не найден.");
                Console.WriteLine("\nНажмите Enter для продолжения...");
                return;
            }

            List<Car> cars =
                logic.GetCarsByBrand(brandID);

            Console.WriteLine(
                $"\nАвтомобили бренда {brand.Name}:\n");

            if (cars.Count == 0)
            {
                Console.WriteLine("Автомобилей этого бренда нет.");
            }
            else
            {
                foreach (Car car in cars)
                {
                    PrintCar(logic, car);
                }
            }

            Console.WriteLine("Бренд не найден.");
            Console.WriteLine("\nНажмите Enter для продолжения...");
        }


 

        static void ShowCarsByFuelConsumption(Logic logic)
        {
            Console.WriteLine(
                "========== ФИЛЬТР ПО РАСХОДУ ==========\n");

            float maxConsumption =
                ReadFloat(
                    "Максимальный расход топлива " +
                    "(л/100 км): ");

            List<Car> cars =
                logic.GetCarsByFuelConsuption(maxConsumption);

            Console.WriteLine(
                $"\nАвтомобили с расходом " +
                $"не более {maxConsumption} л/100 км:\n");

            if (cars.Count == 0)
            {
                Console.WriteLine("Подходящих автомобилей нет.");
            }
            else
            {
                foreach (Car car in cars)
                {
                    PrintCar(logic, car);
                }
            }

            Console.WriteLine("Бренд не найден.");
            Console.WriteLine("\nНажмите Enter для продолжения...");
        }




        static void SaveData(Logic logic)
        {
            logic.SaveAll();

            Console.WriteLine("Данные успешно сохранены.");

            Console.WriteLine("Бренд не найден.");
            Console.WriteLine("\nНажмите Enter для продолжения...");
        }

    }
}