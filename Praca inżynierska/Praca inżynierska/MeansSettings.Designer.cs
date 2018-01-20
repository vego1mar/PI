namespace PI
{
    partial class MeansSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && (components != null) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiGrid_PowRank_Num = new System.Windows.Forms.NumericUpDown();
            this.ui_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.ui_Ok_Btn = new System.Windows.Forms.Button();
            this.ui_Grid_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiGrid_CstDiff_TxtBx = new System.Windows.Forms.TextBox();
            this.uiGrid_Geo_TxtBx = new System.Windows.Forms.TextBox();
            this.uiGrid_GeoVar_ComBx = new System.Windows.Forms.ComboBox();
            this.uiGrid_DiffMode_TxtBx = new System.Windows.Forms.TextBox();
            this.uiGrid_GeoVar_TxtBx = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGrid_PowRank_Num)).BeginInit();
            this.ui_TbLay.SuspendLayout();
            this.ui_Grid_TbLay.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGrid_PowRank_Num
            // 
            this.uiGrid_PowRank_Num.DecimalPlaces = 2;
            this.uiGrid_PowRank_Num.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_PowRank_Num.Location = new System.Drawing.Point(202, 128);
            this.uiGrid_PowRank_Num.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.uiGrid_PowRank_Num.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.uiGrid_PowRank_Num.Name = "uiGrid_PowRank_Num";
            this.uiGrid_PowRank_Num.Size = new System.Drawing.Size(193, 20);
            this.uiGrid_PowRank_Num.TabIndex = 1;
            this.uiGrid_PowRank_Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uiGrid_PowRank_Num.ThousandsSeparator = true;
            // 
            // ui_TbLay
            // 
            this.ui_TbLay.ColumnCount = 1;
            this.ui_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ui_TbLay.Controls.Add(this.ui_Ok_Btn, 0, 1);
            this.ui_TbLay.Controls.Add(this.ui_Grid_TbLay, 0, 0);
            this.ui_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_TbLay.Location = new System.Drawing.Point(0, 0);
            this.ui_TbLay.Name = "ui_TbLay";
            this.ui_TbLay.RowCount = 2;
            this.ui_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ui_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.ui_TbLay.Size = new System.Drawing.Size(404, 381);
            this.ui_TbLay.TabIndex = 1;
            // 
            // ui_Ok_Btn
            // 
            this.ui_Ok_Btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ui_Ok_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_Ok_Btn.Location = new System.Drawing.Point(3, 349);
            this.ui_Ok_Btn.Name = "ui_Ok_Btn";
            this.ui_Ok_Btn.Size = new System.Drawing.Size(398, 29);
            this.ui_Ok_Btn.TabIndex = 1;
            this.ui_Ok_Btn.Text = "OK";
            this.ui_Ok_Btn.UseVisualStyleBackColor = true;
            this.ui_Ok_Btn.Click += new System.EventHandler(this.OnOkClick);
            // 
            // ui_Grid_TbLay
            // 
            this.ui_Grid_TbLay.AutoScroll = true;
            this.ui_Grid_TbLay.BackColor = System.Drawing.Color.Transparent;
            this.ui_Grid_TbLay.ColumnCount = 2;
            this.ui_Grid_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ui_Grid_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_CstDiff_TxtBx, 0, 3);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_Geo_TxtBx, 0, 0);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_GeoVar_ComBx, 1, 1);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_DiffMode_TxtBx, 0, 4);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_GeoVar_TxtBx, 0, 1);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_PowRank_Num, 1, 5);
            this.ui_Grid_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_Grid_TbLay.Location = new System.Drawing.Point(3, 3);
            this.ui_Grid_TbLay.Name = "ui_Grid_TbLay";
            this.ui_Grid_TbLay.RowCount = 12;
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.Size = new System.Drawing.Size(398, 340);
            this.ui_Grid_TbLay.TabIndex = 2;
            // 
            // uiGrid_CstDiff_TxtBx
            // 
            this.uiGrid_CstDiff_TxtBx.BackColor = System.Drawing.SystemColors.Control;
            this.ui_Grid_TbLay.SetColumnSpan(this.uiGrid_CstDiff_TxtBx, 2);
            this.uiGrid_CstDiff_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_CstDiff_TxtBx.Enabled = false;
            this.uiGrid_CstDiff_TxtBx.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.uiGrid_CstDiff_TxtBx.Location = new System.Drawing.Point(3, 78);
            this.uiGrid_CstDiff_TxtBx.Name = "uiGrid_CstDiff_TxtBx";
            this.uiGrid_CstDiff_TxtBx.ReadOnly = true;
            this.uiGrid_CstDiff_TxtBx.Size = new System.Drawing.Size(392, 20);
            this.uiGrid_CstDiff_TxtBx.TabIndex = 29;
            this.uiGrid_CstDiff_TxtBx.Text = "Custom-Differential";
            // 
            // uiGrid_Geo_TxtBx
            // 
            this.uiGrid_Geo_TxtBx.BackColor = System.Drawing.SystemColors.Control;
            this.ui_Grid_TbLay.SetColumnSpan(this.uiGrid_Geo_TxtBx, 2);
            this.uiGrid_Geo_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_Geo_TxtBx.Enabled = false;
            this.uiGrid_Geo_TxtBx.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.uiGrid_Geo_TxtBx.Location = new System.Drawing.Point(3, 3);
            this.uiGrid_Geo_TxtBx.Name = "uiGrid_Geo_TxtBx";
            this.uiGrid_Geo_TxtBx.ReadOnly = true;
            this.uiGrid_Geo_TxtBx.Size = new System.Drawing.Size(392, 20);
            this.uiGrid_Geo_TxtBx.TabIndex = 28;
            this.uiGrid_Geo_TxtBx.Text = "Geometric";
            // 
            // uiGrid_GeoVar_ComBx
            // 
            this.uiGrid_GeoVar_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_GeoVar_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiGrid_GeoVar_ComBx.FormattingEnabled = true;
            this.uiGrid_GeoVar_ComBx.Location = new System.Drawing.Point(202, 28);
            this.uiGrid_GeoVar_ComBx.Name = "uiGrid_GeoVar_ComBx";
            this.uiGrid_GeoVar_ComBx.Size = new System.Drawing.Size(193, 21);
            this.uiGrid_GeoVar_ComBx.TabIndex = 27;
            // 
            // uiGrid_DiffMode_TxtBx
            // 
            this.uiGrid_DiffMode_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiGrid_DiffMode_TxtBx.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiGrid_DiffMode_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_DiffMode_TxtBx.Location = new System.Drawing.Point(3, 103);
            this.uiGrid_DiffMode_TxtBx.Name = "uiGrid_DiffMode_TxtBx";
            this.uiGrid_DiffMode_TxtBx.ReadOnly = true;
            this.uiGrid_DiffMode_TxtBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiGrid_DiffMode_TxtBx.Size = new System.Drawing.Size(193, 20);
            this.uiGrid_DiffMode_TxtBx.TabIndex = 26;
            this.uiGrid_DiffMode_TxtBx.Text = "Workmode:";
            // 
            // uiGrid_GeoVar_TxtBx
            // 
            this.uiGrid_GeoVar_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiGrid_GeoVar_TxtBx.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiGrid_GeoVar_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_GeoVar_TxtBx.Location = new System.Drawing.Point(3, 28);
            this.uiGrid_GeoVar_TxtBx.Name = "uiGrid_GeoVar_TxtBx";
            this.uiGrid_GeoVar_TxtBx.ReadOnly = true;
            this.uiGrid_GeoVar_TxtBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiGrid_GeoVar_TxtBx.Size = new System.Drawing.Size(193, 20);
            this.uiGrid_GeoVar_TxtBx.TabIndex = 25;
            this.uiGrid_GeoVar_TxtBx.Text = "Variant:";
            // 
            // MeansSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 381);
            this.Controls.Add(this.ui_TbLay);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 420);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 420);
            this.Name = "MeansSettings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Means Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.uiGrid_PowRank_Num)).EndInit();
            this.ui_TbLay.ResumeLayout(false);
            this.ui_Grid_TbLay.ResumeLayout(false);
            this.ui_Grid_TbLay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel ui_TbLay;
        private System.Windows.Forms.Button ui_Ok_Btn;
        private System.Windows.Forms.NumericUpDown uiGrid_PowRank_Num;
        private System.Windows.Forms.TableLayoutPanel ui_Grid_TbLay;
        private System.Windows.Forms.TextBox uiGrid_GeoVar_TxtBx;
        private System.Windows.Forms.TextBox uiGrid_DiffMode_TxtBx;
        private System.Windows.Forms.ComboBox uiGrid_GeoVar_ComBx;
        private System.Windows.Forms.TextBox uiGrid_Geo_TxtBx;
        private System.Windows.Forms.TextBox uiGrid_CstDiff_TxtBx;
    }
}