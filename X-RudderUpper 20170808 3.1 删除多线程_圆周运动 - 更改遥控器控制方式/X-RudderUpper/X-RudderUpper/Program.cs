using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows;

namespace X_RudderUpper
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainForm = new MainForm();
            mainForm.StartPosition = FormStartPosition.CenterScreen;
            //Rectangle rect = System.Windows.Forms.SystemInformation.VirtualScreen;
            //mainForm.Location = new System.Drawing.Point((rect.Width - mainForm.Width) / 2, rect.Height / 2 - mainForm.Height / 2);
            Application.Run(mainForm);
        }
    }
}
