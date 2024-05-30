using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace gameproject
{
    public partial class skortablosu : Form
    {
        // Veritabanı bağlantı dizesi
        string connectionString = "Server=DESKTOP-4VP0JLO\\SQLEXPRESS;Database=gaming;Integrated Security=True;";

        public skortablosu()
        {
            InitializeComponent();
        }

        private void skortablosu_Load(object sender, EventArgs e)
        {
            SkorListesiniGuncelle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Tüm kullanıcılar için ToplamSkor değerlerini güncelle
            TumKullanicilarinToplamSkorunuGuncelle();
            // Skor listesi güncelle
            SkorListesiniGuncelle();
        }

        private void SkorListesiniGuncelle()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    string sorgu = "SELECT KullaniciAdi, ToplamSkor FROM Kullanicilar ORDER BY ToplamSkor DESC";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        using (SqlDataReader reader = komut.ExecuteReader())
                        {
                            int siralama = 1;
                            listBox1.Items.Clear(); // ListBox'ı temizle
                            while (reader.Read())
                            {
                                string kullaniciAdi = reader.GetString(0);
                                int? toplamSkor = reader.IsDBNull(1) ? null : (int?)reader.GetInt32(1);

                                string siralanmisVeri = $"{siralama}. {kullaniciAdi}: {(toplamSkor != null ? toplamSkor.ToString() : "0")}";
                                listBox1.Items.Add(siralanmisVeri);

                                siralama++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri okunurken bir hata oluştu: " + ex.Message);
            }
        }

        private void TumKullanicilarinToplamSkorunuGuncelle()
        {
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                // Tüm kullanıcılar için ToplamSkor güncelle
                string toplamSkorGuncelleSorgu = @"
                    UPDATE Kullanicilar
                    SET ToplamSkor = (
                        SELECT SUM(Skor)
                        FROM Skorlar
                        WHERE Skorlar.KullaniciID = Kullanicilar.KullaniciID
                    )";

                using (SqlCommand toplamSkorGuncelleKomut = new SqlCommand(toplamSkorGuncelleSorgu, baglanti))
                {
                    toplamSkorGuncelleKomut.ExecuteNonQuery();
                }
            }
        }
    }
}
