using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Ejercicio2
{
    public partial class Form1 : Form
    {
      
        Process[] processes;

        public Form1()
        {
            InitializeComponent();
            processes = Process.GetProcesses();
            
          
        }

        public String LongitudCadena(string cad, int longitud)
        {
            return cad.Length > longitud? cad.Substring(0,12)+"...":cad;
        }

        public void ViewProcesses() 
        {
            string processName = "";
            string title = "";
            const string FORMAT = "{0,-8}{1,-20}{2,-20}\r\n";
            textBox1.Text = string.Format(FORMAT, "PID", "NAME", "TITLE");
            foreach (Process p in processes)
            {
                processName = LongitudCadena(p.ProcessName, 15);
                title = LongitudCadena(p.MainWindowTitle, 15);
                textBox1.Text += string.Format(FORMAT, p.Id, processName, title);

            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            ViewProcesses();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
               
               
                int pid = int.Parse(textBox2.Text);
                textBox1.Clear();
                Process p = Process.GetProcessById(pid);

                string processName = LongitudCadena(p.ProcessName, 15);
                string title = LongitudCadena(p.MainWindowTitle, 15);



                const string FORMAT = "{0,-8}{1,-20}{2,-20}\r\n";
                const string FORMAT2 = "{0,-8}{1,-20}\r\n";
                const string FORMAT3 = "{0,-20}{1,-35}\r\n";

                textBox1.Text = string.Format(FORMAT, "PID", "NAME", "TITLE");
                textBox1.Text += string.Format(FORMAT, p.Id, processName, title);

                textBox1.Text += String.Format("\r\n{0,-8}{1,-15}{2,-20}\r\n", "ID", "PRIORITY", "STATE");
                ProcessThreadCollection pt = p.Threads;
                foreach (ProcessThread t in pt)
                {
                    textBox1.Text += String.Format("{0,-8}{1,-15}{2,-20}\r\n", t.Id, t.PriorityLevel, t.ThreadState);
                }

                ProcessModuleCollection pm = p.Modules;


                textBox1.Text += string.Format("\r\n"+FORMAT3, "MODULE NAME", "FILE NAME");
                foreach (ProcessModule module in pm)
                {
                    string moduleName = LongitudCadena(module.ModuleName, 15);
                    string fileName = LongitudCadena(module.FileName, 15);

                    textBox1.Text += string.Format(FORMAT3, moduleName, fileName);
                }



            }
            catch (System.ComponentModel.Win32Exception)
            {
                Console.WriteLine("SORRY");
            }
            catch (FormatException)
            {
                Console.WriteLine("Introduce un dato válido");
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int pid = int.Parse(textBox2.Text);
                textBox1.Clear();
                Process p = Process.GetProcessById(pid);

                DialogResult r = MessageBox.Show("¿Seguro quieres cerrar?", "Procesos", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                switch (r)
                {

                    case DialogResult.OK:

                        p.CloseMainWindow();
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("No se cerro nada", "Procesos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
            catch (FormatException) 
            {
                Console.WriteLine("Introduce un dato válido");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int pid = int.Parse(textBox2.Text);
                textBox1.Clear();
                Process p = Process.GetProcessById(pid);

                DialogResult r = MessageBox.Show("¿Seguro quieres cerrar?", "Procesos", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                switch (r)
                {

                    case DialogResult.OK:

                        p.Kill();
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("No se cerro nada", "Procesos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Introduce un dato válido");
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string program = textBox2.Text;
                Process p = Process.Start(program);
            }
            catch
            {
                Console.WriteLine("Introduce un dato válido");
            }
        }
    }
}