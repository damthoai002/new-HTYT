using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UKPI.Presentation;
using UKPI.Utils;

namespace UKPI
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            clsSystemConfig.Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
