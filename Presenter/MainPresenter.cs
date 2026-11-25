using BusinessLogic;
using Shared;

namespace Presenter
{
    /// <summary>
    /// Presenter главной формы: подписан на события View и Model
    /// перерисовывает View вызовом методов <see cref="IMainView"/>.
    /// </summary>
    public class MainPresenter
    {
        private readonly IMainView _view;
        private readonly IModel _model;

        /// <summary>
        /// Связывает View и Model событиями
        /// </summary>
        /// <param name="view">Представление</param>
        /// <param name="model">Модель</param>
        public MainPresenter(IMainView view, IModel model)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _model = model ?? throw new ArgumentNullException(nameof(model));

            // View в Presenter
            _view.ViewLoaded += (_, _) => Safe(() => { ShowBrands(); ShowAllCars(); });
            _view.ShowAllCarsRequested += (_, _) => Safe(ShowAllCars);
            _view.RefreshBrandsRequested += (_, _) => Safe(ShowBrands);
            _view.FindByBrandRequested += (_, brandId) => Safe(() => _view.ShowCars(_model.GetCarsByBrand(brandId)));
            _view.FindByFuelRequested += (_, max) => Safe(() => _view.ShowCars(_model.GetCarsByMaxFuelConsumption(max)));
            _view.FindByPlateRequested += (_, query) => Safe(() => _view.ShowCars(_model.SearchCarsByLicensePlate(query)));

            _view.AddCarRequested += (_, car) => _model.AddCar(car);
            _view.UpdateCarRequested += (_, car) => _model.UpdateCar(car);
            _view.DeleteCarRequested += (_, plate) => _model.DeleteCar(plate);
            _view.AddBrandRequested += (_, brand) => _model.AddBrand(brand);
            _view.UpdateBrandRequested += (_, brand) => _model.UpdateBrand(brand);
            _view.DeleteBrandRequested += (_, id) => _model.DeleteBrand(id);

            // Model в Presenter
            _model.CarsChanged += (_, _) => Safe(ShowAllCars);
            _model.BrandsChanged += (_, _) => Safe(() => { ShowBrands(); ShowAllCars(); });
            _model.OperationFailed += (_, e) => _view.ShowError(e.Message);
        }

        private void ShowAllCars() => _view.ShowCars(_model.GetAllCars());

        private void ShowBrands() => _view.ShowBrands(_model.GetAllBrands());

        /// <summary>
        /// Сообщение для View
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
    }
}
