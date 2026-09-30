namespace AIS_Practica_One
{
    partial class CarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label plateLabel;
        private System.Windows.Forms.Label brandLabel;
        private System.Windows.Forms.Label modelLabel;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.Label horsePowerLabel;
        private System.Windows.Forms.Label tankLabel;
        private System.Windows.Forms.Label consumptionLabel;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.TextBox plateBox;
        private System.Windows.Forms.ComboBox brandBox;
        private System.Windows.Forms.TextBox modelBox;
        private System.Windows.Forms.NumericUpDown yearBox;
        private System.Windows.Forms.NumericUpDown horsePowerBox;
        private System.Windows.Forms.NumericUpDown tankBox;
        private System.Windows.Forms.NumericUpDown consumptionBox;
        private System.Windows.Forms.TextBox colorBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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
            plateBox = new System.Windows.Forms.TextBox();
            brandBox = new System.Windows.Forms.ComboBox();
            modelBox = new System.Windows.Forms.TextBox();
            yearBox = new System.Windows.Forms.NumericUpDown();
            horsePowerBox = new System.Windows.Forms.NumericUpDown();
            tankBox = new System.Windows.Forms.NumericUpDown();
            consumptionBox = new System.Windows.Forms.NumericUpDown();
            colorBox = new System.Windows.Forms.TextBox();
            saveButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)yearBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)horsePowerBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tankBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)consumptionBox).BeginInit();
            SuspendLayout();
            // plateLabel
            plateLabel.AutoSize = true;
            plateLabel.Location = new System.Drawing.Point(24, 26);
            plateLabel.Name = "plateLabel";
            plateLabel.Text = "Госномер:";
            // brandLabel
            brandLabel.AutoSize = true;
            brandLabel.Location = new System.Drawing.Point(24, 69);
            brandLabel.Name = "brandLabel";
            brandLabel.Text = "Бренд:";
            // modelLabel
            modelLabel.AutoSize = true;
            modelLabel.Location = new System.Drawing.Point(24, 112);
            modelLabel.Name = "modelLabel";
            modelLabel.Text = "Модель:";
            // yearLabel
            yearLabel.AutoSize = true;
            yearLabel.Location = new System.Drawing.Point(24, 155);
            yearLabel.Name = "yearLabel";
            yearLabel.Text = "Год выпуска:";
            // horsePowerLabel
            horsePowerLabel.AutoSize = true;
            horsePowerLabel.Location = new System.Drawing.Point(24, 198);
            horsePowerLabel.Name = "horsePowerLabel";
            horsePowerLabel.Text = "Мощность (л.с.):";
            // tankLabel
            tankLabel.AutoSize = true;
            tankLabel.Location = new System.Drawing.Point(24, 241);
            tankLabel.Name = "tankLabel";
            tankLabel.Text = "Объём бака (л):";
            // consumptionLabel
            consumptionLabel.AutoSize = true;
            consumptionLabel.Location = new System.Drawing.Point(24, 284);
            consumptionLabel.Name = "consumptionLabel";
            consumptionLabel.Text = "Расход (л/100 км):";
            // colorLabel
            colorLabel.AutoSize = true;
            colorLabel.Location = new System.Drawing.Point(24, 327);
            colorLabel.Name = "colorLabel";
            colorLabel.Text = "Цвет:";
            // plateBox
            plateBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            plateBox.Location = new System.Drawing.Point(185, 23);
            plateBox.MaxLength = 9;
            plateBox.Name = "plateBox";
            plateBox.Size = new System.Drawing.Size(230, 23);
            // brandBox
            brandBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            brandBox.FormattingEnabled = true;
            brandBox.Location = new System.Drawing.Point(185, 66);
            brandBox.Name = "brandBox";
            brandBox.Size = new System.Drawing.Size(230, 23);
            // modelBox
            modelBox.Location = new System.Drawing.Point(185, 109);
            modelBox.Name = "modelBox";
            modelBox.Size = new System.Drawing.Size(230, 23);
            // yearBox
            yearBox.Location = new System.Drawing.Point(185, 152);
            yearBox.Minimum = 1900;
            yearBox.Maximum = 2100;
            yearBox.Name = "yearBox";
            yearBox.Size = new System.Drawing.Size(230, 23);
            yearBox.Value = 2020;
            // horsePowerBox
            horsePowerBox.DecimalPlaces = 1;
            horsePowerBox.Location = new System.Drawing.Point(185, 195);
            horsePowerBox.Maximum = 3000;
            horsePowerBox.Name = "horsePowerBox";
            horsePowerBox.Size = new System.Drawing.Size(230, 23);
            // tankBox
            tankBox.DecimalPlaces = 1;
            tankBox.Location = new System.Drawing.Point(185, 238);
            tankBox.Maximum = 1000;
            tankBox.Name = "tankBox";
            tankBox.Size = new System.Drawing.Size(230, 23);
            // consumptionBox
            consumptionBox.DecimalPlaces = 1;
            consumptionBox.Location = new System.Drawing.Point(185, 281);
            consumptionBox.Maximum = 100;
            consumptionBox.Name = "consumptionBox";
            consumptionBox.Size = new System.Drawing.Size(230, 23);
            // colorBox
            colorBox.Location = new System.Drawing.Point(185, 324);
            colorBox.Name = "colorBox";
            colorBox.Size = new System.Drawing.Size(230, 23);
            // saveButton
            saveButton.Location = new System.Drawing.Point(185, 376);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(110, 32);
            saveButton.Text = "Сохранить";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // cancelButton
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(305, 376);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(110, 32);
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // CarForm
            AcceptButton = saveButton;
            CancelButton = cancelButton;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(442, 431);
            Controls.Add(plateLabel);
            Controls.Add(brandLabel);
            Controls.Add(modelLabel);
            Controls.Add(yearLabel);
            Controls.Add(horsePowerLabel);
            Controls.Add(tankLabel);
            Controls.Add(consumptionLabel);
            Controls.Add(colorLabel);
            Controls.Add(plateBox);
            Controls.Add(brandBox);
            Controls.Add(modelBox);
            Controls.Add(yearBox);
            Controls.Add(horsePowerBox);
            Controls.Add(tankBox);
            Controls.Add(consumptionBox);
            Controls.Add(colorBox);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CarForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)yearBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)horsePowerBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)tankBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)consumptionBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        
    }
}