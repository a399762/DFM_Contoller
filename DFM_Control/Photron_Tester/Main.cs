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

namespace DFM_Photron
{
    public partial class Main : Form
    {
        private PhotronCamera photronDevice;
   
        public Main()
        {
            InitializeComponent();

            photronDevice = new PhotronCamera();
            photronDevice.OnLiveFeedNewFrame += PhotronDevice_OnLiveFeedNewFrame;
        }

        private void ConnectCamera()
        {
            try
            {
                photronDevice.CloseCamera();
            }
            catch (Exception)
            {
            }


            try
            {

                photronDevice.ConnectCamera(Properties.Settings.Default.CamIP);

                photronDevice.StartLiveFeed();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }



        /// <summary>
        /// test connect,.. remove later
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button1_Click(object sender, EventArgs e)
        {
            //get from settings
            string address = "192.168.0.10";

            try
            {
                photronDevice.CloseCamera();
            }
            catch (Exception)
            {
            }


            try
            {

                photronDevice.ConnectCamera(address);

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
            if (livePictureBox.InvokeRequired)
            {
                livePictureBox.Invoke(new MethodInvoker(
                delegate ()
                {
                    livePictureBox.Image = img;
                }));
            }
            else
            {
                livePictureBox.Image = img;
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
        /// <summary>
        /// temp button for testing starting of recording.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //record start
            try
            {
                photronDevice.StartRecording();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        /// <summary>
        /// test recording stop button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                photronDevice.StopRecording();

                //grab and view file.

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //connect
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //disconect
            try
            {
                photronDevice.CloseCamera();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if on mem tab, 

            // Update information of memory iamges  

        }

        private void button4_Click(object sender, EventArgs e)
        {
      
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = @"C:\";      
                saveFileDialog1.Title = "Save video File";
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.CheckPathExists = true;
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.Filter = "MRAW File(*.mraw)|*.mraw";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                   

               
                    // Create window of file saving status 


                    //    photronDevice.GetMrawFile(saveFileDialog1.FileName, saveFileStartFrameNo, saveFileEndFrameNo, savingFileWindow);
                       // savingFileWindow.Show();
                    
            }
            
        }
    }
}
