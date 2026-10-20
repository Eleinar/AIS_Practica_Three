using System.Text.RegularExpressions;
using Model;
using DataAccessLayer;

namespace BusinessLogic
{
    /// <summary>
    /// Бизнес-логика работы с автомобилями и брендами
    /// </summary>
    public class Logic
   {    
       private readonly IRepository<Car, string> _cars;
        private readonly IRepository<Brand, int> _brands;
        private static readonly Regex PlatePattern = new(@"^[АВЕКМНОРСТУХ][0-9]{3}[АВЕКМНОРСТУХ]{2}[0-9]{2,3}$", RegexOptions.Compiled);

        /// <summary>
        /// Передача репозиториев
        /// </summary>
        /// <param name="cars">Репозиторий автомобилей</param>
        /// <param name="brands">Репозиторий брендов</param>
        /// <exception cref="ArgumentNullException">Если один из репозиториев равен null</exception>
        public Logic(IRepository<Car, string> cars, IRepository<Brand, int> brands)
        {
            _cars = cars ?? throw new ArgumentNullException(nameof(cars));
            _brands = brands ?? throw new ArgumentNullException(nameof(brands));
        }

        /// <summary>
        /// Приводит госномер к верхнему регистру без пробелов
        /// </summary>
        /// <param name="plate">Исходный госномер</param>
        /// <returns>Нормализованный госномер</returns>
        private static string NormalizePlate(string plate) => plate.Trim().ToUpperInvariant();

        /// <summary>
        /// Проверяет формат госномера
        /// </summary>
        /// <param name="plate">Проверяемый госномер</param>
        /// <returns>true, если формат корректен</returns>
        public static bool IsValidLicensePlate(string? plate) =>
            !string.IsNullOrWhiteSpace(plate) && PlatePattern.IsMatch(plate.Trim().ToUpperInvariant());

        #region Авто

        /// <summary>
        /// Возвращает список всех автомобилей
        /// </summary>
        /// <returns>Список всех автомобилей</returns>
        public List<Car> GetAllCars()
        {
            return _cars.ReadAll();
        }


        /// <summary>
        /// Возвращает автомобиль по госномеру
        /// </summary>
        /// <param name="plate">Госномер</param>
        /// <returns>Автомобиль или null</returns>
        public Car? GetCarById(string plate)
        {
            return _cars.ReadById(NormalizePlate(plate));
        }



        /// <summary>
        /// Обновление данных авто
        /// </summary>
        /// <param name="car">Автомобиль с обновлёнными данными</param>
        /// <returns>true, если обновление успешно</returns>
        public bool UpdateCar(Car car) //Изменить реализацию
        {
            if (car == null || GetBrandById(car.BrandID) == null)
            {
                return false;
            }


            return _cars.Update(car);
        }

        /// <summary>
        /// Добавление записи об авто
        /// </summary>
        /// <param name="car">Новый автомобиль</param>
        /// <returns>true, если добавление успешно</returns>
        public bool AddCar(Car car)
        {
            if (car == null || !IsValidLicensePlate(car.LicensePlate) ||
                GetCarById(car.LicensePlate) != null || GetBrandById(car.BrandID) == null)
            {
                return false;
            }

            car.LicensePlate = NormalizePlate(car.LicensePlate);
            _cars.Add(car);
            return true;
        }


        /// <summary>
        /// Удаление записи об авто
        /// </summary>
        /// <param name="licensePlate">Госномер удаляемого автомобиля</param>
        /// <returns>true, если удаление успешно</returns>
        public bool DeleteCar(string licensePlate)
        {
            return _cars.Delete(NormalizePlate(licensePlate));
        }

        #endregion    

        

    

        #region Бренды
        /// <summary>
        /// Возвращает список брендов
        /// </summary>
        /// <returns>Список всех брендов</returns>
        public List<Brand> GetAllBrands()
        {
            return _brands.ReadAll();
        }

        /// <summary>
        /// Возвращает бренд с нужным id
        /// </summary>
        /// <param name="id">Идентификатор бренда</param>
        /// <returns>Бренд или null</returns>
        public Brand? GetBrandById(int id)
        {
            return _brands.ReadById(id);
        }

        /// <summary>
        /// Возвращает имя бренда по id
        /// </summary>
        /// <param name="brandId">Идентификатор бренда</param>
        /// <returns>Название бренда или «Неизвестный бренд»</returns>
        public string GetBrandName(int brandId)
        {
            Brand? brand = GetBrandById(brandId);

            if (brand == null)
            {
                return "Неизвестный бренд";
            }

            return brand.Name;
        }

        /// <summary>
        /// Добавление записи о бренде
        /// </summary>
        /// <param name="brand">Новый бренд</param>
        /// <returns>true, если добавление успешно</returns>
        public bool AddBrand(Brand brand)
        {
            bool alreadyExists = _brands.ReadAll().Any(b =>
                                             b.Name.Equals(brand.Name, StringComparison.OrdinalIgnoreCase));

            if(alreadyExists)
            {
                return false;
            }
      
            _brands.Add(brand);

            return true;
        }

        /// <summary>
        /// Обновление данных бренда
        /// </summary>
        /// <param name="brand">Бренд с обновлёнными данными</param>
        /// <returns>true, если обновление успешно</returns>
        public bool UpdateBrand(Brand brand) //Изменить реализацию
        {
            if(brand == null)
            {
                return false;
            }

            List<Brand> all = _brands.ReadAll();

            if (!all.Any(b => b.ID == brand.ID))
            {
                return false;
            }

            bool name = all.Any(b => b.Name.Equals(brand.Name, StringComparison.OrdinalIgnoreCase) && b.ID != brand.ID);

            if (name)
            {
                return false;
            }

            

            return _brands.Update(brand);

        }

        /// <summary>
        /// Удаление записи о бренде
        /// </summary>
        /// <param name="brandID">Идентификатор удаляемого бренда</param>
        /// <returns>true, если удаление успешно</returns>
        public bool DeleteBrand(int brandID)
        {
            bool isUsed = _cars.ReadAll().Any(car => car.BrandID == brandID);

            if(isUsed)
            {
                return false;
            }

            return _brands.Delete(brandID);
        }

       
        #endregion


        #region Бизнес-функции
        /// <summary>
        /// Бизнес-функция получения авто соответствующих бренду
        /// </summary>
        /// <param name="brandID">Идентификатор бренда</param>
        /// <returns>Список автомобилей бренда</returns>
        public List<Car> GetCarsByBrand(int brandID)
        {
            return _cars.ReadAll().Where(car => car.BrandID == brandID).ToList();
        }
        
        /// <summary>
        /// Бизнес функция получения авто соответствующих расходу
        /// </summary>
        /// <param name="maxFuelConsumption">Максимальный расход топлива</param>
        /// <returns>Список автомобилей с расходом не выше заданного</returns>
        public List<Car> GetCarsByFuelConsuption( float maxFuelConsumption)
        {
            return _cars.ReadAll().Where(car => car.FuelConsumption <= maxFuelConsumption).ToList();
        }

        /// <summary>
        /// Выполняет поиск по номеру
        /// </summary>
        /// <param name="query">Начало или полный госномер</param>
        /// <returns>Список найденных автомобилей</returns>
        public List<Car> SearchCarsByLicensePlate(string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return GetAllCars();
            }

            string trimmedQuery = query.Trim();

            return _cars.ReadAll()
                .Where(c => c.LicensePlate.StartsWith(trimmedQuery, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        #endregion
    }
}
