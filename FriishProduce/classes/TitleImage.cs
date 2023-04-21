using System.Drawing;

namespace FriishProduce
{
    public class TitleImage
    {
        internal Bitmap image;
        internal string path;

        internal Bitmap VCPic;
        internal Bitmap IconVCPic;
        internal Bitmap SaveBanner;
        internal bool shrinkToFit;
        internal int[] SaveImage_Position_Size;


        internal TitleImage()
        {
            image = null;
            path = null;
            VCPic = null;
            IconVCPic = null;
            shrinkToFit = false;
        }

        internal void Generate()
        {
            image = new Bitmap(path);
            path = null;
            VCPic = new Bitmap(256, 192);
            IconVCPic = new Bitmap(128, 96);
            IconVCPic = new Bitmap(128, 96);
            shrinkToFit = false;
        }

        internal void ModifySaveBanner(string inTPL, Bitmap image)
        { 
        }

        internal void Dispose()
        {
            image.Dispose();
            path = null;
            VCPic.Dispose();
            IconVCPic.Dispose();
            SaveBanner.Dispose();
            shrinkToFit = false;
        }
    }
}
