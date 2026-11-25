namespace Shared
{
    /// <summary>
    /// DTO автомобиля
    /// </summary>
    public class CarDto
    {
       
        public string LicensePlate { get; set; } = string.Empty;

        public int BrandId { get; set; }

        public string BrandName { get; set; } = string.Empty;

        public string ModelName { get; set; } = string.Empty;

        public int ProductionYear { get; set; }

        public float HorsePower { get; set; }

        public float FuelTankVolume { get; set; }

        public float FuelConsumption { get; set; }

        public string Color { get; set; } = string.Empty;

        public byte[]? Image { get; set; }
    }
}
