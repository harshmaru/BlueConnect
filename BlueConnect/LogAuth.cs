using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueConnect
{
    public partial class LogAuth : Form
    {
        public static String nickName, passToUnlock;
        public LogAuth()
        {
            InitializeComponent();
        }

        private void LogAuth_Load(object sender, EventArgs e)
        {
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            
            //nickName = txtNick.Text;
            passToUnlock = txtPass.Text;
            this.Close();
        }
    }
}
