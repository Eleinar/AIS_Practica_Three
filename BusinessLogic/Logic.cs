using Model;

namespace BusinessLogic
{
    /// <summary>
    /// Реализация <see cref="ILogic"/>: объединяет <see cref="ICrudLogic"/> и <see cref="IBusinessLogic"/>,
    /// делегируя вызовы внедрённым зависимостям (композиция вместо дублирования кода)
    /// </summary>
    public class Logic : ILogic
    {
        private readonly ICrudLogic _crud;
        private readonly IBusinessLogic _business;

        /// <summary>
        /// Создаёт фасад над CRUD-логикой и бизнес-функциями
        /// </summary>
        /// <param name="crud">CRUD-логика</param>
        /// <param name="business">Бизнес-функции (выборки и поиск)</param>
        public Logic(ICrudLogic crud, IBusinessLogic business)
        {
            _crud = crud ?? throw new ArgumentNullException(nameof(crud));
            _business = business ?? throw new ArgumentNullException(nameof(business));
        }

        #region ICrudLogic

        /// <inheritdoc/>
        public bool IsValidLicensePlate(string? plate) => _crud.IsValidLicensePlate(plate);

        /// <inheritdoc/>
        public List<Car> GetAllCars() => _crud.GetAllCars();

        /// <inheritdoc/>
        public Car? GetCarById(string plate) => _crud.GetCarById(plate);

        /// <inheritdoc/>
        public bool AddCar(Car car) => _crud.AddCar(car);

        /// <inheritdoc/>
        public bool UpdateCar(Car car) => _crud.UpdateCar(car);

        /// <inheritdoc/>
        public bool DeleteCar(string licensePlate) => _crud.DeleteCar(licensePlate);

        /// <inheritdoc/>
        public List<Brand> GetAllBrands() => _crud.GetAllBrands();

        /// <inheritdoc/>
        public Brand? GetBrandById(int id) => _crud.GetBrandById(id);

        /// <inheritdoc/>
        public string GetBrandName(int brandId) => _crud.GetBrandName(brandId);

        /// <inheritdoc/>
        public bool AddBrand(Brand brand) => _crud.AddBrand(brand);

        /// <inheritdoc/>
        public bool UpdateBrand(Brand brand) => _crud.UpdateBrand(brand);

        /// <inheritdoc/>
        public bool DeleteBrand(int brandId) => _crud.DeleteBrand(brandId);

        #endregion

        #region IBusinessLogic

        /// <inheritdoc/>
        public List<Car> GetCarsByBrand(int brandId) => _business.GetCarsByBrand(brandId);

        /// <inheritdoc/>
        public List<Car> GetCarsByMaxFuelConsumption(float maxFuelConsumption) =>
            _business.GetCarsByMaxFuelConsumption(maxFuelConsumption);

        /// <inheritdoc/>
        public List<Car> SearchCarsByLicensePlate(string? query) => _business.SearchCarsByLicensePlate(query);

        #endregion
    }
}
