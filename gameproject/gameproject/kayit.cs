using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace gameproject
{
    public partial class kayit : Form
    {
        public kayit()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void kayit_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;
            string eposta = textBox3.Text;

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre) || string.IsNullOrEmpty(eposta))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            try
            {
                // Veritabanı bağlantı dizesi
                string connectionString = "Server=DESKTOP-4VP0JLO\\SQLEXPRESS;Database=gaming;Integrated Security=True;";

                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    string sorgu = "INSERT INTO Kullanicilar (KullaniciAdi, Sifre, EPosta) VALUES (@KullaniciAdi, @Sifre, @EPosta)";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                        komut.Parameters.AddWithValue("@Sifre", sifre);
                        komut.Parameters.AddWithValue("@EPosta", eposta);

                        int result = komut.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Kayıt başarılı!");
                            
                            // GirisForm girisForm = new GirisForm();
                            // girisForm.Show();
                            // this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt başarısız.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void sifreGosterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sifreGosterCheckBox.Checked)
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
