using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services;
using System.IO;
using System.Xml;
using Hardware;
using System.Threading;
using Hardware.CommunicationModule;

namespace Symulator_PLC
{
    public partial class Form1 : Form
    {
        public Panel actualPanel;
        public bool setDevices = false;
        Point loc = new Point();
        Hardware.PLC plc;


        public Form1()
        {
            InitializeComponent();
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);
            Devices.Device[] devices = Devices.Device.ListDevices(@"Devices\");
            //plc = PLC.InitializePLC(@"Devices\");
            Devices.TreeViewDevices tree = new Devices.TreeViewDevices(devices);
            this.treeView1.Nodes.AddRange(tree.root);
            actualPanel = welcomPanel;

            DataTransfer.PipeServer.DataReceivedOperation += PipeServer.DataReceivedOperation;
            DataTransfer.PipeServer.LoadDataToSend += PipeServer.LoadDataToSend;
            DataTransfer.PipeServer.ConnectionClose += PipeServer_ConnectionClose;
            S7_300_Engine.Engine.Initialize();

        }

        void PipeServer_ConnectionClose()
        {
            try
            {
                if (plc != null)
                {
                    List<string> keyInputsList = new List<string>(PLC.InputModule.Keys);
                    for (int i = 0; i < keyInputsList.Count; i++)
                    {
                        PLC.InputModule[keyInputsList[i]].State = false;
                    }
                }
            }
            catch { }

            System.Diagnostics.Debug.WriteLine("Połączenie z wirtualnym procesem bezpiecznie zamknięte");
        }

