using Shared;

namespace WinForms
{
    public partial class CarForm : Form
    {
        private readonly bool _isNew;
        private byte[]? _imageBytes;

        /// <summary>
        /// Результат заполнения формы — объект автомобиля
        /// </summary>
        public CarDto Car { get; private set; }

        /// <summary>
        /// Признак того, что пользователь подтвердил удаление автомобиля
        /// </summary>
        public bool Deleted { get; private set; }

        /// <summary>
        /// Конструктор формы для работы с автомобилями
        /// </summary>
        /// <param name="brands">Список брендов для выпадающего списка</param>
        /// <param name="car">Автомобиль для редактирования или null при добавлении</param>
        public CarForm(IReadOnlyList<BrandDto> brands, CarDto? car = null)
        {
            InitializeComponent();
            _isNew = car == null;
            Car = car ?? new CarDto();
            Deleted = false;

            Text = _isNew ? "Добавление автомобиля" : "Редактирование автомобиля";
            saveButton.Text = _isNew ? "Добавить" : "Сохранить";
            deleteButton.Visible = !_isNew;

            brandBox.DataSource = brands.ToList();
            brandBox.DisplayMember = nameof(BrandDto.Name);
            brandBox.ValueMember = nameof(BrandDto.Id);

            if (!_isNew)
            {
                plateBox.Text = car!.LicensePlate;
                plateBox.ReadOnly = true;
                brandBox.SelectedValue = car.BrandId;
                modelBox.Text = car.ModelName;
                yearBox.Value = Math.Clamp(car.ProductionYear, (int)yearBox.Minimum, (int)yearBox.Maximum);
                horsePowerBox.Value = Math.Clamp((decimal)car.HorsePower, horsePowerBox.Minimum, horsePowerBox.Maximum);
                tankBox.Value = Math.Clamp((decimal)car.FuelTankVolume, tankBox.Minimum, tankBox.Maximum);
                consumptionBox.Value = Math.Clamp((decimal)car.FuelConsumption, consumptionBox.Minimum, consumptionBox.Maximum);
                colorBox.Text = car.Color;
                _imageBytes = car.Image;
                ShowPhoto(_imageBytes);
            }
        }

        /// <summary>
        /// Отображает фото автомобиля в PictureBox
        /// </summary>
        /// <param name="data">Байты изображения или null</param>
        private void ShowPhoto(byte[]? data)
        {
            if (photoBox.Image != null)
            {
                var old = photoBox.Image;
                photoBox.Image = null;
                old.Dispose();
            }

            if (data == null || data.Length == 0)
            {
                photoBox.Image = null;
                return;
            }

            try
            {
                using var ms = new MemoryStream(data);
                using var img = Image.FromStream(ms);
                photoBox.Image = new Bitmap(img);
            }
            catch
            {
                photoBox.Image = null;
            }
        }

        /// <summary>
        /// Обработчик выбора файла фотографии
        /// </summary>
        private void choosePhotoButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Выберите фото автомобиля",
                Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Все файлы|*.*"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                _imageBytes = File.ReadAllBytes(dialog.FileName);
                ShowPhoto(_imageBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить изображение:\n{ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик удаления фотографии с формы
        /// </summary>
        private void clearPhotoButton_Click(object? sender, EventArgs e)
        {
            _imageBytes = null;
            ShowPhoto(null);
        }

        /// <summary>
        /// Обработчик сохранения или добавления автомобиля
        /// </summary>
        private void saveButton_Click(object? sender, EventArgs e)
        {
            if (brandBox.SelectedItem is not BrandDto brand)
            {
                MessageBox.Show("Сначала добавьте бренд.");
                return;
            }

            if (string.IsNullOrWhiteSpace(modelBox.Text))
            {
                MessageBox.Show("Введите модель.");
                return;
            }

            // Формат госномера проверяет слой Model; о неудаче View узнает от Presenter
            string plate = plateBox.Text.Trim().ToUpperInvariant();
            if (plate.Length == 0)
            {
                MessageBox.Show("Введите госномер.");
                return;
            }

            Car.LicensePlate = plate;
            Car.BrandId = brand.Id;
            Car.ModelName = modelBox.Text.Trim();
            Car.ProductionYear = (int)yearBox.Value;
            Car.HorsePower = (float)horsePowerBox.Value;
            Car.FuelTankVolume = (float)tankBox.Value;
            Car.FuelConsumption = (float)consumptionBox.Value;
            Car.Color = colorBox.Text.Trim();
            // byte[] уходит в PostgreSQL как BYTEA (в клиентах часто отображается как hex \x...)
            Car.Image = _imageBytes;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Обработчик удаления автомобиля
        /// </summary>
        private void deleteButton_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить этот автомобиль?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            Deleted = true;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
