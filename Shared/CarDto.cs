namespace Shared
{
    /// <summary>
    /// DTO автомобиля: передаётся между View, Presenter и Model, чтобы View не зависело от доменных классов
    /// </summary>
    public class CarDto
    {
        /// <summary>Госномер (идентификатор)</summary>
        public string LicensePlate { get; set; } = string.Empty;

        /// <summary>Идентификатор бренда</summary>
        public int BrandId { get; set; }

        /// <summary>Название бренда (заполняется слоем Model для отображения)</summary>
        public string BrandName { get; set; } = string.Empty;

        /// <summary>Название модели</summary>
        public string ModelName { get; set; } = string.Empty;

        /// <summary>Год выпуска</summary>
        public int ProductionYear { get; set; }

        /// <summary>Мощность, л.с.</summary>
        public float HorsePower { get; set; }

        /// <summary>Объём бака, л</summary>
        public float FuelTankVolume { get; set; }

        /// <summary>Расход топлива, л/100 км</summary>
        public float FuelConsumption { get; set; }

        /// <summary>Цвет</summary>
        public string Color { get; set; } = string.Empty;

        /// <summary>Фото автомобиля</summary>
        public byte[]? Image { get; set; }
    }
}
