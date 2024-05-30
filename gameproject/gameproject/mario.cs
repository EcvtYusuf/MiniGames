using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace gameproject
{
    public partial class mario : Form
    {
        private void mario_Load(object sender, EventArgs e)
        {

        }
        bool solaGit, sagaGit, ziplama, anahtarVar;

        int ziplamaHizi = 10;
        int kuvvet = 8;
        int skor = 0;

        int oyuncuHizi = 18;
        int arkaPlanHizi = 8;

        int kullaniciID; // Gerçek kullanıcı ID'si

        // Veritabanı bağlantı dizesi
        string connectionString = "Server=DESKTOP-4VP0JLO\\SQLEXPRESS;Database=gaming;Integrated Security=True;";

        public mario(int kullaniciID)
        {
            InitializeComponent();
            this.kullaniciID = kullaniciID; // Gerçek kullanıcı ID'sini al
            this.KeyDown += KeyIsDown;
            this.KeyUp += KeyIsUp;
            this.FormClosed += OyundanCik;

            GameTimer.Start();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            label1.Text = "Skor : " + skor;

            // Zıplama ve yerçekimi etkisi
            if (ziplama)
            {
                ziplamaHizi = -12;
                kuvvet -= 1;
            }
            else
            {
                ziplamaHizi = 12;
            }

            // Oyuncunun düşüşü
            oyuncu.Top += ziplamaHizi;

            // Yerde kalma kontrolü
            if (oyuncu.Top + oyuncu.Height > this.ClientSize.Height)
            {
                GameTimer.Stop();
                MessageBox.Show("Kaybettin! Tekrar oynamak için tamama basınız.");
                RestartGame();
            }

            // Zıplama sırasında üst sınır kontrolü
            if (oyuncu.Top < 0)
            {
                oyuncu.Top = 0;
                ziplamaHizi = 12; // Yerçekimi etkisi
                ziplama = false; // Zıplamayı durdur
            }

            // Oyuncunun hareketi ve arka plan kaydırması
            if (solaGit)
            {
                if (oyuncu.Left > this.ClientSize.Width / 2 || arkaplan.Left >= 0)
                {
                    oyuncu.Left -= oyuncuHizi;
                }
                else if (arkaplan.Left < 0)
                {
                    arkaplan.Left += arkaPlanHizi;
                    hareketMekanizması("forward");
                }
            }
            if (sagaGit)
            {
                if (oyuncu.Left < this.ClientSize.Width / 2 || arkaplan.Left + arkaplan.Width <= this.ClientSize.Width)
                {
                    oyuncu.Left += oyuncuHizi;
                }
                else if (arkaplan.Left + arkaplan.Width > this.ClientSize.Width)
                {
                    arkaplan.Left -= arkaPlanHizi;
                    hareketMekanizması("back");
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    if (oyuncu.Bounds.IntersectsWith(x.Bounds) && !ziplama)
                    {
                        kuvvet = 8;
                        oyuncu.Top = x.Top - oyuncu.Height;
                        ziplamaHizi = 0;
                    }
                    x.BringToFront();
                }
                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    if (oyuncu.Bounds.IntersectsWith(x.Bounds) && x.Visible)
                    {
                        x.Visible = false;
                        skor += 1;
                    }
                }
            }

            if (oyuncu.Bounds.IntersectsWith(key.Bounds))
            {
                key.Visible = false;
                anahtarVar = true;
            }
            if (oyuncu.Bounds.IntersectsWith(kapı.Bounds) && anahtarVar)
            {
                KaydetSkor();
                kapı.Image = Properties.Resources.door_open;
                GameTimer.Stop();
                MessageBox.Show("Tebrikler! Macerayı başarıyla tamamladın. Tekrar oynamak için tamama basın.");
                RestartGame();
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                solaGit = true;
            }
            if (e.KeyCode == Keys.D)
            {
                sagaGit = true;
            }
            if (e.KeyCode == Keys.Space && !ziplama)
            {
                ziplama = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                solaGit = false;
            }
            if (e.KeyCode == Keys.D)
            {
                sagaGit = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                ziplama = false;
            }
        }

        private void OyundanCik(object sender, FormClosedEventArgs e)
        {
            this.Close();
            
        }

        private void RestartGame()
        {    
            mario mr = new mario(kullaniciID);
            mr.ShowDialog();
            
        }

        private void hareketMekanizması(string yon)
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && ((string)x.Tag == "platform" || (string)x.Tag == "coin" || (string)x.Tag == "key" || (string)x.Tag == "door"))
                {
                    if (yon == "back")
                    {
                        x.Left -= arkaPlanHizi;
                    }
                    if (yon == "forward")
                    {
                        x.Left += arkaPlanHizi;
                    }
                }
            }
        }
        private void KaydetSkor()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    string sorgu = "INSERT INTO Skorlar (KullaniciID, OyunID, Skor) VALUES (@KullaniciID, @OyunID, @Skor)";

                    int oyunID = 3; // Flappy Bird oyununun ID'si

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
