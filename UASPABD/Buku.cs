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

namespace UASPABD
{
    public partial class Buku : Form
    {
        private string connectionString = "data source=Danan-Nitro;database=PinjamBuku;User ID=sa;Password=123";
        private SqlConnection connection;
        public Buku()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            this.FormClosed += Buku_FormClosed;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Buku_Load(object sender, EventArgs e)
        {
            
            LoadData();
            LoadDataKategori();
            LoadPenerbitData();
        }
        private void LoadDataKategori()
        {
            string query = "SELECT ID_kategori, Nama_Kategori FROM Kategori";
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                comboBox1.DataSource = dataTable;
                comboBox1.DisplayMember = "Nama_Kategori";
                comboBox1.ValueMember = "ID_kategori";
            }
        }
        private void LoadPenerbitData()
        {
            string query = "SELECT ID_penerbit, Nama_Penerbit FROM Penerbit";
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                comboBox2.DataSource = dataTable;
                comboBox2.DisplayMember = "Nama_Penerbit";
                comboBox2.ValueMember = "ID_penerbit";
            }
        }

        private void LoadData()
        {
            string query = "SELECT B.ID_buku, B.Judul, B.Penulis, B.Tahun_Terbit, K.Nama_Kategori, P.Nama_Penerbit " +
                           "FROM Buku B LEFT JOIN Kategori K ON B.ID_kategori = K.ID_kategori " +
                           "LEFT JOIN Penerbit P ON B.ID_penerbit = P.ID_penerbit";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
        }
        private void ClearTextBoxes()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int idKategori = Convert.ToInt32(comboBox1.SelectedValue);
            int idPenerbit = Convert.ToInt32(comboBox2.SelectedValue);
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Mohon isi semua data!");
                return;
            }

            string query = "INSERT INTO Buku (Judul, Penulis, Tahun_Terbit, ID_kategori, ID_penerbit) VALUES (@Judul, @Penulis, @TahunTerbit, @IDKategori, @IDPenerbit)";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Judul", textBox1.Text);
                command.Parameters.AddWithValue("@Penulis", textBox2.Text);
                command.Parameters.AddWithValue("@TahunTerbit", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@IDKategori", idKategori);
                command.Parameters.AddWithValue("@IDPenerbit", idPenerbit);

                connection.Open();
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Data berhasil ditambahkan");
            LoadData();
            ClearTextBoxes();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            int idKategori = Convert.ToInt32(comboBox1.SelectedValue);
            int idPenerbit = Convert.ToInt32(comboBox2.SelectedValue);
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih satu baris data!");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Mohon isi semua data!");
                return;
            }

            string query = "UPDATE Buku SET Judul = @Judul, Penulis = @Penulis, Tahun_Terbit = @TahunTerbit, ID_kategori = @IDKategori, ID_penerbit = @IDPenerbit WHERE ID_buku = @IDBuku";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Judul", textBox1.Text);
                command.Parameters.AddWithValue("@Penulis", textBox2.Text);
                command.Parameters.AddWithValue("@TahunTerbit", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@IDPenerbit", idPenerbit);
                command.Parameters.AddWithValue("@IDKategori", idKategori);
                command.Parameters.AddWithValue("@IDBuku", dataGridView1.SelectedRows[0].Cells["ID_buku"].Value);

                connection.Open();
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Data berhasil diupdate!");
            LoadData();
            ClearTextBoxes();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih satu baris data untuk melakukan hapus.");
                return;
            }

            string judul = dataGridView1.SelectedRows[0].Cells["Judul"].Value.ToString();
            DialogResult result = MessageBox.Show($"Apakah anda ingin menghapus '{judul}'?", "Konfirmasi Hapus", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM Buku WHERE ID_buku = @IDBuku";
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDBuku", dataGridView1.SelectedRows[0].Cells["ID_buku"].Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Data berhasil dihapus.");
                LoadData();
                ClearTextBoxes();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Buku_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 fh = new Form1();
            fh.Show();
            this.Hide();
        }
    }
}
