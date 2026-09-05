using System;
using System.Collections.Generic;
using System.Text;

namespace AIS_Practica_One_Model
{
    internal class Logic
    {
        private List<Car> _cars = new();
        private List<Brand> _brands = new();

        public void LoadBrands(string filePath)
        {

        }
               
        public void SaveBrands(string filePath)
        {

        }

        public List<Brand>GetAllBrands()
        {
            return new List<Brand>(_brands);
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

        public void LoadCars(string filePath)
        {

        }

        public void SaveCars(string filePath)
        {
        }

    }
}
