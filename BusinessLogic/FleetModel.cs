using Model;
using Shared;

namespace BusinessLogic
{
    /// <summary>
    /// Реализация <see cref="IModel"/>: фасад над <see cref="ILogic"/>.
    /// Преобразует доменные объекты в DTO и сообщает Presenter о проделанной работе событиями.
    /// </summary>
    public class FleetModel : IModel
    {
        private const string UnknownBrand = "Неизвестный бренд";

        private readonly ILogic _logic;

        /// <inheritdoc/>
        public event EventHandler? CarsChanged;

        /// <inheritdoc/>
        public event EventHandler? BrandsChanged;

        /// <inheritdoc/>
        public event EventHandler<OperationFailedEventArgs>? OperationFailed;

        /// <summary>
        /// Создаёт модель поверх бизнес-логики
        /// </summary>
        /// <param name="logic">Бизнес-логика (CRUD + выборки)</param>
        public FleetModel(ILogic logic)
        {
            _logic = logic ?? throw new ArgumentNullException(nameof(logic));
        }

        #region Запросы

        /// <inheritdoc/>
        public IReadOnlyList<CarDto> GetAllCars() => ToDtos(_logic.GetAllCars());

        /// <inheritdoc/>
        public IReadOnlyList<BrandDto> GetAllBrands() =>
            _logic.GetAllBrands().Select(b => b.ToDto()).ToList();

        /// <inheritdoc/>
        public IReadOnlyList<CarDto> GetCarsByBrand(int brandId) =>
            ToDtos(_logic.GetCarsByBrand(brandId));

        /// <inheritdoc/>
        public IReadOnlyList<CarDto> GetCarsByMaxFuelConsumption(float maxFuelConsumption) =>
            ToDtos(_logic.GetCarsByMaxFuelConsumption(maxFuelConsumption));

        /// <inheritdoc/>
        public IReadOnlyList<CarDto> SearchCarsByLicensePlate(string? query) =>
            ToDtos(_logic.SearchCarsByLicensePlate(query));

        /// <summary>
        /// Преобразует автомобили в DTO, подставляя названия брендов одним запросом
        /// </summary>
        private List<CarDto> ToDtos(IEnumerable<Car> cars)
        {
            Dictionary<int, string> brandNames = _logic.GetAllBrands().ToDictionary(b => b.ID, b => b.Name);
            return cars
                .Select(c => c.ToDto(brandNames.GetValueOrDefault(c.BrandID, UnknownBrand)))
                .ToList();
        }

        #endregion

        #region Команды

        /// <inheritdoc/>
        public void AddCar(CarDto car) =>
            Execute(() => _logic.AddCar(car.ToEntity()),
                "Не удалось добавить автомобиль. Проверьте бренд, уникальность госномера и его формат " +
                "(А001АА777, буквы АВЕКМНОРСТУХ).",
                RaiseCarsChanged);

        /// <inheritdoc/>
        public void UpdateCar(CarDto car) =>
            Execute(() => _logic.UpdateCar(car.ToEntity()),
                "Не удалось изменить автомобиль.",
                RaiseCarsChanged);

        /// <inheritdoc/>
        public void DeleteCar(string licensePlate) =>
            Execute(() => _logic.DeleteCar(licensePlate),
                "Не удалось удалить автомобиль.",
                RaiseCarsChanged);

        /// <inheritdoc/>
        public void AddBrand(BrandDto brand) =>
            Execute(() => _logic.AddBrand(brand.ToEntity()),
                "Бренд с таким названием уже существует.",
                RaiseBrandsChanged);

        /// <inheritdoc/>
        public void UpdateBrand(BrandDto brand) =>
            Execute(() => _logic.UpdateBrand(brand.ToEntity()),
                "Не удалось изменить бренд. Проверьте название.",
                RaiseBrandsChanged);

        /// <inheritdoc/>
        public void DeleteBrand(int brandId) =>
            Execute(() => _logic.DeleteBrand(brandId),
                "Бренд не найден или используется автомобилями.",
                RaiseBrandsChanged);

        #endregion

        /// <summary>
        /// Выполняет операцию бизнес-логики и сообщает о результате событием
        /// </summary>
        /// <param name="operation">Операция, возвращающая признак успеха</param>
        /// <param name="errorMessage">Сообщение при неуспехе</param>
        /// <param name="onSuccess">Действие при успехе (генерация события)</param>
        private void Execute(Func<bool> operation, string errorMessage, Action onSuccess)
        {
            try
            {
                if (operation())
                    onSuccess();
                else
                    OperationFailed?.Invoke(this, new OperationFailedEventArgs(errorMessage));
            }
            catch (Exception ex)
            {
                string details = ex.InnerException?.Message ?? ex.Message;
                OperationFailed?.Invoke(this, new OperationFailedEventArgs($"{errorMessage}\n{details}"));
            }
        }

        private void RaiseCarsChanged() => CarsChanged?.Invoke(this, EventArgs.Empty);

        private void RaiseBrandsChanged() => BrandsChanged?.Invoke(this, EventArgs.Empty);
    }
}
