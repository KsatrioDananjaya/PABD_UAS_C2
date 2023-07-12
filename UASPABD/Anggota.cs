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
    public partial class Anggota : Form
    {
        private string connectionString = "data source=Danan-Nitro;database=PinjamBuku;User ID=sa;Password=123";
        private SqlConnection connection;
        private DataSet dataSet;
        private SqlDataAdapter anggotaAdapter;
        private SqlDataAdapter bukuAdapter;
        private SqlDataAdapter pustakawanAdapter;

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }      

        public Anggota()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            this.FormClosed += Anggota_FormClosed;
        }

        private void Anggota_Load(object sender, EventArgs e)
        {
            dataSet = new DataSet();

            connection = new SqlConnection(connectionString);

            anggotaAdapter = new SqlDataAdapter("SELECT * FROM Anggota", connection);
            bukuAdapter = new SqlDataAdapter("SELECT * FROM Buku", connection);
            pustakawanAdapter = new SqlDataAdapter("SELECT * FROM Pustakawan", connection);

            anggotaAdapter.Fill(dataSet, "Anggota");
            bukuAdapter.Fill(dataSet, "Buku");
            pustakawanAdapter.Fill(dataSet, "Pustakawan");

            bindingNavigator1.BindingSource = bindingSource1;
            bindingSource1.DataMember = "Anggota";
            bindingSource1.DataSource = dataSet;

            textBox1.DataBindings.Add("Text", bindingSource1, "ID_anggota");
            textBox2.DataBindings.Add("Text", bindingSource1, "nama_anggota");
            textBox3.DataBindings.Add("Text", bindingSource1, "alamat");

            comboBox1.DataSource = dataSet.Tables["Buku"];
            comboBox1.DisplayMember = "judul";
            comboBox1.ValueMember = "ID_buku";

            comboBox2.DataSource = dataSet.Tables["Pustakawan"];
            comboBox2.DisplayMember = "nama";
            comboBox2.ValueMember = "ID_pustakawan";
        }
  

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                bindingSource1.EndEdit();

                SqlCommandBuilder builder = new SqlCommandBuilder(anggotaAdapter);
                anggotaAdapter.Update(dataSet, "Anggota");

                MessageBox.Show("Data anggota berhasil disimpan.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindingSource1.AddNew();
            textBox1.Enabled = true;
            textBox1.Focus();
        }
        
        private void Anggota_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 fh = new Form1();
            fh.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }
        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah Anda yakin ingin menghapus anggota ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingSource1.RemoveCurrent();
                SqlCommandBuilder builder = new SqlCommandBuilder(anggotaAdapter);
                anggotaAdapter.Update(dataSet, "Anggota");
                MessageBox.Show("Data anggota berhasil dihapus.");
            }
        }
        private void FormAnggota_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dataSet.HasChanges())
            {
                try
                {
                    SqlCommandBuilder builder = new SqlCommandBuilder(anggotaAdapter);
                    anggotaAdapter.Update(dataSet, "Anggota");
                    MessageBox.Show("Perubahan data anggota berhasil disimpan.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
