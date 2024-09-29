using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {

        private Bitmap originalImage;
        public Form1()
        {
            InitializeComponent();

            button1.Text = "載入圖片";
            button2.Text = "灰階化";
            button3.Text = "二值化";

            // 創建一個新的 Label
            Label pictureBoxLabel = new Label();

            // 設置 Label 的文本
            pictureBoxLabel.Text = "影像顯示";  // 這裡設置名稱

            // 設置 Label 的大小和位置 (這裡設定為 PictureBox 上方)
            pictureBoxLabel.AutoSize = true;  // 使 Label 的大小自動適應文字
            pictureBoxLabel.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 25);  // 位於 PictureBox 上方

            // 設置字體和大小
            pictureBoxLabel.Font = new Font("Arial", 14, FontStyle.Bold);  // 設置字體為 Arial，大小為14，並加粗

            // 設置 PictureBox 的邊框樣式為單線邊框
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;

            // 如果想要保持比例而填滿，可以使用 Zoom 模式：
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            // 添加 Label 到 Form 中
            this.Controls.Add(pictureBoxLabel);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 載入影像並顯示在 PictureBox 中
                originalImage = new Bitmap(openFileDialog.FileName);
                pictureBox1.Image = originalImage;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                // 創建一個新的灰階影像
                Bitmap grayImage = Grayscale(originalImage);
                pictureBox1.Image = grayImage; // 顯示灰階影像
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                Bitmap binaryImage = ConvertToBinary(originalImage);
                pictureBox1.Image = binaryImage;
            }
            else
            {
                MessageBox.Show("請先載入一張影像！");
            }
        }

        private Bitmap Grayscale(Bitmap original)
        {
            Bitmap grayBitmap = new Bitmap(original.Width, original.Height);

            // 逐像素轉換為灰階
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    // 取得像素的顏色值
                    Color originalColor = original.GetPixel(x, y);

                    // 計算灰階值 (這邊使用加權平均法)
                    int grayValue = (int)(originalColor.R * 0.299 + originalColor.G * 0.587 + originalColor.B * 0.114);

                    // 確保灰階值在0-255之間
                    grayValue = Math.Min(255, Math.Max(0, grayValue));

                    // 使用相同的灰階值設定R、G、B
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);

                    // 設定像素為灰階值
                    grayBitmap.SetPixel(x, y, grayColor);
                }
            }
            return grayBitmap;
        }

        private Bitmap ConvertToBinary(Image img)
        {
            Bitmap binaryBitmap = new Bitmap(img.Width, img.Height);
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Color pixelColor = ((Bitmap)img).GetPixel(x, y);
                    // 計算灰度值
                    int gray = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);
                    // 設置二值化 數值為 128
                    binaryBitmap.SetPixel(x, y, gray < 128 ? Color.Black : Color.White);
                }
            }
            return binaryBitmap;
        }
    }
}



