using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace gameproject
{
    public partial class at : Form
    {
        private int kullaniciID;
        private int skor = 0; // Kullanıcının skoru
        private int secilenAt = 0; // Seçilen at numarası
        private bool yarisBasladi = false; // Yarışın başlayıp başlamadığını belirten bayrak

        private Random rd = new Random();
        private int brauzak, ikiuzak, ucuzak, dortuzak;
        private int countdown = 3; // Geri sayım başlangıç değeri

        // Veritabanı bağlantı dizesi
        string connectionString = "Server=DESKTOP-4VP0JLO\\SQLEXPRESS;Database=gaming;Integrated Security=True;";

        public at(int kullaniciID)
        {
            InitializeComponent();
            this.kullaniciID = kullaniciID;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int brgenislik = pictureBox1.Width;
            int ikgenislik = pictureBox2.Width;
            int ucgenislik = pictureBox3.Width;
            int drtgenislik = pictureBox4.Width;
            int bitis = label5.Left;

            pictureBox1.Left += rd.Next(5, 25);
            pictureBox4.Left += rd.Next(5, 25);
            pictureBox3.Left += rd.Next(5, 25);
            pictureBox2.Left += rd.Next(5, 25);

            // Kazananı kontrol etmek için bir flag
            bool kazananBulundu = false;

            // Her atın bitiş çizgisine ulaşıp ulaşmadığını kontrol et
            if (!kazananBulundu && brgenislik + pictureBox1.Left >= bitis)
            {
                kazananBulundu = true;
                timer1.Enabled = false;
                YarisiBitir(1);
            }
            else if (!kazananBulundu && ikgenislik + pictureBox2.Left >= bitis)
            {
                kazananBulundu = true;
                timer1.Enabled = false;
                YarisiBitir(2);
            }
            else if (!kazananBulundu && ucgenislik + pictureBox3.Left >= bitis)
            {
                kazananBulundu = true;
                timer1.Enabled = false;
                YarisiBitir(3);
            }
            else if (!kazananBulundu && drtgenislik + pictureBox4.Left >= bitis)
            {
                kazananBulundu = true;
                timer1.Enabled = false;
                YarisiBitir(4);
            }

            string durum = "";
            if (pictureBox1.Left > pictureBox4.Left && pictureBox1.Left > pictureBox3.Left && pictureBox1.Left > pictureBox2.Left)
            {
                durum = "1 Numaralı At Önde İlerliyor";
            }
            else if (pictureBox2.Left > pictureBox1.Left && pictureBox2.Left > pictureBox3.Left && pictureBox2.Left > pictureBox4.Left)
            {
                durum = "2 Numaralı At Önde İlerliyor";
            }
            else if (pictureBox3.Left > pictureBox1.Left && pictureBox3.Left > pictureBox4.Left && pictureBox3.Left > pictureBox2.Left)
            {
                durum = "3 Numaralı At Önde İlerliyor";
            }
            else if (pictureBox4.Left > pictureBox1.Left && pictureBox4.Left > pictureBox2.Left && pictureBox4.Left > pictureBox3.Left)
            {
                durum = "4 Numaralı At Önde İlerliyor";
            }
            else
            {
                durum = "Atlar Aynı Sırada Devam Ediyor";
            }
            label10.Text = durum;
        }

        private void YarisiBitir(int kazananAt)
        {
            if (secilenAt == kazananAt)
            {
                skor += 15;
                MessageBox.Show($"{kazananAt} Numaralı At Kazandı! Tebrikler! +15 Skor Kazandınız");

                // Kazanan atı seçtiyse skoru kaydet
                KaydetSkor();
            }
            else
            {
                MessageBox.Show($"{kazananAt} Numaralı At Kazandı! Maalesef Seçilen At Değil.");
            }
            bitir();
        }

        private void KaydetSkor()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    string sorgu = "INSERT INTO Skorlar (KullaniciID, OyunID, Skor) VALUES (@KullaniciID, @OyunID, @Skor)";

                    int oyunID = 1; // Oyun ID'si

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

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selectedCheckBox = sender as CheckBox;

            if (selectedCheckBox.Checked)
            {
                secilenAt = Convert.ToInt32(selectedCheckBox.Tag);

                // Diğer CheckBox'ları devre dışı bırak
                foreach (Control control in this.Controls)
                {
                    if (control is CheckBox checkBox && checkBox != selectedCheckBox)
                    {
                        checkBox.CheckedChanged -= checkBox_CheckedChanged;
                        checkBox.Checked = false;
                        checkBox.CheckedChanged += checkBox_CheckedChanged;
                    }
                }
            }
            else
            {
                secilenAt = 0;
            }
        }

        private void Baslat()
        {
            if (yarisBasladi || secilenAt == 0)
                return;

            pictureBox1.Left = brauzak;
            pictureBox2.Left = ikiuzak;
            pictureBox3.Left = ucuzak;
            pictureBox4.Left = dortuzak;

            countdown = 3;
            labelCountdown.Text = countdown.ToString();
            labelCountdown.Visible = true;
            timerCountdown.Enabled = true;

            timer1.Enabled = false;
            yarisBasladi = true;

            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
        }

        private void bitir()
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;

            yarisBasladi = false;
            secilenAt = 0;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (secilenAt == 0)
            {
                MessageBox.Show("Lütfen bir at seçiniz!");
                return;
            }

            Baslat();
        }

        private void at_Load(object sender, EventArgs e)
        {
            brauzak = pictureBox1.Left;
            ikiuzak = pictureBox2.Left;
            ucuzak = pictureBox3.Left;
            dortuzak = pictureBox4.Left;

            // CheckBox'lara Tag değerlerini atayalım
            checkBox1.Tag = 1;
            checkBox2.Tag = 2;
            checkBox3.Tag = 3;
            checkBox4.Tag = 4;

            // CheckBox'ların CheckedChanged olayını bağlayalım
            checkBox1.CheckedChanged += checkBox_CheckedChanged;
            checkBox2.CheckedChanged += checkBox_CheckedChanged;
            checkBox3.CheckedChanged += checkBox_CheckedChanged;
            checkBox4.CheckedChanged += checkBox_CheckedChanged;
        }

        private void timerCountdown_Tick(object sender, EventArgs e)
        {
            if (countdown > 0)
            {
                labelCountdown.Text = countdown.ToString();
                countdown--;
            }
            else
            {
                timerCountdown.Enabled = false;
                labelCountdown.Visible = false;
                timer1.Enabled = true;
            }
        }
    }
}