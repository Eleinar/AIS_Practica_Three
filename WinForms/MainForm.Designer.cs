namespace WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage carsTab;
        private System.Windows.Forms.TabPage brandsTab;
        private System.Windows.Forms.FlowLayoutPanel carsCardsPanel;
        private System.Windows.Forms.FlowLayoutPanel brandsCardsPanel;
        private System.Windows.Forms.FlowLayoutPanel carsButtonsPanel;
        private System.Windows.Forms.FlowLayoutPanel brandsButtonsPanel;
        private System.Windows.Forms.Panel filtersPanel;
        private System.Windows.Forms.GroupBox filtersBox;
        private System.Windows.Forms.Button addCarButton;
        private System.Windows.Forms.Button refreshCarsButton;
        private System.Windows.Forms.Button addBrandButton;
        private System.Windows.Forms.Button refreshBrandsButton;
        private System.Windows.Forms.Button findByBrandButton;
        private System.Windows.Forms.Button findByFuelButton;
        private System.Windows.Forms.Button showAllCarsButton;
        private System.Windows.Forms.ComboBox brandComboBox;
        private System.Windows.Forms.TextBox plateSearchBox;
        private System.Windows.Forms.Button findByPlateButton;
        private System.Windows.Forms.Label plateSearchLabel;
        private System.Windows.Forms.NumericUpDown fuelConsumptionInput;
        private System.Windows.Forms.Label brandLabel;
        private System.Windows.Forms.Label fuelLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabs = new System.Windows.Forms.TabControl();
            carsTab = new System.Windows.Forms.TabPage();
            brandsTab = new System.Windows.Forms.TabPage();
            carsCardsPanel = new System.Windows.Forms.FlowLayoutPanel();
            brandsCardsPanel = new System.Windows.Forms.FlowLayoutPanel();
            carsButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            brandsButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            filtersPanel = new System.Windows.Forms.Panel();
            filtersBox = new System.Windows.Forms.GroupBox();
            addCarButton = new System.Windows.Forms.Button();
            refreshCarsButton = new System.Windows.Forms.Button();
            addBrandButton = new System.Windows.Forms.Button();
            refreshBrandsButton = new System.Windows.Forms.Button();
            findByBrandButton = new System.Windows.Forms.Button();
            findByFuelButton = new System.Windows.Forms.Button();
            showAllCarsButton = new System.Windows.Forms.Button();
            brandComboBox = new System.Windows.Forms.ComboBox();
            plateSearchBox = new System.Windows.Forms.TextBox();
            findByPlateButton = new System.Windows.Forms.Button();
            plateSearchLabel = new System.Windows.Forms.Label();
            fuelConsumptionInput = new System.Windows.Forms.NumericUpDown();
            brandLabel = new System.Windows.Forms.Label();
            fuelLabel = new System.Windows.Forms.Label();
            tabs.SuspendLayout();
            carsTab.SuspendLayout();
            brandsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fuelConsumptionInput).BeginInit();
            filtersBox.SuspendLayout();
            filtersPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tabs
            // 
            tabs.Controls.Add(carsTab);
            tabs.Controls.Add(brandsTab);
            tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            tabs.Name = "tabs";
            tabs.SelectedIndex = 0;
            // 
            // carsTab
            // 
            carsTab.Controls.Add(carsCardsPanel);
            carsTab.Controls.Add(carsButtonsPanel);
            carsTab.Controls.Add(filtersBox);
            carsTab.Name = "carsTab";
            carsTab.Padding = new System.Windows.Forms.Padding(10);
            carsTab.Text = "Автомобили";
            carsTab.UseVisualStyleBackColor = true;
            // 
            // brandsTab
            // 
            brandsTab.Controls.Add(brandsCardsPanel);
            brandsTab.Controls.Add(brandsButtonsPanel);
            brandsTab.Name = "brandsTab";
            brandsTab.Padding = new System.Windows.Forms.Padding(10);
            brandsTab.Text = "Бренды";
            brandsTab.UseVisualStyleBackColor = true;
            // 
            // carsCardsPanel
            // 
            carsCardsPanel.AutoScroll = true;
            carsCardsPanel.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            carsCardsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            carsCardsPanel.Name = "carsCardsPanel";
            carsCardsPanel.Padding = new System.Windows.Forms.Padding(4);
            carsCardsPanel.WrapContents = true;
            // 
            // brandsCardsPanel
            // 
            brandsCardsPanel.AutoScroll = true;
            brandsCardsPanel.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            brandsCardsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            brandsCardsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            brandsCardsPanel.Name = "brandsCardsPanel";
            brandsCardsPanel.Padding = new System.Windows.Forms.Padding(8);
            brandsCardsPanel.WrapContents = false;
            // 
            // filtersBox — фиксированная одна строка
            // 
            filtersBox.Controls.Add(filtersPanel);
            filtersBox.Dock = System.Windows.Forms.DockStyle.Top;
            filtersBox.Name = "filtersBox";
            filtersBox.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            filtersBox.Size = new System.Drawing.Size(952, 62);
            filtersBox.TabStop = false;
            filtersBox.Text = "Поиск автомобилей";
            // 
            // filtersPanel
            // 
            filtersPanel.Controls.Add(brandLabel);
            filtersPanel.Controls.Add(brandComboBox);
            filtersPanel.Controls.Add(findByBrandButton);
            filtersPanel.Controls.Add(fuelLabel);
            filtersPanel.Controls.Add(fuelConsumptionInput);
            filtersPanel.Controls.Add(findByFuelButton);
            filtersPanel.Controls.Add(plateSearchLabel);
            filtersPanel.Controls.Add(plateSearchBox);
            filtersPanel.Controls.Add(findByPlateButton);
            filtersPanel.Controls.Add(showAllCarsButton);
            filtersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            filtersPanel.Name = "filtersPanel";
            // 
            // brandLabel
            // 
            brandLabel.AutoSize = true;
            brandLabel.Location = new System.Drawing.Point(4, 10);
            brandLabel.Name = "brandLabel";
            brandLabel.Text = "Бренд:";
            // 
            // brandComboBox
            // 
            brandComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            brandComboBox.FormattingEnabled = true;
            brandComboBox.Location = new System.Drawing.Point(52, 6);
            brandComboBox.Name = "brandComboBox";
            brandComboBox.Size = new System.Drawing.Size(140, 23);
            // 
            // findByBrandButton
            // 
            findByBrandButton.Location = new System.Drawing.Point(200, 5);
            findByBrandButton.Name = "findByBrandButton";
            findByBrandButton.Size = new System.Drawing.Size(90, 28);
            findByBrandButton.Text = "По бренду";
            findByBrandButton.UseVisualStyleBackColor = true;
            findByBrandButton.Click += findByBrandButton_Click;
            // 
            // fuelLabel
            // 
            fuelLabel.AutoSize = true;
            fuelLabel.Location = new System.Drawing.Point(302, 10);
            fuelLabel.Name = "fuelLabel";
            fuelLabel.Text = "Макс. расход:";
            // 
            // fuelConsumptionInput
            // 
            fuelConsumptionInput.DecimalPlaces = 1;
            fuelConsumptionInput.Location = new System.Drawing.Point(392, 6);
            fuelConsumptionInput.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            fuelConsumptionInput.Name = "fuelConsumptionInput";
            fuelConsumptionInput.Size = new System.Drawing.Size(60, 23);
            fuelConsumptionInput.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // findByFuelButton
            // 
            findByFuelButton.Location = new System.Drawing.Point(460, 5);
            findByFuelButton.Name = "findByFuelButton";
            findByFuelButton.Size = new System.Drawing.Size(90, 28);
            findByFuelButton.Text = "По расходу";
            findByFuelButton.UseVisualStyleBackColor = true;
            findByFuelButton.Click += findByFuelButton_Click;
            // 
            // plateSearchLabel
            // 
            plateSearchLabel.AutoSize = true;
            plateSearchLabel.Location = new System.Drawing.Point(562, 10);
            plateSearchLabel.Name = "plateSearchLabel";
            plateSearchLabel.Text = "Госномер:";
            // 
            // plateSearchBox
            // 
            plateSearchBox.Location = new System.Drawing.Point(630, 6);
            plateSearchBox.Name = "plateSearchBox";
            plateSearchBox.Size = new System.Drawing.Size(110, 23);
            // 
            // findByPlateButton
            // 
            findByPlateButton.Location = new System.Drawing.Point(748, 5);
            findByPlateButton.Name = "findByPlateButton";
            findByPlateButton.Size = new System.Drawing.Size(70, 28);
            findByPlateButton.Text = "Найти";
            findByPlateButton.UseVisualStyleBackColor = true;
            findByPlateButton.Click += findByPlateButton_Click;
            // 
            // showAllCarsButton
            // 
            showAllCarsButton.Location = new System.Drawing.Point(826, 5);
            showAllCarsButton.Name = "showAllCarsButton";
            showAllCarsButton.Size = new System.Drawing.Size(100, 28);
            showAllCarsButton.Text = "Показать все";
            showAllCarsButton.UseVisualStyleBackColor = true;
            showAllCarsButton.Click += showAllCarsButton_Click;
            // 
            // carsButtonsPanel
            // 
            carsButtonsPanel.Controls.Add(addCarButton);
            carsButtonsPanel.Controls.Add(refreshCarsButton);
            carsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            carsButtonsPanel.Name = "carsButtonsPanel";
            carsButtonsPanel.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            carsButtonsPanel.Size = new System.Drawing.Size(952, 44);
            // 
            // addCarButton
            // 
            addCarButton.Name = "addCarButton";
            addCarButton.Size = new System.Drawing.Size(160, 30);
            addCarButton.Text = "Добавить автомобиль";
            addCarButton.UseVisualStyleBackColor = true;
            addCarButton.Click += addCarButton_Click;
            // 
            // refreshCarsButton
            // 
            refreshCarsButton.Name = "refreshCarsButton";
            refreshCarsButton.Size = new System.Drawing.Size(100, 30);
            refreshCarsButton.Text = "Обновить";
            refreshCarsButton.UseVisualStyleBackColor = true;
            refreshCarsButton.Click += refreshCarsButton_Click;
            // 
            // brandsButtonsPanel
            // 
            brandsButtonsPanel.Controls.Add(addBrandButton);
            brandsButtonsPanel.Controls.Add(refreshBrandsButton);
            brandsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            brandsButtonsPanel.Name = "brandsButtonsPanel";
            brandsButtonsPanel.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            brandsButtonsPanel.Size = new System.Drawing.Size(952, 44);
            // 
            // addBrandButton
            // 
            addBrandButton.Name = "addBrandButton";
            addBrandButton.Size = new System.Drawing.Size(140, 30);
            addBrandButton.Text = "Добавить бренд";
            addBrandButton.UseVisualStyleBackColor = true;
            addBrandButton.Click += addBrandButton_Click;
            // 
            // refreshBrandsButton
            // 
            refreshBrandsButton.Name = "refreshBrandsButton";
            refreshBrandsButton.Size = new System.Drawing.Size(100, 30);
            refreshBrandsButton.Text = "Обновить";
            refreshBrandsButton.UseVisualStyleBackColor = true;
            refreshBrandsButton.Click += refreshBrandsButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(980, 640);
            Controls.Add(tabs);
            MinimumSize = new System.Drawing.Size(720, 480);
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Автопарк";
            tabs.ResumeLayout(false);
            carsTab.ResumeLayout(false);
            brandsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)fuelConsumptionInput).EndInit();
            filtersBox.ResumeLayout(false);
            filtersPanel.ResumeLayout(false);
            filtersPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
