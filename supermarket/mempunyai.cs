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
    public partial class mempunyai : Form
    {
        private string stringConnection = "Data Source=AMSONUN;Initial Catalog=supermarket;Persist Security Info=True;User ID=sa;Password=123";
        private SqlConnection koneksi;
        public mempunyai()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
            LoadKodeJenis();
            LoadBarangData();
        }

        private void refreshform()
        {
            cbxkodejenis.Text = "";
            cbxkodejenis.Enabled = false;
            cbxkodebarang.Text = "";
            cbxkodebarang.Enabled = false;
            btnadd.Enabled = true;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
            clearBinding();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            cbxkodejenis.Enabled = true;
            cbxkodebarang.Enabled = true;
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
        private void LoadKodeJenis()
        {
            string query = "SELECT kode_jenis FROM jenis_barang";
            SqlDataAdapter adapter = new SqlDataAdapter(query, koneksi);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cbxkodejenis.DisplayMember = "kode_jenis";
            cbxkodejenis.ValueMember = "kode_jenis";
            cbxkodejenis.DataSource = dataTable;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string kodejenis = cbxkodejenis.SelectedValue.ToString();
            string kodebarang = cbxkodebarang.SelectedValue.ToString();

            if (string.IsNullOrEmpty(kodejenis) || string.IsNullOrEmpty(kodebarang))
            {
                MessageBox.Show("Please fill in all the required fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                koneksi.Open();
                string query = "INSERT INTO mempunyai (kode_jenis, kode_barang) VALUES (@kode_jenis, @kode_barang)";
                SqlCommand cmd = new SqlCommand(query, koneksi);
                cmd.Parameters.AddWithValue("@kode_jenis", kodejenis);
                cmd.Parameters.AddWithValue("@kode_barang", kodebarang);
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();

            }
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            cbxkodejenis.SelectedIndex = -1;
            cbxkodebarang.SelectedIndex = -1;
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            datarelasi fm = new datarelasi();
            fm.Show();
            this.Hide();
        }

        private void clearBinding()
        {
            cbxkodejenis.DataBindings.Clear();
            cbxkodejenis.DataBindings.Clear();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string kodeJenis = dataGridView1.SelectedRows[0].Cells["kode_jenis"].Value.ToString();
                    koneksi.Open();

                    // Hapus baris terkait dari tabel "melayani" terlebih dahulu
                    string deleteMelayaniQuery = "DELETE FROM mempunyai WHERE kode_jenis = @kode_jenis";
                    SqlCommand deleteMelayaniCmd = new SqlCommand(deleteMelayaniQuery, koneksi);
                    deleteMelayaniCmd.Parameters.AddWithValue("@kode_jenis", kodeJenis);
                    deleteMelayaniCmd.ExecuteNonQuery();

                    // Hapus baris dari tabel "pembelian"
                    string deletePembelianQuery = "DELETE FROM jenis_barang WHERE kode_jenis = @kode_jenis";
                    SqlCommand deletePembelianCmd = new SqlCommand(deletePembelianQuery, koneksi);
                    deletePembelianCmd.Parameters.AddWithValue("@kode_jenis", kodeJenis);
                    deletePembelianCmd.ExecuteNonQuery();
                    koneksi.Close();

                    MessageBox.Show("Data berhasil dihapus", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dataGridView();
                    refreshform();
                    LoadKodeJenis(); // Memperbarui data ComboBox
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
            string str = "select kode_jenis, kode_barang from dbo.mempunyai";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }
    }
}
