namespace PI
{
    partial class AvgInfo
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
            this.SuspendLayout();
            // 
            // ui_TbCtrl
            // 
            this.ui_TbCtrl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.ui_TbCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_TbCtrl.Location = new System.Drawing.Point(0, 0);
            this.ui_TbCtrl.Multiline = true;
            this.ui_TbCtrl.Name = "ui_TbCtrl";
            this.ui_TbCtrl.SelectedIndex = 0;
            this.ui_TbCtrl.Size = new System.Drawing.Size(804, 525);
            this.ui_TbCtrl.TabIndex = 0;
            // 
            // AvgInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 525);
            this.Controls.Add(this.ui_TbCtrl);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1366, 768);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(666, 333);
            this.Name = "AvgInfo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Averaging info";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl ui_TbCtrl;
    }
}