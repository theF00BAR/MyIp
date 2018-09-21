using System;
using System.Windows.Forms;

namespace MyIp
{

    internal static class Program
    {

        [STAThread]
        private static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

    }

}
