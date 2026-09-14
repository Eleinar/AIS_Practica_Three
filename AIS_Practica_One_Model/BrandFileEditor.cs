using System;
using System.Collections.Generic;
using System.Text;

namespace AIS_Practica_One_Model
{
    internal class BrandFileEditor
    {
        public List<Brand> Load(string filePath)
        {
            List<Brand> brands = new List<Brand>();

            if(!File.Exists(filePath)) {
                return brands;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] properties = line.Split(';');

                if(properties.Length != 3)
                {
                    continue;
                }

                try
                {
                    Brand brand = new Brand
                    {
                        ID = int.Parse(properties[0]),
                        Name = properties[1],
                        Country = properties[2]
                    };
                    brands.Add(brand);
                }
                catch (FormatException)
                {
                    continue;
                }
            }
            return brands;
        }

        public void Save(string filePath, List<Brand> brands)
        {
            string? directoryPath = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            List<string> lines = new List<string>();

            foreach (Brand brand in brands)
            {
                lines.Add($"{brand.ID};" +
                    $"{brand.Name};" +
                    $"{brand.Country}");
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}
