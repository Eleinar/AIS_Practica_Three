namespace WinForms
{
    partial class CarForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label plateLabel;
        private System.Windows.Forms.Label brandLabel;
        private System.Windows.Forms.Label modelLabel;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.Label horsePowerLabel;
        private System.Windows.Forms.Label tankLabel;
        private System.Windows.Forms.Label consumptionLabel;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.Label photoLabel;
        private System.Windows.Forms.TextBox plateBox;
        private System.Windows.Forms.ComboBox brandBox;
        private System.Windows.Forms.TextBox modelBox;
        private System.Windows.Forms.NumericUpDown yearBox;
        private System.Windows.Forms.NumericUpDown horsePowerBox;
        private System.Windows.Forms.NumericUpDown tankBox;
        private System.Windows.Forms.NumericUpDown consumptionBox;
        private System.Windows.Forms.TextBox colorBox;
        private System.Windows.Forms.PictureBox photoBox;
        private System.Windows.Forms.Button choosePhotoButton;
        private System.Windows.Forms.Button clearPhotoButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button cancelButton;

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
            plateLabel = new System.Windows.Forms.Label();
            brandLabel = new System.Windows.Forms.Label();
            modelLabel = new System.Windows.Forms.Label();
            yearLabel = new System.Windows.Forms.Label();
            horsePowerLabel = new System.Windows.Forms.Label();
            tankLabel = new System.Windows.Forms.Label();
            consumptionLabel = new System.Windows.Forms.Label();
            colorLabel = new System.Windows.Forms.Label();
            photoLabel = new System.Windows.Forms.Label();
            plateBox = new System.Windows.Forms.TextBox();
            brandBox = new System.Windows.Forms.ComboBox();
            modelBox = new System.Windows.Forms.TextBox();
            yearBox = new System.Windows.Forms.NumericUpDown();
            horsePowerBox = new System.Windows.Forms.NumericUpDown();
            tankBox = new System.Windows.Forms.NumericUpDown();
            consumptionBox = new System.Windows.Forms.NumericUpDown();
            colorBox = new System.Windows.Forms.TextBox();
            photoBox = new System.Windows.Forms.PictureBox();
            choosePhotoButton = new System.Windows.Forms.Button();
            clearPhotoButton = new System.Windows.Forms.Button();
            saveButton = new System.Windows.Forms.Button();
            deleteButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)yearBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)horsePowerBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tankBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)consumptionBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)photoBox).BeginInit();
            SuspendLayout();
            // 
            // plateLabel
            // 
            plateLabel.AutoSize = true;
            plateLabel.Location = new System.Drawing.Point(24, 27);
            plateLabel.Name = "plateLabel";
            plateLabel.Size = new System.Drawing.Size(64, 15);
            plateLabel.Text = "Госномер:";
            // 
            // plateBox
            // 
            plateBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            plateBox.Location = new System.Drawing.Point(185, 24);
            plateBox.MaxLength = 9;
            plateBox.Name = "plateBox";
            plateBox.Size = new System.Drawing.Size(230, 23);
            // 
            // brandLabel
            // 
            brandLabel.AutoSize = true;
            brandLabel.Location = new System.Drawing.Point(24, 67);
            brandLabel.Name = "brandLabel";
            brandLabel.Size = new System.Drawing.Size(43, 15);
            brandLabel.Text = "Бренд:";
            // 
            // brandBox
            // 
            brandBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            brandBox.FormattingEnabled = true;
            brandBox.Location = new System.Drawing.Point(185, 64);
            brandBox.Name = "brandBox";
            brandBox.Size = new System.Drawing.Size(230, 23);
            // 
            // modelLabel
            // 
            modelLabel.AutoSize = true;
            modelLabel.Location = new System.Drawing.Point(24, 107);
            modelLabel.Name = "modelLabel";
            modelLabel.Size = new System.Drawing.Size(53, 15);
            modelLabel.Text = "Модель:";
            // 
            // modelBox
            // 
            modelBox.Location = new System.Drawing.Point(185, 104);
            modelBox.Name = "modelBox";
            modelBox.Size = new System.Drawing.Size(230, 23);
            // 
            // yearLabel
            // 
            yearLabel.AutoSize = true;
            yearLabel.Location = new System.Drawing.Point(24, 147);
            yearLabel.Name = "yearLabel";
            yearLabel.Size = new System.Drawing.Size(84, 15);
            yearLabel.Text = "Год выпуска:";
            // 
            // yearBox
            // 
            yearBox.Location = new System.Drawing.Point(185, 144);
            yearBox.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            yearBox.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            yearBox.Name = "yearBox";
            yearBox.Size = new System.Drawing.Size(230, 23);
            yearBox.Value = new decimal(new int[] { 2020, 0, 0, 0 });
            // 
            // horsePowerLabel
            // 
            horsePowerLabel.AutoSize = true;
            horsePowerLabel.Location = new System.Drawing.Point(24, 187);
            horsePowerLabel.Name = "horsePowerLabel";
            horsePowerLabel.Size = new System.Drawing.Size(102, 15);
            horsePowerLabel.Text = "Мощность (л.с.):";
            // 
            // horsePowerBox
            // 
            horsePowerBox.DecimalPlaces = 1;
            horsePowerBox.Location = new System.Drawing.Point(185, 184);
            horsePowerBox.Maximum = new decimal(new int[] { 3000, 0, 0, 0 });
            horsePowerBox.Name = "horsePowerBox";
            horsePowerBox.Size = new System.Drawing.Size(230, 23);
            // 
            // tankLabel
            // 
            tankLabel.AutoSize = true;
            tankLabel.Location = new System.Drawing.Point(24, 227);
            tankLabel.Name = "tankLabel";
            tankLabel.Size = new System.Drawing.Size(96, 15);
            tankLabel.Text = "Объём бака (л):";
            // 
            // tankBox
            // 
            tankBox.DecimalPlaces = 1;
            tankBox.Location = new System.Drawing.Point(185, 224);
            tankBox.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            tankBox.Name = "tankBox";
            tankBox.Size = new System.Drawing.Size(230, 23);
            // 
            // consumptionLabel
            // 
            consumptionLabel.AutoSize = true;
            consumptionLabel.Location = new System.Drawing.Point(24, 267);
            consumptionLabel.Name = "consumptionLabel";
            consumptionLabel.Size = new System.Drawing.Size(114, 15);
            consumptionLabel.Text = "Расход (л/100 км):";
            // 
            // consumptionBox
            // 
            consumptionBox.DecimalPlaces = 1;
            consumptionBox.Location = new System.Drawing.Point(185, 264);
            consumptionBox.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            consumptionBox.Name = "consumptionBox";
            consumptionBox.Size = new System.Drawing.Size(230, 23);
            // 
            // colorLabel
            // 
            colorLabel.AutoSize = true;
            colorLabel.Location = new System.Drawing.Point(24, 307);
            colorLabel.Name = "colorLabel";
            colorLabel.Size = new System.Drawing.Size(36, 15);
            colorLabel.Text = "Цвет:";
            // 
            // colorBox
            // 
            colorBox.Location = new System.Drawing.Point(185, 304);
            colorBox.Name = "colorBox";
            colorBox.Size = new System.Drawing.Size(230, 23);
            // 
            // photoLabel
            // 
            photoLabel.AutoSize = true;
            photoLabel.Location = new System.Drawing.Point(24, 348);
            photoLabel.Name = "photoLabel";
            photoLabel.Size = new System.Drawing.Size(36, 15);
            photoLabel.Text = "Фото:";
            // 
            // photoBox
            // 
            photoBox.BackColor = System.Drawing.Color.WhiteSmoke;
            photoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            photoBox.Location = new System.Drawing.Point(185, 348);
            photoBox.Name = "photoBox";
            photoBox.Size = new System.Drawing.Size(230, 150);
            photoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            photoBox.TabStop = false;
            // 
            // choosePhotoButton
            // 
            choosePhotoButton.Location = new System.Drawing.Point(185, 506);
            choosePhotoButton.Name = "choosePhotoButton";
            choosePhotoButton.Size = new System.Drawing.Size(110, 28);
            choosePhotoButton.Text = "Выбрать...";
            choosePhotoButton.UseVisualStyleBackColor = true;
            choosePhotoButton.Click += choosePhotoButton_Click;
            // 
            // clearPhotoButton
            // 
            clearPhotoButton.Location = new System.Drawing.Point(305, 506);
            clearPhotoButton.Name = "clearPhotoButton";
            clearPhotoButton.Size = new System.Drawing.Size(110, 28);
            clearPhotoButton.Text = "Убрать";
            clearPhotoButton.UseVisualStyleBackColor = true;
            clearPhotoButton.Click += clearPhotoButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(24, 550);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(110, 32);
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            deleteButton.Location = new System.Drawing.Point(185, 550);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new System.Drawing.Size(110, 32);
            deleteButton.Text = "Удалить";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new System.Drawing.Point(305, 550);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(110, 32);
            saveButton.Text = "Сохранить";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // CarForm
            // 
            AcceptButton = saveButton;
            CancelButton = cancelButton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(450, 600);
            Controls.Add(plateLabel);
            Controls.Add(plateBox);
            Controls.Add(brandLabel);
            Controls.Add(brandBox);
            Controls.Add(modelLabel);
            Controls.Add(modelBox);
            Controls.Add(yearLabel);
            Controls.Add(yearBox);
            Controls.Add(horsePowerLabel);
            Controls.Add(horsePowerBox);
            Controls.Add(tankLabel);
            Controls.Add(tankBox);
            Controls.Add(consumptionLabel);
            Controls.Add(consumptionBox);
            Controls.Add(colorLabel);
            Controls.Add(colorBox);
            Controls.Add(photoLabel);
            Controls.Add(photoBox);
            Controls.Add(choosePhotoButton);
            Controls.Add(clearPhotoButton);
            Controls.Add(cancelButton);
            Controls.Add(deleteButton);
            Controls.Add(saveButton);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CarForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Автомобиль";
            ((System.ComponentModel.ISupportInitialize)yearBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)horsePowerBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)tankBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)consumptionBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)photoBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
