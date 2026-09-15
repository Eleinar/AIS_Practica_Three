using System;
using System.Collections.Generic;
using System.Text;

namespace AIS_Practica_One_Model
{
    public class Car
    {
        public int ID { get; set; }
        public int BrandID { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public int ProductionYear { get; set; }
        public float HorsePower { get; set; }
        public float FuelTankVolume { get; set; }
        public float FuelConsumption { get; set; }
        public string Color { get; set; } = string.Empty;


    }
}
