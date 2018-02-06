using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X_RudderUpper
{
    public partial class PID_change : Form
    {
        public MainForm parent;

        public PID_change()
        {
            InitializeComponent();
        }

        public PID_change(MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }


    }
}
