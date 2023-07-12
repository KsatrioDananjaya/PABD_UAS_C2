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
    public partial class Penerbit : Form
    {
        private string connectionString = "data source=Danan-Nitro;" + "database=PinjamBuku;User ID=sa;Password=123";
        private SqlConnection connection;
        public Penerbit()
        {
            InitializeComponent();
            this.FormClosed += Penerbit_FormClosed;
        }

        private void Penerbit_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            string query = "SELECT ID_Penerbit, Nama_Penerbit, Alamat FROM Penerbit";
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
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Mohon isi Data!");
                return;
            }

            string query = "INSERT INTO Penerbit (Nama_Penerbit, Alamat) VALUES (@Nama, @Alamat)";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nama", textBox1.Text);
                command.Parameters.AddWithValue("@Alamat", textBox2.Text);

                connection.Open();
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Data pustakawan berhasil ditambahkan.");
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

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Mohon isi semua data!");
                return;
            }

            string query = "UPDATE Penerbit SET Nama_Penerbit = @Nama, Alamat = @Alamat WHERE ID_penerbit = @IDPenerbit";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nama", textBox1.Text);
                command.Parameters.AddWithValue("@Alamat", textBox2.Text);
                command.Parameters.AddWithValue("@IDPenerbit", dataGridView1.SelectedRows[0].Cells["ID_penerbit"].Value);

                connection.Open();
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Data berhasil diupdate!");
            LoadData();
            ClearTextBoxes();
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
                string query = "DELETE FROM Penerbit WHERE ID_penerbit = @IDPenerbit";
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDPenerbit", dataGridView1.SelectedRows[0].Cells["ID_penerbit"].Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Data terhapus!");
                LoadData();
                ClearTextBoxes();
            }
        }
        private void Penerbit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 fh = new Form1();
            fh.Show();
            this.Hide();
        }
    }
}
