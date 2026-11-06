using Model;
using DataAccessLayer;

namespace BusinessLogic
{
    /// <summary>
    /// CRUD-операции над автомобилями и брендами (с проверками целостности)
    /// </summary>
    public class CrudLogic
    {
        private readonly IRepository<Car, string> _cars;
        private readonly IRepository<Brand, int> _brands;
        private readonly ILicensePlateValidator _plateValidator;

        public CrudLogic(IRepository<Car, string> cars, IRepository<Brand, int> brands,
                         ILicensePlateValidator plateValidator)
        {
            _cars = cars ?? throw new ArgumentNullException(nameof(cars));
            _brands = brands ?? throw new ArgumentNullException(nameof(brands));
            _plateValidator = plateValidator ?? throw new ArgumentNullException(nameof(plateValidator));
        }

        private static string NormalizePlate(string plate) => plate.Trim().ToUpperInvariant();

        /// <summary>
        /// Проверяет формат госномера
        /// </summary>
        public bool IsValidLicensePlate(string? plate) => _plateValidator.IsValid(plate);

        #region Авто

        public List<Car> GetAllCars() => _cars.ReadAll();

        public Car? GetCarById(string plate) => _cars.ReadById(NormalizePlate(plate));

        public bool AddCar(Car car)
        {
            if (car == null || !IsValidLicensePlate(car.LicensePlate) ||
                GetCarById(car.LicensePlate) != null || GetBrandById(car.BrandID) == null)
                return false;

            car.LicensePlate = NormalizePlate(car.LicensePlate);
            _cars.Add(car);
            return true;
        }

        public bool UpdateCar(Car car)
        {
            if (car == null || GetBrandById(car.BrandID) == null)
                return false;

            return _cars.Update(car);
        }

        public bool DeleteCar(string licensePlate) => _cars.Delete(NormalizePlate(licensePlate));

        #endregion

        #region Бренды

        public List<Brand> GetAllBrands() => _brands.ReadAll();

        public Brand? GetBrandById(int id) => _brands.ReadById(id);

        public string GetBrandName(int brandId) => GetBrandById(brandId)?.Name ?? "Неизвестный бренд";

        public bool AddBrand(Brand brand)
        {
            bool exists = _brands.ReadAll().Any(b =>
                b.Name.Equals(brand.Name, StringComparison.OrdinalIgnoreCase));
            if (exists)
                return false;

            _brands.Add(brand);
            return true;
        }

        public bool UpdateBrand(Brand brand)
        {
            if (brand == null)
                return false;

            List<Brand> all = _brands.ReadAll();

            if (!all.Any(b => b.ID == brand.ID))
                return false;

            bool nameTaken = all.Any(b =>
                b.Name.Equals(brand.Name, StringComparison.OrdinalIgnoreCase) && b.ID != brand.ID);
            if (nameTaken)
                return false;

            return _brands.Update(brand);
        }

        public bool DeleteBrand(int brandId)
        {
            if (_cars.ReadAll().Any(c => c.BrandID == brandId))
                return false;

            return _brands.Delete(brandId);
        }

        #endregion
    }
}
