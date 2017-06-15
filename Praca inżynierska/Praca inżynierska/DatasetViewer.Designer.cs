namespace PI
{
    partial class DatasetViewer
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
            this.ui_SpCtn = new System.Windows.Forms.SplitContainer();
            this.uiPnl_TblLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiPnl_Perform_Btn = new System.Windows.Forms.Button();
            this.uiPnl_Value2_TxtBx = new System.Windows.Forms.TextBox();
            this.uiPnl_Value1_TxtBx = new System.Windows.Forms.TextBox();
            this.uiPnl_PointIdx_TxtBx = new System.Windows.Forms.TextBox();
            this.uiPnl_Edit_TxtBx = new System.Windows.Forms.TextBox();
            this.uiPnl_OperT_TxtBx = new System.Windows.Forms.TextBox();
            this.uiPnl_OperT_ComBx = new System.Windows.Forms.ComboBox();
            this.uiPnl_PointIdx_Num = new System.Windows.Forms.NumericUpDown();
            this.uiPnl_PointIdx_TrBr = new System.Windows.Forms.TrackBar();
            this.uiPnl_SaveExit_Btn = new System.Windows.Forms.Button();
            this.uiGrid_TblLay = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.ui_SpCtn)).BeginInit();
            this.ui_SpCtn.Panel1.SuspendLayout();
            this.ui_SpCtn.Panel2.SuspendLayout();
            this.ui_SpCtn.SuspendLayout();
            this.uiPnl_TblLay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiPnl_PointIdx_Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPnl_PointIdx_TrBr)).BeginInit();
            this.SuspendLayout();
            // 
            // ui_SpCtn
            // 
            this.ui_SpCtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_SpCtn.Location = new System.Drawing.Point(0, 0);
            this.ui_SpCtn.Name = "ui_SpCtn";
            // 
            // ui_SpCtn.Panel1
            // 
            this.ui_SpCtn.Panel1.Controls.Add(this.uiPnl_TblLay);
            // 
            // ui_SpCtn.Panel2
            // 
            this.ui_SpCtn.Panel2.Controls.Add(this.uiGrid_TblLay);
            this.ui_SpCtn.Size = new System.Drawing.Size(780, 371);
            this.ui_SpCtn.SplitterDistance = 260;
            this.ui_SpCtn.TabIndex = 0;
            // 
            // uiPnl_TblLay
            // 
            this.uiPnl_TblLay.BackColor = System.Drawing.SystemColors.Control;
            this.uiPnl_TblLay.ColumnCount = 2;
            this.uiPnl_TblLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiPnl_TblLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiPnl_TblLay.Controls.Add(this.uiPnl_Perform_Btn, 1, 6);
            this.uiPnl_TblLay.Controls.Add(this.uiPnl_Value2_TxtBx, 1, 4);
            this.uiPnl_TblLay.Controls.Add(this.uiPnl_Value1_TxtBx, 0, 4);
            this.uiPnl_TblLay.Controls.Add(this.uiPnl_PointIdx_TxtBx, 0, 2);
            this.uiPnl_TblLay.Controls.Add(this.uiPnl_Edit_TxtBx, 0, 0);
            this.uiPnl_TblLay.Controls.Add(this.uiPnl_OperT_TxtBx, 0, 1);
            this.uiPnl_TblLay.Controls.Add(this.uiPnl_OperT_ComBx, 1, 1);
            this.uiPnl_TblLay.Controls.Add(this.uiPnl_PointIdx_Num, 1, 2);
            this.uiPnl_TblLay.Controls.Add(this.uiPnl_PointIdx_TrBr, 1, 3);
            this.uiPnl_TblLay.Controls.Add(this.uiPnl_SaveExit_Btn, 1, 7);
            this.uiPnl_TblLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_TblLay.Location = new System.Drawing.Point(0, 0);
            this.uiPnl_TblLay.Name = "uiPnl_TblLay";
            this.uiPnl_TblLay.RowCount = 11;
            this.uiPnl_TblLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TblLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TblLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TblLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TblLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TblLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TblLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TblLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TblLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TblLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TblLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TblLay.Size = new System.Drawing.Size(260, 371);
            this.uiPnl_TblLay.TabIndex = 0;
            // 
            // uiPnl_Perform_Btn
            // 
            this.uiPnl_Perform_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_Perform_Btn.Location = new System.Drawing.Point(133, 153);
            this.uiPnl_Perform_Btn.Name = "uiPnl_Perform_Btn";
            this.uiPnl_Perform_Btn.Size = new System.Drawing.Size(124, 19);
            this.uiPnl_Perform_Btn.TabIndex = 1;
            this.uiPnl_Perform_Btn.Text = "Perform";
            this.uiPnl_Perform_Btn.UseVisualStyleBackColor = true;
            this.uiPnl_Perform_Btn.Click += new System.EventHandler(this.UiPanel_Perform_Click);
            // 
            // uiPnl_Value2_TxtBx
            // 
            this.uiPnl_Value2_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiPnl_Value2_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_Value2_TxtBx.Location = new System.Drawing.Point(133, 103);
            this.uiPnl_Value2_TxtBx.Name = "uiPnl_Value2_TxtBx";
            this.uiPnl_Value2_TxtBx.Size = new System.Drawing.Size(124, 20);
            this.uiPnl_Value2_TxtBx.TabIndex = 2;
            // 
            // uiPnl_Value1_TxtBx
            // 
            this.uiPnl_Value1_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiPnl_Value1_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_Value1_TxtBx.Location = new System.Drawing.Point(3, 103);
            this.uiPnl_Value1_TxtBx.Name = "uiPnl_Value1_TxtBx";
            this.uiPnl_Value1_TxtBx.ReadOnly = true;
            this.uiPnl_Value1_TxtBx.Size = new System.Drawing.Size(124, 20);
            this.uiPnl_Value1_TxtBx.TabIndex = 3;
            this.uiPnl_Value1_TxtBx.Text = "Value:";
            // 
            // uiPnl_PointIdx_TxtBx
            // 
            this.uiPnl_PointIdx_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiPnl_PointIdx_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_PointIdx_TxtBx.Location = new System.Drawing.Point(3, 53);
            this.uiPnl_PointIdx_TxtBx.Name = "uiPnl_PointIdx_TxtBx";
            this.uiPnl_PointIdx_TxtBx.ReadOnly = true;
            this.uiPnl_PointIdx_TxtBx.Size = new System.Drawing.Size(124, 20);
            this.uiPnl_PointIdx_TxtBx.TabIndex = 6;
            this.uiPnl_PointIdx_TxtBx.Text = "Point index:";
            // 
            // uiPnl_Edit_TxtBx
            // 
            this.uiPnl_TblLay.SetColumnSpan(this.uiPnl_Edit_TxtBx, 2);
            this.uiPnl_Edit_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_Edit_TxtBx.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.uiPnl_Edit_TxtBx.Location = new System.Drawing.Point(3, 3);
            this.uiPnl_Edit_TxtBx.Name = "uiPnl_Edit_TxtBx";
            this.uiPnl_Edit_TxtBx.ReadOnly = true;
            this.uiPnl_Edit_TxtBx.Size = new System.Drawing.Size(254, 20);
            this.uiPnl_Edit_TxtBx.TabIndex = 9;
            this.uiPnl_Edit_TxtBx.Text = "Edit control for ordinates";
            // 
            // uiPnl_OperT_TxtBx
            // 
            this.uiPnl_OperT_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiPnl_OperT_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_OperT_TxtBx.Location = new System.Drawing.Point(3, 28);
            this.uiPnl_OperT_TxtBx.Name = "uiPnl_OperT_TxtBx";
            this.uiPnl_OperT_TxtBx.ReadOnly = true;
            this.uiPnl_OperT_TxtBx.Size = new System.Drawing.Size(124, 20);
            this.uiPnl_OperT_TxtBx.TabIndex = 8;
            this.uiPnl_OperT_TxtBx.Text = "Operation type:";
            // 
            // uiPnl_OperT_ComBx
            // 
            this.uiPnl_OperT_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_OperT_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiPnl_OperT_ComBx.FormattingEnabled = true;
            this.uiPnl_OperT_ComBx.Items.AddRange(new object[] {
            "Overriding",
            "Addition",
            "Substraction",
            "Multiplication",
            "Division",
            "Exponentiation",
            "Logarithmic",
            "Rooting",
            "Constant",
            "Positive",
            "Negative"});
            this.uiPnl_OperT_ComBx.Location = new System.Drawing.Point(133, 28);
            this.uiPnl_OperT_ComBx.Name = "uiPnl_OperT_ComBx";
            this.uiPnl_OperT_ComBx.Size = new System.Drawing.Size(124, 21);
            this.uiPnl_OperT_ComBx.TabIndex = 7;
            this.uiPnl_OperT_ComBx.SelectedIndexChanged += new System.EventHandler(this.UiPanel_OperationType_SelectedIndexChanged);
            // 
            // uiPnl_PointIdx_Num
            // 
            this.uiPnl_PointIdx_Num.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_PointIdx_Num.Enabled = false;
            this.uiPnl_PointIdx_Num.Location = new System.Drawing.Point(133, 53);
            this.uiPnl_PointIdx_Num.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uiPnl_PointIdx_Num.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uiPnl_PointIdx_Num.Name = "uiPnl_PointIdx_Num";
            this.uiPnl_PointIdx_Num.Size = new System.Drawing.Size(124, 20);
            this.uiPnl_PointIdx_Num.TabIndex = 5;
            this.uiPnl_PointIdx_Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uiPnl_PointIdx_Num.ThousandsSeparator = true;
            this.uiPnl_PointIdx_Num.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uiPnl_PointIdx_Num.ValueChanged += new System.EventHandler(this.UiPanel_PointIndex_NumericUpDown_ValueChanged);
            // 
            // uiPnl_PointIdx_TrBr
            // 
            this.uiPnl_PointIdx_TrBr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_PointIdx_TrBr.Enabled = false;
            this.uiPnl_PointIdx_TrBr.Location = new System.Drawing.Point(133, 78);
            this.uiPnl_PointIdx_TrBr.Maximum = 1;
            this.uiPnl_PointIdx_TrBr.Minimum = 1;
            this.uiPnl_PointIdx_TrBr.Name = "uiPnl_PointIdx_TrBr";
            this.uiPnl_PointIdx_TrBr.Size = new System.Drawing.Size(124, 19);
            this.uiPnl_PointIdx_TrBr.TabIndex = 4;
            this.uiPnl_PointIdx_TrBr.TickStyle = System.Windows.Forms.TickStyle.None;
            this.uiPnl_PointIdx_TrBr.Value = 1;
            this.uiPnl_PointIdx_TrBr.Scroll += new System.EventHandler(this.UiPanel_PointIndex_TrackBar_Scroll);
            // 
            // uiPnl_SaveExit_Btn
            // 
            this.uiPnl_SaveExit_Btn.BackColor = System.Drawing.SystemColors.Control;
            this.uiPnl_SaveExit_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiPnl_SaveExit_Btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uiPnl_SaveExit_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_SaveExit_Btn.Location = new System.Drawing.Point(133, 178);
            this.uiPnl_SaveExit_Btn.Name = "uiPnl_SaveExit_Btn";
            this.uiPnl_SaveExit_Btn.Size = new System.Drawing.Size(124, 19);
            this.uiPnl_SaveExit_Btn.TabIndex = 0;
            this.uiPnl_SaveExit_Btn.Text = "Save and exit";
            this.uiPnl_SaveExit_Btn.UseVisualStyleBackColor = false;
            // 
            // uiGrid_TblLay
            // 
            this.uiGrid_TblLay.AutoScroll = true;
            this.uiGrid_TblLay.BackColor = System.Drawing.SystemColors.ControlLight;
            this.uiGrid_TblLay.ColumnCount = 3;
            this.uiGrid_TblLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.45561F));
            this.uiGrid_TblLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.27219F));
            this.uiGrid_TblLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.2722F));
            this.uiGrid_TblLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_TblLay.Location = new System.Drawing.Point(0, 0);
            this.uiGrid_TblLay.Name = "uiGrid_TblLay";
            this.uiGrid_TblLay.RowCount = 2;
            this.uiGrid_TblLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiGrid_TblLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiGrid_TblLay.Size = new System.Drawing.Size(516, 371);
            this.uiGrid_TblLay.TabIndex = 0;
            // 
            // DatasetViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 371);
            this.Controls.Add(this.ui_SpCtn);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "DatasetViewer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dataset Viewer";
            this.ui_SpCtn.Panel1.ResumeLayout(false);
            this.ui_SpCtn.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ui_SpCtn)).EndInit();
            this.ui_SpCtn.ResumeLayout(false);
            this.uiPnl_TblLay.ResumeLayout(false);
            this.uiPnl_TblLay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiPnl_PointIdx_Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPnl_PointIdx_TrBr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer ui_SpCtn;
        private System.Windows.Forms.TableLayoutPanel uiPnl_TblLay;
        private System.Windows.Forms.TableLayoutPanel uiGrid_TblLay;
        private System.Windows.Forms.TextBox uiPnl_Edit_TxtBx;
        private System.Windows.Forms.TextBox uiPnl_OperT_TxtBx;
        private System.Windows.Forms.TextBox uiPnl_PointIdx_TxtBx;
        private System.Windows.Forms.TextBox uiPnl_Value1_TxtBx;
        private System.Windows.Forms.ComboBox uiPnl_OperT_ComBx;
        private System.Windows.Forms.NumericUpDown uiPnl_PointIdx_Num;
        private System.Windows.Forms.TrackBar uiPnl_PointIdx_TrBr;
        private System.Windows.Forms.TextBox uiPnl_Value2_TxtBx;
        private System.Windows.Forms.Button uiPnl_Perform_Btn;
        private System.Windows.Forms.Button uiPnl_SaveExit_Btn;
    }
}