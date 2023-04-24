using System;
using System.ComponentModel;
using System.Windows.Forms;
using libWiiSharp;

namespace FriishProduce
{
    public partial class HBCtransmitter : Form
    {
        private string WADfile;
        public HBCtransmitter(string w) { WADfile = w; InitializeComponent(); SetTopLevel(true); } 
        private void ProgressChanged(object sender, ProgressChangedEventArgs e) => progressBar.Value = e.ProgressPercentage;

        private void HBCtransmitter_Shown(object sender, EventArgs e) => timer.Start();

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            try
            {
                Protocol p = (Protocol)System.Enum.ToObject(typeof(Protocol), Properties.Settings.Default.hbc_protocol);
                HbcTransmitter t = new HbcTransmitter(p, Properties.Settings.Default.hbc_ip);
                t.Compress = true;
                t.Progress += new System.EventHandler<ProgressChangedEventArgs>(ProgressChanged);

                if (!t.TransmitFile(WADfile)) throw new Exception(t.LastError);
            }
            catch (Exception ex) { this.Hide(); MessageBox.Show(String.Format(Strings.errorTransmitter, ex.Message)); }
            finally { this.Dispose(); }
        }
    }
}
