using System.Globalization;
using System.Text;
using Model;
using BusinessLogic;

namespace ConsoleApp
{
    /// <summary>
    /// Консольный интерфейс автопарка
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа консольного приложения
        /// </summary>
        public static void Main(string[] args)
        {
            // Корректный вывод кириллицы в Windows-консоли
            Console.OutputEncoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            Console.InputEncoding = Encoding.UTF8;

            Logic logic;
            try
            {
                logic = LogicFactory.CreateEF();
                // Проверка доступности БД при старте
                _ = logic.GetAllBrands();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось подключиться к базе данных.");
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine(ex.InnerException.Message);
                Console.WriteLine("\nПроверьте строку подключения (CAR_CONNECTION) и доступность PostgreSQL.");
                Pause();
                return;
            }

            while (true)
            {
                Console.Clear();
                ShowMenu();

                Console.Write("\nВыберите действие: ");
                string? choice = Console.ReadLine()?.Trim();

                Console.Clear();

                try
                {
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
                        case "0": return;
                        default:
                            Console.WriteLine("Неизвестная команда.");
                            Pause();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при выполнении операции:");
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                        Console.WriteLine(ex.InnerException.Message);
                    Pause();
                }
            }
        }

        /// <summary>
        /// Выводит главное меню
        /// </summary>
        private static void ShowMenu()
        {
            Console.WriteLine("========== АВТОПАРК ==========\n");
            Console.WriteLine("1.  Показать все автомобили");
            Console.WriteLine("2.  Найти автомобиль по госномеру");
            Console.WriteLine("3.  Добавить автомобиль");
            Console.WriteLine("4.  Изменить автомобиль");
            Console.WriteLine("5.  Удалить автомобиль");
            Console.WriteLine();
            Console.WriteLine("6.  Показать все бренды");
            Console.WriteLine("7.  Добавить бренд");
            Console.WriteLine("8.  Изменить бренд");
            Console.WriteLine("9.  Удалить бренд");
            Console.WriteLine();
            Console.WriteLine("10. Автомобили выбранного бренда");
            Console.WriteLine("11. Автомобили с заданным расходом топлива");
            Console.WriteLine();
            Console.WriteLine("0.  Выход");
        }

        /// <summary>
        /// Пауза до нажатия Enter
        /// </summary>
        private static void Pause()
        {
            Console.WriteLine("\nНажмите Enter, чтобы продолжить...");
            Console.ReadLine();
        }

