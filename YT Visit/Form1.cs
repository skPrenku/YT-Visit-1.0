using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Diagnostics;





namespace YT_Visit
{
    public partial class Form1 : Form
    {
        
        private static void Run(int hrs)
        {
            
            int seconds = hrs*60*60 * 1000;

            var timer = new System.Threading.Timer(TimerMethod, null, 0, seconds);
        }

        private static void TimerMethod(object o)
        {
            Process.Start("renewip.cmd");
        }
        public Form1()
        {
            InitializeComponent();

         

        }

        public async Task VisitLinesSlowly()
        {
            while (true)
            {
                foreach (string item in listFile.Items)
                {
                    visitUrl(item + "?autoplay=1", int.Parse(txtTimeToVisit.Text));
                    await Task.Delay(3 * 1000);

                }
            }
            
        }


        public void visitUrl(string url, int ct)
        {
            var prs = new ProcessStartInfo("firefox.exe");
            prs.Arguments = url;   // + " --incognito";
            Process.Start(prs);
          

            Thread.Sleep(ct * 1000);
            try
            {
                foreach (Process proc in Process.GetProcessesByName("firefox"))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }


        private void BtnBrows_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
              //  op.ShowDialog();
                if (op.ShowDialog() == DialogResult.OK)
                    txtFilePath.Text = op.FileName;
            }
            catch ( Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lines = File.ReadAllLines(txtFilePath.Text.Trim());
                foreach (string line in lines)
                {
                    listFile.Items.Add(line);
                }

                


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
           
        }


        private void BtnStart_Click(object sender, EventArgs e)
        {
           
                int time = int.Parse(txtTimeToRenew.Text);
                
                Run(time);
            
                VisitLinesSlowly();
            
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.iliri.net/");
        }
    }
}
