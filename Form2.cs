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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=YOUR_PC_SQL_NAME;Initial Catalog=SatisVT;Integrated Security=True;Trust Server Certificate=True");
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            SqlCommand listele = new SqlCommand("SELECT * FROM TBLKATEGORI", baglanti);
            //dataadapter veri bağlayıcıdır.
            SqlDataAdapter da = new SqlDataAdapter(listele);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kaydet = new SqlCommand("INSERT INTO TBLKATEGORI (KATEGORIAD) VALUES (@p1)", baglanti);
            kaydet.Parameters.AddWithValue("@p1", TxtAd.Text);
            //SAVE ETMEK İÇİN KULLANIYORUZ.(SORGUYU ÇALIŞTIR VE VERİTABANINA YANSIT)
            kaydet.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori Değeriniz Başarıyla Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sil = new SqlCommand("DELETE FROM TBLKATEGORI WHERE KATEGORIID=@p1", baglanti);
            sil.Parameters.AddWithValue("@p1", TxtID.Text);
            sil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori Değeriniz Başarıyla Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kaydet = new SqlCommand("UPDATE TBLKATEGORI SET KATEGORIAD=@p1 WHERE KATEGORIID=@p2", baglanti);
            kaydet.Parameters.AddWithValue("@p1", TxtAd.Text);
            kaydet.Parameters.AddWithValue("@p2", TxtID.Text);
            kaydet.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori Değeriniz Başarıyla Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
