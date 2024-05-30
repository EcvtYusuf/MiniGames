namespace gameproject
{
    partial class PinPon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Pc = new System.Windows.Forms.PictureBox();
            this.ball = new System.Windows.Forms.PictureBox();
            this.Oyuncu = new System.Windows.Forms.PictureBox();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Pc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Oyuncu)).BeginInit();
            this.SuspendLayout();
            // 
            // Pc
            // 
            this.Pc.Image = global::gameproject.Properties.Resources.computer;
            this.Pc.Location = new System.Drawing.Point(758, 110);
            this.Pc.Name = "Pc";
            this.Pc.Size = new System.Drawing.Size(30, 144);
            this.Pc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pc.TabIndex = 2;
            this.Pc.TabStop = false;
            // 
            // ball
            // 
            this.ball.Image = global::gameproject.Properties.Resources.ball;
            this.ball.Location = new System.Drawing.Point(435, 189);
            this.ball.Name = "ball";
            this.ball.Size = new System.Drawing.Size(30, 30);
            this.ball.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ball.TabIndex = 1;
            this.ball.TabStop = false;
            // 
            // Oyuncu
            // 
            this.Oyuncu.Image = global::gameproject.Properties.Resources.player;
            this.Oyuncu.Location = new System.Drawing.Point(12, 110);
            this.Oyuncu.Name = "Oyuncu";
            this.Oyuncu.Size = new System.Drawing.Size(30, 144);
            this.Oyuncu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Oyuncu.TabIndex = 0;
            this.Oyuncu.TabStop = false;
            this.Oyuncu.Click += new System.EventHandler(this.Oyuncu_Click);
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 20;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimerEvent);
            // 
            // PinPon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Pc);
            this.Controls.Add(this.ball);
            this.Controls.Add(this.Oyuncu);
            this.Name = "PinPon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PinPon";
            this.Load += new System.EventHandler(this.PinPon_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.Pc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Oyuncu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Oyuncu;
        private System.Windows.Forms.PictureBox ball;
        private System.Windows.Forms.PictureBox Pc;
        private System.Windows.Forms.Timer GameTimer;
    }
}