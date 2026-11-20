using Model;

namespace BusinessLogic
{
    /// <summary>
    /// Контракт бизнес-функций автопарка: выборки и поиск (только чтение)
    /// </summary>
    public interface IBusinessLogic
    {
        /// <summary>
        /// Возвращает автомобили выбранного бренда
        /// </summary>
        /// <param name="brandId">Идентификатор бренда</param>
        /// <returns>Список автомобилей бренда</returns>
        List<Car> GetCarsByBrand(int brandId);

        /// <summary>
        /// Возвращает автомобили с расходом топлива не выше заданного
        /// </summary>
        /// <param name="maxFuelConsumption">Максимальный расход топлива</param>
        /// <returns>Список подходящих автомобилей</returns>
        List<Car> GetCarsByMaxFuelConsumption(float maxFuelConsumption);

        /// <summary>
        /// Ищет автомобили по госномеру или его началу
        /// </summary>
        /// <param name="query">Госномер или его начало; при пустом значении возвращаются все автомобили</param>
        /// <returns>Список найденных автомобилей</returns>
        List<Car> SearchCarsByLicensePlate(string? query);
    }
}
