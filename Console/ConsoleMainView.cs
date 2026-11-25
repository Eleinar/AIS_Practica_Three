using System.Globalization;
using Shared;

namespace ConsoleApp
{
    /// <summary>
    /// Консольная View (MVP): реализует <see cref="IMainView"/> так же, как главная форма WinForms.
    /// Читает ввод пользователя и генерирует события, на которые подписан Presenter;
    /// выводит данные в методах ShowCars / ShowBrands / ShowError / ShowMessage.
    /// Моделей, бизнес-логики и БД эта View не знает
    /// </summary>
    public class ConsoleMainView : IMainView
    {
        /// <summary>Последние автомобили, присланные Presenter</summary>
        private IReadOnlyList<CarDto> _cars = new List<CarDto>();

        /// <summary>Последние бренды, присланные Presenter (по ним выбирается ID бренда)</summary>
        private IReadOnlyList<BrandDto> _brands = new List<BrandDto>();

        /// <summary>Выводить ли данные, присланные Presenter (false — только запомнить)</summary>
        private bool _render = true;

        /// <summary>Приходила ли ошибка от Presenter во время текущей операции</summary>
        private bool _errorShown;

        #region События IMainView

        public event EventHandler? ViewLoaded;
        public event EventHandler? ShowAllCarsRequested;
        public event EventHandler? RefreshBrandsRequested;
        public event EventHandler<int>? FindByBrandRequested;
        public event EventHandler<float>? FindByFuelRequested;
        public event EventHandler<string>? FindByPlateRequested;
        public event EventHandler<CarDto>? AddCarRequested;
        public event EventHandler<CarDto>? UpdateCarRequested;
        public event EventHandler<string>? DeleteCarRequested;
        public event EventHandler<BrandDto>? AddBrandRequested;
        public event EventHandler<BrandDto>? UpdateBrandRequested;
        public event EventHandler<int>? DeleteBrandRequested;

        #endregion

        #region Методы отрисовки (вызывает Presenter)

        /// <inheritdoc/>
        public void ShowCars(IReadOnlyList<CarDto> cars)
        {
            _cars = cars.ToList();
            if (_render)
                PrintCars(_cars);
        }

        /// <inheritdoc/>
        public void ShowBrands(IReadOnlyList<BrandDto> brands)
        {
            _brands = brands.ToList();
            if (_render)
                PrintBrands(_brands);
        }

        /// <inheritdoc/>
        public void ShowError(string message)
        {
            _errorShown = true;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка: {message}");
            Console.ResetColor();
        }

        /// <inheritdoc/>
        public void ShowMessage(string message) => Console.WriteLine(message);

        #endregion

        #region Главный цикл

        /// <summary>
        /// Запускает работу View: просит Presenter наполнить её данными и обрабатывает команды меню
        /// </summary>
        public void Run()
        {
            try
            {
                LoadInitialData();
                MainLoop();
            }
            catch (EndOfStreamException)
            {
                // Ввод закрыт (Ctrl+Z / Ctrl+D) — просто завершаем работу
            }
        }

        /// <summary>
        /// Сообщает Presenter, что View готова. Данные запоминаются без вывода: показывать их до выбора пункта меню незачем
        /// </summary>
        private void LoadInitialData()
        {
            _errorShown = false;
            Silently(() => ViewLoaded?.Invoke(this, EventArgs.Empty));

            if (_errorShown)
            {
                Console.WriteLine("\nПроверьте строку подключения (CAR_CONNECTION) и доступность PostgreSQL.");
                Pause();
            }
        }

        private void MainLoop()
        {
            while (true)
            {
                Console.Clear();
                PrintMenu();

                Console.Write("\nВыберите действие: ");
                string choice = ReadLine().Trim();

                Console.Clear();

                if (choice == "0")
                    return;

                try
                {
                    if (!ExecuteCommand(choice))
                        Console.WriteLine("Неизвестная команда.");
                }
                catch (Exception ex) when (ex is not EndOfStreamException)
                {
                    ShowError($"Ошибка при выполнении операции:\n{ex.Message}");
                }

                Pause();
            }
        }

