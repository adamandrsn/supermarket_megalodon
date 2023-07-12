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
    public partial class merk : Form
    {
        private string stringConnection = "Data Source=AMSONUN;Initial Catalog=supermarket;Persist Security Info=True;User ID=sa;Password=123";
        private SqlConnection koneksi;
        public merk()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }
        private void refreshform()
        {
            txtkodemerk.Text = "";
            txtkodemerk.Enabled = false;
            txtnamamerk.Text = "";
            txtnamamerk.Enabled = false;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
        }
        private void dataGridView()
        {
            koneksi.Open();
            string str = "select kode_merk, nama_merk from dbo.merk";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtkodemerk.Text = row.Cells["kode_merk"].Value.ToString();
                txtnamamerk.Text = row.Cells["nama_merk"].Value.ToString();

                txtkodemerk.Enabled = false;
                txtnamamerk.Enabled = true;
                btnSave.Enabled = false;
                btnClear.Enabled = true;
            }
        }


        private void btnadd_Click_1(object sender, EventArgs e)
        {
            txtnamamerk.Enabled = true;
            txtkodemerk.Enabled = true;
            btnSave.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btndelete_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string kdmerk = dataGridView1.SelectedRows[0].Cells["kode_merk"].Value.ToString();

                    koneksi.Open();
                    string str = "DELETE FROM dbo.merk WHERE kode_merk = @kode_merk";
                    SqlCommand cmd = new SqlCommand(str, koneksi);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("kode_merk", kdmerk));
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

        private void btnback_Click(object sender, EventArgs e)
        {
            datamaster fm = new datamaster();
            fm.Show();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            refreshform();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string kdMerk = txtkodemerk.Text.Trim();
            string nmMerk = txtnamamerk.Text.Trim();

            if (kdMerk == "" || nmMerk == "")
            {
                MessageBox.Show("Harap lengkapi semua field", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "insert into dbo.merk (kode_merk, nama_merk)" + "values(@kode_merk, @nama_merk)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("kode_merk", kdMerk));
                cmd.Parameters.Add(new SqlParameter("nama_merk", nmMerk));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }

        private void btnopen_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnopen.Enabled = false;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string kdMerk = txtkodemerk.Text.Trim();
            string nmMerk = txtnamamerk.Text.Trim();

            if (kdMerk == "")
            {
                MessageBox.Show("Masukkan Kode Merk", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "UPDATE dbo.merk SET nama_merk = @nama_merk WHERE kode_merk = @kode_merk";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("nama_merk", nmMerk));
                cmd.Parameters.Add(new SqlParameter("kode_merk", kdMerk));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Diperbarui", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }
    }
}
