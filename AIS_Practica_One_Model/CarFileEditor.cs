using System;
using System.Collections.Generic;
using System.Text;

namespace AIS_Practica_One_Model
{
    internal class CarFileEditor
    {
        public List<Car> LoadCars(string filePath)
        {
            List<Car> cars = new List<Car>();

            if (!File.Exists(filePath))
            {
                return cars;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] properties = line.Split(';');

                if (properties.Length != 8)
                {
                    continue;
                }

                try
                {
                    Car car = new Car
                    {
                        ID = int.Parse(properties[0]),
                        BrandID = int.Parse(properties[1]),
                        ModelName = properties[2],
                        ProductionYear = int.Parse(properties[3]),
                        HorsePower = float.Parse(properties[4]),
                        FuelTankVolume = float.Parse(properties[5]),
                        FuelConsumption = float.Parse(properties[6]),
                        Color = properties[7]
                    };

                    cars.Add(car);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return cars;
        }

        public void SaveCars(string filePath, List<Car> cars)
        {
            string? directory = Path.GetDirectoryName(filePath);

            if(!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            List<string> lines = new List<string>();

            foreach (Car car in cars)
            {
                string line = 
                    $"{car.ID};" +
                    $"{car.BrandID};" +
                    $"{car.ModelName};" +
                    $"{car.ProductionYear};" +
                    $"{car.HorsePower};" +
                    $"{car.FuelTankVolume};" +
                    $"{car.FuelConsumption};" +
                    $"{car.Color}";
                lines.Add(line);
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}
