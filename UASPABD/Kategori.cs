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
    public partial class Kategori : Form
    {
        private string connectionString = "data source=Danan-Nitro;" + "database=PinjamBuku;User ID=sa;Password=123";
        private SqlConnection connection;
        public Kategori()
        {
            InitializeComponent();
            this.FormClosed += Kategori_FormClosed;

        }
        private void LoadData()
        {
            string query = "SELECT ID_kategori, Nama_Kategori FROM Kategori";
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void ClearTextBoxes()
        {
            textBox1.Text = "";
        }

        private void Kategori_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Mohon isi Data!");
                return;
            }

            string query = "INSERT INTO Kategori (Nama_Kategori) VALUES (@Nama)";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nama", textBox1.Text);

                connection.Open();
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Kategori berhasil ditambahkan.");
            LoadData();
            ClearTextBoxes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih satu baris data!");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Mohon isi semua data!");
                return;
            }

            string query = "UPDATE Kategori SET Nama_Kategori = @Nama WHERE ID_kategori = @IDKategori";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nama", textBox1.Text);
                command.Parameters.AddWithValue("@IDKategori", dataGridView1.SelectedRows[0].Cells["ID_kategori"].Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih yang ingin dihapus!");
                return;
            }

            string nama = dataGridView1.SelectedRows[0].Cells["Nama"].Value.ToString();
            DialogResult result = MessageBox.Show($"Ingin menghapus data '{nama}'?", "Konfirmasi Hapus", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM Kategori WHERE ID_Kategori = @IDKategori";
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDKategori", dataGridView1.SelectedRows[0].Cells["ID_kategori"].Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Data terhapus!");
                LoadData();
                ClearTextBoxes();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }
        private void Kategori_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 fh = new Form1();
            fh.Show();
            this.Hide();
        }

    }
}
