using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class BannerPreviewForm : Form
    {
        public BannerPreviewForm(string title, int year, int players, Image img, int altRegion = 0)
        {
            InitializeComponent();
        }
    }
}
