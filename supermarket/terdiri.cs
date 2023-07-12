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
    public partial class terdiri : Form
    {
        private string stringConnection = "Data Source=AMSONUN;Initial Catalog=supermarket;Persist Security Info=True;User ID=sa;Password=123";
        private SqlConnection koneksi;
        public terdiri()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
            LoadBarangData();
            LoadPembelianData();
        }
        private void refreshform()
        {
            cbxkodebarang.Text = "";
            cbxkodebarang.Enabled = false;
            cbxkodepembelian.Text = "";
            cbxkodepembelian.Enabled = false;
            btnadd.Enabled = true;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
            clearBinding();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            cbxkodebarang.Enabled = true;
            cbxkodepembelian.Enabled = true;
            btnSave.Enabled = true;
            btnClear.Enabled = true;
            btnadd.Enabled = true;
        }
        private void LoadBarangData()
        {
            string query = "SELECT kode_barang, nama_barang FROM barang";
            SqlDataAdapter adapter = new SqlDataAdapter(query, koneksi);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cbxkodebarang.DisplayMember = "kode_barang";
            cbxkodebarang.ValueMember = "kode_barang";
            cbxkodebarang.DataSource = dataTable;
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




        private void clearBinding()
        {
            cbxkodebarang.DataBindings.Clear();
            cbxkodepembelian.DataBindings.Clear();
        }


        private void dataGridView()
        {
            koneksi.Open();
            string str = "select kode_barang, kode_pembelian from dbo.terdiri";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string kodepembelian = cbxkodepembelian.SelectedValue.ToString();
            string kodebarang = cbxkodebarang.SelectedValue.ToString();

            if (string.IsNullOrEmpty(kodepembelian) || string.IsNullOrEmpty(kodebarang))
            {
                MessageBox.Show("Please fill in all the required fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                koneksi.Open();
                string query = "INSERT INTO terdiri (kode_barang, kode_pembelian) VALUES (@kode_barang, @kode_pembelian)";
                SqlCommand cmd = new SqlCommand(query, koneksi);
                cmd.Parameters.AddWithValue("@kode_barang", kodebarang);
                cmd.Parameters.AddWithValue("@kode_pembelian", kodepembelian);
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();

            }
        }

        private void btnopen_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnopen.Enabled = false;
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

                    // Hapus baris terkait dari tabel "terdiri" terlebih dahulu
                    string deleteTerdiriQuery = "DELETE FROM terdiri WHERE kode_pembelian = @kode_pembelian";
                    SqlCommand deleteTerdiriCmd = new SqlCommand(deleteTerdiriQuery, koneksi);
                    deleteTerdiriCmd.Parameters.AddWithValue("@kode_pembelian", kodePembelian);
                    deleteTerdiriCmd.ExecuteNonQuery();

                    // Hapus baris dari tabel "pembelian"
                    string deletePembelianQuery = "DELETE FROM pembelian WHERE kode_pembelian = @kode_pembelian";
                    SqlCommand deletePembelianCmd = new SqlCommand(deletePembelianQuery, koneksi);
                    deletePembelianCmd.Parameters.AddWithValue("@kode_pembelian", kodePembelian);
                    deletePembelianCmd.ExecuteNonQuery();
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

        private void btnback_Click(object sender, EventArgs e)
        {
            datarelasi fm = new datarelasi();
            fm.Show();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbxkodebarang.SelectedIndex = -1;
            cbxkodepembelian.SelectedIndex = -1;
        }
    }
}
