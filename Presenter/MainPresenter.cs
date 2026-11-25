using BusinessLogic;
using Shared;

namespace Presenter
{
    /// <summary>
    /// Presenter главной View. Знает только абстракции: <see cref="IMainView"/> (Shared) и <see cref="IModel"/>.
    /// Подписан на события View и Model
    /// </summary>
    public class MainPresenter : IDisposable
    {
        private readonly IMainView _view;
        private readonly IModel _model;

        /// <summary>
        /// Текущая выборка автомобилей
        /// </summary>
        private Func<IReadOnlyList<CarDto>> _carsQuery;

        private bool _disposed;

        /// <summary>
        /// Связывает View и Model событиями
        /// </summary>
        /// <param name="view">Представление (любая реализация: WinForms, консоль)</param>
        /// <param name="model">Модель (фасад над бизнес-логикой)</param>
        public MainPresenter(IMainView view, IModel model)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _carsQuery = _model.GetAllCars;

            SubscribeToView();
            SubscribeToModel();
        }

        #region Подписки

        private void SubscribeToView()
        {
            _view.ViewLoaded += OnViewLoaded;
            _view.ShowAllCarsRequested += OnShowAllCarsRequested;
            _view.RefreshBrandsRequested += OnRefreshBrandsRequested;
            _view.FindByBrandRequested += OnFindByBrandRequested;
            _view.FindByFuelRequested += OnFindByFuelRequested;
            _view.FindByPlateRequested += OnFindByPlateRequested;

            _view.AddCarRequested += OnAddCarRequested;
            _view.UpdateCarRequested += OnUpdateCarRequested;
            _view.DeleteCarRequested += OnDeleteCarRequested;
            _view.AddBrandRequested += OnAddBrandRequested;
            _view.UpdateBrandRequested += OnUpdateBrandRequested;
            _view.DeleteBrandRequested += OnDeleteBrandRequested;
        }

        private void SubscribeToModel()
        {
            _model.CarsChanged += OnCarsChanged;
            _model.BrandsChanged += OnBrandsChanged;
            _model.OperationFailed += OnOperationFailed;
        }

        /// <summary>
        /// Отписывается от View и Model
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;

            _view.ViewLoaded -= OnViewLoaded;
            _view.ShowAllCarsRequested -= OnShowAllCarsRequested;
            _view.RefreshBrandsRequested -= OnRefreshBrandsRequested;
            _view.FindByBrandRequested -= OnFindByBrandRequested;
            _view.FindByFuelRequested -= OnFindByFuelRequested;
            _view.FindByPlateRequested -= OnFindByPlateRequested;

            _view.AddCarRequested -= OnAddCarRequested;
            _view.UpdateCarRequested -= OnUpdateCarRequested;
            _view.DeleteCarRequested -= OnDeleteCarRequested;
            _view.AddBrandRequested -= OnAddBrandRequested;
            _view.UpdateBrandRequested -= OnUpdateBrandRequested;
            _view.DeleteBrandRequested -= OnDeleteBrandRequested;

            _model.CarsChanged -= OnCarsChanged;
            _model.BrandsChanged -= OnBrandsChanged;
            _model.OperationFailed -= OnOperationFailed;
        }
        
        #endregion

        #region События View в Presenter

        private void OnViewLoaded(object? sender, EventArgs e) =>
            Safe(() => { ShowBrands(); RefreshCars(); });

        private void OnShowAllCarsRequested(object? sender, EventArgs e) =>
            Safe(() => ApplyCarsQuery(_model.GetAllCars));

        private void OnRefreshBrandsRequested(object? sender, EventArgs e) =>
            Safe(ShowBrands);

        private void OnFindByBrandRequested(object? sender, int brandId) =>
            Safe(() => ApplyCarsQuery(() => _model.GetCarsByBrand(brandId)));

        private void OnFindByFuelRequested(object? sender, float maxFuelConsumption) =>
            Safe(() => ApplyCarsQuery(() => _model.GetCarsByMaxFuelConsumption(maxFuelConsumption)));

        private void OnFindByPlateRequested(object? sender, string query) =>
            Safe(() => ApplyCarsQuery(() => _model.SearchCarsByLicensePlate(query)));

        private void OnAddCarRequested(object? sender, CarDto car) => Safe(() => _model.AddCar(car));

        private void OnUpdateCarRequested(object? sender, CarDto car) => Safe(() => _model.UpdateCar(car));

        private void OnDeleteCarRequested(object? sender, string licensePlate) =>
            Safe(() => _model.DeleteCar(licensePlate));

        private void OnAddBrandRequested(object? sender, BrandDto brand) => Safe(() => _model.AddBrand(brand));

        private void OnUpdateBrandRequested(object? sender, BrandDto brand) => Safe(() => _model.UpdateBrand(brand));

        private void OnDeleteBrandRequested(object? sender, int brandId) => Safe(() => _model.DeleteBrand(brandId));

        #endregion

        #region События Model в Presenter

        /// <summary>
        /// Автомобили изменились
        /// </summary>
        private void OnCarsChanged(object? sender, EventArgs e) => Safe(RefreshCars);

        /// <summary>
        /// Бренды изменились
        /// </summary>
        private void OnBrandsChanged(object? sender, EventArgs e) =>
            Safe(() => { ShowBrands(); RefreshCars(); });

        /// <summary>
        /// Ошибка в операции
        /// </summary>
        private void OnOperationFailed(object? sender, OperationFailedEventArgs e) =>
            _view.ShowError(e.Message);

        #endregion

        #region Отрисовка

        /// <summary>
        /// Выполняет выборку автомобилей, запоминает её как текущую и отдаёт результат View.
        /// </summary>
        private void ApplyCarsQuery(Func<IReadOnlyList<CarDto>> query)
        {
            IReadOnlyList<CarDto> cars = query();
            _carsQuery = query;
            _view.ShowCars(cars);
        }

        private void RefreshCars() => _view.ShowCars(_carsQuery());

        private void ShowBrands() => _view.ShowBrands(_model.GetAllBrands());

        /// <summary>
        /// Выполняет действие
        /// при исключении показывает ошибку во View
        /// </summary>
        private void Safe(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при обращении к данным:\n{ex.InnerException?.Message ?? ex.Message}");
            }
        }

        #endregion
    }
}
