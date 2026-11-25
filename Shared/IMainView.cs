namespace Shared
{
    /// <summary>
    /// Контракт главной формы: события (View → Presenter) и методы отрисовки (Presenter → View)
    /// </summary>
    public interface IMainView : IView
    {
        #region События View (на них подписан Presenter)

        /// <summary>
        /// Форма загружена и готова к показу данных
        /// </summary>
        event EventHandler? ViewLoaded;

        /// <summary>
        /// Запрошен полный список автомобилей (сброс фильтров / «Обновить»)
        /// </summary>
        event EventHandler? ShowAllCarsRequested;

        /// <summary>
        /// Запрошено обновление списка брендов
        /// </summary>
        event EventHandler? RefreshBrandsRequested;

        /// <summary>
        /// Поиск по бренду (аргумент — идентификатор бренда)
        /// </summary>
        event EventHandler<int>? FindByBrandRequested;

        /// <summary>
        /// Поиск по максимальному расходу топлива
        /// </summary>
        event EventHandler<float>? FindByFuelRequested;

        /// <summary>
        /// Поиск по госномеру или его началу
        /// </summary>
        event EventHandler<string>? FindByPlateRequested;

        /// <summary>
        /// обавление автомобиля
        /// </summary>
        event EventHandler<CarDto>? AddCarRequested;

        /// <summary>
        /// Изменение автомобиля
        /// </summary>
        event EventHandler<CarDto>? UpdateCarRequested;

        /// <summary>
        /// Удаление автомобиля (аргумент — госномер)
        /// </summary>
        event EventHandler<string>? DeleteCarRequested;

        /// <summary>
        /// Добавление бренда
        /// </summary>
        event EventHandler<BrandDto>? AddBrandRequested;

        /// <summary>
        /// Изменение бренда
        /// </summary>
        event EventHandler<BrandDto>? UpdateBrandRequested;

        /// <summary>
        /// Удаление бренда (аргумент — идентификатор)
        /// </summary>
        event EventHandler<int>? DeleteBrandRequested;

        #endregion

        #region Методы отрисовки (вызывает Presenter)

        /// <summary>
        /// Отображает список автомобилей
        /// </summary>
        /// <param name="cars">Автомобили для показа</param>
        void ShowCars(IReadOnlyList<CarDto> cars);

        /// <summary>
        /// Отображает список брендов
        /// </summary>
        /// <param name="brands">Бренды для показа</param>
        void ShowBrands(IReadOnlyList<BrandDto> brands);

        #endregion
    }
}
