using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce.classes
{
    public class TitleImage
    {
        internal Bitmap image;
        internal string path;

        internal Bitmap VCPic;
        internal Bitmap IconVCPic;
        internal Bitmap SaveBanner;
        internal bool shrinkToFit;
        internal ;


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
