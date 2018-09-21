using System.Windows.Forms;

namespace MyIp
{

    public partial class TextForm : Form
    {

        public TextForm()
        {
            InitializeComponent();
        }

        public void LoadText(string text)
        {
            textBox.Text = text.Trim();
        }

    }

}
