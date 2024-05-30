using System;
using System.Windows.Forms;

namespace gameproject
{
    public partial class basla : Form
    {
        public basla()
        {
            InitializeComponent();
        }

        bool islem = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!islem)
            {
                this.Opacity += 0.009;
            }
            if (this.Opacity >= 1.0)
            {
                islem = true;
            }
            if (islem)
            {
                this.Opacity -= 0.009;
                if (this.Opacity <= 0)
                {
                    timer1.Enabled = false;
                    this.Hide();
                    giris gr = new giris();
                    gr.Show();
                }
            }
        }
    }
}
