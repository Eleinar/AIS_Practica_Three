using AIS_Practica_One_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AIS_Practica_One
{
    public partial class CarForm : Form
    {
        public Car Car { get; private set; }
        public CarForm(Logic logic, Car? car = null)
        {
            InitializeComponent();
            Car = car ?? new Car();
            Text = car == null ? "Добавление автомобиля" : "Изменение автомобиля";
            brandBox.DataSource = logic.GetAllBrands();
            brandBox.DisplayMember = nameof(Brand.Name);
            brandBox.ValueMember = nameof(Brand.ID);
            if (car != null)
            {
                brandBox.SelectedValue = car.BrandID;
                modelBox.Text = car.ModelName;
                yearBox.Value = Math.Clamp(car.ProductionYear, (int)yearBox.Minimum, (int)yearBox.Maximum);
                horsePowerBox.Value = Math.Clamp((decimal)car.HorsePower, horsePowerBox.Minimum, horsePowerBox.Maximum);
                tankBox.Value = Math.Clamp((decimal)car.FuelTankVolume, tankBox.Minimum, tankBox.Maximum);
                consumptionBox.Value = Math.Clamp((decimal)car.FuelConsumption, consumptionBox.Minimum, consumptionBox.Maximum);
                colorBox.Text = car.Color;
            }

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (brandBox.SelectedItem is not Brand brand) { MessageBox.Show("Сначала добавьте бренд."); return; }
            if (string.IsNullOrWhiteSpace(modelBox.Text)) { MessageBox.Show("Введите модель."); return; }
            Car.BrandID = brand.ID;
            Car.ModelName = modelBox.Text.Trim();
            Car.ProductionYear = (int)yearBox.Value;
            Car.HorsePower = (float)horsePowerBox.Value;
            Car.FuelTankVolume = (float)tankBox.Value;
            Car.FuelConsumption = (float)consumptionBox.Value;
            Car.Color = colorBox.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
