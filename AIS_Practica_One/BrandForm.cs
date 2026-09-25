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
    public partial class BrandForm : Form
    {
        public Brand Brand { get; private set; }

        /// <summary>
        /// Конструктор формы для работы с брендами
        /// </summary>
        /// <param name="brand"></param>

        public BrandForm(Brand? brand = null)
        {
            InitializeComponent();
            Brand = brand ?? new Brand();
            Text = brand == null ? "Добавление бренда" : "Изменение бренда";
            if (brand != null)
            {
                nameBox.Text = brand.Name;
                countryBox.Text = brand.Country;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text)) { MessageBox.Show("Введите название бренда."); return; }
            Brand.Name = nameBox.Text.Trim();
            Brand.Country = countryBox.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
