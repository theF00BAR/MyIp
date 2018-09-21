using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MyIp
{

    public partial class MainForm : Form
    {

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOACTIVATE = 0x0010;

        public MainForm()
        {
            InitializeComponent();
        }

        private void RefreshLayout()
        {

            var rightmost = Screen.AllScreens[0];
            foreach (var screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
                {
                    rightmost = screen;
                }

            }

            int newLeft = rightmost.WorkingArea.Right - Width;
            int newTop = rightmost.WorkingArea.Bottom - Height;

            if (newLeft != Left || newTop != Top)
            {
                Left = newLeft;
                Top = newTop;
            }

            SetWindowPos(Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);

        }

        private void OnRefresh(object s, EventArgs e)
        {

            try
            {

                var sb = new StringBuilder();

                foreach (var ip in new IpConfigIpFinder().FindAll())
                {
                    sb.AppendLine(ip.ToString());
                }

                outLabel.Text = sb.ToString();
                RefreshLayout();

            }
            catch
            {
                outLabel.Text = "Failed to find IP addresses";
            }

            GC.Collect();

        }

        private void OnClick(object sender, EventArgs e)
        {

            OnRefresh(null, null);

            var frm = new TextForm();
            frm.Show(null);
            frm.LoadText(outLabel.Text);

            GC.Collect();

        }

        private void OnLoad(object s, EventArgs e)
        {
            OnRefresh(null, null);
        }

    }

}
