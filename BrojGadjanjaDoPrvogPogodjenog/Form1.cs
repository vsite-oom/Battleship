using System;
using System.CodeDom.Compiler;
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

namespace NasumicnaTaktikaGadjanja
{
    public partial class Form1 : Form
    {
        public enum ShootingType { 
            LargestShip,
            OneSquare
        }

        BackgroundWorker worker;
        String workingString = "";
        int numOfRep=0;
        List<decimal> res;
        List<decimal> res2;
        int divider=1;



        public Form1()
        {
            InitializeComponent();

            progressBar1.Maximum = 1000;
            worker = new BackgroundWorker { WorkerReportsProgress = true };
            worker.WorkerSupportsCancellation = true;
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            for (int i = 0; i < 10; i++) {
                DataGridViewColumn col = new DataGridViewTextBoxColumn();
                col.Width = 70;
                col.HeaderText = "Step: " + (i + 1);              
                resultGridView.Columns.Add(col);
            }
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
                working.Text = "Done";
                progressBar1.Value = 0;
                
                resultGridView.Rows.Add(divider,numOfRep,"Largest Ship", res[0], res[1], res[2], res[3], res[4], res[5], res[6], res[7], res[8], res[9]);
                divider++;
                resultGridView.Rows.Add(divider,numOfRep, "One Square", res2[0], res2[1], res2[2], res2[3], res2[4], res2[5], res2[6], res2[7], res2[8], res2[9]);
                resultGridView.Rows[divider-1].DividerHeight = 2;
                divider++;
                
            }
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;

        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            workingString = "Working : LargestShip";
            Calculate( ShootingType.LargestShip,e);
            workingString = "Working : OneSquare";
            Calculate( ShootingType.OneSquare,e);
 
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            working.Text = workingString;
            
        }

       

        private void Calculate( ShootingType type, DoWorkEventArgs e) {
            int i;
            List<decimal> results = new List<decimal>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
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
                int index=0;
                while (fleet.AllShipsSunken() != true)
                {
                    
                    while (hitResult != HitResult.Hit)
                    {
                        
                        Square target = gunner.NextTarget();
                        hitResult = fleet.Hit(target);
                        gunner.ProcessHitResult(hitResult);
                        results[index]++;
                        


                    }
                    while (hitResult != HitResult.Sunken)
                    {

                        Square target = gunner.NextTarget();
                        hitResult = fleet.Hit(target);
                        gunner.ProcessHitResult(hitResult);
                    }
                    index++;
                }
                

            }
            if (type == ShootingType.OneSquare)
            {
                res2 = results.Select(x => x / numOfRep).ToList();
               
            }
            else
            {
                res = results.Select(x => x / numOfRep).ToList();
                
            }
           
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

        private void button4_Click(object sender, EventArgs e)
        {
            DisableButtons();
            numOfRep = 500000;
            progressBar1.Maximum = 500000;
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
            button4.Enabled = false;
        }

        
    }
}
