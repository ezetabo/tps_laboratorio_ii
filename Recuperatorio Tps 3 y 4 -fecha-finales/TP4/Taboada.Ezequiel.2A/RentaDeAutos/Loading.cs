using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentaDeAutos
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            pcbLoading.Location = new Point(this.Width / 2 - this.pcbLoading.Width / 2,
                this.Height / 2 - this.pcbLoading.Height / 2);
        }
    }
}
