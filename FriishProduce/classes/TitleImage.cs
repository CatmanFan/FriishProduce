using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FriishProduce
{
    public class TitleImage
    {
        public string path { get; set; }

        public Bitmap VCPic { get; set; }
        public Bitmap IconVCPic { get; set; }
        public Bitmap SaveBanner { get; set; }
        public bool shrinkToFit { get; set; }
        public int[] SaveImage_Position_Size { get; set; }


        internal TitleImage()
        {
            path = null;
            VCPic = null;
            IconVCPic = null;
            shrinkToFit = false;
        }

        internal void Generate(InterpolationMode mode)
        {
            VCPic = new Bitmap(256, 192);
            IconVCPic = new Bitmap(128, 96);

            using (Graphics g = Graphics.FromImage(VCPic))
            {
                Rectangle r = new Rectangle(0, 0, 256, 192);

                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = mode;

                g.DrawImage(Image.FromFile(path), r, 0, 0, 256, 192, GraphicsUnit.Pixel);
            }

            using (Graphics g = Graphics.FromImage(IconVCPic))
            {
                Rectangle r = new Rectangle(0, 0, 128, 96);

                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = mode;

                g.DrawImage(Image.FromFile(path), r, 0, 0, 128, 96, GraphicsUnit.Pixel);
            }
        }

        internal void ModifySaveBanner(string inTPL, Bitmap image)
        { 
        }

        internal void Dispose()
        {
            path = null;
            VCPic.Dispose();
            IconVCPic.Dispose();
            SaveBanner.Dispose();
            shrinkToFit = false;
        }
    }
}
