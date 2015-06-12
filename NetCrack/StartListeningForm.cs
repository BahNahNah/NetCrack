using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCrack
{
    public partial class StartListeningForm : Form
    {
        private int _lp;
        public int ListeningPort
        {
            get { return _lp; }
        }
        public StartListeningForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _lp = (int)numericUpDown1.Value;
            this.DialogResult = DialogResult.OK;
        }
    }
}
