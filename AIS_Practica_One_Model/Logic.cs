using System;
using System.Collections.Generic;
using System.Text;

namespace AIS_Practica_One_Model
{
    public class Logic
    {
        private List<Car> _cars = new();
        private List<Brand> _brands = new();
        private readonly CarFileEditor _carEditor;
        private readonly BrandFileEditor _brandEditor;

        private readonly string _carsFilePath;
        private readonly string _brandsFilePath;

        public Logic(string dataDirectory)
        {
            _carEditor = new CarFileEditor();
            _brandEditor = new BrandFileEditor();

            _carsFilePath = Path.Combine(dataDirectory, "Cars.csv");
            _brandsFilePath = Path.Combine(dataDirectory, "Brands.csv");

            _brands = _brandEditor.Load(_brandsFilePath);
            _cars = _carEditor.LoadCars(_carsFilePath);
        }

        public void SaveAll()
        {
            SaveBrands();
            SaveCars();
        }

        private void SaveCars()
        {
            _carEditor.SaveCars(_carsFilePath, _cars);
        }

        private void SaveBrands()
        {
            _brandEditor.Save(_brandsFilePath, _brands);
        }

     
        public List<Car> GetAllCars()
        {
            return new List<Car>(_cars);
        }

        public Car? GetCarById(int id)
        {
            return _cars.FirstOrDefault(car => car.ID == id);
        }

     
        public int GetNextCarID()
        {
            if(_cars.Count == 0)
            {
                return 1;
            }
            return _cars.Max(car => car.ID) + 1;
        }

        public bool AddCar(Car car)
        {
            if (car == null)
            {
                return false;
            }

            bool brandExists = _brands.Any(b => b.ID == car.BrandID);

            if (!brandExists)
            {
                return false;
            }

            car.ID = GetNextCarID();
            _cars.Add(car);

            return true;
        }

        public bool UpdateCar(Car car)
        {
            if(car == null)
            {
                return false;
            }

            bool brandExists = _brands.Any(b => b.ID == car.BrandID);

            if(!brandExists)
            {
                return false;
            }

            Car? existingCar = GetCarById(car.ID);

            if (existingCar == null)
            {
                return false;
            }

            existingCar.BrandID = car.BrandID;
            existingCar.ModelName = car.ModelName;
            existingCar.ProductionYear = car.ProductionYear;
            existingCar.HorsePower = car.HorsePower;
            existingCar.FuelTankVolume = car.FuelTankVolume;
            existingCar.FuelConsumption = car.FuelConsumption;
            existingCar.Color = car.Color;

            return true;
        }

        public bool DeleteCar(int carID)
        {
            Car? car = GetCarById(carID);
            if (car == null)
            {
                return false;
            }
            _cars.Remove(car);
            return true;
        }

        public List<Brand> GetAllBrands()
        {
            return new List<Brand>(_brands);
        }


        public Brand? GetBrandById(int id)
        {
            return _brands.FirstOrDefault(brand => brand.ID == id);
        }

        public string GetBrandName(int brandId)
        {
            Brand? brand = GetBrandById(brandId);

            if (brand == null)
            {
                return "Неизвестный бренд";
            }

            return brand.Name;
        }

        public bool AddBrand(Brand brand)
        {
            bool alreadyExists = _brands.Any(b =>
                                             b.Name.Equals(brand.Name, StringComparison.OrdinalIgnoreCase));

            if(alreadyExists)
            {
                return false;
            }

            brand.ID = GetNextBrandID();
            _brands.Add(brand);

            return true;
        }

        public bool UpdateBrand(Brand brand)
        {
            if(brand == null)
            {
                return false;
            }

            Brand? existingBrand = GetBrandById(brand.ID);

            if (existingBrand == null)
            {
                return false;
            }

            bool alreadyExists = _brands.Any(b =>
                                             b.Name.Equals(brand.Name, StringComparison.OrdinalIgnoreCase) &&
                                             b.ID != brand.ID);

            if (alreadyExists)
            {
                return false;
            }

            existingBrand.Name = brand.Name;
            existingBrand.Country = brand.Country;

            return true;

        }

        public bool DeleteBrand(int brandID)
        {
            bool isUsed = _cars.Any(car => car.BrandID == brandID);

            if(isUsed)
            {
                return false;
            }

            Brand? brand = _brands.FirstOrDefault(b => b.ID == brandID);
            if(brand == null)
            {
                return false;
            }

            _brands.Remove(brand);
            return true;
        }

        public int GetNextBrandID()
        {
            if (_brands.Count == 0)
            {
                return 1;
            }
            return _brands.Max(b => b.ID) + 1;
        }

        public List<Car> GetCarsByBrand(int brandID)
        {
            return _cars.Where(car => car.BrandID == brandID).ToList();
        }
   
        public List<Car> GetCarsByFuelConsuption( float maxFuelConsumption)
        {
            return _cars.Where(car => car.FuelConsumption <= maxFuelConsumption).ToList();
        }

    }
}
