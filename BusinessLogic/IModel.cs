using Shared;

namespace BusinessLogic
{
    /// <summary>
    /// Контракт слоя Model для Presenter: работа через DTO и события о выполненной работе.
    /// Presenter зависит только от этого интерфейса (DIP / Facade).
    /// </summary>
    public interface IModel
    {
        #region События Model (на них подписан Presenter)

        /// <summary>Данные об автомобилях изменились (добавление, изменение, удаление)</summary>
        event EventHandler? CarsChanged;

        /// <summary>Данные о брендах изменились (добавление, изменение, удаление)</summary>
        event EventHandler? BrandsChanged;

        /// <summary>Операция не выполнена (нарушено правило бизнес-логики или ошибка БД)</summary>
        event EventHandler<OperationFailedEventArgs>? OperationFailed;

        #endregion

        #region Запросы

        /// <summary>Возвращает все автомобили</summary>
        IReadOnlyList<CarDto> GetAllCars();

        /// <summary>Возвращает все бренды</summary>
        IReadOnlyList<BrandDto> GetAllBrands();

        /// <summary>Возвращает автомобили выбранного бренда</summary>
        /// <param name="brandId">Идентификатор бренда</param>
        IReadOnlyList<CarDto> GetCarsByBrand(int brandId);

        /// <summary>Возвращает автомобили с расходом не выше заданного</summary>
        /// <param name="maxFuelConsumption">Максимальный расход, л/100 км</param>
        IReadOnlyList<CarDto> GetCarsByMaxFuelConsumption(float maxFuelConsumption);

        /// <summary>Ищет автомобили по госномеру или его началу</summary>
        /// <param name="query">Госномер или его начало</param>
        IReadOnlyList<CarDto> SearchCarsByLicensePlate(string? query);

        #endregion

        #region Команды (результат сообщается событиями)

        /// <summary>Добавляет автомобиль</summary>
        void AddCar(CarDto car);

        /// <summary>Изменяет автомобиль</summary>
        void UpdateCar(CarDto car);

        /// <summary>Удаляет автомобиль по госномеру</summary>
        void DeleteCar(string licensePlate);

        /// <summary>Добавляет бренд</summary>
        void AddBrand(BrandDto brand);

        /// <summary>Изменяет бренд</summary>
        void UpdateBrand(BrandDto brand);

        /// <summary>Удаляет бренд по идентификатору</summary>
        void DeleteBrand(int brandId);

        #endregion
    }
}
