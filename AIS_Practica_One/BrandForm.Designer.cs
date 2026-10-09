namespace WinForms
{
    partial class BrandForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel fieldsPanel;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.TextBox nameBox, countryBox;
        private System.Windows.Forms.Button saveButton, cancelButton;
        private System.Windows.Forms.Label nameLabel, countryLabel;

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
            fieldsPanel = new TableLayoutPanel();
            nameLabel = new Label();
            nameBox = new TextBox();
            countryLabel = new Label();
            countryBox = new TextBox();
            buttonsPanel = new FlowLayoutPanel();
            cancelButton = new Button();
            saveButton = new Button();
            fieldsPanel.SuspendLayout();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // fieldsPanel
            // 
            fieldsPanel.ColumnCount = 2;
            fieldsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            fieldsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            fieldsPanel.Controls.Add(nameLabel, 0, 0);
            fieldsPanel.Controls.Add(nameBox, 1, 0);
            fieldsPanel.Controls.Add(countryLabel, 0, 1);
            fieldsPanel.Controls.Add(countryBox, 1, 1);
            fieldsPanel.Dock = DockStyle.Fill;
            fieldsPanel.Location = new Point(0, 0);
            fieldsPanel.Name = "fieldsPanel";
            fieldsPanel.Padding = new Padding(15);
            fieldsPanel.RowCount = 2;
            fieldsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            fieldsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            fieldsPanel.Size = new Size(390, 145);
            fieldsPanel.TabIndex = 0;
            // 
            // nameLabel
            // 
            nameLabel.Dock = DockStyle.Fill;
            nameLabel.Location = new Point(18, 15);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(99, 50);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Название:";
            nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nameBox
            // 
            nameBox.Dock = DockStyle.Fill;
            nameBox.Location = new Point(123, 18);
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(249, 23);
            nameBox.TabIndex = 1;
            // 
            // countryLabel
            // 
            countryLabel.Dock = DockStyle.Fill;
            countryLabel.Location = new Point(18, 65);
            countryLabel.Name = "countryLabel";
            countryLabel.Size = new Size(99, 65);
            countryLabel.TabIndex = 2;
            countryLabel.Text = "Страна:";
            countryLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // countryBox
            // 
            countryBox.Dock = DockStyle.Fill;
            countryBox.Location = new Point(123, 68);
            countryBox.Name = "countryBox";
            countryBox.Size = new Size(249, 23);
            countryBox.TabIndex = 3;
            // 
            // buttonsPanel
            // 
            buttonsPanel.Controls.Add(cancelButton);
            buttonsPanel.Controls.Add(saveButton);
            buttonsPanel.Dock = DockStyle.Bottom;
            buttonsPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonsPanel.Location = new Point(0, 145);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Padding = new Padding(10);
            buttonsPanel.Size = new Size(390, 55);
            buttonsPanel.TabIndex = 1;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(267, 13);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(100, 30);
            cancelButton.TabIndex = 0;
            cancelButton.Text = "Отмена";
            // 
            // saveButton
            // 
            saveButton.Location = new Point(161, 13);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(100, 30);
            saveButton.TabIndex = 1;
            saveButton.Text = "Сохранить";
            saveButton.Click += saveButton_Click;
            // 
            // BrandForm
            // 
            AcceptButton = saveButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(390, 200);
            Controls.Add(fieldsPanel);
            Controls.Add(buttonsPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BrandForm";
            StartPosition = FormStartPosition.CenterParent;
            fieldsPanel.ResumeLayout(false);
            fieldsPanel.PerformLayout();
            buttonsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion


    }
}