using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UASPABD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Anggota fa = new Anggota();
            fa.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pustakawan fp = new Pustakawan();
            fp.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Buku fb = new Buku();
            fb.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Kategori fk = new Kategori();
            fk.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Penerbit fpe = new Penerbit();
            fpe.Show();
            this.Hide();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
