namespace PI
{
    partial class GridPreviewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ui_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiGrid_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiGrid_DtSet_TxtBx = new System.Windows.Forms.TextBox();
            this.uiGrid_db_grid = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiPnl_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiPnl_Save_Btn = new System.Windows.Forms.Button();
            this.uiPnl_Reset_Btn = new System.Windows.Forms.Button();
            this.uiPnl_EndIdx_Num = new System.Windows.Forms.NumericUpDown();
            this.uiPnl_Val1_TxtBx = new System.Windows.Forms.TextBox();
            this.uiPnl_EndIdx_TxtBx = new System.Windows.Forms.TextBox();
            this.uiPnl_StartIdx_TxtBx = new System.Windows.Forms.TextBox();
            this.uiPnl_OperT_ComBx = new System.Windows.Forms.ComboBox();
            this.uiPnl_OperT_TxtBx = new System.Windows.Forms.TextBox();
            this.uiPnl_Edit_TxtBx = new System.Windows.Forms.TextBox();
            this.uiPnl_DtGrid_TxtBx = new System.Windows.Forms.TextBox();
            this.uiPnl_AutoSize_TxtBx = new System.Windows.Forms.TextBox();
            this.uiPnl_AutoSize_ComBx = new System.Windows.Forms.ComboBox();
            this.uiPnl_StartIdx_Num = new System.Windows.Forms.NumericUpDown();
            this.uiPnl_Perform_Btn = new System.Windows.Forms.Button();
            this.uiPnl_Val2_TxtBx = new System.Windows.Forms.TextBox();
            this.uiChart_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiChart_Info_TxtBx = new System.Windows.Forms.TextBox();
            this.uiChart_Prv_TxtBx = new System.Windows.Forms.TextBox();
            this.uiChart_Prv = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.uiPnl_Refresh_Btn = new System.Windows.Forms.Button();
            this.uiPnl_Ok_Btn = new System.Windows.Forms.Button();
            this.ui_TbLay.SuspendLayout();
            this.uiGrid_TbLay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGrid_db_grid)).BeginInit();
            this.uiPnl_TbLay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiPnl_EndIdx_Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPnl_StartIdx_Num)).BeginInit();
            this.uiChart_TbLay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiChart_Prv)).BeginInit();
            this.SuspendLayout();
            // 
            // ui_TbLay
            // 
            this.ui_TbLay.ColumnCount = 3;
            this.ui_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.ui_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ui_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ui_TbLay.Controls.Add(this.uiGrid_TbLay, 1, 0);
            this.ui_TbLay.Controls.Add(this.uiPnl_TbLay, 0, 0);
            this.ui_TbLay.Controls.Add(this.uiChart_TbLay, 2, 0);
            this.ui_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_TbLay.Location = new System.Drawing.Point(0, 0);
            this.ui_TbLay.Name = "ui_TbLay";
            this.ui_TbLay.RowCount = 1;
            this.ui_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ui_TbLay.Size = new System.Drawing.Size(919, 464);
            this.ui_TbLay.TabIndex = 0;
            // 
            // uiGrid_TbLay
            // 
            this.uiGrid_TbLay.ColumnCount = 1;
            this.uiGrid_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiGrid_TbLay.Controls.Add(this.uiGrid_DtSet_TxtBx, 0, 0);
            this.uiGrid_TbLay.Controls.Add(this.uiGrid_db_grid, 0, 1);
            this.uiGrid_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_TbLay.Location = new System.Drawing.Point(233, 3);
            this.uiGrid_TbLay.Name = "uiGrid_TbLay";
            this.uiGrid_TbLay.RowCount = 2;
            this.uiGrid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiGrid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiGrid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.uiGrid_TbLay.Size = new System.Drawing.Size(338, 458);
            this.uiGrid_TbLay.TabIndex = 0;
            // 
            // uiGrid_DtSet_TxtBx
            // 
            this.uiGrid_DtSet_TxtBx.BackColor = System.Drawing.SystemColors.Control;
            this.uiGrid_DtSet_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_DtSet_TxtBx.Location = new System.Drawing.Point(3, 3);
            this.uiGrid_DtSet_TxtBx.Name = "uiGrid_DtSet_TxtBx";
            this.uiGrid_DtSet_TxtBx.ReadOnly = true;
            this.uiGrid_DtSet_TxtBx.Size = new System.Drawing.Size(332, 20);
            this.uiGrid_DtSet_TxtBx.TabIndex = 0;
            this.uiGrid_DtSet_TxtBx.Text = "Dataset";
            // 
            // uiGrid_db_grid
            // 
            this.uiGrid_db_grid.AllowUserToAddRows = false;
            this.uiGrid_db_grid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
            this.uiGrid_db_grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.uiGrid_db_grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.uiGrid_db_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uiGrid_db_grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.x,
            this.y});
            this.uiGrid_db_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_db_grid.Location = new System.Drawing.Point(3, 28);
            this.uiGrid_db_grid.Name = "uiGrid_db_grid";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiGrid_db_grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.White;
            this.uiGrid_db_grid.RowsDefaultCellStyle = dataGridViewCellStyle21;
            this.uiGrid_db_grid.Size = new System.Drawing.Size(332, 427);
            this.uiGrid_db_grid.TabIndex = 1;
            // 
            // Index
            // 
            this.Index.HeaderText = "Index";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            // 
            // x
            // 
            this.x.HeaderText = "x";
            this.x.Name = "x";
            this.x.ReadOnly = true;
            // 
            // y
            // 
            this.y.HeaderText = "y";
            this.y.Name = "y";
            // 
            // uiPnl_TbLay
            // 
            this.uiPnl_TbLay.AutoScroll = true;
            this.uiPnl_TbLay.ColumnCount = 2;
            this.uiPnl_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiPnl_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_Ok_Btn, 1, 13);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_Refresh_Btn, 0, 11);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_Save_Btn, 1, 11);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_Reset_Btn, 0, 10);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_EndIdx_Num, 1, 7);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_Val1_TxtBx, 0, 8);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_EndIdx_TxtBx, 0, 7);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_StartIdx_TxtBx, 0, 6);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_OperT_ComBx, 1, 5);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_OperT_TxtBx, 0, 5);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_Edit_TxtBx, 0, 4);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_DtGrid_TxtBx, 0, 0);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_AutoSize_TxtBx, 0, 1);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_AutoSize_ComBx, 1, 1);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_StartIdx_Num, 1, 6);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_Perform_Btn, 1, 10);
            this.uiPnl_TbLay.Controls.Add(this.uiPnl_Val2_TxtBx, 1, 8);
            this.uiPnl_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_TbLay.Location = new System.Drawing.Point(3, 3);
            this.uiPnl_TbLay.Name = "uiPnl_TbLay";
            this.uiPnl_TbLay.RowCount = 16;
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiPnl_TbLay.Size = new System.Drawing.Size(224, 458);
            this.uiPnl_TbLay.TabIndex = 1;
            // 
            // uiPnl_Save_Btn
            // 
            this.uiPnl_Save_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_Save_Btn.Location = new System.Drawing.Point(115, 278);
            this.uiPnl_Save_Btn.Name = "uiPnl_Save_Btn";
            this.uiPnl_Save_Btn.Size = new System.Drawing.Size(106, 19);
            this.uiPnl_Save_Btn.TabIndex = 1;
            this.uiPnl_Save_Btn.Text = "Save";
            this.uiPnl_Save_Btn.UseVisualStyleBackColor = true;
            this.uiPnl_Save_Btn.Click += new System.EventHandler(this.UiPanel_Save_Click);
            // 
            // uiPnl_Reset_Btn
            // 
            this.uiPnl_Reset_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_Reset_Btn.Location = new System.Drawing.Point(3, 253);
            this.uiPnl_Reset_Btn.Name = "uiPnl_Reset_Btn";
            this.uiPnl_Reset_Btn.Size = new System.Drawing.Size(106, 19);
            this.uiPnl_Reset_Btn.TabIndex = 2;
            this.uiPnl_Reset_Btn.Text = "Reset";
            this.uiPnl_Reset_Btn.UseVisualStyleBackColor = true;
            // 
            // uiPnl_EndIdx_Num
            // 
            this.uiPnl_EndIdx_Num.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_EndIdx_Num.Location = new System.Drawing.Point(115, 178);
            this.uiPnl_EndIdx_Num.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.uiPnl_EndIdx_Num.Name = "uiPnl_EndIdx_Num";
            this.uiPnl_EndIdx_Num.Size = new System.Drawing.Size(106, 20);
            this.uiPnl_EndIdx_Num.TabIndex = 12;
            this.uiPnl_EndIdx_Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uiPnl_EndIdx_Num.ThousandsSeparator = true;
            this.uiPnl_EndIdx_Num.ValueChanged += new System.EventHandler(this.UiPanel_EndIndex_ValueChanged);
            // 
            // uiPnl_Val1_TxtBx
            // 
            this.uiPnl_Val1_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiPnl_Val1_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_Val1_TxtBx.Location = new System.Drawing.Point(3, 203);
            this.uiPnl_Val1_TxtBx.Name = "uiPnl_Val1_TxtBx";
            this.uiPnl_Val1_TxtBx.ReadOnly = true;
            this.uiPnl_Val1_TxtBx.Size = new System.Drawing.Size(106, 20);
            this.uiPnl_Val1_TxtBx.TabIndex = 13;
            this.uiPnl_Val1_TxtBx.Text = "Value:";
            // 
            // uiPnl_EndIdx_TxtBx
            // 
            this.uiPnl_EndIdx_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiPnl_EndIdx_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_EndIdx_TxtBx.Location = new System.Drawing.Point(3, 178);
            this.uiPnl_EndIdx_TxtBx.Name = "uiPnl_EndIdx_TxtBx";
            this.uiPnl_EndIdx_TxtBx.ReadOnly = true;
            this.uiPnl_EndIdx_TxtBx.Size = new System.Drawing.Size(106, 20);
            this.uiPnl_EndIdx_TxtBx.TabIndex = 11;
            this.uiPnl_EndIdx_TxtBx.Text = "End index:";
            // 
            // uiPnl_StartIdx_TxtBx
            // 
            this.uiPnl_StartIdx_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiPnl_StartIdx_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_StartIdx_TxtBx.Location = new System.Drawing.Point(3, 153);
            this.uiPnl_StartIdx_TxtBx.Name = "uiPnl_StartIdx_TxtBx";
            this.uiPnl_StartIdx_TxtBx.ReadOnly = true;
            this.uiPnl_StartIdx_TxtBx.Size = new System.Drawing.Size(106, 20);
            this.uiPnl_StartIdx_TxtBx.TabIndex = 9;
            this.uiPnl_StartIdx_TxtBx.Text = "Start index:";
            // 
            // uiPnl_OperT_ComBx
            // 
            this.uiPnl_OperT_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_OperT_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiPnl_OperT_ComBx.FormattingEnabled = true;
            this.uiPnl_OperT_ComBx.Items.AddRange(new object[] {
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
            this.uiPnl_OperT_ComBx.Location = new System.Drawing.Point(115, 128);
            this.uiPnl_OperT_ComBx.Name = "uiPnl_OperT_ComBx";
            this.uiPnl_OperT_ComBx.Size = new System.Drawing.Size(106, 21);
            this.uiPnl_OperT_ComBx.TabIndex = 8;
            this.uiPnl_OperT_ComBx.SelectedIndexChanged += new System.EventHandler(this.UiPanel_OperationType_SelectedIndexChanged);
            // 
            // uiPnl_OperT_TxtBx
            // 
            this.uiPnl_OperT_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiPnl_OperT_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_OperT_TxtBx.Location = new System.Drawing.Point(3, 128);
            this.uiPnl_OperT_TxtBx.Name = "uiPnl_OperT_TxtBx";
            this.uiPnl_OperT_TxtBx.ReadOnly = true;
            this.uiPnl_OperT_TxtBx.Size = new System.Drawing.Size(106, 20);
            this.uiPnl_OperT_TxtBx.TabIndex = 7;
            this.uiPnl_OperT_TxtBx.Text = "Operation type:";
            // 
            // uiPnl_Edit_TxtBx
            // 
            this.uiPnl_TbLay.SetColumnSpan(this.uiPnl_Edit_TxtBx, 2);
            this.uiPnl_Edit_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_Edit_TxtBx.Location = new System.Drawing.Point(3, 103);
            this.uiPnl_Edit_TxtBx.Name = "uiPnl_Edit_TxtBx";
            this.uiPnl_Edit_TxtBx.ReadOnly = true;
            this.uiPnl_Edit_TxtBx.Size = new System.Drawing.Size(218, 20);
            this.uiPnl_Edit_TxtBx.TabIndex = 6;
            this.uiPnl_Edit_TxtBx.Text = "Fast edit";
            // 
            // uiPnl_DtGrid_TxtBx
            // 
            this.uiPnl_TbLay.SetColumnSpan(this.uiPnl_DtGrid_TxtBx, 2);
            this.uiPnl_DtGrid_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_DtGrid_TxtBx.Location = new System.Drawing.Point(3, 3);
            this.uiPnl_DtGrid_TxtBx.Name = "uiPnl_DtGrid_TxtBx";
            this.uiPnl_DtGrid_TxtBx.ReadOnly = true;
            this.uiPnl_DtGrid_TxtBx.Size = new System.Drawing.Size(218, 20);
            this.uiPnl_DtGrid_TxtBx.TabIndex = 3;
            this.uiPnl_DtGrid_TxtBx.Text = "Dataset grid";
            // 
            // uiPnl_AutoSize_TxtBx
            // 
            this.uiPnl_AutoSize_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiPnl_AutoSize_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_AutoSize_TxtBx.Location = new System.Drawing.Point(3, 28);
            this.uiPnl_AutoSize_TxtBx.Name = "uiPnl_AutoSize_TxtBx";
            this.uiPnl_AutoSize_TxtBx.ReadOnly = true;
            this.uiPnl_AutoSize_TxtBx.Size = new System.Drawing.Size(106, 20);
            this.uiPnl_AutoSize_TxtBx.TabIndex = 4;
            this.uiPnl_AutoSize_TxtBx.Text = "AutoSizeColumnsMode:";
            // 
            // uiPnl_AutoSize_ComBx
            // 
            this.uiPnl_AutoSize_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_AutoSize_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiPnl_AutoSize_ComBx.FormattingEnabled = true;
            this.uiPnl_AutoSize_ComBx.Items.AddRange(new object[] {
            "None",
            "AllCells",
            "DisplayedCells",
            "Fill"});
            this.uiPnl_AutoSize_ComBx.Location = new System.Drawing.Point(115, 28);
            this.uiPnl_AutoSize_ComBx.Name = "uiPnl_AutoSize_ComBx";
            this.uiPnl_AutoSize_ComBx.Size = new System.Drawing.Size(106, 21);
            this.uiPnl_AutoSize_ComBx.TabIndex = 5;
            this.uiPnl_AutoSize_ComBx.SelectedIndexChanged += new System.EventHandler(this.UiPanel_AutoSizeColumnsMode_SelectedIndexChanged);
            // 
            // uiPnl_StartIdx_Num
            // 
            this.uiPnl_StartIdx_Num.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_StartIdx_Num.Location = new System.Drawing.Point(115, 153);
            this.uiPnl_StartIdx_Num.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.uiPnl_StartIdx_Num.Name = "uiPnl_StartIdx_Num";
            this.uiPnl_StartIdx_Num.Size = new System.Drawing.Size(106, 20);
            this.uiPnl_StartIdx_Num.TabIndex = 10;
            this.uiPnl_StartIdx_Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uiPnl_StartIdx_Num.ThousandsSeparator = true;
            this.uiPnl_StartIdx_Num.ValueChanged += new System.EventHandler(this.UiPanel_StartIndex_ValueChanged);
            // 
            // uiPnl_Perform_Btn
            // 
            this.uiPnl_Perform_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_Perform_Btn.Location = new System.Drawing.Point(115, 253);
            this.uiPnl_Perform_Btn.Name = "uiPnl_Perform_Btn";
            this.uiPnl_Perform_Btn.Size = new System.Drawing.Size(106, 19);
            this.uiPnl_Perform_Btn.TabIndex = 0;
            this.uiPnl_Perform_Btn.Text = "Perform";
            this.uiPnl_Perform_Btn.UseVisualStyleBackColor = true;
            this.uiPnl_Perform_Btn.Click += new System.EventHandler(this.UiPanel_Perform_Click);
            // 
            // uiPnl_Val2_TxtBx
            // 
            this.uiPnl_Val2_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_Val2_TxtBx.Enabled = false;
            this.uiPnl_Val2_TxtBx.Location = new System.Drawing.Point(115, 203);
            this.uiPnl_Val2_TxtBx.Name = "uiPnl_Val2_TxtBx";
            this.uiPnl_Val2_TxtBx.Size = new System.Drawing.Size(106, 20);
            this.uiPnl_Val2_TxtBx.TabIndex = 14;
            // 
            // uiChart_TbLay
            // 
            this.uiChart_TbLay.ColumnCount = 1;
            this.uiChart_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiChart_TbLay.Controls.Add(this.uiChart_Info_TxtBx, 0, 1);
            this.uiChart_TbLay.Controls.Add(this.uiChart_Prv_TxtBx, 0, 0);
            this.uiChart_TbLay.Controls.Add(this.uiChart_Prv, 0, 2);
            this.uiChart_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiChart_TbLay.Location = new System.Drawing.Point(577, 3);
            this.uiChart_TbLay.Name = "uiChart_TbLay";
            this.uiChart_TbLay.RowCount = 3;
            this.uiChart_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiChart_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.uiChart_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiChart_TbLay.Size = new System.Drawing.Size(339, 458);
            this.uiChart_TbLay.TabIndex = 2;
            // 
            // uiChart_Info_TxtBx
            // 
            this.uiChart_Info_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiChart_Info_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiChart_Info_TxtBx.Location = new System.Drawing.Point(3, 28);
            this.uiChart_Info_TxtBx.Name = "uiChart_Info_TxtBx";
            this.uiChart_Info_TxtBx.ReadOnly = true;
            this.uiChart_Info_TxtBx.Size = new System.Drawing.Size(333, 20);
            this.uiChart_Info_TxtBx.TabIndex = 1;
            this.uiChart_Info_TxtBx.Text = "Info";
            this.uiChart_Info_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // uiChart_Prv_TxtBx
            // 
            this.uiChart_Prv_TxtBx.BackColor = System.Drawing.SystemColors.Control;
            this.uiChart_Prv_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiChart_Prv_TxtBx.Location = new System.Drawing.Point(3, 3);
            this.uiChart_Prv_TxtBx.Name = "uiChart_Prv_TxtBx";
            this.uiChart_Prv_TxtBx.ReadOnly = true;
            this.uiChart_Prv_TxtBx.Size = new System.Drawing.Size(333, 20);
            this.uiChart_Prv_TxtBx.TabIndex = 0;
            this.uiChart_Prv_TxtBx.Text = "Preview";
            // 
            // uiChart_Prv
            // 
            chartArea7.Name = "ChartArea1";
            this.uiChart_Prv.ChartAreas.Add(chartArea7);
            this.uiChart_Prv.Dock = System.Windows.Forms.DockStyle.Fill;
            legend7.Name = "Legend1";
            this.uiChart_Prv.Legends.Add(legend7);
            this.uiChart_Prv.Location = new System.Drawing.Point(3, 53);
            this.uiChart_Prv.Name = "uiChart_Prv";
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            this.uiChart_Prv.Series.Add(series7);
            this.uiChart_Prv.Size = new System.Drawing.Size(333, 402);
            this.uiChart_Prv.TabIndex = 2;
            this.uiChart_Prv.Text = "chart1";
            // 
            // uiPnl_Refresh_Btn
            // 
            this.uiPnl_Refresh_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_Refresh_Btn.Location = new System.Drawing.Point(3, 278);
            this.uiPnl_Refresh_Btn.Name = "uiPnl_Refresh_Btn";
            this.uiPnl_Refresh_Btn.Size = new System.Drawing.Size(106, 19);
            this.uiPnl_Refresh_Btn.TabIndex = 15;
            this.uiPnl_Refresh_Btn.Text = "Refresh";
            this.uiPnl_Refresh_Btn.UseVisualStyleBackColor = true;
            // 
            // uiPnl_Ok_Btn
            // 
            this.uiPnl_Ok_Btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uiPnl_Ok_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPnl_Ok_Btn.ForeColor = System.Drawing.Color.Red;
            this.uiPnl_Ok_Btn.Location = new System.Drawing.Point(115, 328);
            this.uiPnl_Ok_Btn.Name = "uiPnl_Ok_Btn";
            this.uiPnl_Ok_Btn.Size = new System.Drawing.Size(106, 19);
            this.uiPnl_Ok_Btn.TabIndex = 16;
            this.uiPnl_Ok_Btn.Text = "OK";
            this.uiPnl_Ok_Btn.UseVisualStyleBackColor = true;
            // 
            // GridPreviewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 464);
            this.Controls.Add(this.ui_TbLay);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "GridPreviewer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Grid Previewer";
            this.ui_TbLay.ResumeLayout(false);
            this.uiGrid_TbLay.ResumeLayout(false);
            this.uiGrid_TbLay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGrid_db_grid)).EndInit();
            this.uiPnl_TbLay.ResumeLayout(false);
            this.uiPnl_TbLay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiPnl_EndIdx_Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPnl_StartIdx_Num)).EndInit();
            this.uiChart_TbLay.ResumeLayout(false);
            this.uiChart_TbLay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiChart_Prv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ui_TbLay;
        private System.Windows.Forms.TableLayoutPanel uiGrid_TbLay;
        private System.Windows.Forms.TextBox uiGrid_DtSet_TxtBx;
        private System.Windows.Forms.DataGridView uiGrid_db_grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn x;
        private System.Windows.Forms.DataGridViewTextBoxColumn y;
        private System.Windows.Forms.TableLayoutPanel uiPnl_TbLay;
        private System.Windows.Forms.TextBox uiPnl_DtGrid_TxtBx;
        private System.Windows.Forms.TextBox uiPnl_AutoSize_TxtBx;
        private System.Windows.Forms.ComboBox uiPnl_AutoSize_ComBx;
        private System.Windows.Forms.TextBox uiPnl_Edit_TxtBx;
        private System.Windows.Forms.ComboBox uiPnl_OperT_ComBx;
        private System.Windows.Forms.TextBox uiPnl_OperT_TxtBx;
        private System.Windows.Forms.TextBox uiPnl_Val1_TxtBx;
        private System.Windows.Forms.TextBox uiPnl_EndIdx_TxtBx;
        private System.Windows.Forms.TextBox uiPnl_StartIdx_TxtBx;
        private System.Windows.Forms.NumericUpDown uiPnl_StartIdx_Num;
        private System.Windows.Forms.NumericUpDown uiPnl_EndIdx_Num;
        private System.Windows.Forms.Button uiPnl_Save_Btn;
        private System.Windows.Forms.Button uiPnl_Reset_Btn;
        private System.Windows.Forms.Button uiPnl_Perform_Btn;
        private System.Windows.Forms.TextBox uiPnl_Val2_TxtBx;
        private System.Windows.Forms.TableLayoutPanel uiChart_TbLay;
        private System.Windows.Forms.TextBox uiChart_Info_TxtBx;
        private System.Windows.Forms.TextBox uiChart_Prv_TxtBx;
        private System.Windows.Forms.DataVisualization.Charting.Chart uiChart_Prv;
        private System.Windows.Forms.Button uiPnl_Refresh_Btn;
        private System.Windows.Forms.Button uiPnl_Ok_Btn;
    }
}