using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.QrCode;

namespace PharmacyManagement
{
    public partial class BarcodeScanner : Form
    {

        FilterInfoCollection CaptureDevice;
        VideoCaptureDevice FinalFrame;

        public string decodedstr { get; set; }

        public BarcodeScanner()
        {
            InitializeComponent();
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            FinalFrame = new VideoCaptureDevice(CaptureDevice[0].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                BarcodeReader Reader = new BarcodeReader();
                Result result = Reader.Decode((Bitmap)barcodeCam.Image);
                string decoded = result.ToString().Trim();

                if (decoded != "")
                {
                    barcodeCam.Image = null;
                    this.decodedstr = decoded;
                    this.DialogResult = DialogResult.OK;
                    FinalFrame.Stop();
                    timer1.Stop();
                    this.Close();


                }
            }
            catch (Exception ex)
            {

            }
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //throw new NotImplementedException();
            barcodeCam.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void BarcodeScanner_Load(object sender, EventArgs e)
        {
            FinalFrame.Start();
            timer1.Enabled = true;
        }

        private void BarcodeScanner_FormClosed(object sender, FormClosedEventArgs e)
        {
            FinalFrame.Stop();
            timer1.Stop();
            this.Close();
        }
    }
}
