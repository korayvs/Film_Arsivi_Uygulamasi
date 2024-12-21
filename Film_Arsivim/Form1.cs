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
using CefSharp;
using CefSharp.WinForms;

namespace Film_Arsivim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-F1A12T8\KORAY;Initial Catalog=FilmArsivim;Integrated Security=True");

        void filmler()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLFILMLER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        
        ChromiumWebBrowser tarayici;
        private void Form1_Load(object sender, EventArgs e)
        {
            filmler();

            tarayici = new ChromiumWebBrowser(); 
            this.PnlBrowser.Controls.Add(tarayici);                                                      
            tarayici.Dock = DockStyle.Fill;
            
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Insert into TBLFILMLER (AD, KATEGORI, LINK) Values (@p1, @p2, @p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtFilmAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtKategori.Text);
            komut.Parameters.AddWithValue("@p3", TxtLink.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Film listeniz eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            filmler();
        }
        ChromiumWebBrowser chromiumWebBrowser;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            string link = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            chromiumWebBrowser = new ChromiumWebBrowser(link);
            this.groupBox2.Controls.Add(chromiumWebBrowser);
            chromiumWebBrowser.Dock = DockStyle.Fill;
            chromiumWebBrowser.Load(link);
        }

        private void BtnHak_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu proje Koray Vatansever tarafından 26.11.2024'de kodlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnRenkDeg_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int red = rand.Next(0, 255);
            int green = rand.Next(0, 255);
            int blue = rand.Next(0, 255);

            this.BackColor = Color.FromArgb(red, green, blue);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tarayici.Load("www.youtube.com");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tarayici.Load(TxtAdresSatiri.Text);
        }

        private void CmbAdresler_SelectedIndexChanged(object sender, EventArgs e)
        {
            tarayici.Load(CmbAdresler.SelectedItem.ToString());
        }

        private void BtnTamEkr_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            groupBox2.Size = new Size(1920, 1080);
            groupBox2.Location = new Point(0, 0);
        }
    }
}
