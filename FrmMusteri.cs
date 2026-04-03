using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Proje_SQL_DB
{
    public partial class FrmMusteri : Form
    {
        public FrmMusteri()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=YOUR_PC_DB\\SQLEXPRESS;Initial Catalog=SatisVT;Integrated Security=True;Trust Server Certificate=True");

        void Listele()
        {

            SqlCommand listele = new SqlCommand("SELECT * FROM TBLMUSTERI",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(listele);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void FrmMusteri_Load(object sender, EventArgs e)
        {
            Listele();
            baglanti.Open();
            SqlCommand veri_cek = new SqlCommand("SELECT * FROM TBL_SEHIRLER", baglanti);
            SqlDataReader dr = veri_cek.ExecuteReader();
            while(dr.Read())
            {
                CmbSehir.Items.Add(dr["SEHIRAD"]);
            }
            baglanti.Close();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand ekle = new SqlCommand("INSERT INTO TBLMUSTERI (MUSTERAD, MUSTERSOYAD, MUSTERSEHIR, MUSTERIBAKIYE) VALUES (@P1, @P2, @P3, @P4)", baglanti);
            ekle.Parameters.AddWithValue("@P1", TxtAd.Text);
            ekle.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            ekle.Parameters.AddWithValue("@P3", (CmbSehir.Text).ToUpper());
            ekle.Parameters.AddWithValue("@P4", decimal.Parse(TxtBakiye.Text));
            ekle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Kaydı Başarıyla Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbSehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtBakiye.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sil = new SqlCommand("DELETE FROM TBLMUSTERI WHERE MUSTERIID=@P1", baglanti);
            sil.Parameters.AddWithValue("@P1", TxtID.Text);
            sil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Kaydı Başarıyla Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("UPDATE TBLMUSTERI SET MUSTERAD=@P1, MUSTERSOYAD=@P2, MUSTERSEHIR=@P3, MUSTERIBAKIYE=@P4 WHERE MUSTERIID=@P5", baglanti);
            guncelle.Parameters.AddWithValue("@P1", TxtAd.Text);
            guncelle.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            guncelle.Parameters.AddWithValue("@P3", (CmbSehir.Text).ToUpper());
            guncelle.Parameters.AddWithValue("@P4", decimal.Parse(TxtBakiye.Text));
            guncelle.Parameters.AddWithValue("@P5", TxtID.Text);
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Kaydı Başarıyla Sistemde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLMUSTERI WHERE MUSTERAD=@P1", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
