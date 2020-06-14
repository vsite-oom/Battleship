using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace BrojGadjanjaDoPrvogPogodjenog
{
    public partial class Form1 : Form
    {
        public enum ShootingType { 
            LargestShip,
            OneSquare
        }

        BackgroundWorker worker;
        decimal result1;
        decimal result2;
        String workingString = "";
        int numOfRep=0;

        public Form1()
        {
            InitializeComponent();

            progressBar1.Maximum = 1000;
            worker = new BackgroundWorker { WorkerReportsProgress = true };
            worker.WorkerSupportsCancellation = true;
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            if (e.Error != null)
            {
                working.Text = e.Error.Message;
            }
            else if (e.Cancelled)
            {
                working.Text = "cancelled";
            }
            else
            {          
                prosjekPokusaja1.Text = result1.ToString();
                prosjekPokusaja2.Text = result2.ToString();
                working.Text = "Done";
                progressBar1.Value = 0;
            }
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;

        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            workingString = "Working : LargestShip";
            result1 = Calculate( ShootingType.LargestShip,e);
            workingString = "Working : OneSquare";
            result2 = Calculate( ShootingType.OneSquare,e);
 
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            working.Text = workingString;
            
        }

       

        private decimal Calculate( ShootingType type, DoWorkEventArgs e) {
            decimal prosjek1;
            int i;
            decimal count = 0;
            
            
            
            for (i = 0; i < numOfRep; ++i)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }


                worker.ReportProgress(i);
                Shipwright sw = new Shipwright(10, 10);
                Fleet fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
                Gunner gunner = new Gunner(10, 10, new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
                if (type == ShootingType.OneSquare) {
                    gunner.shipLenght = 1;
                }
                HitResult hitResult = HitResult.Missed;

                while (hitResult != HitResult.Hit)
                {

                    Square target = gunner.NextTarget();
                    hitResult = fleet.Hit(target);
                    gunner.ProcessHitResult(hitResult);
                    count++;


                }

            }
           
            return prosjek1 = count / numOfRep;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DisableButtons();

            numOfRep = 1000;
            progressBar1.Maximum = 1000;
            worker.RunWorkerAsync();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DisableButtons();
            numOfRep = 10000;
            progressBar1.Maximum = 10000;
            worker.RunWorkerAsync();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DisableButtons();
            numOfRep = 100000;
            progressBar1.Maximum = 100000;
            worker.RunWorkerAsync();

        }

        private void StopButton_Click(object sender, EventArgs e)
        {

            worker.CancelAsync();
          
        }

        private void DisableButtons() {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }
    }
}
