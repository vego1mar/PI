namespace PI
{
    partial class StatAnalysis
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ui_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiR_Prv_TxtBx = new System.Windows.Forms.TextBox();
            this.uiL_StdDev_TxtBx = new System.Windows.Forms.TextBox();
            this.uiL_TbCtrl = new System.Windows.Forms.TabControl();
            this.uiL_Peek_TbPg = new System.Windows.Forms.TabPage();
            this.uiL_Deform_TbPg = new System.Windows.Forms.TabPage();
            this.uiR_TbCtrl = new System.Windows.Forms.TabControl();
            this.uiR_Chart_TbPg = new System.Windows.Forms.TabPage();
            this.uiR_Formula_TbPg = new System.Windows.Forms.TabPage();
            this.uiLPeek_Grid = new System.Windows.Forms.DataGridView();
            this.uiLPeekGrid_Noise01_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLPeekGrid_Noise05_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLPeekGrid_Noise1_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLPeekGrid_Noise2_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLDeform_Grid = new System.Windows.Forms.DataGridView();
            this.uiLDeformGrid_Noise01_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLDeformGrid_Noise05_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLDeformGrid_Noise1_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLDeformGrid_Noise2_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiRChart_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiRChart_Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.uiRChart_Down_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiRChartDown_CrvT_ComBx = new System.Windows.Forms.ComboBox();
            this.uiRChartDown_CrvIdx_Num = new System.Windows.Forms.NumericUpDown();
            this.uiRFormula_PicBx = new System.Windows.Forms.PictureBox();
            this.uiRChartDown_DtSet_Btn = new System.Windows.Forms.Button();
            this.uiRChartDown_Phen_ComBx = new System.Windows.Forms.ComboBox();
            this.uiRChartDown_Surr_ComBx = new System.Windows.Forms.ComboBox();
            this.uiRChartDown_MeanT_ComBx = new System.Windows.Forms.ComboBox();
            this.ui_TbLay.SuspendLayout();
            this.uiL_TbCtrl.SuspendLayout();
            this.uiL_Peek_TbPg.SuspendLayout();
            this.uiL_Deform_TbPg.SuspendLayout();
            this.uiR_TbCtrl.SuspendLayout();
            this.uiR_Chart_TbPg.SuspendLayout();
            this.uiR_Formula_TbPg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiLPeek_Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiLDeform_Grid)).BeginInit();
            this.uiRChart_TbLay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiRChart_Chart)).BeginInit();
            this.uiRChart_Down_TbLay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiRChartDown_CrvIdx_Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiRFormula_PicBx)).BeginInit();
            this.SuspendLayout();
            // 
            // ui_TbLay
            // 
            this.ui_TbLay.ColumnCount = 2;
            this.ui_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ui_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ui_TbLay.Controls.Add(this.uiL_StdDev_TxtBx, 0, 0);
            this.ui_TbLay.Controls.Add(this.uiR_Prv_TxtBx, 1, 0);
            this.ui_TbLay.Controls.Add(this.uiL_TbCtrl, 0, 1);
            this.ui_TbLay.Controls.Add(this.uiR_TbCtrl, 1, 1);
            this.ui_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_TbLay.Location = new System.Drawing.Point(0, 0);
            this.ui_TbLay.Name = "ui_TbLay";
            this.ui_TbLay.RowCount = 2;
            this.ui_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ui_TbLay.Size = new System.Drawing.Size(1069, 585);
            this.ui_TbLay.TabIndex = 0;
            // 
            // uiR_Prv_TxtBx
            // 
            this.uiR_Prv_TxtBx.BackColor = System.Drawing.Color.White;
            this.uiR_Prv_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiR_Prv_TxtBx.Location = new System.Drawing.Point(537, 3);
            this.uiR_Prv_TxtBx.Name = "uiR_Prv_TxtBx";
            this.uiR_Prv_TxtBx.ReadOnly = true;
            this.uiR_Prv_TxtBx.Size = new System.Drawing.Size(529, 20);
            this.uiR_Prv_TxtBx.TabIndex = 21;
            this.uiR_Prv_TxtBx.Text = "Preview";
            // 
            // uiL_StdDev_TxtBx
            // 
            this.uiL_StdDev_TxtBx.BackColor = System.Drawing.Color.White;
            this.uiL_StdDev_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiL_StdDev_TxtBx.Location = new System.Drawing.Point(3, 3);
            this.uiL_StdDev_TxtBx.Name = "uiL_StdDev_TxtBx";
            this.uiL_StdDev_TxtBx.ReadOnly = true;
            this.uiL_StdDev_TxtBx.Size = new System.Drawing.Size(528, 20);
            this.uiL_StdDev_TxtBx.TabIndex = 22;
            this.uiL_StdDev_TxtBx.Text = "Standard deviation";
            // 
            // uiL_TbCtrl
            // 
            this.uiL_TbCtrl.Controls.Add(this.uiL_Peek_TbPg);
            this.uiL_TbCtrl.Controls.Add(this.uiL_Deform_TbPg);
            this.uiL_TbCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiL_TbCtrl.Location = new System.Drawing.Point(3, 28);
            this.uiL_TbCtrl.Name = "uiL_TbCtrl";
            this.uiL_TbCtrl.SelectedIndex = 0;
            this.uiL_TbCtrl.Size = new System.Drawing.Size(528, 554);
            this.uiL_TbCtrl.TabIndex = 23;
            // 
            // uiL_Peek_TbPg
            // 
            this.uiL_Peek_TbPg.Controls.Add(this.uiLPeek_Grid);
            this.uiL_Peek_TbPg.Location = new System.Drawing.Point(4, 22);
            this.uiL_Peek_TbPg.Name = "uiL_Peek_TbPg";
            this.uiL_Peek_TbPg.Padding = new System.Windows.Forms.Padding(3);
            this.uiL_Peek_TbPg.Size = new System.Drawing.Size(520, 528);
            this.uiL_Peek_TbPg.TabIndex = 0;
            this.uiL_Peek_TbPg.Text = "Peek";
            this.uiL_Peek_TbPg.UseVisualStyleBackColor = true;
            // 
            // uiL_Deform_TbPg
            // 
            this.uiL_Deform_TbPg.Controls.Add(this.uiLDeform_Grid);
            this.uiL_Deform_TbPg.Location = new System.Drawing.Point(4, 22);
            this.uiL_Deform_TbPg.Name = "uiL_Deform_TbPg";
            this.uiL_Deform_TbPg.Padding = new System.Windows.Forms.Padding(3);
            this.uiL_Deform_TbPg.Size = new System.Drawing.Size(520, 528);
            this.uiL_Deform_TbPg.TabIndex = 1;
            this.uiL_Deform_TbPg.Text = "Deformation";
            this.uiL_Deform_TbPg.UseVisualStyleBackColor = true;
            // 
            // uiR_TbCtrl
            // 
            this.uiR_TbCtrl.Controls.Add(this.uiR_Chart_TbPg);
            this.uiR_TbCtrl.Controls.Add(this.uiR_Formula_TbPg);
            this.uiR_TbCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiR_TbCtrl.Location = new System.Drawing.Point(537, 28);
            this.uiR_TbCtrl.Name = "uiR_TbCtrl";
            this.uiR_TbCtrl.SelectedIndex = 0;
            this.uiR_TbCtrl.Size = new System.Drawing.Size(529, 554);
            this.uiR_TbCtrl.TabIndex = 24;
            // 
            // uiR_Chart_TbPg
            // 
            this.uiR_Chart_TbPg.Controls.Add(this.uiRChart_TbLay);
            this.uiR_Chart_TbPg.Location = new System.Drawing.Point(4, 22);
            this.uiR_Chart_TbPg.Name = "uiR_Chart_TbPg";
            this.uiR_Chart_TbPg.Padding = new System.Windows.Forms.Padding(3);
            this.uiR_Chart_TbPg.Size = new System.Drawing.Size(521, 528);
            this.uiR_Chart_TbPg.TabIndex = 0;
            this.uiR_Chart_TbPg.Text = "Chart";
            this.uiR_Chart_TbPg.UseVisualStyleBackColor = true;
            // 
            // uiR_Formula_TbPg
            // 
            this.uiR_Formula_TbPg.Controls.Add(this.uiRFormula_PicBx);
            this.uiR_Formula_TbPg.Location = new System.Drawing.Point(4, 22);
            this.uiR_Formula_TbPg.Name = "uiR_Formula_TbPg";
            this.uiR_Formula_TbPg.Padding = new System.Windows.Forms.Padding(3);
            this.uiR_Formula_TbPg.Size = new System.Drawing.Size(521, 528);
            this.uiR_Formula_TbPg.TabIndex = 1;
            this.uiR_Formula_TbPg.Text = "Formula";
            this.uiR_Formula_TbPg.UseVisualStyleBackColor = true;
            // 
            // uiLPeek_Grid
            // 
            this.uiLPeek_Grid.AllowUserToAddRows = false;
            this.uiLPeek_Grid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            this.uiLPeek_Grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.uiLPeek_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.uiLPeek_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uiLPeek_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uiLPeekGrid_Noise01_Col,
            this.uiLPeekGrid_Noise05_Col,
            this.uiLPeekGrid_Noise1_Col,
            this.uiLPeekGrid_Noise2_Col});
            this.uiLPeek_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLPeek_Grid.Location = new System.Drawing.Point(3, 3);
            this.uiLPeek_Grid.Name = "uiLPeek_Grid";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiLPeek_Grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiLPeek_Grid.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.uiLPeek_Grid.Size = new System.Drawing.Size(514, 522);
            this.uiLPeek_Grid.TabIndex = 0;
            // 
            // uiLPeekGrid_Noise01_Col
            // 
            this.uiLPeekGrid_Noise01_Col.HeaderText = "Noise 0.1";
            this.uiLPeekGrid_Noise01_Col.Name = "uiLPeekGrid_Noise01_Col";
            this.uiLPeekGrid_Noise01_Col.ReadOnly = true;
            // 
            // uiLPeekGrid_Noise05_Col
            // 
            this.uiLPeekGrid_Noise05_Col.HeaderText = "Noise 0.5";
            this.uiLPeekGrid_Noise05_Col.Name = "uiLPeekGrid_Noise05_Col";
            this.uiLPeekGrid_Noise05_Col.ReadOnly = true;
            // 
            // uiLPeekGrid_Noise1_Col
            // 
            this.uiLPeekGrid_Noise1_Col.HeaderText = "Noise 1.0";
            this.uiLPeekGrid_Noise1_Col.Name = "uiLPeekGrid_Noise1_Col";
            this.uiLPeekGrid_Noise1_Col.ReadOnly = true;
            // 
            // uiLPeekGrid_Noise2_Col
            // 
            this.uiLPeekGrid_Noise2_Col.HeaderText = "Noise 2.0";
            this.uiLPeekGrid_Noise2_Col.Name = "uiLPeekGrid_Noise2_Col";
            this.uiLPeekGrid_Noise2_Col.ReadOnly = true;
            // 
            // uiLDeform_Grid
            // 
            this.uiLDeform_Grid.AllowUserToAddRows = false;
            this.uiLDeform_Grid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            this.uiLDeform_Grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.uiLDeform_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.uiLDeform_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uiLDeform_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uiLDeformGrid_Noise01_Col,
            this.uiLDeformGrid_Noise05_Col,
            this.uiLDeformGrid_Noise1_Col,
            this.uiLDeformGrid_Noise2_Col});
            this.uiLDeform_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLDeform_Grid.Location = new System.Drawing.Point(3, 3);
            this.uiLDeform_Grid.Name = "uiLDeform_Grid";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiLDeform_Grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiLDeform_Grid.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.uiLDeform_Grid.Size = new System.Drawing.Size(514, 522);
            this.uiLDeform_Grid.TabIndex = 1;
            // 
            // uiLDeformGrid_Noise01_Col
            // 
            this.uiLDeformGrid_Noise01_Col.HeaderText = "Noise 0.1";
            this.uiLDeformGrid_Noise01_Col.Name = "uiLDeformGrid_Noise01_Col";
            this.uiLDeformGrid_Noise01_Col.ReadOnly = true;
            // 
            // uiLDeformGrid_Noise05_Col
            // 
            this.uiLDeformGrid_Noise05_Col.HeaderText = "Noise 0.5";
            this.uiLDeformGrid_Noise05_Col.Name = "uiLDeformGrid_Noise05_Col";
            this.uiLDeformGrid_Noise05_Col.ReadOnly = true;
            // 
            // uiLDeformGrid_Noise1_Col
            // 
            this.uiLDeformGrid_Noise1_Col.HeaderText = "Noise 1.0";
            this.uiLDeformGrid_Noise1_Col.Name = "uiLDeformGrid_Noise1_Col";
            this.uiLDeformGrid_Noise1_Col.ReadOnly = true;
            // 
            // uiLDeformGrid_Noise2_Col
            // 
            this.uiLDeformGrid_Noise2_Col.HeaderText = "Noise 2.0";
            this.uiLDeformGrid_Noise2_Col.Name = "uiLDeformGrid_Noise2_Col";
            this.uiLDeformGrid_Noise2_Col.ReadOnly = true;
            // 
            // uiRChart_TbLay
            // 
            this.uiRChart_TbLay.ColumnCount = 1;
            this.uiRChart_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiRChart_TbLay.Controls.Add(this.uiRChart_Chart, 0, 0);
            this.uiRChart_TbLay.Controls.Add(this.uiRChart_Down_TbLay, 0, 1);
            this.uiRChart_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChart_TbLay.Location = new System.Drawing.Point(3, 3);
            this.uiRChart_TbLay.Name = "uiRChart_TbLay";
            this.uiRChart_TbLay.RowCount = 2;
            this.uiRChart_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiRChart_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.uiRChart_TbLay.Size = new System.Drawing.Size(515, 522);
            this.uiRChart_TbLay.TabIndex = 0;
            // 
            // uiRChart_Chart
            // 
            chartArea2.Name = "ChartArea1";
            this.uiRChart_Chart.ChartAreas.Add(chartArea2);
            this.uiRChart_Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.uiRChart_Chart.Legends.Add(legend2);
            this.uiRChart_Chart.Location = new System.Drawing.Point(3, 3);
            this.uiRChart_Chart.Name = "uiRChart_Chart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.uiRChart_Chart.Series.Add(series2);
            this.uiRChart_Chart.Size = new System.Drawing.Size(509, 481);
            this.uiRChart_Chart.TabIndex = 0;
            this.uiRChart_Chart.Text = "uiRightChart_Chart";
            // 
            // uiRChart_Down_TbLay
            // 
            this.uiRChart_Down_TbLay.ColumnCount = 6;
            this.uiRChart_Down_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.uiRChart_Down_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.uiRChart_Down_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.uiRChart_Down_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.uiRChart_Down_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.uiRChart_Down_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.uiRChart_Down_TbLay.Controls.Add(this.uiRChartDown_MeanT_ComBx, 4, 0);
            this.uiRChart_Down_TbLay.Controls.Add(this.uiRChartDown_Surr_ComBx, 3, 0);
            this.uiRChart_Down_TbLay.Controls.Add(this.uiRChartDown_Phen_ComBx, 2, 0);
            this.uiRChart_Down_TbLay.Controls.Add(this.uiRChartDown_DtSet_Btn, 5, 0);
            this.uiRChart_Down_TbLay.Controls.Add(this.uiRChartDown_CrvIdx_Num, 1, 0);
            this.uiRChart_Down_TbLay.Controls.Add(this.uiRChartDown_CrvT_ComBx, 0, 0);
            this.uiRChart_Down_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChart_Down_TbLay.Location = new System.Drawing.Point(3, 490);
            this.uiRChart_Down_TbLay.Name = "uiRChart_Down_TbLay";
            this.uiRChart_Down_TbLay.RowCount = 1;
            this.uiRChart_Down_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiRChart_Down_TbLay.Size = new System.Drawing.Size(509, 29);
            this.uiRChart_Down_TbLay.TabIndex = 1;
            // 
            // uiRChartDown_CrvT_ComBx
            // 
            this.uiRChartDown_CrvT_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartDown_CrvT_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiRChartDown_CrvT_ComBx.FormattingEnabled = true;
            this.uiRChartDown_CrvT_ComBx.Location = new System.Drawing.Point(3, 3);
            this.uiRChartDown_CrvT_ComBx.Name = "uiRChartDown_CrvT_ComBx";
            this.uiRChartDown_CrvT_ComBx.Size = new System.Drawing.Size(78, 21);
            this.uiRChartDown_CrvT_ComBx.TabIndex = 7;
            // 
            // uiRChartDown_CrvIdx_Num
            // 
            this.uiRChartDown_CrvIdx_Num.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartDown_CrvIdx_Num.Location = new System.Drawing.Point(87, 3);
            this.uiRChartDown_CrvIdx_Num.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.uiRChartDown_CrvIdx_Num.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uiRChartDown_CrvIdx_Num.Name = "uiRChartDown_CrvIdx_Num";
            this.uiRChartDown_CrvIdx_Num.Size = new System.Drawing.Size(78, 20);
            this.uiRChartDown_CrvIdx_Num.TabIndex = 9;
            this.uiRChartDown_CrvIdx_Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uiRChartDown_CrvIdx_Num.ThousandsSeparator = true;
            this.uiRChartDown_CrvIdx_Num.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // uiRFormula_PicBx
            // 
            this.uiRFormula_PicBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRFormula_PicBx.Location = new System.Drawing.Point(3, 3);
            this.uiRFormula_PicBx.Name = "uiRFormula_PicBx";
            this.uiRFormula_PicBx.Size = new System.Drawing.Size(515, 522);
            this.uiRFormula_PicBx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.uiRFormula_PicBx.TabIndex = 0;
            this.uiRFormula_PicBx.TabStop = false;
            // 
            // uiRChartDown_DtSet_Btn
            // 
            this.uiRChartDown_DtSet_Btn.BackColor = System.Drawing.Color.White;
            this.uiRChartDown_DtSet_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartDown_DtSet_Btn.Location = new System.Drawing.Point(423, 3);
            this.uiRChartDown_DtSet_Btn.Name = "uiRChartDown_DtSet_Btn";
            this.uiRChartDown_DtSet_Btn.Size = new System.Drawing.Size(83, 23);
            this.uiRChartDown_DtSet_Btn.TabIndex = 29;
            this.uiRChartDown_DtSet_Btn.Text = "Dataset";
            this.uiRChartDown_DtSet_Btn.UseVisualStyleBackColor = false;
            this.uiRChartDown_DtSet_Btn.Click += new System.EventHandler(this.UiRightChartDown_ShowDataset_Button_Click);
            // 
            // uiRChartDown_Phen_ComBx
            // 
            this.uiRChartDown_Phen_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartDown_Phen_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiRChartDown_Phen_ComBx.FormattingEnabled = true;
            this.uiRChartDown_Phen_ComBx.Location = new System.Drawing.Point(171, 3);
            this.uiRChartDown_Phen_ComBx.Name = "uiRChartDown_Phen_ComBx";
            this.uiRChartDown_Phen_ComBx.Size = new System.Drawing.Size(78, 21);
            this.uiRChartDown_Phen_ComBx.TabIndex = 30;
            // 
            // uiRChartDown_Surr_ComBx
            // 
            this.uiRChartDown_Surr_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartDown_Surr_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiRChartDown_Surr_ComBx.FormattingEnabled = true;
            this.uiRChartDown_Surr_ComBx.Location = new System.Drawing.Point(255, 3);
            this.uiRChartDown_Surr_ComBx.Name = "uiRChartDown_Surr_ComBx";
            this.uiRChartDown_Surr_ComBx.Size = new System.Drawing.Size(78, 21);
            this.uiRChartDown_Surr_ComBx.TabIndex = 31;
            // 
            // uiRChartDown_MeanT_ComBx
            // 
            this.uiRChartDown_MeanT_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartDown_MeanT_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiRChartDown_MeanT_ComBx.FormattingEnabled = true;
            this.uiRChartDown_MeanT_ComBx.Location = new System.Drawing.Point(339, 3);
            this.uiRChartDown_MeanT_ComBx.Name = "uiRChartDown_MeanT_ComBx";
            this.uiRChartDown_MeanT_ComBx.Size = new System.Drawing.Size(78, 21);
            this.uiRChartDown_MeanT_ComBx.TabIndex = 32;
            // 
            // StatAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 585);
            this.Controls.Add(this.ui_TbLay);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "StatAnalysis";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Statistical Analysis";
            this.Load += new System.EventHandler(this.UiStatAnalysis_Load);
            this.ui_TbLay.ResumeLayout(false);
            this.ui_TbLay.PerformLayout();
            this.uiL_TbCtrl.ResumeLayout(false);
            this.uiL_Peek_TbPg.ResumeLayout(false);
            this.uiL_Deform_TbPg.ResumeLayout(false);
            this.uiR_TbCtrl.ResumeLayout(false);
            this.uiR_Chart_TbPg.ResumeLayout(false);
            this.uiR_Formula_TbPg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiLPeek_Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiLDeform_Grid)).EndInit();
            this.uiRChart_TbLay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiRChart_Chart)).EndInit();
            this.uiRChart_Down_TbLay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiRChartDown_CrvIdx_Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiRFormula_PicBx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ui_TbLay;
        private System.Windows.Forms.TextBox uiL_StdDev_TxtBx;
        private System.Windows.Forms.TextBox uiR_Prv_TxtBx;
        private System.Windows.Forms.TabControl uiL_TbCtrl;
        private System.Windows.Forms.TabPage uiL_Peek_TbPg;
        private System.Windows.Forms.TabPage uiL_Deform_TbPg;
        private System.Windows.Forms.TabControl uiR_TbCtrl;
        private System.Windows.Forms.TabPage uiR_Chart_TbPg;
        private System.Windows.Forms.TabPage uiR_Formula_TbPg;
        private System.Windows.Forms.DataGridView uiLPeek_Grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLPeekGrid_Noise01_Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLPeekGrid_Noise05_Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLPeekGrid_Noise1_Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLPeekGrid_Noise2_Col;
        private System.Windows.Forms.DataGridView uiLDeform_Grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLDeformGrid_Noise01_Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLDeformGrid_Noise05_Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLDeformGrid_Noise1_Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLDeformGrid_Noise2_Col;
        private System.Windows.Forms.TableLayoutPanel uiRChart_TbLay;
        private System.Windows.Forms.DataVisualization.Charting.Chart uiRChart_Chart;
        private System.Windows.Forms.TableLayoutPanel uiRChart_Down_TbLay;
        private System.Windows.Forms.ComboBox uiRChartDown_CrvT_ComBx;
        private System.Windows.Forms.NumericUpDown uiRChartDown_CrvIdx_Num;
        private System.Windows.Forms.PictureBox uiRFormula_PicBx;
        private System.Windows.Forms.Button uiRChartDown_DtSet_Btn;
        private System.Windows.Forms.ComboBox uiRChartDown_MeanT_ComBx;
        private System.Windows.Forms.ComboBox uiRChartDown_Surr_ComBx;
        private System.Windows.Forms.ComboBox uiRChartDown_Phen_ComBx;
    }
}