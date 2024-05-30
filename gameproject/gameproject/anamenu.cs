using System;
using System.Windows.Forms;

namespace gameproject
{
    public partial class anamenu : Form
    {
        private int kullaniciID; // Kullanıcı kimliği saklamak için bir değişken

        public anamenu(int kullaniciID)
        {
            InitializeComponent();
            this.kullaniciID = kullaniciID;
        }

        public anamenu()
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(kullaniciID);
            form1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            at atForm = new at(kullaniciID); // at formunu kullanıcı kimliği ile başlat
            atForm.ShowDialog();
        }

        private void anamenu_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PinPon ppForm = new PinPon(kullaniciID);
            ppForm.ShowDialog();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            t_rex tr = new t_rex(kullaniciID);
            tr.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mario mr = new mario(kullaniciID);
            mr.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            skortablosu sr = new skortablosu();
                sr.ShowDialog();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}