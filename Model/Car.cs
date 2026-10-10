using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Model
{
    [Table("cars")]
    public class Car : IDomainObject<string>
    {
        [Key]
        [Column("license_plate")]
        public string LicensePlate { get; set; } = string.Empty;

        [Column("brand_id")]
        public int BrandID { get; set; }

        [Column("model_name")]
        public string ModelName { get; set; } = string.Empty;

        [Column("production_year")]
        public int ProductionYear { get; set; }

        [Column("horse_power")]
        public float HorsePower { get; set; }

        [Column("fuel_tank_volume")]
        public float FuelTankVolume { get; set; }

        [Column("fuel_consumption")]
        public float FuelConsumption { get; set; }

        [Column("color")]
        public string Color { get; set; } = string.Empty;

        [Column("image")]
        public byte[]? Image { get; set; }

        [NotMapped]
        public string ID => LicensePlate;

    }
}
