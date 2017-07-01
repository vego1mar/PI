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
            this.ui_TbCtrl = new System.Windows.Forms.TabControl();
            this.ui_Power_TbPg = new System.Windows.Forms.TabPage();
            this.uiPower_GrBx = new System.Windows.Forms.GroupBox();
            this.ui_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.ui_Ok_Btn = new System.Windows.Forms.Button();
            this.uiPower_k_Lb = new System.Windows.Forms.Label();
            this.uiPower_Num = new System.Windows.Forms.NumericUpDown();
            this.ui_TbCtrl.SuspendLayout();
            this.ui_Power_TbPg.SuspendLayout();
            this.uiPower_GrBx.SuspendLayout();
            this.ui_TbLay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiPower_Num)).BeginInit();
            this.SuspendLayout();
            // 
            // ui_TbCtrl
            // 
            this.ui_TbCtrl.Controls.Add(this.ui_Power_TbPg);
            this.ui_TbCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_TbCtrl.Location = new System.Drawing.Point(3, 3);
            this.ui_TbCtrl.Name = "ui_TbCtrl";
            this.ui_TbCtrl.SelectedIndex = 0;
            this.ui_TbCtrl.Size = new System.Drawing.Size(398, 164);
            this.ui_TbCtrl.TabIndex = 0;
            // 
            // ui_Power_TbPg
            // 
            this.ui_Power_TbPg.Controls.Add(this.uiPower_GrBx);
            this.ui_Power_TbPg.Location = new System.Drawing.Point(4, 22);
            this.ui_Power_TbPg.Name = "ui_Power_TbPg";
            this.ui_Power_TbPg.Padding = new System.Windows.Forms.Padding(3);
            this.ui_Power_TbPg.Size = new System.Drawing.Size(390, 138);
            this.ui_Power_TbPg.TabIndex = 0;
            this.ui_Power_TbPg.Text = "Power";
            this.ui_Power_TbPg.UseVisualStyleBackColor = true;
            // 
            // uiPower_GrBx
            // 
            this.uiPower_GrBx.Controls.Add(this.uiPower_Num);
            this.uiPower_GrBx.Controls.Add(this.uiPower_k_Lb);
            this.uiPower_GrBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPower_GrBx.Location = new System.Drawing.Point(3, 3);
            this.uiPower_GrBx.Name = "uiPower_GrBx";
            this.uiPower_GrBx.Size = new System.Drawing.Size(384, 132);
            this.uiPower_GrBx.TabIndex = 0;
            this.uiPower_GrBx.TabStop = false;
            this.uiPower_GrBx.Text = "Rank k";
            // 
            // ui_TbLay
            // 
            this.ui_TbLay.ColumnCount = 1;
            this.ui_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ui_TbLay.Controls.Add(this.ui_TbCtrl, 0, 0);
            this.ui_TbLay.Controls.Add(this.ui_Ok_Btn, 0, 1);
            this.ui_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_TbLay.Location = new System.Drawing.Point(0, 0);
            this.ui_TbLay.Name = "ui_TbLay";
            this.ui_TbLay.RowCount = 2;
            this.ui_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ui_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.ui_TbLay.Size = new System.Drawing.Size(404, 205);
            this.ui_TbLay.TabIndex = 1;
            // 
            // ui_Ok_Btn
            // 
            this.ui_Ok_Btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ui_Ok_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_Ok_Btn.Location = new System.Drawing.Point(3, 173);
            this.ui_Ok_Btn.Name = "ui_Ok_Btn";
            this.ui_Ok_Btn.Size = new System.Drawing.Size(398, 29);
            this.ui_Ok_Btn.TabIndex = 1;
            this.ui_Ok_Btn.Text = "OK";
            this.ui_Ok_Btn.UseVisualStyleBackColor = true;
            this.ui_Ok_Btn.Click += new System.EventHandler(this.Ui_Ok_Click);
            // 
            // uiPower_k_Lb
            // 
            this.uiPower_k_Lb.AutoSize = true;
            this.uiPower_k_Lb.Location = new System.Drawing.Point(48, 52);
            this.uiPower_k_Lb.Name = "uiPower_k_Lb";
            this.uiPower_k_Lb.Size = new System.Drawing.Size(31, 13);
            this.uiPower_k_Lb.TabIndex = 0;
            this.uiPower_k_Lb.Text = "k = ";
            // 
            // uiPower_Num
            // 
            this.uiPower_Num.DecimalPlaces = 2;
            this.uiPower_Num.Location = new System.Drawing.Point(85, 50);
            this.uiPower_Num.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.uiPower_Num.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.uiPower_Num.Name = "uiPower_Num";
            this.uiPower_Num.Size = new System.Drawing.Size(120, 20);
            this.uiPower_Num.TabIndex = 1;
            this.uiPower_Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uiPower_Num.ThousandsSeparator = true;
            // 
            // MeansSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 205);
            this.Controls.Add(this.ui_TbLay);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 240);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 240);
            this.Name = "MeansSettings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Means Settings";
            this.ui_TbCtrl.ResumeLayout(false);
            this.ui_Power_TbPg.ResumeLayout(false);
            this.uiPower_GrBx.ResumeLayout(false);
            this.uiPower_GrBx.PerformLayout();
            this.ui_TbLay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiPower_Num)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ui_TbCtrl;
        private System.Windows.Forms.TabPage ui_Power_TbPg;
        private System.Windows.Forms.GroupBox uiPower_GrBx;
        private System.Windows.Forms.TableLayoutPanel ui_TbLay;
        private System.Windows.Forms.Button ui_Ok_Btn;
        private System.Windows.Forms.NumericUpDown uiPower_Num;
        private System.Windows.Forms.Label uiPower_k_Lb;
    }
}