        private bool ExecuteCommand(string choice)
        {
            switch (choice)
            {
                case "1": ShowAllCars(); break;
                case "2": FindCarsByPlate(); break;
                case "3": AddCar(); break;
                case "4": UpdateCar(); break;
                case "5": DeleteCar(); break;
                case "6": ShowAllBrands(); break;
                case "7": AddBrand(); break;
                case "8": UpdateBrand(); break;
                case "9": DeleteBrand(); break;
                case "10": FindCarsByBrand(); break;
                case "11": FindCarsByFuelConsumption(); break;
                default: return false;
            }
            return true;
        }

        private static void PrintMenu()
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

        #endregion

        #region Команды: выборки (результат рисует Presenter через ShowCars / ShowBrands)

        private void ShowAllCars()
        {
            Console.WriteLine("АВТОМОБИЛИ\n");
            ShowAllCarsRequested?.Invoke(this, EventArgs.Empty);
        }

        private void FindCarsByPlate()
        {
            string query = ReadString("Госномер или его начало");
            if (query.Length == 0)
                ShowAllCarsRequested?.Invoke(this, EventArgs.Empty);
            else
                FindByPlateRequested?.Invoke(this, query);
        }

        private void FindCarsByBrand()
        {
            Console.WriteLine("АВТОМОБИЛИ ПО БРЕНДУ\n");
            RefreshBrandsRequested?.Invoke(this, EventArgs.Empty);
            if (_brands.Count == 0)
                return;

            Console.WriteLine();
            BrandDto brand = ReadBrand("ID бренда");
            Console.WriteLine();
            FindByBrandRequested?.Invoke(this, brand.Id);
        }

        private void FindCarsByFuelConsumption()
        {
            Console.WriteLine("АВТОМОБИЛИ ПО РАСХОДУ\n");
            float max = ReadFloat("Максимальный расход (л/100 км)");
            Console.WriteLine();
            FindByFuelRequested?.Invoke(this, max);
        }

