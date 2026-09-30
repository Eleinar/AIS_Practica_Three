using System;
using System.Collections.Generic;
using System.Text;

namespace AIS_Practica_One_Model
{
    internal class CarFileEditor
    {
        /// <summary>
        /// Читает список автомобилей из файла по указанному пути
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
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
                if (cars.Any(c => c.LicensePlate == properties[0].Trim().ToUpper()))
                {
                    throw new FormatException($"Повторяющийся номерной знак: {properties[0].Trim().ToUpper()}");
                }

                    try
                {
                    Car car = new Car
                    {
                        LicensePlate = properties[0].Trim().ToUpper(),
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

        /// <summary>
        /// Сохраняет список автомобилей в файл по указанному пути
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="cars"></param>
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
                    $"{car.LicensePlate};" +
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
