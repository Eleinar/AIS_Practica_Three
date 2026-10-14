namespace WinForms
{
    partial class BrandForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label countryLabel;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox countryBox;
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
            nameLabel = new System.Windows.Forms.Label();
            countryLabel = new System.Windows.Forms.Label();
            nameBox = new System.Windows.Forms.TextBox();
            countryBox = new System.Windows.Forms.TextBox();
            saveButton = new System.Windows.Forms.Button();
            deleteButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            SuspendLayout();

            nameLabel.AutoSize = true;
            nameLabel.Location = new System.Drawing.Point(24, 28);
            nameLabel.Text = "Название:";

            nameBox.Location = new System.Drawing.Point(120, 25);
            nameBox.Size = new System.Drawing.Size(250, 23);
            nameBox.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;

            countryLabel.AutoSize = true;
            countryLabel.Location = new System.Drawing.Point(24, 72);
            countryLabel.Text = "Страна:";

            countryBox.Location = new System.Drawing.Point(120, 69);
            countryBox.Size = new System.Drawing.Size(250, 23);
            countryBox.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;

            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(24, 120);
            cancelButton.Size = new System.Drawing.Size(100, 32);
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;

            deleteButton.Location = new System.Drawing.Point(150, 120);
            deleteButton.Size = new System.Drawing.Size(100, 32);
            deleteButton.Text = "Удалить";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;

            saveButton.Location = new System.Drawing.Point(270, 120);
            saveButton.Size = new System.Drawing.Size(100, 32);
            saveButton.Text = "Сохранить";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;

            AcceptButton = saveButton;
            CancelButton = cancelButton;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(400, 175);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BrandForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Бренд";

            Controls.Add(nameLabel);
            Controls.Add(nameBox);
            Controls.Add(countryLabel);
            Controls.Add(countryBox);
            Controls.Add(cancelButton);
            Controls.Add(deleteButton);
            Controls.Add(saveButton);

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
