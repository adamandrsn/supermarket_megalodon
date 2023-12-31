﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace supermarket
{
    public partial class Pembelian : Form
    {
        private string stringConnection = "Data Source=AMSONUN;Initial Catalog=supermarket;Persist Security Info=True;User ID=sa;Password=123";
        private SqlConnection koneksi;
        public Pembelian()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }
        private void refreshform()
        {
            txtkodepem.Text = "";
            txtkodepem.Enabled = false;
            txttotalpem.Text = "";
            txttotalpem.Enabled = false;
            dttglpem.Text = "";
            dttglpem.Enabled = false;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
        }
        private void dataGridView()
        {
            koneksi.Open();
            string str = "select kode_pembelian, total_pembelian, tanggal_pembelian from dbo.pembelian";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }
        private void txtkodepem_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            txtkodepem.Enabled = true;
            txttotalpem.Enabled = true;
            dttglpem.Enabled = true;
            btnSave.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnopen_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnopen.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string kdPem = txtkodepem.Text.Trim();
            string ttlPem = txttotalpem.Text.Trim();
            DateTime tgl = dttglpem.Value;

            if (kdPem == "" || ttlPem == "")
            {
                MessageBox.Show("Harap lengkapi semua field", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "insert into dbo.pembelian (kode_pembelian, total_pembelian, tanggal_pembelian)" + "values(@kode_pembelian, @total_pembelian, @tanggal_pembelian)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("kode_pembelian", kdPem));
                cmd.Parameters.Add(new SqlParameter("total_pembelian", ttlPem));
                cmd.Parameters.Add(new SqlParameter("tanggal_pembelian", tgl));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            refreshform();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            datamaster fm = new datamaster();
            fm.Show();
            this.Hide();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string kdPem = dataGridView1.SelectedRows[0].Cells["kode_pembelian"].Value.ToString();

                    koneksi.Open();
                    string str = "DELETE FROM dbo.pembelian WHERE kode_pembelian = @kode_pembelian";
                    SqlCommand cmd = new SqlCommand(str, koneksi);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("kode_pembelian", kdPem));
                    cmd.ExecuteNonQuery();
                    koneksi.Close();

                    MessageBox.Show("Data berhasil dihapus", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView();
                    refreshform();
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang akan dihapus", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    txtkodepem.Text = row.Cells["kode_pembelian"].Value.ToString();
                    txttotalpem.Text = row.Cells["total_pembelian"].Value.ToString();
                    dttglpem.Text = row.Cells["tanggal_pembelian"].Value.ToString();

                    dttglpem.Enabled = true;
                    txttotalpem.Enabled = true;
                    btnSave.Enabled = false;
                    btnClear.Enabled = true;
                }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string kdPem = txtkodepem.Text.Trim();
            string ttlPem = txttotalpem.Text.Trim();
            DateTime tgl = dttglpem.Value;

            if (kdPem == "")
            {
                MessageBox.Show("Masukkan Kode Pembelian", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "UPDATE dbo.pembelian SET total_pembelian = @total_pembelian, tanggal_pembelian = @tanggal_pembelian WHERE kode_pembelian = @kode_pembelian";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("total_pembelian", ttlPem));
                cmd.Parameters.Add(new SqlParameter("tanggal_pembelian", tgl));
                cmd.Parameters.Add(new SqlParameter("kode_pembelian", kdPem));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Diperbarui", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }

        }
    }
}
