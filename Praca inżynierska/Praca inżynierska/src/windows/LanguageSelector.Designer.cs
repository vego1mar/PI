namespace PI.src.windows
{
    partial class LanguageSelector
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
            this.ui_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiDown_Ok_Btn = new System.Windows.Forms.Button();
            this.uiUp_LstBx = new System.Windows.Forms.ListBox();
            this.ui_TbLay.SuspendLayout();
            this.SuspendLayout();
            // 
            // ui_TbLay
            // 
            this.ui_TbLay.ColumnCount = 1;
            this.ui_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ui_TbLay.Controls.Add(this.uiDown_Ok_Btn, 0, 1);
            this.ui_TbLay.Controls.Add(this.uiUp_LstBx, 0, 0);
            this.ui_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_TbLay.Location = new System.Drawing.Point(0, 0);
            this.ui_TbLay.Name = "ui_TbLay";
            this.ui_TbLay.RowCount = 2;
            this.ui_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ui_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.ui_TbLay.Size = new System.Drawing.Size(304, 211);
            this.ui_TbLay.TabIndex = 0;
            // 
            // uiDown_Ok_Btn
            // 
            this.uiDown_Ok_Btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uiDown_Ok_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiDown_Ok_Btn.Location = new System.Drawing.Point(3, 179);
            this.uiDown_Ok_Btn.Name = "uiDown_Ok_Btn";
            this.uiDown_Ok_Btn.Size = new System.Drawing.Size(298, 29);
            this.uiDown_Ok_Btn.TabIndex = 17;
            this.uiDown_Ok_Btn.Text = "OK";
            this.uiDown_Ok_Btn.UseVisualStyleBackColor = true;
            // 
            // uiUp_LstBx
            // 
            this.uiUp_LstBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiUp_LstBx.FormattingEnabled = true;
            this.uiUp_LstBx.Location = new System.Drawing.Point(3, 3);
            this.uiUp_LstBx.Name = "uiUp_LstBx";
            this.uiUp_LstBx.Size = new System.Drawing.Size(298, 170);
            this.uiUp_LstBx.TabIndex = 0;
            // 
            // LangSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 211);
            this.Controls.Add(this.ui_TbLay);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(320, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(320, 39);
            this.Name = "LangSelector";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Language Selector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.ui_TbLay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ui_TbLay;
        private System.Windows.Forms.ListBox uiUp_LstBx;
        private System.Windows.Forms.Button uiDown_Ok_Btn;
    }
}