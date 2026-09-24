namespace AIS_Practica_One
{
    partial class CarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel fieldsPanel;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.ComboBox brandBox;
        private System.Windows.Forms.TextBox modelBox, colorBox;
        private System.Windows.Forms.NumericUpDown yearBox, horsePowerBox, tankBox, consumptionBox;
        private System.Windows.Forms.Button saveButton, cancelButton;

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
            components = new System.ComponentModel.Container();
            fieldsPanel = new System.Windows.Forms.TableLayoutPanel();
            buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            brandBox = new System.Windows.Forms.ComboBox();
            modelBox = new System.Windows.Forms.TextBox();
            colorBox = new System.Windows.Forms.TextBox();
            yearBox = new System.Windows.Forms.NumericUpDown();
            horsePowerBox = new System.Windows.Forms.NumericUpDown();
            tankBox = new System.Windows.Forms.NumericUpDown();
            consumptionBox = new System.Windows.Forms.NumericUpDown();
            saveButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            SuspendLayout();
            fieldsPanel.Name = "fieldsPanel";
            fieldsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            fieldsPanel.Padding = new System.Windows.Forms.Padding(12);
            fieldsPanel.ColumnCount = 2;
            fieldsPanel.RowCount = 7;
            fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145));
            fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100));
            fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45));
            fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45));
            fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45));
            fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45));
            fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45));
            fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45));
            fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45));
            brandBox.Name = "brandBox";
            brandBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            modelBox.Name = "modelBox";
            colorBox.Name = "colorBox";
            yearBox.Name = "yearBox";
            yearBox.Minimum = 1900;
            yearBox.Maximum = 2100;
            yearBox.Value = 2020;
            horsePowerBox.Name = "horsePowerBox";
            horsePowerBox.Maximum = 3000;
            horsePowerBox.DecimalPlaces = 1;
            tankBox.Name = "tankBox";
            tankBox.Maximum = 1000;
            tankBox.DecimalPlaces = 1;
            consumptionBox.Name = "consumptionBox";
            consumptionBox.Maximum = 100;
            consumptionBox.DecimalPlaces = 1;
            AddField(0, "Бренд:", brandBox);
            AddField(1, "Модель:", modelBox);
            AddField(2, "Год выпуска:", yearBox);
            AddField(3, "Мощность (л.с.):", horsePowerBox);
            AddField(4, "Объём бака (л):", tankBox);
            AddField(5, "Расход (л/100 км):", consumptionBox);
            AddField(6, "Цвет:", colorBox);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            buttonsPanel.Height = 55;
            buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            buttonsPanel.Padding = new System.Windows.Forms.Padding(10);
            cancelButton.Name = "cancelButton";
            cancelButton.Text = "Отмена";
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Size = new System.Drawing.Size(100, 30);
            saveButton.Name = "saveButton";
            saveButton.Text = "Сохранить";
            saveButton.Size = new System.Drawing.Size(100, 30);
            saveButton.Click += saveButton_Click;
            buttonsPanel.Controls.Add(cancelButton);
            buttonsPanel.Controls.Add(saveButton);
            AcceptButton = saveButton;
            CancelButton = cancelButton;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(440, 390);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Controls.Add(fieldsPanel);
            Controls.Add(buttonsPanel);
            ResumeLayout(false);
        }

        #endregion

        private void AddField(int row, string caption, System.Windows.Forms.Control control)
        {
            var label = new System.Windows.Forms.Label
            {
                Text = caption,
                Name = "label" + row,
                Dock = System.Windows.Forms.DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            control.Dock = System.Windows.Forms.DockStyle.Fill;
            control.Margin = new System.Windows.Forms.Padding(3, 5, 3, 8);
            fieldsPanel.Controls.Add(label, 0, row);
            fieldsPanel.Controls.Add(control, 1, row);
        }
    }
}