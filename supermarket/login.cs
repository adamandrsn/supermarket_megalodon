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
    public partial class login : Form
    {
        private string correctUsername = "Adam Alwan Ganteng";
        private string correctPassword = "123";
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Periksa apakah username dan password benar
            if (username == correctUsername && password == correctPassword)
            {
                // Jika benar, tampilkan pesan berhasil dan lanjut ke halaman utama
                MessageBox.Show("Login berhasil!");
                // Ganti "HomePageForm" dengan nama form halaman utama Anda
                homepage optionPage = new homepage();
                optionPage.Show();
                this.Hide();
            }
            else
            {
                // Jika salah, tampilkan pesan kesalahan
                MessageBox.Show("Username atau password salah. Silakan coba lagi.");
            }
        }
    }
}
