using AIS_Practica_One_Model;

namespace AIS_Practica_One
{
    public partial class Form1 : Form
    {
        private readonly Logic _logic;

        public Form1()
        {
            InitializeComponent();
            _logic = new Logic("C:\\Users\\eleinar\\source\\repos\\AIS_Practica_One");
            RefreshBrands();
            RefreshCars();
        }

        private void RefreshCars() => ShowCars(_logic.GetAllCars());

        private void ShowCars(List<Car> cars)
        {
            carsGrid.DataSource = null;
            carsGrid.DataSource = cars.Select(car => new
            {
                car.ID,
                Бренд = _logic.GetBrandName(car.BrandID),
                Модель = car.ModelName,
                Год = car.ProductionYear,
                Мощность = car.HorsePower,
                Бак = car.FuelTankVolume,
                Расход = car.FuelConsumption,
                Цвет = car.Color
            }).ToList();
            carsGrid.ClearSelection();
        }

        private void RefreshBrands()
        {
            var brands = _logic.GetAllBrands();
            brandsGrid.DataSource = null;
            brandsGrid.DataSource = brands.Select(b => new { b.ID, Название = b.Name, Страна = b.Country }).ToList();
            brandsGrid.ClearSelection();
            brandComboBox.DataSource = null;
            brandComboBox.DataSource = new List<Brand>(brands);
            brandComboBox.DisplayMember = nameof(Brand.Name);
            brandComboBox.ValueMember = nameof(Brand.ID);
        }

        private int? SelectedId(DataGridView grid)
        {
            if (grid.SelectedRows.Count == 0) return null;
            return Convert.ToInt32(grid.SelectedRows[0].Cells["ID"].Value);
        }

        private void addCarButton_Click(object sender, EventArgs e)
        {
            using var form = new CarForm(_logic);
            if (form.ShowDialog(this) != DialogResult.OK) return;
            if (!_logic.AddCar(form.Car)) { MessageBox.Show("Не удалось добавить автомобиль. Проверьте бренд."); return; }
            _logic.SaveAll();
            RefreshCars();
        }

        private void editCarButton_Click(object sender, EventArgs e)
        {
            int? id = SelectedId(carsGrid);
            if (id == null) { MessageBox.Show("Выберите автомобиль."); return; }
            Car? source = _logic.GetCarById(id.Value);
            if (source == null) return;
            var copy = new Car
            {
                ID = source.ID,
                BrandID = source.BrandID,
                ModelName = source.ModelName,
                ProductionYear = source.ProductionYear,
                HorsePower = source.HorsePower,
                FuelTankVolume = source.FuelTankVolume,
                FuelConsumption = source.FuelConsumption,
                Color = source.Color
            };
            using var form = new CarForm(_logic, copy);
            if (form.ShowDialog(this) != DialogResult.OK) return;
            if (!_logic.UpdateCar(form.Car)) { MessageBox.Show("Не удалось изменить автомобиль."); return; }
            _logic.SaveAll();
            RefreshCars();
        }

        private void deleteCarButton_Click(object sender, EventArgs e)
        {
            int? id = SelectedId(carsGrid);
            if (id == null) { MessageBox.Show("Выберите автомобиль."); return; }
            if (MessageBox.Show("Удалить автомобиль?", "Подтверждение", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            if (!_logic.DeleteCar(id.Value)) { MessageBox.Show("Не удалось удалить автомобиль."); return; }
            _logic.SaveAll();
            RefreshCars();
        }

        private void refreshCarsButton_Click(object sender, EventArgs e) => RefreshCars();

        private void findByBrandButton_Click(object sender, EventArgs e)
        {
            if (brandComboBox.SelectedItem is Brand brand)
                ShowCars(_logic.GetCarsByBrand(brand.ID));
            else
                MessageBox.Show("Сначала добавьте или выберите бренд.");
        }

        private void findByFuelButton_Click(object sender, EventArgs e) =>
            ShowCars(_logic.GetCarsByFuelConsuption((float)fuelConsumptionInput.Value));

        private void showAllCarsButton_Click(object sender, EventArgs e) => RefreshCars();

        private void addBrandButton_Click(object sender, EventArgs e)
        {
            using var form = new BrandForm();
            if (form.ShowDialog(this) != DialogResult.OK) return;
            if (!_logic.AddBrand(form.Brand)) { MessageBox.Show("Бренд с таким названием уже существует."); return; }
            _logic.SaveAll();
            RefreshBrands();
            RefreshCars();
        }

        private void editBrandButton_Click(object sender, EventArgs e)
        {
            int? id = SelectedId(brandsGrid);
            if (id == null) { MessageBox.Show("Выберите бренд."); return; }
            Brand? source = _logic.GetBrandById(id.Value);
            if (source == null) return;
            using var form = new BrandForm(new Brand { ID = source.ID, Name = source.Name, Country = source.Country });
            if (form.ShowDialog(this) != DialogResult.OK) return;
            if (!_logic.UpdateBrand(form.Brand)) { MessageBox.Show("Не удалось изменить бренд. Проверьте название."); return; }
            _logic.SaveAll();
            RefreshBrands();
            RefreshCars();
        }

        private void deleteBrandButton_Click(object sender, EventArgs e)
        {
            int? id = SelectedId(brandsGrid);
            if (id == null) { MessageBox.Show("Выберите бренд."); return; }
            if (MessageBox.Show("Удалить бренд?", "Подтверждение", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            if (!_logic.DeleteBrand(id.Value)) { MessageBox.Show("Бренд не найден или используется автомобилями."); return; }
            _logic.SaveAll();
            RefreshBrands();
        }

        private void refreshBrandsButton_Click(object sender, EventArgs e) => RefreshBrands();
    }
}
