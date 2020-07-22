using Math_Hacks.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace Math_Hack
{
    public partial class Form1 : Form
    {
        int num;
        int Operation;
        string result;
        public Form1()
        {
            InitializeComponent();
        }

        private void SetupBtn_Click(object sender, EventArgs e)
        {
            TakeScreenshot();
            ReadFromImage();
            ParseText();
            ArrayZ.ParseText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TakeScreenshot();
        }

        void TakeScreenshot()
        {
            string DeleteDir = Application.StartupPath + "\\resim.png";
            File.Delete(DeleteDir);
            Point picpoint = PictureBox.PointToScreen(new Point(0, 0));
            var bmpScreenshot = new Bitmap(PictureBox.Size.Width,
                                           PictureBox.Size.Height,
                                           PixelFormat.Format32bppArgb);
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            gfxScreenshot.CopyFromScreen(picpoint.X,
                                        picpoint.Y,
                                        0,
                                        0,
                                        PictureBox.Size,
                                        CopyPixelOperation.SourceCopy);
            bmpScreenshot.Save(DeleteDir, System.Drawing.Imaging.ImageFormat.Png);
        }
        void ReadFromImage()
        {
            string tessDataDir = Application.StartupPath;
            string imageDir = Application.StartupPath + "\\resim.png";

            using (var engine = new TesseractEngine(tessDataDir, "eng", EngineMode.Default))
            using (var image = Pix.LoadFromFile(imageDir))
            using (var page = engine.Process(image))
            {
                result = page.GetText();
            }
        }
        void ParseText()
        {
            if(result != "")
            {
                num = 0;
                Operation = -1;
                Char[] splitChars = {'+','-','x','/'};
                string[] Parsed = result.Split(splitChars);
                if (Operation == 1)
                {
                    foreach (string x in Parsed)
                    {
                        num += Convert.ToInt32(x);
                        MessageBox.Show(x);
                    }
                }
                if (Operation == 1)
                {
                   
                }
                ResultTextBox.Text = "Question: " + result + "Answer: " + num;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void PictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}