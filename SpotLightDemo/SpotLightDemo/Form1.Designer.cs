
namespace SpotLightDemo
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.spotLightControl1 = new SpotLightDemo.SpotLightControl();
            this.SuspendLayout();
            // 
            // spotLightControl1
            // 
            this.spotLightControl1.Font = new System.Drawing.Font("Arial Black", 50F, System.Drawing.FontStyle.Bold);
            this.spotLightControl1.Location = new System.Drawing.Point(30, 29);
            this.spotLightControl1.Name = "spotLightControl1";
            this.spotLightControl1.OffsetX = 0;
            this.spotLightControl1.Padding = new System.Windows.Forms.Padding(10);
            this.spotLightControl1.Size = new System.Drawing.Size(819, 447);
            this.spotLightControl1.TabIndex = 0;
            this.spotLightControl1.Text = "spotLightControl1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 514);
            this.Controls.Add(this.spotLightControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private SpotLightControl spotLightControl1;
    }
}

