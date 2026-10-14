using Model;

namespace WinForms
{
    public partial class BrandForm : Form
    {
        private readonly bool _isNew;

        /// <summary>
        /// Результат заполнения формы — объект бренда
        /// </summary>
        public Brand Brand { get; private set; }

        /// <summary>
        /// Признак того, что пользователь подтвердил удаление бренда
        /// </summary>
        public bool Deleted { get; private set; }

        /// <summary>
        /// Конструктор формы для работы с брендами
        /// </summary>
        /// <param name="brand">Бренд для редактирования или null при добавлении</param>
        public BrandForm(Brand? brand = null)
        {
            InitializeComponent();
            _isNew = brand == null;
            Brand = brand ?? new Brand();
            Deleted = false;

            Text = _isNew ? "Добавление бренда" : "Редактирование бренда";
            saveButton.Text = _isNew ? "Добавить" : "Сохранить";
            deleteButton.Visible = !_isNew;

            if (!_isNew)
            {
                nameBox.Text = brand!.Name;
                countryBox.Text = brand.Country;
            }
        }

        /// <summary>
        /// Обработчик сохранения или добавления бренда
        /// </summary>
        private void saveButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                MessageBox.Show("Введите название бренда.");
                return;
            }

            Brand.Name = nameBox.Text.Trim();
            Brand.Country = countryBox.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Обработчик удаления бренда
        /// </summary>
        private void deleteButton_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить этот бренд?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            Deleted = true;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
