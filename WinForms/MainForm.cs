using Model;
using BusinessLogic;

namespace WinForms
{
    public partial class MainForm : Form
    {
        private readonly Logic _logic;
        private float _lastCardScale = 1f;

        /// <summary>
        /// Конструктор главной формы с внедрением бизнес-логики (DIP / IoC)
        /// </summary>
        /// <param name="logic">Экземпляр бизнес-логики, полученный из IoC-контейнера</param>
        public MainForm(Logic logic)
        {
            InitializeComponent();
            _logic = logic ?? throw new ArgumentNullException(nameof(logic));
            EnsureCarsTabLayout();
            brandsCardsPanel.WrapContents = false;
            brandsCardsPanel.FlowDirection = FlowDirection.TopDown;
            brandsCardsPanel.SizeChanged += (_, _) => ResizeBrandCards();
            carsCardsPanel.SizeChanged += (_, _) => ResizeCarCards();
            filtersPanel.SizeChanged += (_, _) => LayoutSearchFilters();
            tabs.SelectedIndexChanged += (_, _) =>
            {
                if (tabs.SelectedTab == brandsTab)
                    BeginInvoke(ResizeBrandCards);
                if (tabs.SelectedTab == carsTab)
                    BeginInvoke(LayoutSearchFilters);
            };
            Shown += (_, _) =>
            {
                LayoutSearchFilters();
                ResizeBrandCards();
                ResizeCarCards();
            };
            RefreshBrands();
            RefreshCars();
        }

