using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMRAImage1024x768Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            string splash = String.Empty;
            string icon = String.Empty;

            string[] files = System.IO.Directory.GetFiles(".", "*.jpg");
            foreach (string file in files)
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(file);
                FixedSize(image, 1024, 748).Save(file + "_1024_748.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            grPhoto.Clear(System.Drawing.Color.FromArgb(0, 0, 0, 0));
            grPhoto.DrawImage(imgPhoto, new System.Drawing.Rectangle(destX, destY, destWidth, destHeight), new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), System.Drawing.GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
    }
}
