using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChromaKey_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        private void fileOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileList.Items.Clear();

                foreach (string filePath in openFileDialog1.FileNames)
                {

                    fileList.Items.Add(filePath);
                }
                System.IO.FileStream fs = new System.IO.FileStream((string)fileList.Items[0], System.IO.FileMode.Open, System.IO.FileAccess.Read);
                Image pic = System.Drawing.Image.FromStream(fs);
                if (pic.Width > pic.Height) pic.RotateFlip(RotateFlipType.Rotate270FlipNone);
                previewPicture.Image = pic;
                fs.Close();
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void previewPicture_Click(object sender, EventArgs e)
        {
            Bitmap colorBit = new Bitmap(1, 1);
            int x = System.Windows.Forms.Cursor.Position.X;
            int y = System.Windows.Forms.Cursor.Position.Y;
            Graphics g = Graphics.FromImage(colorBit);
            g.CopyFromScreen(new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y),
                new Point(0, 0), new Size(1, 1));
            Color rgb = colorBit.GetPixel(0, 0);
            Cursor.Current = Cursors.Default;

            redDrop.Value = rgb.R;
            greenDrop.Value = rgb.G;
            blueDrop.Value = rgb.B;
        }
        

        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileList.SelectedIndex >= 0)
            {
                System.IO.FileStream fs = new System.IO.FileStream((string)fileList.Items[fileList.SelectedIndex], System.IO.FileMode.Open, System.IO.FileAccess.Read);
                Image pic = System.Drawing.Image.FromStream(fs);
                if (pic.Width > pic.Height) pic.RotateFlip(RotateFlipType.Rotate270FlipNone);
                previewPicture.Image = pic;
                fs.Close();
            }
        }

        private void redDrop_ValueChanged(object sender, EventArgs e)
        {
            baseColor.BackColor = Color.FromArgb((int)redDrop.Value, (int)greenDrop.Value, (int)blueDrop.Value);
        }

        private void greenDrop_ValueChanged(object sender, EventArgs e)
        {
            baseColor.BackColor = Color.FromArgb((int)redDrop.Value, (int)greenDrop.Value, (int)blueDrop.Value);
        }

        private void blueDrop_ValueChanged(object sender, EventArgs e)
        {
            baseColor.BackColor = Color.FromArgb((int)redDrop.Value, (int)greenDrop.Value, (int)blueDrop.Value);
        }

        private void preview_Click(object sender, EventArgs e)
        {
            if (fileList.SelectedIndex >= 0)
            {
                previewImage((string)fileList.Items[fileList.SelectedIndex]);
            }
        }

        private void apply_Click(object sender, EventArgs e)
        {
            if (fileList.Items.Count > 0)
            {
                progressBar1.Maximum = fileList.Items.Count;
                progressBar1.Value = 0;
                for (int i = 0; i < fileList.Items.Count; i++)
                {
                    progressBar1.Value++;
                    progressBar1.Update();
                    convertImage((string)fileList.Items[i]);
                }
            }
        }

        public Bitmap Chroma(string fileName)
        {
            //ビットマップイメージに展開
            System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(fileName);
            System.Drawing.Bitmap chroma_bmp = (Bitmap)bmp.Clone();

            Color chroma_color = Color.FromArgb((int)redDrop.Value, (int)greenDrop.Value, (int)blueDrop.Value);

            //バイナリに変換する
            System.Drawing.Imaging.BitmapData bmpBitmapData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, bmp.PixelFormat);
            byte[] bmp_px = new byte[bmpBitmapData.Stride * bmpBitmapData.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpBitmapData.Scan0, bmp_px, 0, bmp_px.Length);

            System.Drawing.Imaging.BitmapData chromaBitmapData = chroma_bmp.LockBits(new System.Drawing.Rectangle(0, 0, chroma_bmp.Width, chroma_bmp.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, chroma_bmp.PixelFormat);
            byte[] chroma_px = new byte[chromaBitmapData.Stride * chromaBitmapData.Height];

            //画像サイズ、こうするほうが速い
            int image_height = bmpBitmapData.Height;
            int image_width = bmpBitmapData.Width;

            //クロマキー判定
            for (int y = 0; y < image_height; y++)
            {
                for (int x = 0; x < image_width; x++)
                {
                    int position = x * 3 + bmpBitmapData.Stride * y;
                    byte[] fix_color = { bmp_px[position], bmp_px[position + 1], bmp_px[position + 2] };
                    /*旧アルゴ
                    System.Drawing.Color pixel = bmp.GetPixel(x, y);
                    if (colorDistance(pixel, chroma_color, 80))
                    {
                        Color fix_color = Color.FromArgb(0, pixel.R, pixel.G, pixel.B);
                        chroma_bmp.SetPixel(x, y, fix_color);
                    }
                    else chroma_bmp.SetPixel(x,y,pixel);
                    */
                    if (colorDistance(bmp_px, chroma_color, x, y, bmpBitmapData.Stride, border.Value))
                    {
                        fix_color[0] = 255; fix_color[1] = 255; fix_color[2] = 255;
                    }
                    chroma_px[position] = fix_color[0];
                    chroma_px[position + 1] = fix_color[1];
                    chroma_px[position + 2] = fix_color[2];
                }
            }

            //画像生成
            System.Runtime.InteropServices.Marshal.Copy(chroma_px, 0, chromaBitmapData.Scan0, chroma_px.Length);
            bmp.UnlockBits(bmpBitmapData);
            chroma_bmp.UnlockBits(chromaBitmapData);
            bmp.Dispose();
            /*            Regex regex = new System.Text.RegularExpressions.Regex(".JPG$|.jpg$|.jpeg$|.JPEG$|.PNG$|.png$");
                        string chromafilename = regex.Replace(fileName, "_chroma.jpg");
                        chroma_bmp.Save(chromafilename, System.Drawing.Imaging.ImageFormat.Jpeg);
                        chroma_bmp.Dispose();*/

            return chroma_bmp;

        }

        static Boolean colorDistance(Color source_pixel, Color comp_pixel, int allow_distance)
        {
            double distance = Math.Sqrt(
                Math.Pow((source_pixel.R - comp_pixel.R), 2) +
                Math.Pow((source_pixel.G - comp_pixel.G), 2) +
                Math.Pow((source_pixel.B - comp_pixel.B), 2)
                );
            if (distance < allow_distance) return true;
            else return false;
        }

        //クロマキー判定のメソッド本体、消すと判定した場合true,しない場合false
        static Boolean colorDistance(byte[] pixel, Color comp_pixel, int x, int y, int stride, int allow_distance)
        {

            int position = x * 3 + stride * y;
            int source_b = pixel[position] < 0 ? pixel[position] + 256 : pixel[position];
            int source_g = pixel[position + 1] < 0 ? pixel[position + 1] + 256 : pixel[position + 1];
            int source_r = pixel[position + 2] < 0 ? pixel[position + 2] + 256 : pixel[position + 2];

            double distance = Math.Sqrt(
                Math.Pow((source_b - comp_pixel.B), 2) +
                Math.Pow((source_g - comp_pixel.G), 2) +
                Math.Pow((source_r - comp_pixel.R), 2)
                );
            if (distance < allow_distance) return true;
            else return false;
        }

        public void convertImage(string fileName)
        {
            Bitmap convert_bmp = Chroma(fileName);
            Regex regex = new System.Text.RegularExpressions.Regex(".JPG$|.jpg$|.jpeg$|.JPEG$|.PNG$|.png$");
            string chromafilename = regex.Replace(fileName, "_chroma.jpg");
            convert_bmp.Save(chromafilename, System.Drawing.Imaging.ImageFormat.Jpeg);
            convert_bmp.Dispose();
        }

        public void previewImage(string fileName)
        {
            Bitmap preview_bmp = Chroma(fileName);
            previewForm previewWindow = new previewForm(preview_bmp);
            previewWindow.Show();
//            preview_bmp.Dispose();
        }

    }
}
