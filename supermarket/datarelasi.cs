using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace supermarket
{
    public partial class datarelasi : Form
    {
        public datarelasi()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            homepage fm = new homepage();
            fm.Show();
            this.Hide();
        }

        private void btnmelayani_Click(object sender, EventArgs e)
        {
            melayani fm = new melayani();
            fm.Show();
            this.Hide();
        }

        private void btnpembelian_Click(object sender, EventArgs e)
        {
            terdiri fm = new terdiri();
            fm.Show();
            this.Hide();
        }

        private void btnbarang_Click(object sender, EventArgs e)
        {
            mempunyai fm = new mempunyai();
            fm.Show();
            this.Hide();
        }

        private void btnmerk_Click(object sender, EventArgs e)
        {
            memiliki fm = new memiliki();
            fm.Show();
            this.Hide();
        }
    }
}
