using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhotronWrapper;
using PhotronWrapper.Models;

namespace Photron_Tester
{
    public partial class Form1 : Form
    {
        private PhotronCamera photronDevice;
        private Resolution selectedResolution;


        public Form1()
        {
            InitializeComponent();
        }
        CancellationTokenSource source = new CancellationTokenSource();
        private void button1_Click(object sender, EventArgs e)
        {
            string  address = "192.168.0.10";

            photronDevice = new PhotronCamera(address);

            try
            {
                if (photronDevice.ConnectCamera())
                {
                    //get res list
                    var resList = photronDevice.GetResolutionList();

                    selectedResolution = resList[0];


                    //start live feed.. not sure when to stop this yet,... does it need to stop during recording?
                    RealtimeLoadImageTask(source.Token);
                }
                else
                {
                    //report error
                    string ft = "";
                }
                

                String t = "";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void RealtimeLoadImageCancellableWork(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Cancelled work before start");
                cancellationToken.ThrowIfCancellationRequested();
            }

            while (true)
            {
                Thread.Sleep(1);
                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                try
                {
                    //ask device to load image
                    photronDevice.RealtimeLoadImage(selectedResolution);

                    //image should now be loaded in 
                }
                catch (PdclibException ex)
                {

                }

            }
        }

        public Task RealtimeLoadImageTask(CancellationToken ct)
        {
            return Task.Factory.StartNew(() => RealtimeLoadImageCancellableWork(ct), ct);
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                photronDevice.CloseCamera();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
