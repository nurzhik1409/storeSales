namespace trpoMainProject
{
    partial class AddOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.productGrid = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.clientBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addOrderBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.addProductBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.qtyNumeric = new System.Windows.Forms.NumericUpDown();
            this.productBox = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.productGrid)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qtyNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // productGrid
            // 
            this.productGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.productName,
            this.qty,
            this.price});
            this.productGrid.Location = new System.Drawing.Point(12, 12);
            this.productGrid.Name = "productGrid";
            this.productGrid.ReadOnly = true;
            this.productGrid.Size = new System.Drawing.Size(420, 280);
            this.productGrid.TabIndex = 0;
            this.productGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // id
            // 
            this.id.HeaderText = "Id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // productName
            // 
            this.productName.HeaderText = "Название Товара";
            this.productName.Name = "productName";
            this.productName.Width = 150;
            // 
            // qty
            // 
            this.qty.HeaderText = "Количество";
            this.qty.Name = "qty";
            // 
            // price
            // 
            this.price.HeaderText = "Стоимость";
            this.price.Name = "price";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 295);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(657, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabel1.Text = "Стоимость";
            // 
            // clientBox
            // 
            this.clientBox.FormattingEnabled = true;
            this.clientBox.Location = new System.Drawing.Point(438, 25);
            this.clientBox.Name = "clientBox";
            this.clientBox.Size = new System.Drawing.Size(206, 21);
            this.clientBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(435, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Клиент";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // addOrderBtn
            // 
            this.addOrderBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addOrderBtn.Location = new System.Drawing.Point(438, 226);
            this.addOrderBtn.Name = "addOrderBtn";
            this.addOrderBtn.Size = new System.Drawing.Size(207, 66);
            this.addOrderBtn.TabIndex = 9;
            this.addOrderBtn.Text = "Соверишть Заказ";
            this.addOrderBtn.UseVisualStyleBackColor = true;
            this.addOrderBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.addProductBtn);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.qtyNumeric);
            this.panel1.Controls.Add(this.productBox);
            this.panel1.Location = new System.Drawing.Point(438, 98);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 122);
            this.panel1.TabIndex = 10;
            // 
            // addProductBtn
            // 
            this.addProductBtn.Location = new System.Drawing.Point(125, 96);
            this.addProductBtn.Name = "addProductBtn";
            this.addProductBtn.Size = new System.Drawing.Size(75, 23);
            this.addProductBtn.TabIndex = 13;
            this.addProductBtn.Text = "Добавить";
            this.addProductBtn.UseVisualStyleBackColor = true;
            this.addProductBtn.Click += new System.EventHandler(this.addProductBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Количество товара";
            this.label3.Click += new System.EventHandler(this.Label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Товар";
            // 
            // qtyNumeric
            // 
            this.qtyNumeric.Location = new System.Drawing.Point(3, 72);
            this.qtyNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.qtyNumeric.Name = "qtyNumeric";
            this.qtyNumeric.Size = new System.Drawing.Size(197, 20);
            this.qtyNumeric.TabIndex = 10;
            this.qtyNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // productBox
            // 
            this.productBox.FormattingEnabled = true;
            this.productBox.Location = new System.Drawing.Point(3, 22);
            this.productBox.Name = "productBox";
            this.productBox.Size = new System.Drawing.Size(197, 21);
            this.productBox.TabIndex = 9;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(438, 72);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(207, 20);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(435, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Дата Оформления";
            // 
            // AddOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 317);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.addOrderBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clientBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.productGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddOrder";
            this.Text = "AddOrder";
            this.Load += new System.EventHandler(this.AddOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.productGrid)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qtyNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView productGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn productName;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ComboBox clientBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addOrderBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button addProductBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown qtyNumeric;
        private System.Windows.Forms.ComboBox productBox;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
    }
}