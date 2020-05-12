using System;
using System.Windows.Forms;

namespace Chess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tBoard1.BlackPlayer.IsComputer = true;
        }

        private void TBoard1_Load(object sender, EventArgs e)
        {

        }
    }
}
