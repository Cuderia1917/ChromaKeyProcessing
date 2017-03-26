using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChromaKey_GUI
{
    public partial class previewForm : Form
    {
        public previewForm(Bitmap image)
        {
            InitializeComponent();
            pictureBox1.Image = image;
        }

        private void previewForm_Load(object sender, EventArgs e)
        {
            if (pictureBox1.Image.Width > pictureBox1.Image.Height) pictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox1.Show();
        }
    }
}
