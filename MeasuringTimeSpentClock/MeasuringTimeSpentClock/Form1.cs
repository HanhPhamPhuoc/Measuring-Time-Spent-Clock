using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MeasuringTimeSpentClock
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 100; // set interval
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
            timeLabel.Text = DateTime.Now.ToString("F");
            timeLabel.Anchor = (AnchorStyles)(AnchorStyles.Left) | (AnchorStyles.Right);
            timeLabel.AutoSize = false;
            //timeLabel.Dock = DockStyle.Fill;
            timeLabel.TextAlign = ContentAlignment.MiddleCenter;
            //MessageBox.Show(DateTime.Now.ToString("mm, yyyy hh:mm:ss"));
            label5.Text = DateTime.Now.ToString("F");
            this.Opacity = .83;
        }
            private void Timer_Tick(object sender, EventArgs e)
            {
                timeLabel.Text = DateTime.Now.ToString("F"); //mm, yyyy hh:mm:ss
                Refresh();
            //if (isStarted)
            {
                TimeSpan elapsedTime = sw.Elapsed;
                timeCount_label.Text = elapsedTime.ToString();
                string finalString = "";
                finalString = finalString + elapsedTime.Hours + " Hours " +
                    elapsedTime.Minutes + " Minutes " + elapsedTime.Seconds +  " Seconds " +
                    elapsedTime.Milliseconds + " Miliseconds ";
                timeString.Text = finalString;
                if (!minOnce && sw.Elapsed.Seconds == 3) // Auto minimize after 3 seconds once, for lazy people 
                {
                    try
                    {
                        Form1.ActiveForm.WindowState = FormWindowState.Minimized; 
                    }
                    catch (Exception)
                    {
                    }
                    minOnce = true;
                }
            }
            }
        Stopwatch sw = Stopwatch.StartNew();
        bool isStopped = false;
        bool minOnce = false;
        //bool isStarted = false;
        private void BtnStart_Click(object sender, EventArgs e)
        {
            //isStarted = true;
            if (!isStopped)
            {
                sw = Stopwatch.StartNew();
                isStopped = false;
            }
            else
            {
                sw.Start();
            }
            label2.Text = "";
            label1.Text = "Started ! ";
            label5.Text = DateTime.Now.ToString("F");

        }
        private void BtnStop_Click(object sender, EventArgs e)
        {
            sw.Stop();
            isStopped = true;
            //isStarted = false;
            label1.Text = "";
            label2.Text = "Stopped !";
            label7.Text = DateTime.Now.ToString("F");
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            sw.Reset();
            label5.Text = "";
            label7.Text = "";
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program was made by Yuki in his free time");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                timer.Interval = int.Parse(textBox1.Text);
            }
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) // Doesn't work yet
        {
            switch (e.KeyCode)
            {
                case Keys.M: // M for minimizing 
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case Keys.A: // A for "Start"
                    BtnStart_Click(sender, e);
                    break;
                case Keys.S: // S for "Stop"
                    BtnStop_Click(sender,e);
                    break;
                case Keys.D: // D for "Refresh"
                    BtnRefresh_Click(sender, e);
                    break;
                default:
                    break;
            }
        }


    }
}
