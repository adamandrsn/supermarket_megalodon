﻿using System;
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
    public partial class datamaster : Form
    {
        public datamaster()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnkaryawan_Click(object sender, EventArgs e)
        {
            Karyawan fm = new Karyawan();
            fm.Show();
            this.Hide();
        }

        private void btnpembelian_Click(object sender, EventArgs e)
        {
            Pembelian fm = new Pembelian();
            fm.Show();
            this.Hide();
        }

        private void btnbarang_Click(object sender, EventArgs e)
        {
            barang fm = new barang();
            fm.Show();
            this.Hide();
        }

        private void btnmerk_Click(object sender, EventArgs e)
        {
            merk fm = new merk();
            fm.Show();
            this.Hide();
        }

        private void btnjenis_Click(object sender, EventArgs e)
        {
            jenisbarang fm = new jenisbarang();
            fm.Show();
            this.Hide();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            homepage fm = new homepage();
            fm.Show();
            this.Hide();
        }
    }
}