        private void ShowAllBrands()
        {
            Console.WriteLine("БРЕНДЫ\n");
            RefreshBrandsRequested?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Команды: автомобили

        private void AddCar()
        {
            Console.WriteLine("ДОБАВЛЕНИЕ АВТОМОБИЛЯ\n");
            RefreshBrandsRequested?.Invoke(this, EventArgs.Empty);
            if (_brands.Count == 0)
            {
                Console.WriteLine("Сначала добавьте бренд.");
                return;
            }

            Console.WriteLine();
            // Формат и уникальность госномера проверяет слой Model; о неудаче View узнает от Presenter
            string plate = ReadRequiredString("Госномер (формат А001АА777)").ToUpperInvariant();
            BrandDto brand = ReadBrand("ID бренда");
            CarDto car = ReadCarDetails(plate, brand);

            Execute(() => AddCarRequested?.Invoke(this, car), "Автомобиль добавлен.");
        }

        private void UpdateCar()
        {
            Console.WriteLine("ИЗМЕНЕНИЕ АВТОМОБИЛЯ\n");
            CarDto? existing = FindCarByPlate(ReadRequiredString("Госномер автомобиля"));
            if (existing == null)
            {
                Console.WriteLine("Автомобиль не найден.");
                return;
            }

            Console.WriteLine("\nТекущие данные:");
            PrintCar(existing);

            RefreshBrands();
            if (_brands.Count == 0)
            {
                Console.WriteLine("\nБренды не найдены.");
                return;
            }

            Console.WriteLine();
            PrintBrands(_brands);
            Console.WriteLine("Новые данные (Enter — оставить текущее значение)\n");

            BrandDto brand = ReadBrand("ID бренда", existing.BrandId);
            // Фото в консоли не редактируется: ReadCarDetails переносит его из текущих данных
            CarDto updated = ReadCarDetails(existing.LicensePlate, brand, existing);

            Execute(() => UpdateCarRequested?.Invoke(this, updated), "Автомобиль изменён.");
        }

        private void DeleteCar()
        {
            Console.WriteLine("УДАЛЕНИЕ АВТОМОБИЛЯ\n");
            CarDto? existing = FindCarByPlate(ReadRequiredString("Госномер автомобиля"));
            if (existing == null)
            {
                Console.WriteLine("Автомобиль не найден.");
                return;
            }

            Console.WriteLine();
            PrintCar(existing);
            Console.WriteLine();
            if (!Confirm("Удалить автомобиль?"))
            {
                Console.WriteLine("Отменено.");
                return;
            }

            Execute(() => DeleteCarRequested?.Invoke(this, existing.LicensePlate), "Автомобиль удалён.");
        }

        /// <summary>
        /// Считывает поля автомобиля; при изменении в скобках подставляются текущие значения
        /// </summary>
        private CarDto ReadCarDetails(string plate, BrandDto brand, CarDto? current = null)
        {
            string model = ReadRequiredString("Модель", current?.ModelName);
            int year = ReadInt("Год выпуска", current?.ProductionYear);
            float power = ReadFloat("Мощность (л.с.)", current?.HorsePower);
            float tank = ReadFloat("Объём бака (л)", current?.FuelTankVolume);
            float consumption = ReadFloat("Расход (л/100 км)", current?.FuelConsumption);
            string color = ReadString("Цвет", current?.Color);

            return new CarDto
            {
                LicensePlate = plate,
                BrandId = brand.Id,
                BrandName = brand.Name,
                ModelName = model,
                ProductionYear = year,
                HorsePower = power,
                FuelTankVolume = tank,
                FuelConsumption = consumption,
                Color = color,
                Image = current?.Image
            };
        }

        /// <summary>
        /// Ищет автомобиль по госномеру. Перед поиском просит Presenter прислать полный список,
        /// чтобы не зависеть от последней выборки (фильтра)
        /// </summary>
        private CarDto? FindCarByPlate(string plate)
        {
            RefreshCars();
            string normalized = plate.Trim().ToUpperInvariant();
            return _cars.FirstOrDefault(c => c.LicensePlate.Equals(normalized, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region Команды: бренды

        private void AddBrand()
        {
            Console.WriteLine("ДОБАВЛЕНИЕ БРЕНДА\n");
            string name = ReadRequiredString("Название");
            string country = ReadString("Страна");

            Execute(() => AddBrandRequested?.Invoke(this, new BrandDto { Name = name, Country = country }),
                "Бренд добавлен.");
        }

        private void UpdateBrand()
        {
            Console.WriteLine("ИЗМЕНЕНИЕ БРЕНДА\n");
            RefreshBrandsRequested?.Invoke(this, EventArgs.Empty);
            if (_brands.Count == 0)
                return;

            Console.WriteLine();
            BrandDto existing = ReadBrand("ID бренда");

            Console.WriteLine("\nНовые данные (Enter — оставить текущее значение)\n");
            string name = ReadRequiredString("Название", existing.Name);
            string country = ReadString("Страна", existing.Country);

            Execute(() => UpdateBrandRequested?.Invoke(this, new BrandDto { Id = existing.Id, Name = name, Country = country }),
                "Бренд изменён.");
        }

        private void DeleteBrand()
        {
            Console.WriteLine("УДАЛЕНИЕ БРЕНДА\n");
            RefreshBrandsRequested?.Invoke(this, EventArgs.Empty);
            if (_brands.Count == 0)
                return;

            Console.WriteLine();
            BrandDto existing = ReadBrand("ID бренда");

            Console.WriteLine();
            if (!Confirm($"Удалить бренд «{existing.Name}»?"))
            {
                Console.WriteLine("Отменено.");
                return;
            }

            // Если у бренда есть автомобили, бизнес-логика откажет, а Presenter покажет ошибку через ShowError
            Execute(() => DeleteBrandRequested?.Invoke(this, existing.Id), "Бренд удалён.");
        }

        #endregion

        #region Обмен с Presenter

        /// <summary>
        /// Выполняет действие, не выводя присланные данные (они только запоминаются). Ошибки выводятся всегда
        /// </summary>
        private void Silently(Action action)
        {
            bool previous = _render;
            _render = false;
            try
            {
                action();
            }
            finally
            {
                _render = previous;
            }
        }

        /// <summary>
        /// Генерирует событие изменения данных. Перерисовку списков Presenter делает сам, но в консоли
        /// после операции достаточно короткого сообщения; при ошибке сообщение приходит из Presenter
        /// </summary>
        /// <param name="raiseEvent">Генерация события View</param>
        /// <param name="successMessage">Сообщение при успехе</param>
        private void Execute(Action raiseEvent, string successMessage)
        {
            _errorShown = false;
            Silently(raiseEvent);

            if (!_errorShown)
                Console.WriteLine(successMessage);
        }

        /// <summary>Просит Presenter прислать все автомобили (без вывода)</summary>
        private void RefreshCars() =>
            Silently(() => ShowAllCarsRequested?.Invoke(this, EventArgs.Empty));

        /// <summary>Просит Presenter прислать все бренды (без вывода)</summary>
        private void RefreshBrands() =>
            Silently(() => RefreshBrandsRequested?.Invoke(this, EventArgs.Empty));

        #endregion

        #region Вывод

        private static void PrintCars(IReadOnlyList<CarDto> cars)
        {
            if (cars.Count == 0)
            {
                Console.WriteLine("Автомобили не найдены.");
                return;
            }

            Console.WriteLine($"Найдено: {cars.Count}\n");
            Console.WriteLine(CarRow("Госномер", "Бренд", "Модель", "Год", "Л.с.", "Бак, л", "Расход", "Цвет", "Фото"));
            Console.WriteLine(new string('-', 100));

            foreach (CarDto car in cars)
            {
                Console.WriteLine(CarRow(
                    car.LicensePlate,
                    car.BrandName,
                    car.ModelName,
                    car.ProductionYear.ToString(),
                    Number(car.HorsePower),
                    Number(car.FuelTankVolume),
                    Number(car.FuelConsumption),
                    car.Color,
                    HasPhoto(car) ? "есть" : "нет"));
            }
        }

        private static string CarRow(string plate, string brand, string model, string year, string power,
                                     string tank, string consumption, string color, string photo) =>
            string.Join(" | ",
                Fit(plate, 11).PadRight(11),
                Fit(brand, 14).PadRight(14),
                Fit(model, 14).PadRight(14),
                year.PadLeft(4),
                power.PadLeft(6),
                tank.PadLeft(6),
                consumption.PadLeft(7),
                Fit(color, 10).PadRight(10),
                photo);

        /// <summary>
        /// Выводит данные одного автомобиля
        /// </summary>
        private static void PrintCar(CarDto car)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Госномер:   {car.LicensePlate}");
            Console.WriteLine($"Бренд:      {car.BrandName}");
            Console.WriteLine($"Модель:     {car.ModelName}");
            Console.WriteLine($"Год:        {car.ProductionYear}");
            Console.WriteLine($"Мощность:   {Number(car.HorsePower)} л.с.");
            Console.WriteLine($"Объём бака: {Number(car.FuelTankVolume)} л");
            Console.WriteLine($"Расход:     {Number(car.FuelConsumption)} л/100 км");
            Console.WriteLine($"Цвет:       {car.Color}");
            Console.WriteLine($"Фото:       {(HasPhoto(car) ? "есть" : "нет")}");
            Console.WriteLine("----------------------------------------");
        }

        private static void PrintBrands(IReadOnlyList<BrandDto> brands)
        {
            if (brands.Count == 0)
            {
                Console.WriteLine("Бренды не найдены.");
                return;
            }

            Console.WriteLine("ID".PadRight(4) + " | " + "Название".PadRight(22) + " | Страна");
            Console.WriteLine("-----+------------------------+---------------");
            foreach (BrandDto brand in brands)
            {
                Console.WriteLine(brand.Id.ToString().PadRight(4) + " | " +
                                  Fit(brand.Name, 22).PadRight(22) + " | " +
                                  Fit(brand.Country, 15));
            }
        }

        private static bool HasPhoto(CarDto car) => car.Image != null && car.Image.Length > 0;

        private static string Number(float value) => value.ToString("0.##", CultureInfo.CurrentCulture);

        /// <summary>
        /// Обрезает строку до заданной ширины, добавляя многоточие
        /// </summary>
        private static string Fit(string value, int width) =>
            value.Length <= width ? value : value[..(width - 1)] + "…";

        private static void Pause()
        {
            Console.WriteLine("\nНажмите Enter, чтобы продолжить...");
            ReadLine();
        }

        #endregion

        #region Ввод

        /// <summary>
        /// Читает строку; при закрытом вводе (null) прерывает работу View
        /// </summary>
        private static string ReadLine() => Console.ReadLine() ?? throw new EndOfStreamException();

        /// <summary>
        /// Выводит приглашение (с текущим значением в скобках, если оно есть) и читает ответ
        /// </summary>
        private static string Ask(string prompt, string? current)
        {
            Console.Write(current == null ? $"{prompt}: " : $"{prompt} [{current}]: ");
            return ReadLine().Trim();
        }

        /// <summary>
        /// Читает строку; пустой ввод оставляет текущее значение (если оно задано)
        /// </summary>
        private static string ReadString(string prompt, string? current = null)
        {
            string input = Ask(prompt, current);
            return input.Length == 0 && current != null ? current : input;
        }

        /// <summary>
        /// Читает непустую строку с повторным запросом при пустом вводе
        /// </summary>
        private static string ReadRequiredString(string prompt, string? current = null)
        {
            while (true)
            {
                string value = ReadString(prompt, current);
                if (value.Length > 0)
                    return value;
                Console.WriteLine("Значение не может быть пустым.");
            }
        }

        /// <summary>
        /// Читает целое число с повторным запросом при ошибке
        /// </summary>
        private static int ReadInt(string prompt, int? current = null)
        {
            while (true)
            {
                string input = Ask(prompt, current?.ToString(CultureInfo.InvariantCulture));
                if (input.Length == 0 && current.HasValue)
                    return current.Value;
                if (int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
                    return value;
                Console.WriteLine("Введите целое число.");
            }
        }

        /// <summary>
        /// Читает число с плавающей точкой (поддерживает запятую и точку)
        /// </summary>
        private static float ReadFloat(string prompt, float? current = null)
        {
            while (true)
            {
                string input = Ask(prompt, current?.ToString(CultureInfo.InvariantCulture));
                if (input.Length == 0 && current.HasValue)
                    return current.Value;
                if (float.TryParse(input.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
                    return value;
                Console.WriteLine("Введите число.");
            }
        }

        /// <summary>
        /// Читает ID бренда и находит бренд среди присланных Presenter (аналог выпадающего списка в WinForms)
        /// </summary>
        private BrandDto ReadBrand(string prompt, int? currentId = null)
        {
            while (true)
            {
                int id = ReadInt(prompt, currentId);
                BrandDto? brand = _brands.FirstOrDefault(b => b.Id == id);
                if (brand != null)
                    return brand;
                Console.WriteLine("Бренда с таким ID нет в списке.");
            }
        }

        private static bool Confirm(string question)
        {
            Console.Write($"{question} (y/n): ");
            string answer = ReadLine().Trim().ToLowerInvariant();
            return answer is "y" or "yes" or "д" or "да";
        }

        #endregion
    }
}
