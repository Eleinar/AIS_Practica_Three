using System.Text.RegularExpressions;

namespace Model
{
    public class Logic
   {    
       private readonly IRepository<Car, string> _cars;
        private readonly IRepository<Brand, int> _brands;
        private static readonly Regex PlatePattern = new(@"^[АВЕКМНОРСТУХ][0-9]{3}[АВЕКМНОРСТУХ]{2}[0-9]{2,3}$", RegexOptions.Compiled);

        /// <summary>
        /// Передача репозиториев
        /// </summary>
        /// <param name="cars"></param>
        /// <param name="brands"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Logic(IRepository<Car, string> cars, IRepository<Brand, int> brands)
        {
            _cars = cars ?? throw new ArgumentNullException(nameof(cars));
            _brands = brands ?? throw new ArgumentNullException(nameof(brands));
        }

        private static string NormalizePlate(string plate) => plate.Trim().ToUpperInvariant();

        /// <summary>
        /// Проверяет формат госномера
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        public static bool IsValidLicensePlate(string? plate) =>
            !string.IsNullOrWhiteSpace(plate) && PlatePattern.IsMatch(plate.Trim().ToUpperInvariant());

        #region Авто

        /// <summary>
        /// Возвращает список всех автомобилей
        /// </summary>
        /// <returns></returns>
        public List<Car> GetAllCars()
        {
            return _cars.ReadAll();
        }


        /// <summary>
        /// Возвращает автомобиль по госномеру
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        public Car? GetCarById(string plate)
        {
            return _cars.ReadById(NormalizePlate(plate));
        }



        /// <summary>
        /// Обновление данных авто
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
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
        /// <param name="car"></param>
        /// <returns></returns>
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
        /// <param name="licensePlate"></param>
        /// <returns></returns>
        public bool DeleteCar(string licensePlate)
        {
            return _cars.Delete(NormalizePlate(licensePlate));
        }

        #endregion    

        

    

        #region Бренды
        /// <summary>
        /// Возвращает список брендов
        /// </summary>
        /// <returns></returns>
        public List<Brand> GetAllBrands()
        {
            return _brands.ReadAll();
        }

        /// <summary>
        /// Возвращает бренд с нужным id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Brand? GetBrandById(int id)
        {
            return _brands.ReadById(id);
        }

        /// <summary>
        /// Возвращает имя бренда по id
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
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
        /// <param name="brand"></param>
        /// <returns></returns>
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
        /// <param name="brand"></param>
        /// <returns></returns>
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
        /// <param name="brandID"></param>
        /// <returns></returns>
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
        /// <param name="brandID"></param>
        /// <returns></returns>
        public List<Car> GetCarsByBrand(int brandID)
        {
            return _cars.ReadAll().Where(car => car.BrandID == brandID).ToList();
        }
        
        /// <summary>
        /// Бизнес функция получения авто соответствующих расходу
        /// </summary>
        /// <param name="maxFuelConsumption"></param>
        /// <returns></returns>
        public List<Car> GetCarsByFuelConsuption( float maxFuelConsumption)
        {
            return _cars.ReadAll().Where(car => car.FuelConsumption <= maxFuelConsumption).ToList();
        }

        /// <summary>
        /// Выполняет поиск по номеру
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
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
