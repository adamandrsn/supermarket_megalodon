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
    public partial class homepage : Form
    {
        public homepage()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            datamaster homePage = new datamaster();
            homePage.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            datarelasi homePage = new datarelasi();
            homePage.Show();
            this.Hide();
        }
    }
}
