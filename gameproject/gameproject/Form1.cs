using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace gameproject
{
    public partial class Form1 : Form
    {
        int boruhız = 8;
        int yercekimi = 5;
        int skor = 0;
        bool oyunDevamEdiyor = false;
        int kullaniciID; // Gerçek kullanıcı ID'si

        Label oyunBittiLabel;
        Label skorLabel;
        Button anaMenuButton;

        // Veritabanı bağlantı dizesi
        string connectionString = "Server=DESKTOP-4VP0JLO\\SQLEXPRESS;Database=gaming;Integrated Security=True;";

        public Form1(int kullaniciID)
        {
            InitializeComponent();
            this.kullaniciID = kullaniciID; // Gerçek kullanıcı ID'sini al
            InitializeGameOverControls();
            this.KeyDown += OyunAnahtarı;
            this.KeyUp += OyunAnahtarıKeyUp;
            this.FormClosed += Form1_FormClosed; // Form kapanırken skoru kaydet

            anaMenuButton = new Button();
            anaMenuButton.Text = "Ana Menüye Dön";
            anaMenuButton.Size = new Size(150, 50);
            anaMenuButton.Location = new Point((this.ClientSize.Width - anaMenuButton.Width) / 2, (this.ClientSize.Height - anaMenuButton.Height) / 2);
            anaMenuButton.Click += AnaMenuButton_Click;
            anaMenuButton.Visible = false;
            this.Controls.Add(anaMenuButton);
        }

        private void AnaMenuButton_Click(object sender, EventArgs e)
        {
            anamenu an = new anamenu();
            an.Show();
            this.Close();


            anaMenuButton.Visible = false;
        }

        private void InitializeGameOverControls()
        {
            oyunBittiLabel = new Label();
            oyunBittiLabel.Text = "Oyun Bitti";
            oyunBittiLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            oyunBittiLabel.Size = new Size(200, 50);
            oyunBittiLabel.TextAlign = ContentAlignment.MiddleCenter;
            oyunBittiLabel.Visible = false;
            this.Controls.Add(oyunBittiLabel);

            skorLabel = new Label();
            skorLabel.Text = "";
            skorLabel.Font = new Font("Arial", 16, FontStyle.Regular);
            skorLabel.Size = new Size(200, 30);
            skorLabel.TextAlign = ContentAlignment.MiddleCenter;
            skorLabel.Visible = false;
            this.Controls.Add(skorLabel);
        }

        private void timerEtkin(object sender, EventArgs e)
        {
            if (oyunDevamEdiyor)
            {
                flappybird.Top += yercekimi;
                altboru.Left -= boruhız;
                ustboru.Left -= boruhız;

                if (altboru.Left < -altboru.Width)
                {
                    altboru.Left = this.ClientSize.Width;
                    skor++;
                    skorLabel.Text = "Skor: " + skor;
                }

                if (ustboru.Left < -ustboru.Width)
                {
                    ustboru.Left = this.ClientSize.Width;
                    skor++;
                    skorLabel.Text = "Skor: " + skor;
                }

                if (flappybird.Bounds.IntersectsWith(altboru.Bounds) ||
                    flappybird.Bounds.IntersectsWith(ustboru.Bounds) ||
                    flappybird.Bounds.IntersectsWith(zemin.Bounds) ||
                    flappybird.Top < -25)
                {
                    oyunbitir();
                }
            }
        }

        private void OyunAnahtarı(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !oyunDevamEdiyor)
            {
                Baslat();
            }
            else if (e.KeyCode == Keys.Space && oyunDevamEdiyor)
            {
                YukariHareket();
            }
        }

        private void OyunAnahtarıKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && oyunDevamEdiyor)
            {
                YerçekimiNormal();
            }
        }

        private void Baslat()
        {
            oyunDevamEdiyor = true;
            timer1.Start();
            flappybird.Location = new Point(50, 150);
            altboru.Left = this.ClientSize.Width;
            ustboru.Left = this.ClientSize.Width;
            skor = 0;
            skorLabel.Text = "Skor: " + skor;
            skorLabel.Visible = true;
            YerçekimiNormal();
            oyunBittiLabel.Visible = false;
            anaMenuButton.Visible = false; // Oyun başladığında butonu gizle
        }

        private void YukariHareket()
        {
            yercekimi = -5;
        }

        private void YerçekimiNormal()
        {
            yercekimi = 5;
        }

        private void oyunbitir()
        {
            oyunDevamEdiyor = false;
            timer1.Stop();
            oyunBittiLabel.Text = "Oyun Bitti. Skor: " + skor;
            oyunBittiLabel.Location = new Point((this.ClientSize.Width - oyunBittiLabel.Width) / 2, this.ClientSize.Height / 4);
            oyunBittiLabel.Visible = true;
            skorLabel.Visible = true;
            anaMenuButton.Visible = true; // Oyun bittiğinde butonu görünür yap

            // Oyun bittiğinde skoru kaydet
            KaydetSkor();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Form kapanırken skoru kaydet
            KaydetSkor();
        }

        private void KaydetSkor()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    string sorgu = "INSERT INTO Skorlar (KullaniciID, OyunID, Skor) VALUES (@KullaniciID, @OyunID, @Skor)";

                    int oyunID = 2; // Flappy Bird oyununun ID'si

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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
