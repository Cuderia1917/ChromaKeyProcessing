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


    }
}
