using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace gameproject
{
    public partial class t_rex : Form
    {
        bool ziplama = false;
        int ziplamaHızı = 12;
        int kuvvet = 12;
        int skor = 0;
        int kaktusHız = 10;
        Random rd = new Random();
        int pozisyon;
        bool Oyun_Bitti = false;
        int kullaniciID;
        string connectionString = "Server=DESKTOP-4VP0JLO\\SQLEXPRESS;Database=gaming;Integrated Security=True;";

        public t_rex(int kullaniciID)
        {
            this.kullaniciID = kullaniciID;
            InitializeComponent();
            YenidenBaslat();
        }

        private void AnaOyunEvent(object sender, EventArgs e)
        {
            trex.Top += ziplamaHızı;
            label1.Text = "Skor : " + skor;

            if (ziplama == true && kuvvet < 0)
            {
                ziplama = false;
            }

            if (ziplama == true)
            {
                ziplamaHızı = -12;
                kuvvet -= 1;
            }
            else
            {
                ziplamaHızı = 12;
            }

            if (trex.Top > 366 && ziplama == false)
            {
                kuvvet = 12;
                trex.Top = 367;
                ziplamaHızı = 0;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= kaktusHız;

                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + rd.Next(300, 600);
                        skor++;
                    }

                    if (trex.Bounds.IntersectsWith(x.Bounds))
                    {
                        KaydetSkor();

                        timer1.Stop();
                        trex.Image = Properties.Resources.dead;
                        label2.Visible = true;
                        label2.Text = "Tekrar Başlatmak İçin R Tuşuna Basınız";
                        Oyun_Bitti = true;
                    }

                    if (skor > 5)
                    {
                        kaktusHız = 12;
                    }
                    if (skor > 10)
                    {
                        kaktusHız = 14;
                    }
                    if (skor > 15)
                    {
                        kaktusHız = 16;
                    }
                    if (skor > 20)
                    {
                        kaktusHız = 18;
                    }
                }
            }
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && ziplama == false)
            {
                ziplama = true;
            }
        }

        private void Key_Up(object sender, KeyEventArgs e)
        {
            if (ziplama == true)
            {
                ziplama = false;
            }
            if (e.KeyCode == Keys.R && Oyun_Bitti == true)
            {
                label2.Visible = false;
                YenidenBaslat();
            }
        }

        private void YenidenBaslat()
        {
            kuvvet = 12;
            ziplamaHızı = 0;
            ziplama = false;
            skor = 0;
            kaktusHız = 10;
            label1.Text = "Skor : " + skor;
            trex.Image = Properties.Resources.running;
            Oyun_Bitti = false;
            trex.Top = 356;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    pozisyon = this.ClientSize.Width + rd.Next(300, 600);
                    x.Left = pozisyon;
                }
            }
            timer1.Start();
        }

        private void t_rex_Load(object sender, EventArgs e)
        {

        }

        private void KaydetSkor()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    string sorgu = "INSERT INTO Skorlar (KullaniciID, OyunID, Skor) VALUES (@KullaniciID, @OyunID, @Skor)";

                    int oyunID = 4;

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                        komut.Parameters.AddWithValue("@OyunID", oyunID);
                        komut.Parameters.AddWithValue("@Skor", skor);

                        komut.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Skor kaydedilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
