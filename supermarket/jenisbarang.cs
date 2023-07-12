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
    public partial class jenisbarang : Form
    {
        private string stringConnection = "Data Source=AMSONUN;Initial Catalog=supermarket;Persist Security Info=True;User ID=sa;Password=123";
        private SqlConnection koneksi;
        public jenisbarang()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }
        private void refreshform()
        {
            txtkodejenis.Text = "";
            txtkodejenis.Enabled = false;
            txtnamajenis.Text = "";
            txtnamajenis.Enabled = false;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
        }
        private void dataGridView()
        {
            koneksi.Open();
            string str = "select kode_jenis, nama_jenis from dbo.jenis_barang";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void btnopen_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnopen.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string kdJenis = txtkodejenis.Text.Trim();
            string nmJenis = txtnamajenis.Text.Trim();

            if (kdJenis == "" || nmJenis == "")
            {
                MessageBox.Show("Harap lengkapi semua field", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "insert into dbo.jenis_barang (kode_jenis, nama_jenis)" + "values(@kode_jenis, @nama_jenis)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("kode_jenis", kdJenis));
                cmd.Parameters.Add(new SqlParameter("nama_jenis", nmJenis));
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
                    string kdjenis = dataGridView1.SelectedRows[0].Cells["kode_jenis"].Value.ToString();

                    koneksi.Open();
                    string str = "DELETE FROM dbo.jenis_barang WHERE kode_jenis = @kode_jenis";
                    SqlCommand cmd = new SqlCommand(str, koneksi);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("kode_jenis", kdjenis));
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
                txtkodejenis.Text = row.Cells["kode_jenis"].Value.ToString();
                txtnamajenis.Text = row.Cells["nama_jenis"].Value.ToString();

                txtkodejenis.Enabled = false;
                txtnamajenis.Enabled = true;
                btnSave.Enabled = false;
                btnClear.Enabled = true;
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            txtnamajenis.Enabled = true;
            txtkodejenis.Enabled = true;
            btnSave.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string kdJenis = txtkodejenis.Text.Trim();
            string nmJenis = txtnamajenis.Text.Trim();

            if (kdJenis == "")
            {
                MessageBox.Show("Masukkan Kode Jenis", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "UPDATE dbo.jenis_barang SET nama_jenis = @nama_jenis WHERE kode_jenis = @kode_jenis";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("nama_jenis", nmJenis));
                cmd.Parameters.Add(new SqlParameter("kode_jenis", kdJenis));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Diperbarui", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }
    }
}
