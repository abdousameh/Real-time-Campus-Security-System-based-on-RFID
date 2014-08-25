namespace CLOTHES
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.DGV = new System.Windows.Forms.DataGridView();
            this.brandDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.styleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sizeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.othersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS = new System.Windows.Forms.BindingSource(this.components);
            this.DS = new System.Data.DataSet();
            this.DT1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dt2 = new System.Data.DataTable();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn12 = new System.Data.DataColumn();
            this.B1 = new System.Windows.Forms.Button();
            this.B2 = new System.Windows.Forms.Button();
            this.BC = new System.Windows.Forms.Button();
            this.GBD = new System.Windows.Forms.GroupBox();
            this.RBU = new System.Windows.Forms.RadioButton();
            this.RBD = new System.Windows.Forms.RadioButton();
            this.RBA = new System.Windows.Forms.RadioButton();
            this.BCONN = new System.Windows.Forms.Button();
            this.BI = new System.Windows.Forms.Button();
            this.BCOUNT = new System.Windows.Forms.Button();
            this.TBCOUNT = new System.Windows.Forms.TextBox();
            this.BCHECK = new System.Windows.Forms.Button();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn13 = new System.Data.DataColumn();
            this.dataColumn14 = new System.Data.DataColumn();
            this.dataColumn15 = new System.Data.DataColumn();
            this.dataColumn16 = new System.Data.DataColumn();
            this.dataColumn17 = new System.Data.DataColumn();
            this.dataColumn18 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
            this.dataColumn19 = new System.Data.DataColumn();
            this.dataColumn20 = new System.Data.DataColumn();
            this.dataColumn21 = new System.Data.DataColumn();
            this.dataColumn22 = new System.Data.DataColumn();
            this.dataColumn23 = new System.Data.DataColumn();
            this.dataColumn24 = new System.Data.DataColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DT1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt2)).BeginInit();
            this.GBD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            this.SuspendLayout();
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.AutoGenerateColumns = false;
            this.DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.brandDataGridViewTextBoxColumn,
            this.styleDataGridViewTextBoxColumn,
            this.sizeDataGridViewTextBoxColumn,
            this.priceDataGridViewTextBoxColumn,
            this.amountDataGridViewTextBoxColumn,
            this.othersDataGridViewTextBoxColumn});
            this.DGV.DataSource = this.BS;
            this.DGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DGV.Location = new System.Drawing.Point(10, 12);
            this.DGV.Name = "DGV";
            this.DGV.ReadOnly = true;
            this.DGV.RowHeadersVisible = false;
            this.DGV.RowTemplate.Height = 23;
            this.DGV.Size = new System.Drawing.Size(658, 469);
            this.DGV.TabIndex = 6;
            this.DGV.TabStop = false;
            // 
            // brandDataGridViewTextBoxColumn
            // 
            this.brandDataGridViewTextBoxColumn.DataPropertyName = "brand";
            this.brandDataGridViewTextBoxColumn.HeaderText = "brand";
            this.brandDataGridViewTextBoxColumn.Name = "brandDataGridViewTextBoxColumn";
            this.brandDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // styleDataGridViewTextBoxColumn
            // 
            this.styleDataGridViewTextBoxColumn.DataPropertyName = "style";
            this.styleDataGridViewTextBoxColumn.HeaderText = "style";
            this.styleDataGridViewTextBoxColumn.Name = "styleDataGridViewTextBoxColumn";
            this.styleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sizeDataGridViewTextBoxColumn
            // 
            this.sizeDataGridViewTextBoxColumn.DataPropertyName = "size";
            this.sizeDataGridViewTextBoxColumn.HeaderText = "size";
            this.sizeDataGridViewTextBoxColumn.Name = "sizeDataGridViewTextBoxColumn";
            this.sizeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "price";
            this.priceDataGridViewTextBoxColumn.HeaderText = "price";
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            this.priceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // amountDataGridViewTextBoxColumn
            // 
            this.amountDataGridViewTextBoxColumn.DataPropertyName = "amount";
            this.amountDataGridViewTextBoxColumn.HeaderText = "amount";
            this.amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
            this.amountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // othersDataGridViewTextBoxColumn
            // 
            this.othersDataGridViewTextBoxColumn.DataPropertyName = "others";
            this.othersDataGridViewTextBoxColumn.HeaderText = "others";
            this.othersDataGridViewTextBoxColumn.Name = "othersDataGridViewTextBoxColumn";
            this.othersDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // BS
            // 
            this.BS.DataMember = "TEMP";
            this.BS.DataSource = this.DS;
            // 
            // DS
            // 
            this.DS.DataSetName = "NewDataSet";
            this.DS.Tables.AddRange(new System.Data.DataTable[] {
            this.DT1,
            this.dt2,
            this.dataTable1,
            this.dataTable2});
            // 
            // DT1
            // 
            this.DT1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6});
            this.DT1.TableName = "TB";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "brand";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "style";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "size";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "price";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "amount";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "others";
            // 
            // dt2
            // 
            this.dt2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11,
            this.dataColumn12});
            this.dt2.TableName = "TEMP";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "brand";
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "style";
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "size";
            // 
            // dataColumn10
            // 
            this.dataColumn10.ColumnName = "price";
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "amount";
            // 
            // dataColumn12
            // 
            this.dataColumn12.ColumnName = "others";
            // 
            // B1
            // 
            this.B1.Location = new System.Drawing.Point(528, 515);
            this.B1.Name = "B1";
            this.B1.Size = new System.Drawing.Size(140, 45);
            this.B1.TabIndex = 7;
            this.B1.Text = "读出数据库";
            this.B1.UseVisualStyleBackColor = true;
            this.B1.Click += new System.EventHandler(this.B1_Click);
            // 
            // B2
            // 
            this.B2.Location = new System.Drawing.Point(337, 514);
            this.B2.Name = "B2";
            this.B2.Size = new System.Drawing.Size(140, 44);
            this.B2.TabIndex = 8;
            this.B2.Text = "更新数据库";
            this.B2.UseVisualStyleBackColor = true;
            this.B2.Click += new System.EventHandler(this.B2_Click);
            // 
            // BC
            // 
            this.BC.Location = new System.Drawing.Point(12, 515);
            this.BC.Name = "BC";
            this.BC.Size = new System.Drawing.Size(145, 44);
            this.BC.TabIndex = 10;
            this.BC.Text = "新建CLOTHES库";
            this.BC.UseVisualStyleBackColor = true;
            this.BC.Click += new System.EventHandler(this.BC_Click);
            // 
            // GBD
            // 
            this.GBD.Controls.Add(this.RBU);
            this.GBD.Controls.Add(this.RBD);
            this.GBD.Controls.Add(this.RBA);
            this.GBD.Location = new System.Drawing.Point(695, 459);
            this.GBD.Name = "GBD";
            this.GBD.Size = new System.Drawing.Size(200, 100);
            this.GBD.TabIndex = 11;
            this.GBD.TabStop = false;
            this.GBD.Text = "数据库更新选项";
            // 
            // RBU
            // 
            this.RBU.AutoSize = true;
            this.RBU.Location = new System.Drawing.Point(105, 30);
            this.RBU.Name = "RBU";
            this.RBU.Size = new System.Drawing.Size(47, 16);
            this.RBU.TabIndex = 2;
            this.RBU.TabStop = true;
            this.RBU.Text = "更新";
            this.RBU.UseVisualStyleBackColor = true;
            // 
            // RBD
            // 
            this.RBD.AutoSize = true;
            this.RBD.Location = new System.Drawing.Point(40, 70);
            this.RBD.Name = "RBD";
            this.RBD.Size = new System.Drawing.Size(47, 16);
            this.RBD.TabIndex = 1;
            this.RBD.TabStop = true;
            this.RBD.Text = "出库";
            this.RBD.UseVisualStyleBackColor = true;
            // 
            // RBA
            // 
            this.RBA.AutoSize = true;
            this.RBA.Location = new System.Drawing.Point(40, 30);
            this.RBA.Name = "RBA";
            this.RBA.Size = new System.Drawing.Size(47, 16);
            this.RBA.TabIndex = 0;
            this.RBA.TabStop = true;
            this.RBA.Text = "入库";
            this.RBA.UseVisualStyleBackColor = true;
            // 
            // BCONN
            // 
            this.BCONN.Location = new System.Drawing.Point(709, 12);
            this.BCONN.Name = "BCONN";
            this.BCONN.Size = new System.Drawing.Size(73, 60);
            this.BCONN.TabIndex = 12;
            this.BCONN.Text = "连接阅读器";
            this.BCONN.UseVisualStyleBackColor = true;
            this.BCONN.Click += new System.EventHandler(this.BCONN_Click);
            // 
            // BI
            // 
            this.BI.Location = new System.Drawing.Point(824, 12);
            this.BI.Name = "BI";
            this.BI.Size = new System.Drawing.Size(71, 60);
            this.BI.TabIndex = 13;
            this.BI.Text = "清点货物";
            this.BI.UseVisualStyleBackColor = true;
            this.BI.Click += new System.EventHandler(this.BI_Click);
            // 
            // BCOUNT
            // 
            this.BCOUNT.Location = new System.Drawing.Point(711, 124);
            this.BCOUNT.Name = "BCOUNT";
            this.BCOUNT.Size = new System.Drawing.Size(71, 33);
            this.BCOUNT.TabIndex = 14;
            this.BCOUNT.Text = "计数";
            this.BCOUNT.UseVisualStyleBackColor = true;
            this.BCOUNT.Click += new System.EventHandler(this.BCOUNT_Click);
            // 
            // TBCOUNT
            // 
            this.TBCOUNT.Location = new System.Drawing.Point(709, 97);
            this.TBCOUNT.Name = "TBCOUNT";
            this.TBCOUNT.Size = new System.Drawing.Size(73, 21);
            this.TBCOUNT.TabIndex = 15;
            // 
            // BCHECK
            // 
            this.BCHECK.Location = new System.Drawing.Point(824, 97);
            this.BCHECK.Name = "BCHECK";
            this.BCHECK.Size = new System.Drawing.Size(71, 60);
            this.BCHECK.TabIndex = 17;
            this.BCHECK.Text = "查漏";
            this.BCHECK.UseVisualStyleBackColor = true;
            this.BCHECK.Click += new System.EventHandler(this.BCHECK_Click);
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn13,
            this.dataColumn14,
            this.dataColumn15,
            this.dataColumn16,
            this.dataColumn17,
            this.dataColumn18});
            this.dataTable1.TableName = "CTB";
            // 
            // dataColumn13
            // 
            this.dataColumn13.ColumnName = "brand";
            // 
            // dataColumn14
            // 
            this.dataColumn14.ColumnName = "style";
            // 
            // dataColumn15
            // 
            this.dataColumn15.ColumnName = "size";
            // 
            // dataColumn16
            // 
            this.dataColumn16.ColumnName = "price";
            // 
            // dataColumn17
            // 
            this.dataColumn17.ColumnName = "amount";
            // 
            // dataColumn18
            // 
            this.dataColumn18.ColumnName = "others";
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn19,
            this.dataColumn20,
            this.dataColumn21,
            this.dataColumn22,
            this.dataColumn23,
            this.dataColumn24});
            this.dataTable2.TableName = "TEMPI";
            // 
            // dataColumn19
            // 
            this.dataColumn19.ColumnName = "brand";
            // 
            // dataColumn20
            // 
            this.dataColumn20.ColumnName = "style";
            // 
            // dataColumn21
            // 
            this.dataColumn21.ColumnName = "size";
            // 
            // dataColumn22
            // 
            this.dataColumn22.ColumnName = "price";
            // 
            // dataColumn23
            // 
            this.dataColumn23.ColumnName = "amount";
            // 
            // dataColumn24
            // 
            this.dataColumn24.ColumnName = "others";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 571);
            this.Controls.Add(this.BCHECK);
            this.Controls.Add(this.TBCOUNT);
            this.Controls.Add(this.BCOUNT);
            this.Controls.Add(this.BI);
            this.Controls.Add(this.BCONN);
            this.Controls.Add(this.GBD);
            this.Controls.Add(this.BC);
            this.Controls.Add(this.B2);
            this.Controls.Add(this.B1);
            this.Controls.Add(this.DGV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "clothes.hdu";
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DT1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt2)).EndInit();
            this.GBD.ResumeLayout(false);
            this.GBD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV;
        private System.Data.DataSet DS;
        private System.Data.DataTable DT1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataTable dt2;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataColumn dataColumn9;
        private System.Data.DataColumn dataColumn10;
        private System.Data.DataColumn dataColumn11;
        private System.Data.DataColumn dataColumn12;
        private System.Windows.Forms.BindingSource BS;
        private System.Windows.Forms.Button B1;
        private System.Windows.Forms.Button B2;
        private System.Windows.Forms.Button BC;
        private System.Windows.Forms.GroupBox GBD;
        private System.Windows.Forms.RadioButton RBD;
        private System.Windows.Forms.RadioButton RBA;
        private System.Windows.Forms.DataGridViewTextBoxColumn brandDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn styleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sizeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn othersDataGridViewTextBoxColumn;
        private System.Windows.Forms.RadioButton RBU;
        private System.Windows.Forms.Button BCONN;
        private System.Windows.Forms.Button BI;
        private System.Windows.Forms.Button BCOUNT;
        private System.Windows.Forms.TextBox TBCOUNT;
        private System.Windows.Forms.Button BCHECK;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn13;
        private System.Data.DataColumn dataColumn14;
        private System.Data.DataColumn dataColumn15;
        private System.Data.DataColumn dataColumn16;
        private System.Data.DataColumn dataColumn17;
        private System.Data.DataColumn dataColumn18;
        private System.Data.DataTable dataTable2;
        private System.Data.DataColumn dataColumn19;
        private System.Data.DataColumn dataColumn20;
        private System.Data.DataColumn dataColumn21;
        private System.Data.DataColumn dataColumn22;
        private System.Data.DataColumn dataColumn23;
        private System.Data.DataColumn dataColumn24;
    }
}

