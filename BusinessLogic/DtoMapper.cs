using Model;
using Shared;

namespace BusinessLogic
{
    /// <summary>
    /// Преобразование доменных объектов в DTO и обратно
    /// </summary>
    internal static class DtoMapper
    {
        public static CarDto ToDto(this Car car, string brandName) => new()
        {
            LicensePlate = car.LicensePlate,
            BrandId = car.BrandID,
            BrandName = brandName,
            ModelName = car.ModelName,
            ProductionYear = car.ProductionYear,
            HorsePower = car.HorsePower,
            FuelTankVolume = car.FuelTankVolume,
            FuelConsumption = car.FuelConsumption,
            Color = car.Color,
            Image = car.Image
        };

        public static Car ToEntity(this CarDto dto) => new()
        {
            LicensePlate = dto.LicensePlate,
            BrandID = dto.BrandId,
            ModelName = dto.ModelName,
            ProductionYear = dto.ProductionYear,
            HorsePower = dto.HorsePower,
            FuelTankVolume = dto.FuelTankVolume,
            FuelConsumption = dto.FuelConsumption,
            Color = dto.Color,
            Image = dto.Image
        };

        public static BrandDto ToDto(this Brand brand) => new()
        {
            Id = brand.ID,
            Name = brand.Name,
            Country = brand.Country
        };

        public static Brand ToEntity(this BrandDto dto) => new()
        {
            ID = dto.Id,
            Name = dto.Name,
            Country = dto.Country
        };
    }
}
