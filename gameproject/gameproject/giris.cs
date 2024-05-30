using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace gameproject
{
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }

        private void giris_Load(object sender, EventArgs e)
        {
            // Buraya formun yüklendiğinde yapılmasını istediğiniz işlemleri ekleyebilirsiniz
            textBox2.PasswordChar = '*';
        }

        private void label4_Click(object sender, EventArgs e)
        {
            kayit kayitForm = new kayit();
            kayitForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz.");
                return;
            }

            try
            {
                // Veritabanı bağlantı dizesi
                string connectionString = "Server=DESKTOP-4VP0JLO\\SQLEXPRESS;Database=gaming;Integrated Security=True;";

                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    string sorgu = "SELECT KullaniciID FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                        komut.Parameters.AddWithValue("@Sifre", sifre);

                        object sonuc = komut.ExecuteScalar();

                        if (sonuc != null)
                        {
                            int kullaniciID = Convert.ToInt32(sonuc);
                            MessageBox.Show("Giriş başarılı!");
                            // Ana menü formuna yönlendirme
                            anamenu anaMenuForm = new anamenu(kullaniciID);
                            anaMenuForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre hatalı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0'; // işaretlendiğinde metin açık olarak gösterilecek
            }
            else
            {
                textBox2.PasswordChar = '*'; // işaret kaldırıldığında metin yıldız karakterleriyle gizlenecek
            }
        }
    }
}