        /// <summary>
        /// Гарантирует порядок элементов на вкладке автомобилей: поиск → кнопки → карточки
        /// </summary>
        private void EnsureCarsTabLayout()
        {
            carsTab.Controls.SetChildIndex(carsCardsPanel, 0);
            carsTab.Controls.SetChildIndex(carsButtonsPanel, 1);
            carsTab.Controls.SetChildIndex(filtersBox, 2);
            filtersBox.Dock = DockStyle.Top;
            carsButtonsPanel.Dock = DockStyle.Top;
            carsCardsPanel.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// Размещает элементы поиска в одну строку на всю ширину панели.
        /// При широком окне растягивает комбобокс и поле госномера;
        /// при узком — пропорционально сжимает все элементы, чтобы ничего не обрезалось.
        /// </summary>
        private void LayoutSearchFilters()
        {
            if (filtersPanel == null || filtersPanel.IsDisposed)
                return;

            const int gap0 = 8;
            const int pad = 6;
            const int rowY = 6;
            const int labelY = 10;
            const int btnY = 5;
            const int btnH = 28;

            // Базовые (желаемые) размеры
            int brandLabelW0 = Math.Max(brandLabel.PreferredWidth, 45);
            int fuelLabelW0 = Math.Max(fuelLabel.PreferredWidth, 85);
            int plateLabelW0 = Math.Max(plateSearchLabel.PreferredWidth, 65);
            const int findBrandW0 = 90;
            const int findFuelW0 = 90;
            const int findPlateW0 = 70;
            const int showAllW0 = 100;
            const int fuelInputW0 = 60;
            const int brandComboW0 = 140;
            const int plateBoxW0 = 110;

            int available = filtersPanel.ClientSize.Width - pad * 2;
            if (available < 50)
                return;

            // Ширина при базовых размерах (8 промежутков между 10 элементами? 
            // элементы: L combo btn L num btn L box btn btn = 10, gaps = 9)
            const int gapsCount = 9;
            int baseTotal =
                brandLabelW0 + brandComboW0 + findBrandW0 +
                fuelLabelW0 + fuelInputW0 + findFuelW0 +
                plateLabelW0 + plateBoxW0 + findPlateW0 + showAllW0 +
                gap0 * gapsCount;

            int brandLabelW, brandComboW, findBrandW, fuelLabelW, fuelInputW, findFuelW,
                plateLabelW, plateBoxW, findPlateW, showAllW, gap;

            if (available >= baseTotal)
            {
                // Широкое окно: базовые размеры + всё лишнее в combo и plate
                int extra = available - baseTotal;
                brandLabelW = brandLabelW0;
                findBrandW = findBrandW0;
                fuelLabelW = fuelLabelW0;
                fuelInputW = fuelInputW0;
                findFuelW = findFuelW0;
                plateLabelW = plateLabelW0;
                findPlateW = findPlateW0;
                showAllW = showAllW0;
                gap = gap0;
                brandComboW = brandComboW0 + extra * 55 / 100;
                plateBoxW = plateBoxW0 + extra - (brandComboW - brandComboW0);
            }
            else
            {
                // Узкое окно: пропорционально сжимаем всё
                double scale = (double)available / baseTotal;
                // Не сжимаем сильнее чем до 0.72, иначе нечитаемо
                scale = Math.Max(scale, 0.72);

                brandLabelW = Math.Max(36, (int)(brandLabelW0 * scale));
                brandComboW = Math.Max(70, (int)(brandComboW0 * scale));
                findBrandW = Math.Max(70, (int)(findBrandW0 * scale));
                fuelLabelW = Math.Max(60, (int)(fuelLabelW0 * scale));
                fuelInputW = Math.Max(48, (int)(fuelInputW0 * scale));
                findFuelW = Math.Max(70, (int)(findFuelW0 * scale));
                plateLabelW = Math.Max(50, (int)(plateLabelW0 * scale));
                plateBoxW = Math.Max(60, (int)(plateBoxW0 * scale));
                findPlateW = Math.Max(55, (int)(findPlateW0 * scale));
                showAllW = Math.Max(75, (int)(showAllW0 * scale));
                gap = Math.Max(4, (int)(gap0 * scale));

                // Подгонка: если после минимумов всё ещё шире available — ужимаем combo/plate
                int total =
                    brandLabelW + brandComboW + findBrandW +
                    fuelLabelW + fuelInputW + findFuelW +
                    plateLabelW + plateBoxW + findPlateW + showAllW +
                    gap * gapsCount;
                if (total > available)
                {
                    int overflow = total - available;
                    int cutCombo = Math.Min(overflow / 2, brandComboW - 60);
                    brandComboW -= cutCombo;
                    overflow -= cutCombo;
                    plateBoxW = Math.Max(50, plateBoxW - overflow);
                }
            }

            int x = pad;

            brandLabel.Location = new Point(x, labelY);
            x += brandLabelW + gap;

            brandComboBox.SetBounds(x, rowY, brandComboW, brandComboBox.Height);
            x += brandComboW + gap;

            findByBrandButton.SetBounds(x, btnY, findBrandW, btnH);
            x += findBrandW + gap;

            fuelLabel.Location = new Point(x, labelY);
            x += fuelLabelW + gap;

            fuelConsumptionInput.SetBounds(x, rowY, fuelInputW, fuelConsumptionInput.Height);
            x += fuelInputW + gap;

            findByFuelButton.SetBounds(x, btnY, findFuelW, btnH);
            x += findFuelW + gap;

            plateSearchLabel.Location = new Point(x, labelY);
            x += plateLabelW + gap;

            plateSearchBox.SetBounds(x, rowY, plateBoxW, plateSearchBox.Height);
            x += plateBoxW + gap;

            findByPlateButton.SetBounds(x, btnY, findPlateW, btnH);
            x += findPlateW + gap;

            showAllCarsButton.SetBounds(x, btnY, showAllW, btnH);
        }

        /// <summary>
        /// Обновляет список карточек автомобилей из базы данных
        /// </summary>
        private void RefreshCars() => ShowCars(_logic.GetAllCars());

        /// <summary>
        /// Отображает переданный список автомобилей в виде карточек
        /// </summary>
        /// <param name="cars">Список автомобилей для отображения</param>
        private void ShowCars(List<Car> cars)
        {
            carsCardsPanel.SuspendLayout();
            carsCardsPanel.Controls.Clear();
            _lastCardScale = CardScale();

            foreach (Car car in cars)
                carsCardsPanel.Controls.Add(CreateCarCard(car));

            if (cars.Count == 0)
            {
                carsCardsPanel.Controls.Add(new Label
                {
                    Text = "Автомобили не найдены",
                    AutoSize = true,
                    ForeColor = Color.Gray,
                    Font = new Font(Font.FontFamily, 11f, FontStyle.Italic),
                    Margin = new Padding(16)
                });
            }

            carsCardsPanel.ResumeLayout(true);
        }

        /// <summary>
        /// Коэффициент масштаба карточек в зависимости от ширины панели
        /// </summary>
        private float CardScale()
        {
            // Базовая ширина панели ~900 px → scale 1.0; шире — крупнее, уже — мельче
            int w = carsCardsPanel.ClientSize.Width;
            if (w <= 0) return 1f;
            float scale = w / 900f;
            return Math.Clamp(scale, 0.85f, 1.45f);
        }

        /// <summary>
        /// Создаёт карточку автомобиля с фото, брендом, моделью, госномером и цветом
        /// </summary>
        /// <param name="car">Автомобиль</param>
        /// <returns>Элемент управления-карточка</returns>
        private Control CreateCarCard(Car car)
        {
            float s = CardScale();
            int cardW = (int)(200 * s);
            int cardH = (int)(260 * s);
            int pad = (int)(10 * s);
            int photoH = (int)(140 * s);
            int photoW = cardW - pad * 2;

            string brandName = _logic.GetBrandName(car.BrandID);

            var card = new Panel
            {
                Width = cardW,
                Height = cardH,
                Margin = new Padding((int)(10 * s)),
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Tag = car.LicensePlate
            };
            card.Paint += (_, e) =>
            {
                using var pen = new Pen(Color.FromArgb(220, 224, 230));
                e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };

            var photo = new PictureBox
            {
                Location = new Point(pad, pad),
                Size = new Size(photoW, photoH),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(240, 242, 245),
                Name = "photo"
            };
            SetCardPhoto(photo, car.Image);

            float titleSize = Math.Clamp(9.5f * s, 8.5f, 13f);
            float bodySize = Math.Clamp(9f * s, 8f, 12f);
            float smallSize = Math.Clamp(8.5f * s, 7.5f, 11f);
            float hintSize = Math.Clamp(7.5f * s, 7f, 10f);

            int y = pad + photoH + (int)(8 * s);
            int textW = photoW;

            var title = new Label
            {
                Location = new Point(pad, y),
                Size = new Size(textW, (int)(22 * s)),
                Text = $"{brandName} {car.ModelName}",
                Font = new Font(Font.FontFamily, titleSize, FontStyle.Bold),
                AutoEllipsis = true,
                Name = "title"
            };
            y += (int)(24 * s);

            var plate = new Label
            {
                Location = new Point(pad, y),
                Size = new Size(textW, (int)(20 * s)),
                Text = car.LicensePlate,
                Font = new Font(Font.FontFamily, bodySize),
                ForeColor = Color.FromArgb(40, 40, 40),
                AutoEllipsis = true,
                Name = "plate"
            };
            y += (int)(22 * s);

            var color = new Label
            {
                Location = new Point(pad, y),
                Size = new Size(textW, (int)(20 * s)),
                Text = string.IsNullOrWhiteSpace(car.Color) ? "Цвет не указан" : car.Color,
                Font = new Font(Font.FontFamily, smallSize),
                ForeColor = Color.Gray,
                AutoEllipsis = true,
                Name = "color"
            };
            y += (int)(22 * s);

            var hint = new Label
            {
                Location = new Point(pad, y),
                Size = new Size(textW, (int)(18 * s)),
                Text = "Нажмите, чтобы открыть",
                Font = new Font(Font.FontFamily, hintSize, FontStyle.Italic),
                ForeColor = Color.Silver,
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "hint"
            };

            void openEditor(object? sdr, EventArgs e) => OpenCarEditor(car.LicensePlate);
            foreach (Control c in new Control[] { card, photo, title, plate, color, hint })
                c.Click += openEditor;

            card.Controls.AddRange(new Control[] { photo, title, plate, color, hint });
            return card;
        }

        /// <summary>
        /// Масштабирует существующие карточки автомобилей под ширину панели
        /// </summary>
        private void ResizeCarCards()
        {
            if (carsCardsPanel == null || carsCardsPanel.IsDisposed)
                return;

            float s = CardScale();
            if (Math.Abs(s - _lastCardScale) < 0.04f)
                return;
            _lastCardScale = s;

            int cardW = (int)(200 * s);
            int cardH = (int)(260 * s);
            int pad = (int)(10 * s);
            int photoH = (int)(140 * s);
            int photoW = cardW - pad * 2;
            int margin = (int)(10 * s);

            float titleSize = Math.Clamp(9.5f * s, 8.5f, 13f);
            float bodySize = Math.Clamp(9f * s, 8f, 12f);
            float smallSize = Math.Clamp(8.5f * s, 7.5f, 11f);
            float hintSize = Math.Clamp(7.5f * s, 7f, 10f);

            carsCardsPanel.SuspendLayout();
            foreach (Control c in carsCardsPanel.Controls)
            {
                if (c is not Panel card || card.Tag is not string)
                    continue;

                card.Width = cardW;
                card.Height = cardH;
                card.Margin = new Padding(margin);

                int y = pad + photoH + (int)(8 * s);
                foreach (Control child in card.Controls)
                {
                    switch (child.Name)
                    {
                        case "photo":
                            child.SetBounds(pad, pad, photoW, photoH);
                            break;
                        case "title":
                            child.SetBounds(pad, y, photoW, (int)(22 * s));
                            child.Font = new Font(Font.FontFamily, titleSize, FontStyle.Bold);
                            y += (int)(24 * s);
                            break;
                        case "plate":
                            child.SetBounds(pad, y, photoW, (int)(20 * s));
                            child.Font = new Font(Font.FontFamily, bodySize);
                            y += (int)(22 * s);
                            break;
                        case "color":
                            child.SetBounds(pad, y, photoW, (int)(20 * s));
                            child.Font = new Font(Font.FontFamily, smallSize);
                            y += (int)(22 * s);
                            break;
                        case "hint":
                            child.SetBounds(pad, y, photoW, (int)(18 * s));
                            child.Font = new Font(Font.FontFamily, hintSize, FontStyle.Italic);
                            break;
                    }
                }
            }
            carsCardsPanel.ResumeLayout(true);
        }

        /// <summary>
        /// Устанавливает изображение на карточке из массива байт
        /// </summary>
        /// <param name="box">Элемент PictureBox</param>
        /// <param name="data">Байты изображения или null</param>
        private static void SetCardPhoto(PictureBox box, byte[]? data)
        {
            if (data == null || data.Length == 0)
            {
                box.Image = null;
                return;
            }

            try
            {
                using var ms = new MemoryStream(data);
                using var img = Image.FromStream(ms);
                box.Image = new Bitmap(img);
            }
            catch
            {
                box.Image = null;
            }
        }

        /// <summary>
        /// Открывает форму редактирования автомобиля по госномеру
        /// </summary>
        /// <param name="licensePlate">Госномер автомобиля</param>
        private void OpenCarEditor(string licensePlate)
        {
            Car? source = _logic.GetCarById(licensePlate);
            if (source == null)
            {
                MessageBox.Show("Автомобиль не найден.");
                RefreshCars();
                return;
            }

            using var form = new CarForm(_logic, CloneCar(source));
            if (form.ShowDialog(this) != DialogResult.OK)
                return;

            if (form.Deleted)
            {
                if (!_logic.DeleteCar(licensePlate))
                    MessageBox.Show("Не удалось удалить автомобиль.");
            }
            else if (!_logic.UpdateCar(form.Car))
            {
                MessageBox.Show("Не удалось изменить автомобиль.");
            }

            RefreshCars();
        }

        /// <summary>
        /// Создаёт копию объекта автомобиля для редактирования
        /// </summary>
        /// <param name="source">Исходный автомобиль</param>
        /// <returns>Копия автомобиля</returns>
        private static Car CloneCar(Car source) => new()
        {
            LicensePlate = source.LicensePlate,
            BrandID = source.BrandID,
            ModelName = source.ModelName,
            ProductionYear = source.ProductionYear,
            HorsePower = source.HorsePower,
            FuelTankVolume = source.FuelTankVolume,
            FuelConsumption = source.FuelConsumption,
            Color = source.Color,
            Image = source.Image
        };

        /// <summary>
        /// Обработчик добавления нового автомобиля
        /// </summary>
        private void addCarButton_Click(object? sender, EventArgs e)
        {
            using var form = new CarForm(_logic);
            if (form.ShowDialog(this) != DialogResult.OK)
                return;

            if (!_logic.AddCar(form.Car))
            {
                MessageBox.Show("Не удалось добавить автомобиль. Проверьте бренд, формат и уникальность госномера.");
                return;
            }

            RefreshCars();
        }

        /// <summary>
        /// Обработчик обновления списка автомобилей
        /// </summary>
        private void refreshCarsButton_Click(object? sender, EventArgs e) => RefreshCars();

        /// <summary>
        /// Обработчик поиска автомобилей по выбранному бренду
        /// </summary>
        private void findByBrandButton_Click(object? sender, EventArgs e)
        {
            if (brandComboBox.SelectedItem is Brand brand)
                ShowCars(_logic.GetCarsByBrand(brand.ID));
            else
                MessageBox.Show("Сначала добавьте или выберите бренд.");
        }

        /// <summary>
        /// Обработчик поиска автомобилей по максимальному расходу топлива
        /// </summary>
        private void findByFuelButton_Click(object? sender, EventArgs e) =>
            ShowCars(_logic.GetCarsByFuelConsuption((float)fuelConsumptionInput.Value));

        /// <summary>
        /// Обработчик поиска автомобилей по госномеру (или его началу)
        /// </summary>
        private void findByPlateButton_Click(object? sender, EventArgs e)
        {
            string query = plateSearchBox.Text.Trim();
            if (query.Length == 0)
            {
                RefreshCars();
                return;
            }
            ShowCars(_logic.SearchCarsByLicensePlate(query));
        }

        /// <summary>
        /// Обработчик сброса фильтров и показа всех автомобилей
        /// </summary>
        private void showAllCarsButton_Click(object? sender, EventArgs e) => RefreshCars();

        /// <summary>
        /// Обновляет список карточек брендов и комбобокс фильтра
        /// </summary>
        private void RefreshBrands()
        {
            var brands = _logic.GetAllBrands();

            brandComboBox.DataSource = null;
            brandComboBox.DataSource = new List<Brand>(brands);
            brandComboBox.DisplayMember = nameof(Brand.Name);
            brandComboBox.ValueMember = nameof(Brand.ID);

            brandsCardsPanel.SuspendLayout();
            brandsCardsPanel.Controls.Clear();

            foreach (Brand brand in brands)
                brandsCardsPanel.Controls.Add(CreateBrandCard(brand));

            if (brands.Count == 0)
            {
                brandsCardsPanel.Controls.Add(new Label
                {
                    Text = "Бренды не найдены",
                    AutoSize = true,
                    ForeColor = Color.Gray,
                    Font = new Font(Font.FontFamily, 11f, FontStyle.Italic),
                    Margin = new Padding(16)
                });
            }

            brandsCardsPanel.ResumeLayout(true);
            ResizeBrandCards();
        }

        /// <summary>
        /// Вычисляет ширину горизонтальной карточки бренда по ширине панели
        /// </summary>
        /// <returns>Ширина карточки в пикселях</returns>
        private int BrandCardWidth()
        {
            int w = brandsCardsPanel.ClientSize.Width - brandsCardsPanel.Padding.Horizontal - 24;
            return Math.Max(w, 280);
        }

        /// <summary>
        /// Создаёт горизонтальную карточку бренда с названием и страной
        /// </summary>
        /// <param name="brand">Бренд</param>
        /// <returns>Элемент управления-карточка</returns>
        private Control CreateBrandCard(Brand brand)
        {
            int width = BrandCardWidth();

            var card = new Panel
            {
                Width = width,
                Height = 64,
                Margin = new Padding(8, 6, 8, 6),
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Tag = brand.ID
            };
            card.Paint += (_, e) =>
            {
                using var pen = new Pen(Color.FromArgb(220, 224, 230));
                Rectangle r = card.ClientRectangle;
                r.Width -= 1;
                r.Height -= 1;
                e.Graphics.DrawRectangle(pen, r);
            };

            var nameLabel = new Label
            {
                Location = new Point(16, 12),
                Size = new Size(Math.Max(width - 32, 40), 24),
                Text = brand.Name,
                Font = new Font(Font.FontFamily, 11f, FontStyle.Bold),
                AutoEllipsis = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var countryLabel = new Label
            {
                Location = new Point(16, 36),
                Size = new Size(Math.Max(width - 32, 40), 20),
                Text = brand.Country,
                Font = new Font(Font.FontFamily, 9f),
                ForeColor = Color.Gray,
                AutoEllipsis = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            void openEditor(object? s, EventArgs e) => OpenBrandEditor(brand.ID);
            card.Click += openEditor;
            nameLabel.Click += openEditor;
            countryLabel.Click += openEditor;

            card.Controls.Add(nameLabel);
            card.Controls.Add(countryLabel);
            return card;
        }

        /// <summary>
        /// Подгоняет ширину карточек брендов под размер панели
        /// </summary>
        private void ResizeBrandCards()
        {
            if (brandsCardsPanel == null || brandsCardsPanel.IsDisposed)
                return;

            int width = BrandCardWidth();
            brandsCardsPanel.SuspendLayout();
            foreach (Control c in brandsCardsPanel.Controls)
            {
                if (c is not Panel panel || panel.Tag is not int)
                    continue;

                panel.Width = width;
                foreach (Control child in panel.Controls)
                {
                    if (child is Label lbl)
                        lbl.Width = Math.Max(width - 32, 40);
                }
            }
            brandsCardsPanel.ResumeLayout(true);
        }

        /// <summary>
        /// Открывает форму редактирования бренда по идентификатору
        /// </summary>
        /// <param name="brandId">Идентификатор бренда</param>
        private void OpenBrandEditor(int brandId)
        {
            Brand? source = _logic.GetBrandById(brandId);
            if (source == null)
            {
                MessageBox.Show("Бренд не найден.");
                RefreshBrands();
                return;
            }

            using var form = new BrandForm(new Brand
            {
                ID = source.ID,
                Name = source.Name,
                Country = source.Country
            });
            if (form.ShowDialog(this) != DialogResult.OK)
                return;

            if (form.Deleted)
            {
                if (!_logic.DeleteBrand(brandId))
                    MessageBox.Show("Бренд не найден или используется автомобилями.");
            }
            else if (!_logic.UpdateBrand(form.Brand))
            {
                MessageBox.Show("Не удалось изменить бренд. Проверьте название.");
                return;
            }

            RefreshBrands();
            RefreshCars();
        }

        /// <summary>
        /// Обработчик добавления нового бренда
        /// </summary>
        private void addBrandButton_Click(object? sender, EventArgs e)
        {
            using var form = new BrandForm();
            if (form.ShowDialog(this) != DialogResult.OK) return;
            if (!_logic.AddBrand(form.Brand))
            {
                MessageBox.Show("Бренд с таким названием уже существует.");
                return;
            }
            RefreshBrands();
            RefreshCars();
        }

        /// <summary>
        /// Обработчик обновления списка брендов
        /// </summary>
        private void refreshBrandsButton_Click(object? sender, EventArgs e) => RefreshBrands();
    }
}
