using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AIS_Practica_One_Model
{
    public class Logic
    {
        private readonly List<Car> _cars;
        private readonly List<Brand> _brands;
        private readonly CarFileEditor _carEditor = new();
        private readonly BrandFileEditor _brandEditor = new();
        private readonly string _carsFilePath;
        private readonly string _brandsFilePath;
        private static readonly Regex PlatePattern = new(@"^[АВЕКМНОРСТУХ][0-9]{3}[АВЕКМНОРСТУХ]{2}[0-9]{2,3}$", RegexOptions.Compiled);

        /// <summary>
        /// Загружает данные из файлов 
        /// </summary>
        /// <param name="dataDirectory"></param>
        public Logic(string dataDirectory)
        {
            _carEditor = new CarFileEditor();
            _brandEditor = new BrandFileEditor();

            _carsFilePath = Path.Combine(dataDirectory, "Cars.csv");
            _brandsFilePath = Path.Combine(dataDirectory, "Brands.csv");

            _brands = _brandEditor.Load(_brandsFilePath);
            _cars = _carEditor.LoadCars(_carsFilePath);
        }

        /// <summary>
        /// Проверяет формат госномера
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        public static bool IsValidLicensePlate(string? plate) =>
            !string.IsNullOrWhiteSpace(plate) && PlatePattern.IsMatch(plate.Trim().ToUpperInvariant());

        /// <summary>
        /// Сохраняет все данные
        /// </summary>
        public void SaveAll()
        {
            SaveBrands();
            SaveCars();
        }

        /// <summary>
        /// Сохраняет данные об авто
        /// </summary>
        private void SaveCars()
        {
            _carEditor.SaveCars(_carsFilePath, _cars);
        }

        /// <summary>
        /// Сохраняет данные о брендах
        /// </summary>
        private void SaveBrands()
        {
            _brandEditor.Save(_brandsFilePath, _brands);
        }

        /// <summary>
        /// Возвращает список всех автомобилей
        /// </summary>
        /// <returns></returns>
        public List<Car> GetAllCars()
        {
            return new List<Car>(_cars);
        }

        /// <summary>
        /// Возвращает автомобиль по госномеру
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        public Car? GetCarById(string plate)
        {
            return _cars.FirstOrDefault(car => car.LicensePlate == plate);
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

            return _cars
                .Where(c => c.LicensePlate.StartsWith(trimmedQuery, StringComparison.OrdinalIgnoreCase))
                .ToList();
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

            car.LicensePlate = car.LicensePlate.Trim().ToUpper();
            _cars.Add(car);
            return true;
        }

        /// <summary>
        /// Обновление данных авто
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public bool UpdateCar(Car car) //Изменить реализацию
        {
            if(car == null)
            {
                return false;
            }

            bool brandExists = _brands.Any(b => b.ID == car.BrandID);

            if(!brandExists)
            {
                return false;
            }

            Car? existingCar = GetCarById(car.LicensePlate);

            if (existingCar == null)
            {
                return false;
            }

            existingCar.BrandID = car.BrandID;
            existingCar.ModelName = car.ModelName;
            existingCar.ProductionYear = car.ProductionYear;
            existingCar.HorsePower = car.HorsePower;
            existingCar.FuelTankVolume = car.FuelTankVolume;
            existingCar.FuelConsumption = car.FuelConsumption;
            existingCar.Color = car.Color;

            return true;
        }

        /// <summary>
        /// Удаление записи об авто
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns></returns>
        public bool DeleteCar(string licensePlate)
        {
            Car? car = GetCarById(licensePlate);
            if (car == null)
            {
                return false;
            }
            _cars.Remove(car);
            return true;
        }

        /// <summary>
        /// Возвращает список брендов
        /// </summary>
        /// <returns></returns>
        public List<Brand> GetAllBrands()
        {
            return new List<Brand>(_brands);
        }

        /// <summary>
        /// Возвращает бренд с нужным id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Brand? GetBrandById(int id)
        {
            return _brands.FirstOrDefault(brand => brand.ID == id);
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
            bool alreadyExists = _brands.Any(b =>
                                             b.Name.Equals(brand.Name, StringComparison.OrdinalIgnoreCase));

            if(alreadyExists)
            {
                return false;
            }

            brand.ID = GetNextBrandID();
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

            Brand? existingBrand = GetBrandById(brand.ID);

            if (existingBrand == null)
            {
                return false;
            }

            bool alreadyExists = _brands.Any(b =>
                                             b.Name.Equals(brand.Name, StringComparison.OrdinalIgnoreCase) &&
                                             b.ID != brand.ID);

            if (alreadyExists)
            {
                return false;
            }

            existingBrand.Name = brand.Name;
            existingBrand.Country = brand.Country;

            return true;

        }

        /// <summary>
        /// Удаление записи о бренде
        /// </summary>
        /// <param name="brandID"></param>
        /// <returns></returns>
        public bool DeleteBrand(int brandID)
        {
            bool isUsed = _cars.Any(car => car.BrandID == brandID);

            if(isUsed)
            {
                return false;
            }

            Brand? brand = _brands.FirstOrDefault(b => b.ID == brandID);
            if(brand == null)
            {
                return false;
            }

            _brands.Remove(brand);
            return true;
        }

        /// <summary>
        /// Автоинкремент ID брендов
        /// </summary>
        /// <returns></returns>
        public int GetNextBrandID()
        {
            if (_brands.Count == 0)
            {
                return 1;
            }
            return _brands.Max(b => b.ID) + 1;
        }
        /// <summary>
        /// Бизнес-функция получения авто соответствующих бренду
        /// </summary>
        /// <param name="brandID"></param>
        /// <returns></returns>
        public List<Car> GetCarsByBrand(int brandID)
        {
            return _cars.Where(car => car.BrandID == brandID).ToList();
        }
        
        /// <summary>
        /// Бизнес функция получения авто соответствующих расходу
        /// </summary>
        /// <param name="maxFuelConsumption"></param>
        /// <returns></returns>
        public List<Car> GetCarsByFuelConsuption( float maxFuelConsumption)
        {
            return _cars.Where(car => car.FuelConsumption <= maxFuelConsumption).ToList();
        }

    }
}