        #region titlesBar

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                loc = e.Location;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.SetDesktopLocation(MousePosition.X - loc.X, MousePosition.Y - loc.Y);
            }

        }

        private void label17_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                loc = e.Location;
            }
        }

        private void label17_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.SetDesktopLocation(MousePosition.X - loc.X, MousePosition.Y - loc.Y);
            }
        }




        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox1.BackgroundImage = Properties.Resources.close2;
            pictureBox1.BackColor = System.Drawing.SystemColors.Control;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Properties.Resources.close1;
            pictureBox1.BackColor = Color.Transparent;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            pictureBox2.BackColor = System.Drawing.SystemColors.Control;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent;
        }   

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;            
            this.Show();
        }
        #endregion



        private void specialButton1_Click(object sender, EventArgs e)
        {
            if (actualPanel != null)
            {
                actualPanel.Visible = false;
            }
            if (setDevices)
            {
                actualPanel = devicesPanel;
            }
            else
            {
                actualPanel = devicePanelChooseModule;
            }
            actualPanel.Visible = true;
        }

        private void specialButton2_Click(object sender, EventArgs e)
        {
            if (actualPanel != null)
            {
                actualPanel.Visible = false;
            }
            actualPanel = networkPanel;
            actualPanel.Visible = true;
            label2.Visible = false;
            List<string> adapterName = NetInfo.GetAvailableAdapterName();
            comboBox1.Items.Clear();
            if (adapterName != null && adapterName.Count != 0)
            {
                foreach (string name in adapterName)
                {
                    comboBox1.Items.Add(name);
                }
            }
            else
            {
                label2.Visible = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = NetInfo.GetIP(comboBox1.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void specialButton3_Click(object sender, EventArgs e)
        {
            if (actualPanel != null)
            {
                actualPanel.Visible = false;
            }
            actualPanel = virtualSystemPanel;
            actualPanel.Visible = true;
            try
            {
                button2.Visible = false;
                button5.Visible = false;
                List<string> names = VirtualSystem.VirtualSystemList(@"Virtual System\");
                if (names != null && names.Count != 0)
                {
                    comboBox2.Items.Clear();
                    foreach (var name in names)
                    {
                        comboBox2.Items.Add(name);
                    }
                    if (comboBox2.SelectedItem == null)
                    {
                        comboBox2.SelectedItem = comboBox2.Items[0];
                    }
                    button2.Visible = true;
                    button5.Visible = true;
                }
            }
            catch (DirectoryNotFoundException dnfe)
            {
                MessageBox.Show(dnfe.Message);
            }
            catch (FileNotFoundException fnfe)
            {
                System.Windows.Forms.MessageBox.Show(fnfe.Message +
                    "\n\rMożliwe przyczyny:" +
                    "\n\r1. Brak rozszerzenia w wyszukiwanej nazwie pliku" +
                    "\n\r2. Plik został usunięty w trakcie uruchamiania bądź działania programu.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Level == 2)
            {
                string orderNumber = treeView1.SelectedNode.Text;
                string shortDesignation = treeView1.SelectedNode.Parent.Text;
                string filePath = @"Devices\" + shortDesignation + @" (" + orderNumber + @").xml";
                try
                {
                    plc = new PLC(filePath);
                    int x = (devicesPanel.Size.Width / 2) - (PLC.Info.picture.Width/2);
                    int y = (devicesPanel.Size.Height / 2) - (PLC.Info.picture.Height/2);
                    pictureBox4.Size = new System.Drawing.Size(PLC.Info.picture.Width, PLC.Info.picture.Height);
                    pictureBox4.Location = new Point(x, y);
                    Bitmap bmp = new Bitmap(PLC.Info.picture.FilePath);
                    this.pictureBox4.Image = (System.Drawing.Image)bmp;
                    labelShortDesignation.Text = PLC.Info.device.modules[0].ShortDesignation;
                    labelOrderNumber.Text = PLC.Info.device.modules[0].OrderNumber;
                    labelFirmwareVersion.Text = PLC.Info.device.modules[0].Version;
                    foreach (var led in PLC.InputModule)
                    {
                        this.pictureBox4.Controls.Add(led.Value);
                    }
                    foreach (var led in PLC.OutputModule)
                    {
                        this.pictureBox4.Controls.Add(led.Value);
                    }
                    foreach (var led in PLC.CpuModule)
                    {
                        this.pictureBox4.Controls.Add(led.Value);
                    }
                    actualPanel = devicesPanel;
                    devicePanelChooseModule.Visible = false;
                    devicesPanel.Visible = true;
                    setDevices = true;

                    if (PLC.Info.device.FamilyName == "SIMATIC S7-300")
                    {
                        CommunicationModule.Server.DataReceived += S7_300_Engine.Engine.client_DataReceived;
                        S7_300_Engine.Engine.SetPLCInfo(PLC.Info.device.modules[0].ShortDesignation, PLC.Info.device.modules[0].OrderNumber);
                    }

                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        PLC.Run();
                    }
                    );
                }
                catch (FileNotFoundException fsince)
                {
                    MessageBox.Show(fsince.Message);
                }
                catch (Devices.FileStructIsNotCorectException fsince)
                {
                    MessageBox.Show(fsince.Message);
                }
            }
            else
            {
                MessageBox.Show("Wybierz konkretny model");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() != "")
            {

                try
                {
                    VirtualSystem.Load(comboBox2.SelectedItem.ToString() + ".xml");
                    label3.Text = VirtualSystem.Name;
                    label4.Text = "Number of digital sensors: " + VirtualSystem.NumberOfDigitalSensors.ToString();
                    label5.Text = "Number of digital actuators: " + VirtualSystem.NumberOfDigitalActuators.ToString();
                    label6.Text = "Description:";
                    textBox2.Text = VirtualSystem.Description;
                    textBox2.Visible = true;
                    pictureBox5.Size = new System.Drawing.Size(VirtualSystem.picture.Width, VirtualSystem.picture.Height);
                    Bitmap bmp = new Bitmap(VirtualSystem.picture.FilePath);
                    this.pictureBox5.Image = (System.Drawing.Image)bmp;
                }
                catch (ArgumentException ae)
                {
                    MessageBox.Show("Brak pliku z podglądowym obrazkiem");
                }
                catch (FileNotFoundException fsince)
                {
                    MessageBox.Show(fsince.Message);
                }
                catch (Devices.FileStructIsNotCorectException fsince)
                {
                    MessageBox.Show(fsince.Message);
                }
            }
        }



        //SynchronizationContext originalContext2;
        private void button5_Click(object sender, EventArgs e)
        {
            
            bool isRun = false;
            //System.Diagnostics.Debug.WriteLine(VirtualSystem.RunningProcess);
            if (VirtualSystem.RunningProcess.StartInfo.FileName != "")
            {

                if (!VirtualSystem.RunningProcess.HasExited)
                {
                    System.Diagnostics.Debug.WriteLine("Program uruchomiony");
                    isRun = true;
                }
            }
            else
            {
                isRun = false;
            }
            if (!isRun)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    DataTransfer.PipeServer.StartListening();
                }
                );
                try
                {
                    VirtualSystem.RunningProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(VirtualSystem.AssemblySources);
                    VirtualSystem.RunningProcess.Start();//(VirtualSystem.AssemblySources);                    
                }
                catch (Win32Exception)
                {
                    MessageBox.Show("Nie można uruchomić wybranego procesu");
                }                
            }
        }

        
        private Form connectionInfoForm = null;

        void okno_FormClosed(object sender, FormClosedEventArgs e)
        {
            connectionInfoForm = null;            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionInfoForm != null) return;
                connectionInfoForm = new Form();
                Bitmap bmp = new Bitmap(VirtualSystem.connectionInfoPicture.FilePath);
                connectionInfoForm.BackgroundImage = (System.Drawing.Image)bmp;
                connectionInfoForm.Size = new System.Drawing.Size(VirtualSystem.connectionInfoPicture.Width, VirtualSystem.connectionInfoPicture.Height + 18);
                connectionInfoForm.FormBorderStyle = FormBorderStyle.FixedSingle;
                connectionInfoForm.MaximizeBox = false;
                connectionInfoForm.Text = "Connection Info";
                connectionInfoForm.Show();

                connectionInfoForm.FormClosed += new FormClosedEventHandler(okno_FormClosed);
            }
            catch (Exception)
            {
                MessageBox.Show("Nie można załadować pliku\n\r" +
                                "Możliwe przyczyny:\n\r" +
                                "1.Brak pliku ze strukturą połączeń\n\r" +
                                "2.Struktura pliku xml odpowiadającego danemu procesowi jest nieprawidłowa");
            }
        }
        bool plcInfoVisible = false;
        private void button1_MouseHover(object sender, EventArgs e)
        {
            if (!plcInfoVisible)
            {
                panelPLCInfo.Visible = true;
            }
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            if (!plcInfoVisible)
            {
                panelPLCInfo.Visible = false;
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (plcInfoVisible)
            {
                panelPLCInfo.Visible = false;
                plcInfoVisible = false;
            }
            else
            {
                panelPLCInfo.Visible = true;
                plcInfoVisible = true;
            }
        }        
    }
}

