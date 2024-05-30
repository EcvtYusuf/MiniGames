using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameproject
{
    public partial class PinPon : Form
    {
        int ball_x_hız = 4;
        int ball_y_hız = 4;
        int hız = 2;
        Random rand = new Random();
        bool Assagı, Yukarı;
        int pc_hız_degisimi = 20;
        int oyuncu_skoru = 0;
        int pc_skoru = 0;
        int oyuncu_hızı = 8;
        int skor = 0;
        int[] i = { 5, 6, 8, 9 };
        int[] j = { 10, 9, 8, 11, 12 };
        int kullaniciID;
        string connectionString = "Server=DESKTOP-4VP0JLO\\SQLEXPRESS;Database=gaming;Integrated Security=True;";

        private Timer hareketTimer;

        public PinPon(int kullaniciID)
        {
            this.kullaniciID = kullaniciID;
            InitializeComponent();

            // Hareket kontrolü için timer oluşturma
            hareketTimer = new Timer();
            hareketTimer.Interval = 20; // 20 ms'de bir çalışacak
            hareketTimer.Tick += HareketTimer_Tick;
            hareketTimer.Start();
        }

        private void Oyuncu_Click(object sender, EventArgs e)
        {

        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            ball.Top -= ball_y_hız;
            ball.Left -= ball_x_hız;
            this.Text = "Player Score: " + oyuncu_skoru + " -- Computer Score: " + pc_skoru;

            if (ball.Top < 0 || ball.Bottom > this.ClientSize.Height)
            {
                ball_y_hız = -ball_y_hız;
            }

            if (ball.Left < -2)
            {
                ball.Left = 300;
                ball_x_hız = -ball_x_hız;
                pc_skoru++;
            }

            if (ball.Right > this.ClientSize.Width + 2)
            {
                ball.Left = 300;
                ball_x_hız = -ball_x_hız;
                oyuncu_skoru++;
            }

            if (Pc.Top <= 1)
            {
                Pc.Top = 0;
            }
            else if (Pc.Bottom >= this.ClientSize.Height)
            {
                Pc.Top = this.ClientSize.Height - Pc.Height;
            }

            if (ball.Top < Pc.Top + (Pc.Height / 2) && ball.Left > 300)
            {
                Pc.Top -= hız;
            }

            if (ball.Top > Pc.Top + (Pc.Height / 2) && ball.Left > 300)
            {
                Pc.Top += hız;
            }

            pc_hız_degisimi -= 1;
            if (pc_hız_degisimi < 0)
            {
                hız = i[rand.Next(i.Length)];
                pc_hız_degisimi = 50;
            }

            carpismaKontrolu(ball, Oyuncu, Oyuncu.Right + 5);
            carpismaKontrolu(ball, Pc, Pc.Left - 35);

            if (pc_skoru >=3)
            {
                OyunBitti("Üzgünüm Kaybettin Dostum.");
            }
            else if (oyuncu_skoru >= 1)

            {
                skor += 15;
                KaydetSkor();  
                OyunBitti("Bravo Kazandın :) ");
            }
        }

        private void OyunBitti(string message)
        {
           
            GameTimer.Stop();
            DialogResult result = MessageBox.Show(message + "\nDevam etmek istiyor musun?", "Oyun Bitti", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Oyunu yeniden başlat
                pc_skoru = 0;
                oyuncu_skoru = 0;
                ball_x_hız = ball_y_hız = 4;
                pc_hız_degisimi = 50;
                ball.Left = this.ClientSize.Width / 2;
                ball.Top = this.ClientSize.Height / 2;
                GameTimer.Start();
            }
            else if (result == DialogResult.No)
            {
                // Formu kapat
                this.Close();
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                Assagı = true;
            }
            if (e.KeyCode == Keys.W)
            {
                Yukarı = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                Assagı = false;
            }
            if (e.KeyCode == Keys.W)
            {
                Yukarı = false;
            }
        }

        private void HareketTimer_Tick(object sender, EventArgs e)
        {
            if (Assagı && Oyuncu.Top + Oyuncu.Height < this.ClientSize.Height)
            {
                Oyuncu.Top += oyuncu_hızı;
            }
            if (Yukarı && Oyuncu.Top > 0)
            {
                Oyuncu.Top -= oyuncu_hızı;
            }
        }

        private void carpismaKontrolu(PictureBox PicOne, PictureBox PicTwo, int offset)
        {
            if (PicOne.Bounds.IntersectsWith(PicTwo.Bounds))
            {
                PicOne.Left = offset;
                int x = j[rand.Next(j.Length)];
                int y = j[rand.Next(j.Length)];
                if (ball_x_hız < 0)
                {
                    ball_x_hız = x;
                }
                else
                {
                    ball_x_hız = -x;
                }
                if (ball_y_hız < 0)
                {
                    ball_y_hız = -y;
                }
                else
                {
                    ball_y_hız = y;
                }
            }
        }

        private void PinPon_Load(object sender, EventArgs e)
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

                    int oyunID = 5;

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
