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
    public partial class Karyawan : Form
    {
        private string stringConnection = "Data Source=AMSONUN;Initial Catalog=supermarket;Persist Security Info=True;User ID=sa;Password=123";
        private SqlConnection koneksi;
        private void refreshform()
        {
            txtnama.Text = "";
            txtnama.Enabled = false;
            txtalamat.Text = "";
            txtalamat.Enabled = false;
            txtkode.Text = "";
            txtkode.Enabled = false;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
        }

        public Karyawan()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void dataGridView()
        {
            koneksi.Open();
            string str = "select kode_karyawan, nama_karyawan, alamat from dbo.karyawan";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            txtnama.Enabled = true;
            txtalamat.Enabled = true;
            txtkode.Enabled = true;
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
            string nmKaryawan = txtnama.Text.Trim();
            string kdKaryawan = txtkode.Text.Trim();
            string alamat = txtalamat.Text.Trim();

            if (nmKaryawan == "" || kdKaryawan == "" || alamat == "")
            {
                MessageBox.Show("Harap lengkapi semua field", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "insert into dbo.karyawan (kode_karyawan, nama_karyawan, alamat)" + "values(@kode_karyawan, @nama_karyawan, @alamat)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("nama_karyawan", nmKaryawan));
                cmd.Parameters.Add(new SqlParameter("kode_karyawan", kdKaryawan));
                cmd.Parameters.Add(new SqlParameter("alamat", alamat));
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
                    string kodeKaryawan = dataGridView1.SelectedRows[0].Cells["kode_karyawan"].Value.ToString();

                    koneksi.Open();
                    string str = "DELETE FROM dbo.karyawan WHERE kode_karyawan = @kode_karyawan";
                    SqlCommand cmd = new SqlCommand(str, koneksi);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("kode_karyawan", kodeKaryawan));
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
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtkode.Text = row.Cells["kode_karyawan"].Value.ToString();
                txtnama.Text = row.Cells["nama_karyawan"].Value.ToString();
                txtalamat.Text = row.Cells["alamat"].Value.ToString();

                txtnama.Enabled = true;
                txtalamat.Enabled = true;
                btnSave.Enabled = false;
                btnClear.Enabled = true;
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string nmKaryawan = txtnama.Text.Trim();
            string kdKaryawan = txtkode.Text.Trim();
            string alamat = txtalamat.Text.Trim();

            if (nmKaryawan == "")
            {
                MessageBox.Show("Masukkan Nama Karyawan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "UPDATE dbo.karyawan SET nama_karyawan = @nama_karyawan, alamat = @alamat WHERE kode_karyawan = @kode_karyawan";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("nama_karyawan", nmKaryawan));
                cmd.Parameters.Add(new SqlParameter("alamat", alamat));
                cmd.Parameters.Add(new SqlParameter("kode_karyawan", kdKaryawan));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Diperbarui", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }

        }
    }
}
