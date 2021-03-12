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
   
        public Form1()
        {
            InitializeComponent();
            string address = "192.168.0.10";

            photronDevice = new PhotronCamera(address);
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            photronDevice.OnLiveFeedNewFrame += PhotronDevice_OnLiveFeedNewFrame;


            try
            {
                photronDevice.CloseCamera();
            }
            catch (Exception)
            {
                String t = "'";
            }


            try
            {

                photronDevice.ConnectCamera();

                photronDevice.StartLiveFeed();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void PhotronDevice_OnLiveFeedNewFrame(object sender, LiveFeedEventArgs e)
        {
            //invoke this...
            var newFrame = e.Frame;
            SetPicture(newFrame);
        }

        private void SetPicture(Image img)
        {
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new MethodInvoker(
                delegate ()
                {
                    pictureBox1.Image = img;
                }));
            }
            else
            {
                pictureBox1.Image = img;
            }
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void focalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void darkCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void illuminationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dimensionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
