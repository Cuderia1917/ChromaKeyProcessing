using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChromaKey_GUI
{
    public partial class Form1 : Form
    {
        
        private CancellationTokenSource tokenSource = new CancellationTokenSource();//タスクキャンセル用、他の方法を探したい

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //ファイル開く
        private void fileOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
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
        
        //色指定する
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
        
        //画面右側プレビュー切り替え
        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileList.SelectedIndex >= 0)
            {
                System.IO.FileStream fs = new System.IO.FileStream((string)fileList.Items[fileList.SelectedIndex], System.IO.FileMode.Open, System.IO.FileAccess.Read);
                Image pic = Image.FromStream(fs);
                if (pic.Width > pic.Height) pic.RotateFlip(RotateFlipType.Rotate270FlipNone);
                previewPicture.Image = pic;
                fs.Close();
            }
        }

        //R指定時の色変え
        private void redDrop_ValueChanged(object sender, EventArgs e)
        {
            baseColor.BackColor = Color.FromArgb((int)redDrop.Value, (int)greenDrop.Value, (int)blueDrop.Value);
        }
        //G指定時の色変え
        private void greenDrop_ValueChanged(object sender, EventArgs e)
        {
            baseColor.BackColor = Color.FromArgb((int)redDrop.Value, (int)greenDrop.Value, (int)blueDrop.Value);
        }
        //B指定時の色変え
        private void blueDrop_ValueChanged(object sender, EventArgs e)
        {
            baseColor.BackColor = Color.FromArgb((int)redDrop.Value, (int)greenDrop.Value, (int)blueDrop.Value);
        }

        //プレビューボタン押したら新窓で結果が出る
        private void preview_Click(object sender, EventArgs e)
        {
            if (fileList.Items.Count > 0)
            {
                if (fileList.SelectedIndex >= 0)
                    previewImage((string)fileList.Items[fileList.SelectedIndex]);
                else
                    previewImage((string)fileList.Items[0]);
            }
            else
            {
                MessageBox.Show("ファイルを選択してください", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //実行(ガバ設計故見直しが必要)
        private async void apply_Click(object sender, EventArgs e)
        {
            if (fileList.Items.Count > 0)
            {
                apply.Visible = false;
                cancel.Visible = true;
                conv_progress.Maximum = fileList.Items.Count;
                conv_progress.Value = 0;
                for (int i = 0; i < fileList.Items.Count; i++)
                {
                    conv_progress.Value++;
                    try
                    {
                        await Task.Run(() => {
                            tokenSource.Token.ThrowIfCancellationRequested();
                            convertImage((string)fileList.Items[i]);
                        }, tokenSource.Token);
                    }
                    catch (Exception)
                    {
                        cancel.Visible = false;
                        apply.Visible = true;
                        conv_progress.Value = 0;
                        MessageBox.Show("中止しました","Canceled",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                        return;
                    }
                }
                MessageBox.Show("完了しました", "Complate", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ファイルを選択してください", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //中止ボタン(ガバ設計故見直しが必要)
        private void cancel_Click(object sender, EventArgs e)
        {
            try
            {
                tokenSource.Cancel();
                Task.WaitAll();
            }
            catch (Exception)
            {
            }
        }

        //本体(改修の余地あり)
        private Bitmap Chroma(string fileName)
        {
            //ビットマップイメージに展開
            Bitmap bmp = (Bitmap)Image.FromFile(fileName);
            Color chroma_color = Color.FromArgb((int)redDrop.Value, (int)greenDrop.Value, (int)blueDrop.Value);

            int border_line=0;//閾値用変数
            Invoke((MethodInvoker)(() =>
            {
                border_line = border.Value;
            }));
            
            //データ吸出しのおまじない
            System.Drawing.Imaging.BitmapData bmpBitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, bmp.PixelFormat);
            
            //画像サイズ
            int image_height = bmpBitmapData.Height;
            int image_width = bmpBitmapData.Width;
            int stride = bmpBitmapData.Stride;

            //バイナリ化
            byte[] bmp_px = new byte[stride * image_height];
            System.Runtime.InteropServices.Marshal.Copy(bmpBitmapData.Scan0, bmp_px, 0, bmp_px.Length);
            
            //クロマキー判定
            Parallel.For(0, image_height, y =>
            {
                Parallel.For(0, image_width, x =>
                {
                    int position = x * 3 + stride * y;
                    if (colorDistance(bmp_px, chroma_color, x, y, stride, border_line))
                    {
                        bmp_px[position] = 255;
                        bmp_px[position + 1] = 255;
                        bmp_px[position + 2] = 255;
                    }
                });
            });
            
            //変換したビットマップデータをコピー
            System.Runtime.InteropServices.Marshal.Copy(bmp_px, 0, bmpBitmapData.Scan0, bmp_px.Length);
            bmp.UnlockBits(bmpBitmapData);
            
            return bmp;
        }

        //ユークリッド距離算出(旧,削除予定)
        private static bool colorDistance(Color source_pixel, Color comp_pixel, int allow_distance)
        {
            double distance = Math.Sqrt(
                Math.Pow((source_pixel.R - comp_pixel.R), 2) +
                Math.Pow((source_pixel.G - comp_pixel.G), 2) +
                Math.Pow((source_pixel.B - comp_pixel.B), 2)
                );
            if (distance < allow_distance) return true;
            else return false;
        }

        //ユークリッド距離算出、消すと判定した場合true,しない場合false
        private static bool colorDistance(byte[] pixel, Color comp_pixel, int x, int y, int stride, int allow_distance)
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
            
            if (distance < allow_distance)
                return true;
            else
                return false;
        }

        //変換する本体
        public void convertImage(string fileName)
        {
            Bitmap convert_bmp = Chroma(fileName);
            Regex regex = new System.Text.RegularExpressions.Regex(".JPG$|.jpg$|.jpeg$|.JPEG$|.PNG$|.png$");
            string chromafilename = regex.Replace(fileName, "_chroma.jpg");
            convert_bmp.Save(chromafilename, System.Drawing.Imaging.ImageFormat.Jpeg);
            convert_bmp.Dispose();
        }

        //使わなくなったので削除予定
        public async void convertImage(string fileName,bool sync)
        {
            await Task.Run(() =>
            {
                Bitmap convert_bmp = Chroma(fileName);
                Regex regex = new System.Text.RegularExpressions.Regex(".JPG$|.jpg$|.jpeg$|.JPEG$|.PNG$|.png$");
                string chromafilename = regex.Replace(fileName, "_chroma.jpg");
                convert_bmp.Save(chromafilename, System.Drawing.Imaging.ImageFormat.Jpeg);
                convert_bmp.Dispose();
                
            });
        }
        
        //新窓開く本体
        public void previewImage(string fileName)
        {
            Bitmap preview_bmp = Chroma(fileName);
            previewForm previewWindow = new previewForm(preview_bmp);
            previewWindow.Show();
        }
    }
}
