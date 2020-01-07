using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebEye;

namespace Ipcam_WebEye
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeForm();
            System.Windows.Forms.Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeForm();
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            connectIpcam();
        }

        private void bGetPict_Click(object sender, EventArgs e)
        {
            Bitmap b = null;
            try
            {
                b = streamPlayerControl1.GetCurrentFrame();
                if (b != null)
                {
                    pictureBox1.Image = b;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    bSavePict.Enabled = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void bSavePict_Click(object sender, EventArgs e)
        {
            string picName = "Picture1";
            try
            {
                if (pictureBox1.Image != null)
                {
                    using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                    {
                        fbd.ShowDialog();
                        string recPath = fbd.SelectedPath;
                        pictureBox1.Image.Save(recPath + @"\" + picName + ".jpg", ImageFormat.Jpeg);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void connectIpcam()
        {
            string tempUrl = txtUrl.Text;
            var url = new Uri(tempUrl);
            try
            {
                if (tempUrl != "")
                {
                    streamPlayerControl1.StartPlay(url);
                    bGetPict.Enabled = true;
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void closeForm()
        {
            try
            {
                if (streamPlayerControl1.IsPlaying)
                {
                    streamPlayerControl1.Stop();
                }
            }
            catch (Exception e)
            {

            }

            this.Dispose();
        }
    }
}
