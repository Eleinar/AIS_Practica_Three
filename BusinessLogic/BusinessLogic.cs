using DataAccessLayer;
using Model;


namespace BusinessLogic
{
    /// <summary>
    /// Бизнес-функции автопарка: выборки и поиск (только чтение)
    /// </summary>
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IReadRepository<Car, string> _cars;

        public BusinessLogic(IReadRepository<Car, string> cars)
        {
            _cars = cars ?? throw new ArgumentNullException(nameof(cars));
        }

        /// <inheritdoc/>
        public List<Car> GetCarsByBrand(int brandId) =>
            _cars.ReadAll().Where(c => c.BrandID == brandId).ToList();

        /// <inheritdoc/>
        public List<Car> GetCarsByMaxFuelConsumption(float maxFuelConsumption) =>
            _cars.ReadAll().Where(c => c.FuelConsumption <= maxFuelConsumption).ToList();

        /// <inheritdoc/>
        public List<Car> SearchCarsByLicensePlate(string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return _cars.ReadAll();

            string q = query.Trim();
            return _cars.ReadAll()
                .Where(c => c.LicensePlate.StartsWith(q, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
