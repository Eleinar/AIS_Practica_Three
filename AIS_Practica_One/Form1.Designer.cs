namespace AIS_Practica_One
{
    partial class Form1 //Переименовать форму
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage carsTab;
        private System.Windows.Forms.TabPage brandsTab;
        private System.Windows.Forms.DataGridView carsGrid;
        private System.Windows.Forms.DataGridView brandsGrid;
        private System.Windows.Forms.FlowLayoutPanel carsButtonsPanel;
        private System.Windows.Forms.FlowLayoutPanel brandsButtonsPanel;
        private System.Windows.Forms.FlowLayoutPanel filtersPanel;
        private System.Windows.Forms.GroupBox filtersBox;
        private System.Windows.Forms.Button addCarButton, editCarButton, deleteCarButton, refreshCarsButton;
        private System.Windows.Forms.Button addBrandButton, editBrandButton, deleteBrandButton, refreshBrandsButton;
        private System.Windows.Forms.Button findByBrandButton, findByFuelButton, showAllCarsButton;
        private System.Windows.Forms.ComboBox brandComboBox;
        private System.Windows.Forms.NumericUpDown fuelConsumptionInput;
        private System.Windows.Forms.Label brandLabel, fuelLabel;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabs = new System.Windows.Forms.TabControl();
            carsTab = new System.Windows.Forms.TabPage();
            brandsTab = new System.Windows.Forms.TabPage();
            carsGrid = new System.Windows.Forms.DataGridView();
            brandsGrid = new System.Windows.Forms.DataGridView();
            carsButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            brandsButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            filtersPanel = new System.Windows.Forms.FlowLayoutPanel();
            filtersBox = new System.Windows.Forms.GroupBox();
            addCarButton = new System.Windows.Forms.Button();
            editCarButton = new System.Windows.Forms.Button();
            deleteCarButton = new System.Windows.Forms.Button();
            refreshCarsButton = new System.Windows.Forms.Button();
            addBrandButton = new System.Windows.Forms.Button();
            editBrandButton = new System.Windows.Forms.Button();
            deleteBrandButton = new System.Windows.Forms.Button();
            refreshBrandsButton = new System.Windows.Forms.Button();
            findByBrandButton = new System.Windows.Forms.Button();
            findByFuelButton = new System.Windows.Forms.Button();
            showAllCarsButton = new System.Windows.Forms.Button();
            brandComboBox = new System.Windows.Forms.ComboBox();
            fuelConsumptionInput = new System.Windows.Forms.NumericUpDown();
            brandLabel = new System.Windows.Forms.Label();
            fuelLabel = new System.Windows.Forms.Label();
            
            tabs.SuspendLayout();
            carsTab.SuspendLayout();
            brandsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)carsGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)brandsGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fuelConsumptionInput).BeginInit();
            filtersBox.SuspendLayout();
            SuspendLayout();
            ///
            ///tabs
            ///
            tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            tabs.Controls.Add(carsTab);
            tabs.Controls.Add(brandsTab);
            tabs.Name = "tabs";
            tabs.SelectedIndex = 0;
            ///
            ///carsTab
            ///
            carsTab.Text = "Автомобили";
            carsTab.Padding = new System.Windows.Forms.Padding(10);
            carsTab.UseVisualStyleBackColor = true;
            ///
            ///brandsTab
            ///
            brandsTab.Text = "Бренды";
            brandsTab.Padding = new System.Windows.Forms.Padding(10);
            brandsTab.UseVisualStyleBackColor = true;
            ///
            ///carsGrid
            ///
            carsGrid.Name = "carsGrid";
            carsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            carsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            carsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            carsGrid.MultiSelect = false;
            carsGrid.ReadOnly = true;
            carsGrid.AllowUserToAddRows = false;
            carsGrid.AllowUserToDeleteRows = false;
            carsGrid.RowHeadersVisible = false;
            ///
            ///brandsGrid
            ///
            brandsGrid.Name = "brandsGrid";
            brandsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            brandsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            brandsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            brandsGrid.MultiSelect = false;
            brandsGrid.ReadOnly = true;
            brandsGrid.AllowUserToAddRows = false;
            brandsGrid.AllowUserToDeleteRows = false;
            brandsGrid.RowHeadersVisible = false;
            ///
            ///carsButtonsPanel
            ///
            carsButtonsPanel.Name = "carsButtonsPanel";
            carsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            carsButtonsPanel.Height = 44;
            carsButtonsPanel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            ///
            ///brandsButtonsPanel
            ///
            brandsButtonsPanel.Name = "brandsButtonsPanel";
            brandsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            brandsButtonsPanel.Height = 44;
            brandsButtonsPanel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            ///
            ///addCarButton
            ///
            addCarButton.Name = "addCarButton";
            addCarButton.Text = "Добавить";
            addCarButton.Size = new System.Drawing.Size(100, 30);
            addCarButton.UseVisualStyleBackColor = true;
            addCarButton.Click += addCarButton_Click;
            ///
            ///editCarButton
            ///
            editCarButton.Name = "editCarButton";
            editCarButton.Text = "Изменить";
            editCarButton.Size = new System.Drawing.Size(100, 30);
            editCarButton.UseVisualStyleBackColor = true;
            editCarButton.Click += editCarButton_Click;
            ///
            ///deleteCarButton
            ///
            deleteCarButton.Name = "deleteCarButton";
            deleteCarButton.Text = "Удалить";
            deleteCarButton.Size = new System.Drawing.Size(100, 30);
            deleteCarButton.UseVisualStyleBackColor = true;
            deleteCarButton.Click += deleteCarButton_Click;
            ///
            ///refreshCarsButton
            ///
            refreshCarsButton.Name = "refreshCarsButton";
            refreshCarsButton.Text = "Обновить";
            refreshCarsButton.Size = new System.Drawing.Size(100, 30);
            refreshCarsButton.UseVisualStyleBackColor = true;
            refreshCarsButton.Click += refreshCarsButton_Click;
            
            carsButtonsPanel.Controls.AddRange(new System.Windows.Forms.Control[] { addCarButton, editCarButton, deleteCarButton, refreshCarsButton });
            ///
            ///addBrandButton
            ///
            addBrandButton.Name = "addBrandButton";
            addBrandButton.Text = "Добавить";
            addBrandButton.Size = new System.Drawing.Size(100, 30);
            addBrandButton.UseVisualStyleBackColor = true;
            addBrandButton.Click += addBrandButton_Click;
            ///
            ///editBrandButton
            ///
            editBrandButton.Name = "editBrandButton";
            editBrandButton.Text = "Изменить";
            editBrandButton.Size = new System.Drawing.Size(100, 30);
            editBrandButton.UseVisualStyleBackColor = true;
            editBrandButton.Click += editBrandButton_Click;
            ///
            ///deleteBrandButton
            ///
            deleteBrandButton.Name = "deleteBrandButton";
            deleteBrandButton.Text = "Удалить";
            deleteBrandButton.Size = new System.Drawing.Size(100, 30);
            deleteBrandButton.UseVisualStyleBackColor = true;
            deleteBrandButton.Click += deleteBrandButton_Click;
            ///
            ///refreshBrandsButton
            ///
            refreshBrandsButton.Name = "refreshBrandsButton";
            refreshBrandsButton.Text = "Обновить";
            refreshBrandsButton.Size = new System.Drawing.Size(100, 30);
            refreshBrandsButton.UseVisualStyleBackColor = true;
            refreshBrandsButton.Click += refreshBrandsButton_Click;
            
            brandsButtonsPanel.Controls.AddRange(new System.Windows.Forms.Control[] { addBrandButton, editBrandButton, deleteBrandButton, refreshBrandsButton });
            ///
            ///filtersBox
            ///
            filtersBox.Name = "filtersBox";
            filtersBox.Text = "Поиск автомобилей";
            filtersBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            filtersBox.Height = 110;
            filtersBox.Padding = new System.Windows.Forms.Padding(10, 20, 10, 5);
            filtersPanel.Name = "filtersPanel";
            filtersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            filtersPanel.WrapContents = true;
            filtersPanel.AutoScroll = true;
            ///
            ///brandLabel
            ///
            brandLabel.Name = "brandLabel";
            brandLabel.Text = "Бренд:";
            brandLabel.AutoSize = true;
            ///
            ///brandLabel
            ///
            brandLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            brandComboBox.Name = "brandComboBox";
            brandComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            brandComboBox.Width = 150;
            ///
            ///findByBrandButton
            ///
            findByBrandButton.Name = "findByBrandButton";
            findByBrandButton.Text = "Найти по бренду";
            findByBrandButton.Size = new System.Drawing.Size(100, 30);
            findByBrandButton.UseVisualStyleBackColor = true;
            findByBrandButton.Click += findByBrandButton_Click;
            findByBrandButton.Width = 135;
            ///
            ///fuelLabel
            ///
            fuelLabel.Name = "fuelLabel";
            fuelLabel.Text = "Макс. расход:";
            fuelLabel.AutoSize = true;
            fuelLabel.Margin = new System.Windows.Forms.Padding(20, 9, 3, 3);
            ///
            ///fuelConsumptionInput
            ///
            fuelConsumptionInput.Name = "fuelConsumptionInput";
            fuelConsumptionInput.DecimalPlaces = 1;
            fuelConsumptionInput.Maximum = 100;
            fuelConsumptionInput.Width = 75;
            ///
            ///findByFuelButton
            ///
            findByFuelButton.Name = "findByFuelButton";
            findByFuelButton.Text = "Найти по расходу";
            findByFuelButton.Size = new System.Drawing.Size(100, 30);
            findByFuelButton.UseVisualStyleBackColor = true;
            findByFuelButton.Click += findByFuelButton_Click;
            findByFuelButton.Width = 140;
            ///
            ///showAllCarsButton
            ///
            showAllCarsButton.Name = "showAllCarsButton";
            showAllCarsButton.Text = "Показать все";
            showAllCarsButton.Size = new System.Drawing.Size(100, 30);
            showAllCarsButton.UseVisualStyleBackColor = true;
            showAllCarsButton.Click += showAllCarsButton_Click;
            showAllCarsButton.Width = 115;
            
            filtersPanel.Controls.AddRange(new System.Windows.Forms.Control[] { brandLabel, brandComboBox, findByBrandButton, fuelLabel, fuelConsumptionInput, findByFuelButton, showAllCarsButton });
            filtersBox.Controls.Add(filtersPanel);

            carsTab.Controls.Add(carsGrid);
            carsTab.Controls.Add(carsButtonsPanel);
            carsTab.Controls.Add(filtersBox);
            
            brandsTab.Controls.Add(brandsGrid);
            brandsTab.Controls.Add(brandsButtonsPanel);

            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1120, 650);
            MinimumSize = new System.Drawing.Size(800, 450);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Автомобили и бренды";
            Controls.Add(tabs);
            filtersBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)fuelConsumptionInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)brandsGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)carsGrid).EndInit();
            brandsTab.ResumeLayout(false);
            carsTab.ResumeLayout(false);
            tabs.ResumeLayout(false);
            ResumeLayout(false);
        }



        #endregion
    }
}
