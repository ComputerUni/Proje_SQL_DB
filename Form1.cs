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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-213H3J8\\SQLEXPRESS;Initial Catalog=SatisVT;Integrated Security=True;Trust Server Certificate=True");
        private void BtnKategoriler_Click(object sender, EventArgs e)
        {
            FrmUrunler fr = new FrmUrunler();
            fr.Show();
        }

        private void BtnMusteriler_Click(object sender, EventArgs e)
        {
            FrmMusteri frm = new FrmMusteri();
            frm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Ürünlerin Durum Seviyesi

            //chart1.Series["Akdeniz"].Points.AddXY("Adana", 25);
            //chart1.Series["Akdeniz"].Points.AddXY("Isparta", 15);
            SqlCommand komut = new SqlCommand("EXECUTE STOKKONTROL", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Grafiğe Veri Çekme
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT KATEGORIAD, COUNT(*) FROM TBLKATEGORI INNER JOIN TBLURUNLER ON TBLKATEGORI.KATEGORIID = TBLURUNLER.KATEGORI GROUP BY KATEGORIAD", baglanti);
            SqlDataReader dr = komut2.ExecuteReader();
            while (dr.Read())
            {
                chart1.Series["Kategoriler"].Points.AddXY(dr[0], dr[1]);
            }
            baglanti.Close();

            //Grafiğe Veri Çekme-2
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("SELECT MUSTERSEHIR, COUNT(*) FROM TBLMUSTERI GROUP BY MUSTERSEHIR", baglanti);
            SqlDataReader dr2 = komut3.ExecuteReader();
            while (dr2.Read())
            {
                chart2.Series["Sehirler"].Points.AddXY(dr2[0], dr2[1]);
            }
            baglanti.Close();


        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
