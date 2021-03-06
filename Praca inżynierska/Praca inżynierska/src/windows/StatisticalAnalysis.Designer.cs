﻿namespace PI.src.windows
{
    partial class StatisticalAnalysis
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ui_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiL_StdDev_TxtBx = new System.Windows.Forms.TextBox();
            this.uiR_Prv_TxtBx = new System.Windows.Forms.TextBox();
            this.uiL_TbCtrl = new System.Windows.Forms.TabControl();
            this.uiL_Peek_TbPg = new System.Windows.Forms.TabPage();
            this.uiLPeek_Grid = new System.Windows.Forms.DataGridView();
            this.uiLPeekGrid_NoiseA_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLPeekGrid_NoiseB_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLPeekGrid_NoiseC_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLPeekGrid_NoiseD_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiL_Sat_TbPg = new System.Windows.Forms.TabPage();
            this.uiLSat_Grid = new System.Windows.Forms.DataGridView();
            this.uiLSatGrid_NoiseA_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLSatGrid_NoiseB_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLSatGrid_NoiseC_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLSatGrid_NoiseD_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiR_TbCtrl = new System.Windows.Forms.TabControl();
            this.uiR_Chart_TbPg = new System.Windows.Forms.TabPage();
            this.uiRChart_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiRChart_Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.uiRChart_Down_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiRChartDown_MeanT_ComBx = new System.Windows.Forms.ComboBox();
            this.uiRChartDown_Noises_ComBx = new System.Windows.Forms.ComboBox();
            this.uiRChartDown_Phen_ComBx = new System.Windows.Forms.ComboBox();
            this.uiRChartDown_DtSet_Btn = new System.Windows.Forms.Button();
            this.uiRChartDown_CrvIdx_Num = new System.Windows.Forms.NumericUpDown();
            this.uiRChartDown_CrvT_ComBx = new System.Windows.Forms.ComboBox();
            this.uiRChart_Up_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiRChartUp_DtSet_TxtBx = new System.Windows.Forms.TextBox();
            this.uiRChartUp_MeanT_TxtBx = new System.Windows.Forms.TextBox();
            this.uiRChartUp_Surr_TxtBx = new System.Windows.Forms.TextBox();
            this.uiRChartUp_Phen_TxtBx = new System.Windows.Forms.TextBox();
            this.uiRChartUp_CrvIdx_TxtBx = new System.Windows.Forms.TextBox();
            this.uiRChartUp_CrvT_TxtBx = new System.Windows.Forms.TextBox();
            this.ui_TbLay.SuspendLayout();
            this.uiL_TbCtrl.SuspendLayout();
            this.uiL_Peek_TbPg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiLPeek_Grid)).BeginInit();
            this.uiL_Sat_TbPg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiLSat_Grid)).BeginInit();
            this.uiR_TbCtrl.SuspendLayout();
            this.uiR_Chart_TbPg.SuspendLayout();
            this.uiRChart_TbLay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiRChart_Chart)).BeginInit();
            this.uiRChart_Down_TbLay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiRChartDown_CrvIdx_Num)).BeginInit();
            this.uiRChart_Up_TbLay.SuspendLayout();
            this.SuspendLayout();
            // 
            // ui_TbLay
            // 
            this.ui_TbLay.ColumnCount = 2;
            this.ui_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.ui_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
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
            // uiL_StdDev_TxtBx
            // 
            this.uiL_StdDev_TxtBx.BackColor = System.Drawing.Color.White;
            this.uiL_StdDev_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiL_StdDev_TxtBx.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.uiL_StdDev_TxtBx.Location = new System.Drawing.Point(3, 3);
            this.uiL_StdDev_TxtBx.Name = "uiL_StdDev_TxtBx";
            this.uiL_StdDev_TxtBx.ReadOnly = true;
            this.uiL_StdDev_TxtBx.Size = new System.Drawing.Size(421, 20);
            this.uiL_StdDev_TxtBx.TabIndex = 22;
            this.uiL_StdDev_TxtBx.Text = "Standard deviation";
            // 
            // uiR_Prv_TxtBx
            // 
            this.uiR_Prv_TxtBx.BackColor = System.Drawing.Color.White;
            this.uiR_Prv_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiR_Prv_TxtBx.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.uiR_Prv_TxtBx.Location = new System.Drawing.Point(430, 3);
            this.uiR_Prv_TxtBx.Name = "uiR_Prv_TxtBx";
            this.uiR_Prv_TxtBx.ReadOnly = true;
            this.uiR_Prv_TxtBx.Size = new System.Drawing.Size(636, 20);
            this.uiR_Prv_TxtBx.TabIndex = 21;
            this.uiR_Prv_TxtBx.Text = "Preview";
            // 
            // uiL_TbCtrl
            // 
            this.uiL_TbCtrl.Controls.Add(this.uiL_Peek_TbPg);
            this.uiL_TbCtrl.Controls.Add(this.uiL_Sat_TbPg);
            this.uiL_TbCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiL_TbCtrl.Location = new System.Drawing.Point(3, 28);
            this.uiL_TbCtrl.Name = "uiL_TbCtrl";
            this.uiL_TbCtrl.SelectedIndex = 0;
            this.uiL_TbCtrl.Size = new System.Drawing.Size(421, 554);
            this.uiL_TbCtrl.TabIndex = 23;
            this.uiL_TbCtrl.SelectedIndexChanged += new System.EventHandler(this.OnTabSelection);
            // 
            // uiL_Peek_TbPg
            // 
            this.uiL_Peek_TbPg.Controls.Add(this.uiLPeek_Grid);
            this.uiL_Peek_TbPg.Location = new System.Drawing.Point(4, 22);
            this.uiL_Peek_TbPg.Name = "uiL_Peek_TbPg";
            this.uiL_Peek_TbPg.Padding = new System.Windows.Forms.Padding(3);
            this.uiL_Peek_TbPg.Size = new System.Drawing.Size(413, 528);
            this.uiL_Peek_TbPg.TabIndex = 0;
            this.uiL_Peek_TbPg.Text = "Peek";
            this.uiL_Peek_TbPg.UseVisualStyleBackColor = true;
            // 
            // uiLPeek_Grid
            // 
            this.uiLPeek_Grid.AllowUserToAddRows = false;
            this.uiLPeek_Grid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.uiLPeek_Grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.uiLPeek_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.uiLPeek_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uiLPeek_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uiLPeekGrid_NoiseA_Col,
            this.uiLPeekGrid_NoiseB_Col,
            this.uiLPeekGrid_NoiseC_Col,
            this.uiLPeekGrid_NoiseD_Col});
            this.uiLPeek_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLPeek_Grid.Location = new System.Drawing.Point(3, 3);
            this.uiLPeek_Grid.Name = "uiLPeek_Grid";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiLPeek_Grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiLPeek_Grid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.uiLPeek_Grid.Size = new System.Drawing.Size(407, 522);
            this.uiLPeek_Grid.TabIndex = 0;
            // 
            // uiLPeekGrid_NoiseA_Col
            // 
            this.uiLPeekGrid_NoiseA_Col.HeaderText = "Noise A";
            this.uiLPeekGrid_NoiseA_Col.Name = "uiLPeekGrid_NoiseA_Col";
            this.uiLPeekGrid_NoiseA_Col.ReadOnly = true;
            // 
            // uiLPeekGrid_NoiseB_Col
            // 
            this.uiLPeekGrid_NoiseB_Col.HeaderText = "Noise B";
            this.uiLPeekGrid_NoiseB_Col.Name = "uiLPeekGrid_NoiseB_Col";
            this.uiLPeekGrid_NoiseB_Col.ReadOnly = true;
            // 
            // uiLPeekGrid_NoiseC_Col
            // 
            this.uiLPeekGrid_NoiseC_Col.HeaderText = "Noise C";
            this.uiLPeekGrid_NoiseC_Col.Name = "uiLPeekGrid_NoiseC_Col";
            this.uiLPeekGrid_NoiseC_Col.ReadOnly = true;
            // 
            // uiLPeekGrid_NoiseD_Col
            // 
            this.uiLPeekGrid_NoiseD_Col.HeaderText = "Noise D";
            this.uiLPeekGrid_NoiseD_Col.Name = "uiLPeekGrid_NoiseD_Col";
            this.uiLPeekGrid_NoiseD_Col.ReadOnly = true;
            // 
            // uiL_Sat_TbPg
            // 
            this.uiL_Sat_TbPg.Controls.Add(this.uiLSat_Grid);
            this.uiL_Sat_TbPg.Location = new System.Drawing.Point(4, 22);
            this.uiL_Sat_TbPg.Name = "uiL_Sat_TbPg";
            this.uiL_Sat_TbPg.Padding = new System.Windows.Forms.Padding(3);
            this.uiL_Sat_TbPg.Size = new System.Drawing.Size(520, 528);
            this.uiL_Sat_TbPg.TabIndex = 1;
            this.uiL_Sat_TbPg.Text = "Saturation";
            this.uiL_Sat_TbPg.UseVisualStyleBackColor = true;
            // 
            // uiLSat_Grid
            // 
            this.uiLSat_Grid.AllowUserToAddRows = false;
            this.uiLSat_Grid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.uiLSat_Grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.uiLSat_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.uiLSat_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uiLSat_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uiLSatGrid_NoiseA_Col,
            this.uiLSatGrid_NoiseB_Col,
            this.uiLSatGrid_NoiseC_Col,
            this.uiLSatGrid_NoiseD_Col});
            this.uiLSat_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLSat_Grid.Location = new System.Drawing.Point(3, 3);
            this.uiLSat_Grid.Name = "uiLSat_Grid";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiLSat_Grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiLSat_Grid.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.uiLSat_Grid.Size = new System.Drawing.Size(514, 522);
            this.uiLSat_Grid.TabIndex = 1;
            // 
            // uiLSatGrid_NoiseA_Col
            // 
            this.uiLSatGrid_NoiseA_Col.HeaderText = "Noise A";
            this.uiLSatGrid_NoiseA_Col.Name = "uiLSatGrid_NoiseA_Col";
            this.uiLSatGrid_NoiseA_Col.ReadOnly = true;
            // 
            // uiLSatGrid_NoiseB_Col
            // 
            this.uiLSatGrid_NoiseB_Col.HeaderText = "Noise B";
            this.uiLSatGrid_NoiseB_Col.Name = "uiLSatGrid_NoiseB_Col";
            this.uiLSatGrid_NoiseB_Col.ReadOnly = true;
            // 
            // uiLSatGrid_NoiseC_Col
            // 
            this.uiLSatGrid_NoiseC_Col.HeaderText = "Noise C";
            this.uiLSatGrid_NoiseC_Col.Name = "uiLSatGrid_NoiseC_Col";
            this.uiLSatGrid_NoiseC_Col.ReadOnly = true;
            // 
            // uiLSatGrid_NoiseD_Col
            // 
            this.uiLSatGrid_NoiseD_Col.HeaderText = "Noise D";
            this.uiLSatGrid_NoiseD_Col.Name = "uiLSatGrid_NoiseD_Col";
            this.uiLSatGrid_NoiseD_Col.ReadOnly = true;
            // 
            // uiR_TbCtrl
            // 
            this.uiR_TbCtrl.Controls.Add(this.uiR_Chart_TbPg);
            this.uiR_TbCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiR_TbCtrl.Location = new System.Drawing.Point(430, 28);
            this.uiR_TbCtrl.Name = "uiR_TbCtrl";
            this.uiR_TbCtrl.SelectedIndex = 0;
            this.uiR_TbCtrl.Size = new System.Drawing.Size(636, 554);
            this.uiR_TbCtrl.TabIndex = 24;
            // 
            // uiR_Chart_TbPg
            // 
            this.uiR_Chart_TbPg.Controls.Add(this.uiRChart_TbLay);
            this.uiR_Chart_TbPg.Location = new System.Drawing.Point(4, 22);
            this.uiR_Chart_TbPg.Name = "uiR_Chart_TbPg";
            this.uiR_Chart_TbPg.Padding = new System.Windows.Forms.Padding(3);
            this.uiR_Chart_TbPg.Size = new System.Drawing.Size(628, 528);
            this.uiR_Chart_TbPg.TabIndex = 0;
            this.uiR_Chart_TbPg.Text = "Chart";
            this.uiR_Chart_TbPg.UseVisualStyleBackColor = true;
            // 
            // uiRChart_TbLay
            // 
            this.uiRChart_TbLay.ColumnCount = 1;
            this.uiRChart_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiRChart_TbLay.Controls.Add(this.uiRChart_Chart, 0, 0);
            this.uiRChart_TbLay.Controls.Add(this.uiRChart_Down_TbLay, 0, 2);
            this.uiRChart_TbLay.Controls.Add(this.uiRChart_Up_TbLay, 0, 1);
            this.uiRChart_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChart_TbLay.Location = new System.Drawing.Point(3, 3);
            this.uiRChart_TbLay.Name = "uiRChart_TbLay";
            this.uiRChart_TbLay.RowCount = 3;
            this.uiRChart_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiRChart_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.uiRChart_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.uiRChart_TbLay.Size = new System.Drawing.Size(622, 522);
            this.uiRChart_TbLay.TabIndex = 0;
            // 
            // uiRChart_Chart
            // 
            chartArea1.Name = "ChartArea1";
            this.uiRChart_Chart.ChartAreas.Add(chartArea1);
            this.uiRChart_Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.uiRChart_Chart.Legends.Add(legend1);
            this.uiRChart_Chart.Location = new System.Drawing.Point(3, 3);
            this.uiRChart_Chart.Name = "uiRChart_Chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.uiRChart_Chart.Series.Add(series1);
            this.uiRChart_Chart.Size = new System.Drawing.Size(616, 446);
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
            this.uiRChart_Down_TbLay.Controls.Add(this.uiRChartDown_Noises_ComBx, 3, 0);
            this.uiRChart_Down_TbLay.Controls.Add(this.uiRChartDown_Phen_ComBx, 2, 0);
            this.uiRChart_Down_TbLay.Controls.Add(this.uiRChartDown_DtSet_Btn, 5, 0);
            this.uiRChart_Down_TbLay.Controls.Add(this.uiRChartDown_CrvIdx_Num, 1, 0);
            this.uiRChart_Down_TbLay.Controls.Add(this.uiRChartDown_CrvT_ComBx, 0, 0);
            this.uiRChart_Down_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChart_Down_TbLay.Location = new System.Drawing.Point(3, 490);
            this.uiRChart_Down_TbLay.Name = "uiRChart_Down_TbLay";
            this.uiRChart_Down_TbLay.RowCount = 1;
            this.uiRChart_Down_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiRChart_Down_TbLay.Size = new System.Drawing.Size(616, 29);
            this.uiRChart_Down_TbLay.TabIndex = 1;
            // 
            // uiRChartDown_MeanT_ComBx
            // 
            this.uiRChartDown_MeanT_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartDown_MeanT_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiRChartDown_MeanT_ComBx.FormattingEnabled = true;
            this.uiRChartDown_MeanT_ComBx.Location = new System.Drawing.Point(411, 3);
            this.uiRChartDown_MeanT_ComBx.Name = "uiRChartDown_MeanT_ComBx";
            this.uiRChartDown_MeanT_ComBx.Size = new System.Drawing.Size(96, 21);
            this.uiRChartDown_MeanT_ComBx.TabIndex = 32;
            this.uiRChartDown_MeanT_ComBx.SelectedIndexChanged += new System.EventHandler(this.OnMeanTypeSelection);
            // 
            // uiRChartDown_Noises_ComBx
            // 
            this.uiRChartDown_Noises_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartDown_Noises_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiRChartDown_Noises_ComBx.FormattingEnabled = true;
            this.uiRChartDown_Noises_ComBx.Location = new System.Drawing.Point(309, 3);
            this.uiRChartDown_Noises_ComBx.Name = "uiRChartDown_Noises_ComBx";
            this.uiRChartDown_Noises_ComBx.Size = new System.Drawing.Size(96, 21);
            this.uiRChartDown_Noises_ComBx.TabIndex = 31;
            this.uiRChartDown_Noises_ComBx.SelectedIndexChanged += new System.EventHandler(this.OnNoiseSelection);
            // 
            // uiRChartDown_Phen_ComBx
            // 
            this.uiRChartDown_Phen_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartDown_Phen_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiRChartDown_Phen_ComBx.FormattingEnabled = true;
            this.uiRChartDown_Phen_ComBx.Location = new System.Drawing.Point(207, 3);
            this.uiRChartDown_Phen_ComBx.Name = "uiRChartDown_Phen_ComBx";
            this.uiRChartDown_Phen_ComBx.Size = new System.Drawing.Size(96, 21);
            this.uiRChartDown_Phen_ComBx.TabIndex = 30;
            this.uiRChartDown_Phen_ComBx.SelectedIndexChanged += new System.EventHandler(this.OnPhenomenonSelection);
            // 
            // uiRChartDown_DtSet_Btn
            // 
            this.uiRChartDown_DtSet_Btn.BackColor = System.Drawing.Color.White;
            this.uiRChartDown_DtSet_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartDown_DtSet_Btn.Location = new System.Drawing.Point(513, 3);
            this.uiRChartDown_DtSet_Btn.Name = "uiRChartDown_DtSet_Btn";
            this.uiRChartDown_DtSet_Btn.Size = new System.Drawing.Size(100, 23);
            this.uiRChartDown_DtSet_Btn.TabIndex = 29;
            this.uiRChartDown_DtSet_Btn.Text = "Dataset";
            this.uiRChartDown_DtSet_Btn.UseVisualStyleBackColor = false;
            this.uiRChartDown_DtSet_Btn.Click += new System.EventHandler(this.OnShowDatasetClick);
            // 
            // uiRChartDown_CrvIdx_Num
            // 
            this.uiRChartDown_CrvIdx_Num.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartDown_CrvIdx_Num.Location = new System.Drawing.Point(105, 3);
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
            this.uiRChartDown_CrvIdx_Num.Size = new System.Drawing.Size(96, 20);
            this.uiRChartDown_CrvIdx_Num.TabIndex = 9;
            this.uiRChartDown_CrvIdx_Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uiRChartDown_CrvIdx_Num.ThousandsSeparator = true;
            this.uiRChartDown_CrvIdx_Num.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uiRChartDown_CrvIdx_Num.ValueChanged += new System.EventHandler(this.OnCurveIndexAlteration);
            // 
            // uiRChartDown_CrvT_ComBx
            // 
            this.uiRChartDown_CrvT_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartDown_CrvT_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiRChartDown_CrvT_ComBx.FormattingEnabled = true;
            this.uiRChartDown_CrvT_ComBx.Location = new System.Drawing.Point(3, 3);
            this.uiRChartDown_CrvT_ComBx.Name = "uiRChartDown_CrvT_ComBx";
            this.uiRChartDown_CrvT_ComBx.Size = new System.Drawing.Size(96, 21);
            this.uiRChartDown_CrvT_ComBx.TabIndex = 7;
            this.uiRChartDown_CrvT_ComBx.SelectedIndexChanged += new System.EventHandler(this.OnCurveTypeSelection);
            // 
            // uiRChart_Up_TbLay
            // 
            this.uiRChart_Up_TbLay.ColumnCount = 6;
            this.uiRChart_Up_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.uiRChart_Up_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.uiRChart_Up_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.uiRChart_Up_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.uiRChart_Up_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.uiRChart_Up_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.uiRChart_Up_TbLay.Controls.Add(this.uiRChartUp_DtSet_TxtBx, 5, 0);
            this.uiRChart_Up_TbLay.Controls.Add(this.uiRChartUp_MeanT_TxtBx, 4, 0);
            this.uiRChart_Up_TbLay.Controls.Add(this.uiRChartUp_Surr_TxtBx, 3, 0);
            this.uiRChart_Up_TbLay.Controls.Add(this.uiRChartUp_Phen_TxtBx, 2, 0);
            this.uiRChart_Up_TbLay.Controls.Add(this.uiRChartUp_CrvIdx_TxtBx, 1, 0);
            this.uiRChart_Up_TbLay.Controls.Add(this.uiRChartUp_CrvT_TxtBx, 0, 0);
            this.uiRChart_Up_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChart_Up_TbLay.Location = new System.Drawing.Point(3, 455);
            this.uiRChart_Up_TbLay.Name = "uiRChart_Up_TbLay";
            this.uiRChart_Up_TbLay.RowCount = 1;
            this.uiRChart_Up_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiRChart_Up_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.uiRChart_Up_TbLay.Size = new System.Drawing.Size(616, 29);
            this.uiRChart_Up_TbLay.TabIndex = 2;
            // 
            // uiRChartUp_DtSet_TxtBx
            // 
            this.uiRChartUp_DtSet_TxtBx.BackColor = System.Drawing.SystemColors.Control;
            this.uiRChartUp_DtSet_TxtBx.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiRChartUp_DtSet_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartUp_DtSet_TxtBx.Location = new System.Drawing.Point(513, 3);
            this.uiRChartUp_DtSet_TxtBx.Name = "uiRChartUp_DtSet_TxtBx";
            this.uiRChartUp_DtSet_TxtBx.ReadOnly = true;
            this.uiRChartUp_DtSet_TxtBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiRChartUp_DtSet_TxtBx.Size = new System.Drawing.Size(100, 20);
            this.uiRChartUp_DtSet_TxtBx.TabIndex = 18;
            this.uiRChartUp_DtSet_TxtBx.Text = "Selection:";
            // 
            // uiRChartUp_MeanT_TxtBx
            // 
            this.uiRChartUp_MeanT_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiRChartUp_MeanT_TxtBx.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiRChartUp_MeanT_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartUp_MeanT_TxtBx.Location = new System.Drawing.Point(411, 3);
            this.uiRChartUp_MeanT_TxtBx.Name = "uiRChartUp_MeanT_TxtBx";
            this.uiRChartUp_MeanT_TxtBx.ReadOnly = true;
            this.uiRChartUp_MeanT_TxtBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiRChartUp_MeanT_TxtBx.Size = new System.Drawing.Size(96, 20);
            this.uiRChartUp_MeanT_TxtBx.TabIndex = 17;
            this.uiRChartUp_MeanT_TxtBx.Text = "Mean type:";
            // 
            // uiRChartUp_Surr_TxtBx
            // 
            this.uiRChartUp_Surr_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiRChartUp_Surr_TxtBx.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiRChartUp_Surr_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartUp_Surr_TxtBx.Location = new System.Drawing.Point(309, 3);
            this.uiRChartUp_Surr_TxtBx.Name = "uiRChartUp_Surr_TxtBx";
            this.uiRChartUp_Surr_TxtBx.ReadOnly = true;
            this.uiRChartUp_Surr_TxtBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiRChartUp_Surr_TxtBx.Size = new System.Drawing.Size(96, 20);
            this.uiRChartUp_Surr_TxtBx.TabIndex = 16;
            this.uiRChartUp_Surr_TxtBx.Text = "Noise:";
            // 
            // uiRChartUp_Phen_TxtBx
            // 
            this.uiRChartUp_Phen_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiRChartUp_Phen_TxtBx.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiRChartUp_Phen_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartUp_Phen_TxtBx.Location = new System.Drawing.Point(207, 3);
            this.uiRChartUp_Phen_TxtBx.Name = "uiRChartUp_Phen_TxtBx";
            this.uiRChartUp_Phen_TxtBx.ReadOnly = true;
            this.uiRChartUp_Phen_TxtBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiRChartUp_Phen_TxtBx.Size = new System.Drawing.Size(96, 20);
            this.uiRChartUp_Phen_TxtBx.TabIndex = 15;
            this.uiRChartUp_Phen_TxtBx.Text = "Phenomenon:";
            // 
            // uiRChartUp_CrvIdx_TxtBx
            // 
            this.uiRChartUp_CrvIdx_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiRChartUp_CrvIdx_TxtBx.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiRChartUp_CrvIdx_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartUp_CrvIdx_TxtBx.Location = new System.Drawing.Point(105, 3);
            this.uiRChartUp_CrvIdx_TxtBx.Name = "uiRChartUp_CrvIdx_TxtBx";
            this.uiRChartUp_CrvIdx_TxtBx.ReadOnly = true;
            this.uiRChartUp_CrvIdx_TxtBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiRChartUp_CrvIdx_TxtBx.Size = new System.Drawing.Size(96, 20);
            this.uiRChartUp_CrvIdx_TxtBx.TabIndex = 14;
            this.uiRChartUp_CrvIdx_TxtBx.Text = "Curve index:";
            // 
            // uiRChartUp_CrvT_TxtBx
            // 
            this.uiRChartUp_CrvT_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiRChartUp_CrvT_TxtBx.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiRChartUp_CrvT_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRChartUp_CrvT_TxtBx.Location = new System.Drawing.Point(3, 3);
            this.uiRChartUp_CrvT_TxtBx.Name = "uiRChartUp_CrvT_TxtBx";
            this.uiRChartUp_CrvT_TxtBx.ReadOnly = true;
            this.uiRChartUp_CrvT_TxtBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiRChartUp_CrvT_TxtBx.Size = new System.Drawing.Size(96, 20);
            this.uiRChartUp_CrvT_TxtBx.TabIndex = 13;
            this.uiRChartUp_CrvT_TxtBx.Text = "Curve type:";
            // 
            // StatisticalAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 585);
            this.Controls.Add(this.ui_TbLay);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "StatisticalAnalysis";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Statistical Analysis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.OnLoad);
            this.Shown += new System.EventHandler(this.OnShown);
            this.ui_TbLay.ResumeLayout(false);
            this.ui_TbLay.PerformLayout();
            this.uiL_TbCtrl.ResumeLayout(false);
            this.uiL_Peek_TbPg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiLPeek_Grid)).EndInit();
            this.uiL_Sat_TbPg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiLSat_Grid)).EndInit();
            this.uiR_TbCtrl.ResumeLayout(false);
            this.uiR_Chart_TbPg.ResumeLayout(false);
            this.uiRChart_TbLay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiRChart_Chart)).EndInit();
            this.uiRChart_Down_TbLay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiRChartDown_CrvIdx_Num)).EndInit();
            this.uiRChart_Up_TbLay.ResumeLayout(false);
            this.uiRChart_Up_TbLay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ui_TbLay;
        private System.Windows.Forms.TextBox uiL_StdDev_TxtBx;
        private System.Windows.Forms.TextBox uiR_Prv_TxtBx;
        private System.Windows.Forms.TabControl uiL_TbCtrl;
        private System.Windows.Forms.TabPage uiL_Peek_TbPg;
        private System.Windows.Forms.TabPage uiL_Sat_TbPg;
        private System.Windows.Forms.TabControl uiR_TbCtrl;
        private System.Windows.Forms.TabPage uiR_Chart_TbPg;
        private System.Windows.Forms.DataGridView uiLPeek_Grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLPeekGrid_NoiseA_Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLPeekGrid_NoiseB_Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLPeekGrid_NoiseC_Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLPeekGrid_NoiseD_Col;
        private System.Windows.Forms.DataGridView uiLSat_Grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLSatGrid_NoiseA_Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLSatGrid_NoiseB_Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLSatGrid_NoiseC_Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiLSatGrid_NoiseD_Col;
        private System.Windows.Forms.TableLayoutPanel uiRChart_TbLay;
        private System.Windows.Forms.DataVisualization.Charting.Chart uiRChart_Chart;
        private System.Windows.Forms.TableLayoutPanel uiRChart_Down_TbLay;
        private System.Windows.Forms.ComboBox uiRChartDown_CrvT_ComBx;
        private System.Windows.Forms.NumericUpDown uiRChartDown_CrvIdx_Num;
        private System.Windows.Forms.Button uiRChartDown_DtSet_Btn;
        private System.Windows.Forms.ComboBox uiRChartDown_MeanT_ComBx;
        private System.Windows.Forms.ComboBox uiRChartDown_Noises_ComBx;
        private System.Windows.Forms.ComboBox uiRChartDown_Phen_ComBx;
        private System.Windows.Forms.TableLayoutPanel uiRChart_Up_TbLay;
        private System.Windows.Forms.TextBox uiRChartUp_CrvT_TxtBx;
        private System.Windows.Forms.TextBox uiRChartUp_DtSet_TxtBx;
        private System.Windows.Forms.TextBox uiRChartUp_MeanT_TxtBx;
        private System.Windows.Forms.TextBox uiRChartUp_Surr_TxtBx;
        private System.Windows.Forms.TextBox uiRChartUp_Phen_TxtBx;
        private System.Windows.Forms.TextBox uiRChartUp_CrvIdx_TxtBx;
    }
}