        /// <summary>
        /// Выводит данные одного автомобиля
        /// </summary>
        private static void PrintCar(Logic logic, Car car)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Госномер:   {car.LicensePlate}");
            Console.WriteLine($"Бренд:      {logic.GetBrandName(car.BrandID)}");
            Console.WriteLine($"Модель:     {car.ModelName}");
            Console.WriteLine($"Год:        {car.ProductionYear}");
            Console.WriteLine($"Мощность:   {car.HorsePower} л.с.");
            Console.WriteLine($"Объём бака: {car.FuelTankVolume} л");
            Console.WriteLine($"Расход:     {car.FuelConsumption} л/100 км");
            Console.WriteLine($"Цвет:       {car.Color}");
            Console.WriteLine($"Фото:       {(car.Image != null && car.Image.Length > 0 ? "есть" : "нет")}");
        }

        /// <summary>
        /// Выводит список автомобилей и ждёт подтверждения
        /// </summary>
        private static void PrintCars(Logic logic, List<Car> cars)
        {
            if (cars.Count == 0)
            {
                Console.WriteLine("Автомобили не найдены.");
            }
            else
            {
                Console.WriteLine($"Найдено: {cars.Count}\n");
                foreach (Car car in cars)
                    PrintCar(logic, car);
                Console.WriteLine("----------------------------------------");
            }
            Pause();
        }

        /// <summary>
        /// Выводит список брендов без паузы (для выбора ID)
        /// </summary>
        private static void ListBrands(Logic logic)
        {
            List<Brand> brands = logic.GetAllBrands();
            if (brands.Count == 0)
            {
                Console.WriteLine("Бренды не найдены.");
                return;
            }

            Console.WriteLine("ID  | Название              | Страна");
            Console.WriteLine("----+-----------------------+---------------");
            foreach (Brand brand in brands)
            {
                string name = brand.Name.Length > 21 ? brand.Name[..18] + "..." : brand.Name;
                string country = brand.Country.Length > 13 ? brand.Country[..10] + "..." : brand.Country;
                Console.WriteLine($"{brand.ID,-3} | {name,-21} | {country}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Показывает все автомобили
        /// </summary>
        private static void ShowAllCars(Logic logic)
        {
            Console.WriteLine("АВТОМОБИЛИ\n");
            PrintCars(logic, logic.GetAllCars());
        }

        /// <summary>
        /// Поиск автомобилей по госномеру (или его началу)
        /// </summary>
        private static void FindCar(Logic logic)
        {
            Console.Write("Госномер или его начало: ");
            PrintCars(logic, logic.SearchCarsByLicensePlate(ReadString()));
        }

        /// <summary>
        /// Добавление автомобиля
        /// </summary>
        private static void AddCar(Logic logic)
        {
            Console.WriteLine("ДОБАВЛЕНИЕ АВТОМОБИЛЯ\n");
            ListBrands(logic);

            Console.Write("Госномер (формат А001АА777): ");
            string plate = ReadString().ToUpperInvariant();

            if (!Logic.IsValidLicensePlate(plate))
            {
                Console.WriteLine("Неверный формат госномера. Используйте кириллические буквы АВЕКМНОРСТУХ.");
                Pause();
                return;
            }

            int brandId = ReadInt("ID бренда: ");
            Car car = ReadCarDetails(plate, brandId);

            if (logic.AddCar(car))
                Console.WriteLine("Автомобиль добавлен.");
            else
                Console.WriteLine("Не удалось добавить. Проверьте бренд, формат и уникальность госномера.");

            Pause();
        }

        /// <summary>
        /// Изменение автомобиля
        /// </summary>
        private static void UpdateCar(Logic logic)
        {
            Console.Write("Госномер автомобиля: ");
            string plate = ReadString();
            Car? car = logic.GetCarById(plate);
            if (car == null)
            {
                Console.WriteLine("Автомобиль не найден.");
                Pause();
                return;
            }

            Console.WriteLine("\nТекущие данные:");
            PrintCar(logic, car);
            Console.WriteLine();

            ListBrands(logic);
            int brandId = ReadInt("Новый ID бренда: ");
            Car updated = ReadCarDetails(car.LicensePlate, brandId);
            // Сохраняем фото, если было
            updated.Image = car.Image;

            if (logic.UpdateCar(updated))
                Console.WriteLine("Автомобиль изменён.");
            else
                Console.WriteLine("Не удалось изменить автомобиль. Проверьте бренд.");

            Pause();
        }

        /// <summary>
        /// Удаление автомобиля
        /// </summary>
        private static void DeleteCar(Logic logic)
        {
            Console.Write("Госномер автомобиля: ");
            string plate = ReadString();
            Car? car = logic.GetCarById(plate);
            if (car == null)
            {
                Console.WriteLine("Автомобиль не найден.");
                Pause();
                return;
            }

            PrintCar(logic, car);
            Console.Write("\nУдалить? (y/n): ");
            if (!IsYes(Console.ReadLine()))
            {
                Console.WriteLine("Отменено.");
                Pause();
                return;
            }

            Console.WriteLine(logic.DeleteCar(plate) ? "Автомобиль удалён." : "Не удалось удалить автомобиль.");
            Pause();
        }

        /// <summary>
        /// Считывает поля автомобиля с консоли
        /// </summary>
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

        /// <summary>
        /// Показывает все бренды
        /// </summary>
        private static void ShowAllBrands(Logic logic)
        {
            Console.WriteLine("БРЕНДЫ\n");
            ListBrands(logic);
            Pause();
        }

        /// <summary>
        /// Добавление бренда
        /// </summary>
        private static void AddBrand(Logic logic)
        {
            Console.WriteLine("ДОБАВЛЕНИЕ БРЕНДА\n");
            Console.Write("Название: ");
            string name = ReadString();
            Console.Write("Страна: ");
            string country = ReadString();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Название не может быть пустым.");
                Pause();
                return;
            }

            Brand brand = new() { Name = name, Country = country };
            if (logic.AddBrand(brand))
                Console.WriteLine($"Бренд добавлен. ID: {brand.ID}");
            else
                Console.WriteLine("Не удалось добавить бренд (возможно, такое название уже есть).");

            Pause();
        }

        /// <summary>
        /// Изменение бренда
        /// </summary>
        private static void UpdateBrand(Logic logic)
        {
            Console.WriteLine("ИЗМЕНЕНИЕ БРЕНДА\n");
            ListBrands(logic);

            int id = ReadInt("ID бренда: ");
            Brand? existing = logic.GetBrandById(id);
            if (existing == null)
            {
                Console.WriteLine("Бренд не найден.");
                Pause();
                return;
            }

            Console.WriteLine($"Текущее: {existing.Name} — {existing.Country}");
            Console.Write("Новое название: ");
            string name = ReadString();
            Console.Write("Новая страна: ");
            string country = ReadString();

            Brand brand = new() { ID = id, Name = name, Country = country };
            if (logic.UpdateBrand(brand))
                Console.WriteLine("Бренд изменён.");
            else
                Console.WriteLine("Не удалось изменить бренд (проверьте название).");

            Pause();
        }

        /// <summary>
        /// Удаление бренда
        /// </summary>
        private static void DeleteBrand(Logic logic)
        {
            Console.WriteLine("УДАЛЕНИЕ БРЕНДА\n");
            ListBrands(logic);

            int id = ReadInt("ID бренда: ");
            Brand? existing = logic.GetBrandById(id);
            if (existing == null)
            {
                Console.WriteLine("Бренд не найден.");
                Pause();
                return;
            }

            Console.WriteLine($"Удалить «{existing.Name}»? (y/n): ");
            if (!IsYes(Console.ReadLine()))
            {
                Console.WriteLine("Отменено.");
                Pause();
                return;
            }

            if (logic.DeleteBrand(id))
                Console.WriteLine("Бренд удалён.");
            else
                Console.WriteLine("Не удалось удалить: бренд используется автомобилями или не найден.");

            Pause();
        }

        /// <summary>
        /// Показывает автомобили выбранного бренда
        /// </summary>
        private static void ShowCarsByBrand(Logic logic)
        {
            Console.WriteLine("АВТОМОБИЛИ ПО БРЕНДУ\n");
            ListBrands(logic);
            int id = ReadInt("ID бренда: ");
            PrintCars(logic, logic.GetCarsByBrand(id));
        }

        /// <summary>
        /// Показывает автомобили с расходом не выше заданного
        /// </summary>
        private static void ShowCarsByFuelConsumption(Logic logic)
        {
            Console.WriteLine("АВТОМОБИЛИ ПО РАСХОДУ\n");
            float max = ReadFloat("Максимальный расход (л/100 км): ");
            PrintCars(logic, logic.GetCarsByFuelConsuption(max));
        }

        /// <summary>
        /// Считывает строку с консоли
        /// </summary>
        private static string ReadString() => Console.ReadLine()?.Trim() ?? string.Empty;

        /// <summary>
        /// Считывает целое число с повторным запросом при ошибке
        /// </summary>
        private static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine()?.Trim(), out int value))
                    return value;
                Console.WriteLine("Введите целое число.");
            }
        }

        /// <summary>
        /// Считывает число с плавающей точкой (поддерживает запятую и точку)
        /// </summary>
        private static float ReadFloat(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = (Console.ReadLine() ?? "").Trim().Replace(',', '.');
                if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
                    return value;
                Console.WriteLine("Введите число.");
            }
        }

        /// <summary>
        /// Проверяет ответ пользователя на подтверждение (y/д/yes/да)
        /// </summary>
        private static bool IsYes(string? answer)
        {
            string s = (answer ?? "").Trim().ToLowerInvariant();
            return s is "y" or "yes" or "д" or "да";
        }
    }
}
