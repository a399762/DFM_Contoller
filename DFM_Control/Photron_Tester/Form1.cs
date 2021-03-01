using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhotronWrapper;

namespace Photron_Tester
{
    public partial class Form1 : Form
    {
        private PhotronCamera photronDevice;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string  address = "192.168.0.10";

            photronDevice = new PhotronCamera(address);

            try
            {
                var camera = photronDevice.ConnectCamera();
                
                if (camera != null)
                { 
                    //start poll for video
                
                }


                String t = "";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
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
    }
}
