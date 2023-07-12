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

namespace supermarket
{
    public partial class melayani : Form
    {
        private string stringConnection = "Data Source=AMSONUN;Initial Catalog=supermarket;Persist Security Info=True;User ID=sa;Password=123";
        private SqlConnection koneksi;
        public melayani()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
            LoadKaryawanData();
            LoadPembelianData();
        }

        private void refreshform()
        {
            txtjumlah.Text = "";
            txtjumlah.Enabled = false;
            cbxkodekaryawan.Text = "";
            cbxkodekaryawan.Enabled = false;
            cbxkodepembelian.Text = "";
            cbxkodepembelian.Enabled = false;
            btnadd.Enabled = true;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
            clearBinding();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            cbxkodekaryawan.Enabled = true;
            cbxkodepembelian.Enabled = true;
            txtjumlah.Enabled = true;
            btnSave.Enabled = true;
            btnClear.Enabled = true;
            btnadd.Enabled = true;
        }

        private void LoadKaryawanData()
        {
            string query = "SELECT kode_karyawan, nama_karyawan FROM karyawan";
            SqlDataAdapter adapter = new SqlDataAdapter(query, koneksi);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cbxkodekaryawan.DisplayMember = "kode_karyawan";
            cbxkodekaryawan.ValueMember = "kode_karyawan";
            cbxkodekaryawan.DataSource = dataTable;
        }

        private void LoadPembelianData()
        {
            string query = "SELECT kode_pembelian FROM pembelian";
            SqlDataAdapter adapter = new SqlDataAdapter(query, koneksi);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cbxkodepembelian.DisplayMember = "kode_pembelian";
            cbxkodepembelian.ValueMember = "kode_pembelian";
            cbxkodepembelian.DataSource = dataTable;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string kodepembelian = cbxkodepembelian.SelectedValue.ToString();
            string kodekaryawan = cbxkodekaryawan.SelectedValue.ToString();
            string jumlah = txtjumlah.Text.Trim();

            if (string.IsNullOrEmpty(kodepembelian) || string.IsNullOrEmpty(kodekaryawan) || string.IsNullOrEmpty(jumlah))
            {
                MessageBox.Show("Harap isi semua field yang diperlukan!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string query = "INSERT INTO melayani (kode_karyawan, kode_pembelian, jumlah_pembelian) VALUES (@kode_karyawan, @kode_pembelian, @jumlah_pembelian)";
                SqlCommand cmd = new SqlCommand(query, koneksi);
                cmd.Parameters.AddWithValue("@kode_karyawan", kodekaryawan);
                cmd.Parameters.AddWithValue("@kode_pembelian", kodepembelian);
                cmd.Parameters.AddWithValue("@jumlah_pembelian", jumlah);
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtjumlah.Text = "";
            cbxkodekaryawan.SelectedIndex = -1;
            cbxkodepembelian.SelectedIndex = -1;
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            datarelasi fm = new datarelasi();
            fm.Show();
            this.Hide();
        }
        private void clearBinding()
        {
            txtjumlah.DataBindings.Clear();
            cbxkodekaryawan.DataBindings.Clear();
            cbxkodepembelian.DataBindings.Clear();
        }

        private void cbckodekaryawan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string kodePembelian = dataGridView1.SelectedRows[0].Cells["kode_pembelian"].Value.ToString();
                    koneksi.Open();

                    // Hapus baris terkait dari tabel "melayani" terlebih dahulu
                    string deleteMelayaniQuery = "DELETE FROM melayani WHERE kode_pembelian = @kode_pembelian";
                    SqlCommand deleteMelayaniCmd = new SqlCommand(deleteMelayaniQuery, koneksi);
                    deleteMelayaniCmd.Parameters.AddWithValue("@kode_pembelian", kodePembelian);
                    deleteMelayaniCmd.ExecuteNonQuery();

                    // Hapus baris dari tabel "pembelian"
                    string deletePembelianQuery = "DELETE FROM pembelian WHERE kode_pembelian = @kode_pembelian";
                    SqlCommand deletePembelianCmd = new SqlCommand(deletePembelianQuery, koneksi);
                    deletePembelianCmd.Parameters.AddWithValue("@kode_pembelian", kodePembelian);
                    deletePembelianCmd.ExecuteNonQuery();
                    koneksi.Close();

                    MessageBox.Show("Data berhasil dihapus", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dataGridView();
                    refreshform();
                    LoadPembelianData(); // Memperbarui data ComboBox
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang akan dihapus", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void btnopen_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnopen.Enabled = false;
        }
        private void dataGridView()
        {
            koneksi.Open();
            string str = "select kode_karyawan, kode_pembelian, jumlah_pembelian from dbo.melayani";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void cbxkodepembelian_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtjumlah_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
