namespace VehicleTracking.Win.Forms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            tabControl1 = new TabControl();
            tabSaveUpdate = new TabPage();
            button1 = new Button();
            textBoxAmount = new TextBox();
            label3 = new Label();
            textBoxRawMaterial = new TextBox();
            label2 = new Label();
            textBoxPlateNumber = new TextBox();
            label1 = new Label();
            tabFirstApprovel = new TabPage();
            dataGridView1 = new DataGridView();
            tabControl1.SuspendLayout();
            tabSaveUpdate.SuspendLayout();
            tabFirstApprovel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabSaveUpdate);
            tabControl1.Controls.Add(tabFirstApprovel);
            tabControl1.Location = new Point(10, 9);
            tabControl1.Margin = new Padding(3, 2, 3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1204, 522);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabSaveUpdate
            // 
            tabSaveUpdate.Controls.Add(button1);
            tabSaveUpdate.Controls.Add(textBoxAmount);
            tabSaveUpdate.Controls.Add(label3);
            tabSaveUpdate.Controls.Add(textBoxRawMaterial);
            tabSaveUpdate.Controls.Add(label2);
            tabSaveUpdate.Controls.Add(textBoxPlateNumber);
            tabSaveUpdate.Controls.Add(label1);
            tabSaveUpdate.Location = new Point(4, 24);
            tabSaveUpdate.Margin = new Padding(3, 2, 3, 2);
            tabSaveUpdate.Name = "tabSaveUpdate";
            tabSaveUpdate.Padding = new Padding(3, 2, 3, 2);
            tabSaveUpdate.Size = new Size(1196, 494);
            tabSaveUpdate.TabIndex = 0;
            tabSaveUpdate.Text = "EKLE/DÜZENLE";
            tabSaveUpdate.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(452, 297);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(151, 22);
            button1.TabIndex = 6;
            button1.Text = "KAYDET";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBoxAmount
            // 
            textBoxAmount.Location = new Point(408, 252);
            textBoxAmount.Margin = new Padding(3, 2, 3, 2);
            textBoxAmount.Name = "textBoxAmount";
            textBoxAmount.Size = new Size(252, 23);
            textBoxAmount.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(408, 235);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 4;
            label3.Text = "Miktar";
            // 
            // textBoxRawMaterial
            // 
            textBoxRawMaterial.Location = new Point(408, 194);
            textBoxRawMaterial.Margin = new Padding(3, 2, 3, 2);
            textBoxRawMaterial.Name = "textBoxRawMaterial";
            textBoxRawMaterial.Size = new Size(252, 23);
            textBoxRawMaterial.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(408, 176);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 2;
            label2.Text = "Ham Madde";
            // 
            // textBoxPlateNumber
            // 
            textBoxPlateNumber.Location = new Point(408, 136);
            textBoxPlateNumber.Margin = new Padding(3, 2, 3, 2);
            textBoxPlateNumber.Name = "textBoxPlateNumber";
            textBoxPlateNumber.Size = new Size(252, 23);
            textBoxPlateNumber.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(408, 118);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 0;
            label1.Text = "Plaka";
            // 
            // tabFirstApprovel
            // 
            tabFirstApprovel.Controls.Add(dataGridView1);
            tabFirstApprovel.Location = new Point(4, 24);
            tabFirstApprovel.Margin = new Padding(3, 2, 3, 2);
            tabFirstApprovel.Name = "tabFirstApprovel";
            tabFirstApprovel.Padding = new Padding(3, 2, 3, 2);
            tabFirstApprovel.Size = new Size(1196, 494);
            tabFirstApprovel.TabIndex = 1;
            tabFirstApprovel.Text = "KAYITLAR";
            tabFirstApprovel.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(1190, 490);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1225, 540);
            Controls.Add(tabControl1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabSaveUpdate.ResumeLayout(false);
            tabSaveUpdate.PerformLayout();
            tabFirstApprovel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabSaveUpdate;
        private TabPage tabFirstApprovel;
        private TextBox textBoxAmount;
        private Label label3;
        private TextBox textBoxRawMaterial;
        private Label label2;
        private TextBox textBoxPlateNumber;
        private Label label1;
        private Button button1;
        private DataGridView dataGridView1;
    }
}
