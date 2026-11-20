using Model;

namespace BusinessLogic
{
    /// <summary>
    /// Контракт CRUD-операций над автомобилями и брендами (с проверками целостности)
    /// </summary>
    public interface ICrudLogic
    {
        /// <summary>
        /// Проверяет формат госномера
        /// </summary>
        /// <param name="plate">Проверяемый госномер</param>
        /// <returns>true, если формат корректен</returns>
        bool IsValidLicensePlate(string? plate);

        #region Авто

        /// <summary>
        /// Возвращает все автомобили
        /// </summary>
        /// <returns>Список всех автомобилей</returns>
        List<Car> GetAllCars();

        /// <summary>
        /// Возвращает автомобиль по госномеру
        /// </summary>
        /// <param name="plate">Госномер (регистр и пробелы по краям не учитываются)</param>
        /// <returns>Автомобиль или null, если не найден</returns>
        Car? GetCarById(string plate);

        /// <summary>
        /// Добавляет автомобиль
        /// </summary>
        /// <param name="car">Добавляемый автомобиль</param>
        /// <returns>
        /// true, если автомобиль добавлен; false, если госномер некорректен или уже занят,
        /// либо бренд не существует
        /// </returns>
        bool AddCar(Car car);

        /// <summary>
        /// Обновляет данные автомобиля
        /// </summary>
        /// <param name="car">Автомобиль с новыми данными</param>
        /// <returns>true, если запись обновлена; false, если бренд не существует или авто не найдено</returns>
        bool UpdateCar(Car car);

        /// <summary>
        /// Удаляет автомобиль по госномеру
        /// </summary>
        /// <param name="licensePlate">Госномер удаляемого автомобиля</param>
        /// <returns>true, если запись удалена</returns>
        bool DeleteCar(string licensePlate);

        #endregion

        #region Бренды

        /// <summary>
        /// Возвращает все бренды
        /// </summary>
        /// <returns>Список всех брендов</returns>
        List<Brand> GetAllBrands();

        /// <summary>
        /// Возвращает бренд по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор бренда</param>
        /// <returns>Бренд или null, если не найден</returns>
        Brand? GetBrandById(int id);

        /// <summary>
        /// Возвращает название бренда по идентификатору
        /// </summary>
        /// <param name="brandId">Идентификатор бренда</param>
        /// <returns>Название бренда или «Неизвестный бренд», если бренд не найден</returns>
        string GetBrandName(int brandId);

        /// <summary>
        /// Добавляет бренд
        /// </summary>
        /// <param name="brand">Добавляемый бренд</param>
        /// <returns>true, если бренд добавлен; false, если бренд с таким названием уже есть</returns>
        bool AddBrand(Brand brand);

        /// <summary>
        /// Обновляет данные бренда
        /// </summary>
        /// <param name="brand">Бренд с новыми данными</param>
        /// <returns>
        /// true, если запись обновлена; false, если бренд не найден
        /// или название уже занято другим брендом
        /// </returns>
        bool UpdateBrand(Brand brand);

        /// <summary>
        /// Удаляет бренд по идентификатору
        /// </summary>
        /// <param name="brandId">Идентификатор бренда</param>
        /// <returns>true, если бренд удалён; false, если у бренда есть автомобили или он не найден</returns>
        bool DeleteBrand(int brandId);

        #endregion
    }
}
