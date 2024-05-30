namespace gameproject
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ustboru = new System.Windows.Forms.PictureBox();
            this.flappybird = new System.Windows.Forms.PictureBox();
            this.altboru = new System.Windows.Forms.PictureBox();
            this.zemin = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ustboru)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flappybird)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.altboru)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zemin)).BeginInit();
            this.SuspendLayout();
            // 
            // ustboru
            // 
            this.ustboru.Image = ((System.Drawing.Image)(resources.GetObject("ustboru.Image")));
            this.ustboru.Location = new System.Drawing.Point(550, -1);
            this.ustboru.Name = "ustboru";
            this.ustboru.Size = new System.Drawing.Size(70, 123);
            this.ustboru.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ustboru.TabIndex = 0;
            this.ustboru.TabStop = false;
            // 
            // flappybird
            // 
            this.flappybird.Image = ((System.Drawing.Image)(resources.GetObject("flappybird.Image")));
            this.flappybird.Location = new System.Drawing.Point(55, 138);
            this.flappybird.Name = "flappybird";
            this.flappybird.Size = new System.Drawing.Size(72, 60);
            this.flappybird.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.flappybird.TabIndex = 0;
            this.flappybird.TabStop = false;
            // 
            // altboru
            // 
            this.altboru.Image = ((System.Drawing.Image)(resources.GetObject("altboru.Image")));
            this.altboru.Location = new System.Drawing.Point(457, 247);
            this.altboru.Name = "altboru";
            this.altboru.Size = new System.Drawing.Size(70, 145);
            this.altboru.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.altboru.TabIndex = 0;
            this.altboru.TabStop = false;
            // 
            // zemin
            // 
            this.zemin.Image = ((System.Drawing.Image)(resources.GetObject("zemin.Image")));
            this.zemin.Location = new System.Drawing.Point(-3, 389);
            this.zemin.Name = "zemin";
            this.zemin.Size = new System.Drawing.Size(702, 61);
            this.zemin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.zemin.TabIndex = 0;
            this.zemin.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timerEtkin);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(698, 450);
            this.Controls.Add(this.zemin);
            this.Controls.Add(this.altboru);
            this.Controls.Add(this.ustboru);
            this.Controls.Add(this.flappybird);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Flappy Bird";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ustboru)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flappybird)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.altboru)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zemin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ustboru;
        private System.Windows.Forms.PictureBox flappybird;
        private System.Windows.Forms.PictureBox altboru;
        private System.Windows.Forms.PictureBox zemin;
        private System.Windows.Forms.Timer timer1;
    }
}

