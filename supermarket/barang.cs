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
    public partial class barang : Form
    {
        private string stringConnection = "Data Source=AMSONUN;Initial Catalog=supermarket;Persist Security Info=True;User ID=sa;Password=123";
        private SqlConnection koneksi;
        public barang()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }
        private void refreshform()
        {
            txtnamabarang.Text = "";
            txtnamabarang.Enabled = false;
            txtkodebarang.Text = "";
            txtkodebarang.Enabled = false;
            txtjumlah.Text = "";
            txtjumlah.Enabled = false;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
        }
        private void dataGridView()
        {
            koneksi.Open();
            string str = "select nama_barang, kode_barang, jumlah_barang from dbo.barang";
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
            string kdBar = txtkodebarang.Text.Trim();
            string nmBar = txtnamabarang.Text.Trim();
            string jmlBar = txtjumlah.Text.Trim();

            if (kdBar == "" || nmBar == "" || jmlBar == "")
            {
                MessageBox.Show("Harap lengkapi semua field", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "insert into dbo.barang (kode_barang, nama_barang, jumlah_barang)" + "values(@kode_barang, @nama_barang, @jumlah_barang)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("kode_barang", kdBar));
                cmd.Parameters.Add(new SqlParameter("nama_barang", nmBar));
                cmd.Parameters.Add(new SqlParameter("jumlah_barang", jmlBar));
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
                    string kdBar = dataGridView1.SelectedRows[0].Cells["kode_barang"].Value.ToString();

                    koneksi.Open();
                    string str = "DELETE FROM dbo.barang WHERE kode_barang = @kode_barang";
                    SqlCommand cmd = new SqlCommand(str, koneksi);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("kode_barang", kdBar));
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
                txtkodebarang.Text = row.Cells["kode_barang"].Value.ToString();
                txtnamabarang.Text = row.Cells["nama_barang"].Value.ToString();
                txtjumlah.Text = row.Cells["jumlah_barang"].Value.ToString();
                txtkodebarang.Enabled = false;
                txtjumlah.Enabled = true;
                txtnamabarang.Enabled = true;
                btnSave.Enabled = false;
                btnClear.Enabled = true;
            }
        }

        private void btnadd_Click_1(object sender, EventArgs e)
        {
            txtnamabarang.Enabled = true;
            txtkodebarang.Enabled = true;
            txtjumlah.Enabled = true;
            btnSave.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string kdBar = txtkodebarang.Text.Trim();
            string nmBar = txtnamabarang.Text.Trim();
            string jmlBar = txtjumlah.Text.Trim();

            if (kdBar == "")
            {
                MessageBox.Show("Masukkan Kode Barang", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "UPDATE dbo.barang SET nama_barang = @nama_barang, jumlah_barang = @jumlah_barang WHERE kode_barang = @kode_barang";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("nama_barang", nmBar));
                cmd.Parameters.Add(new SqlParameter("jumlah_barang", jmlBar));
                cmd.Parameters.Add(new SqlParameter("kode_barang", kdBar));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Diperbarui", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }
    }
}
