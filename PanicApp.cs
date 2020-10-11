using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PanicButtonAlarm
{
    public partial class PanicApp : Form
    {
        int buttonmikro;
        int onoff = 0;
        public PanicApp()
        {
            InitializeComponent();
            string tanggal = DateTime.Now.ToString("dd MMM yyyy");
            string waktu = DateTime.Now.ToString("hh:mm:ss tt");
            label5.Text = waktu+"  ||  "+tanggal;
            label6.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            button3.Enabled = true;
            button4.Enabled = false;
            button3.BringToFront();
            label4.Text = "Not Connected";
            label4.ForeColor = System.Drawing.Color.Red;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (comboBox2.Text == "")
            {
                label4.Text = "Please Select Baudrate";
                Task.Delay(1000).Wait();
                label4.Text = "Not Connected";
                label4.ForeColor = System.Drawing.Color.Red;
            }
            else if (comboBox1.Text == "")
            {
                label4.Text = "Please Select Port";
                Task.Delay(1000).Wait();
                label4.Text = "Not Connected";
                label4.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                label4.Text = "Connected";
                label4.ForeColor = System.Drawing.Color.LightGreen;
                button3.Enabled = false;
                button4.Enabled = true;
                button4.BringToFront();
                serialPort1.Open();
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            buttonmikro = serialPort1.ReadChar();
            if (buttonmikro == 'D')
            {

                System.IO.Stream str = Properties.Resources.alarm;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
                onoff = 1;

            }

        }


        private void PanicApp_Load(object sender, EventArgs e)
        {
            String[] portList = System.IO.Ports.SerialPort.GetPortNames();
            foreach (String portName in portList)
            comboBox1.Items.Add(portName); 
            comboBox1.Text = comboBox1.Items[comboBox1.Items.Count - 1].ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.IO.Stream str = Properties.Resources.alarm;
            System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
            snd.Stop();
            onoff = 0;
            label6.Visible = false;
        }
        bool blink = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (onoff==1)
            {
                if (blink)
                {
                    label6.Visible = true;
                }
                else
                {
                    label6.Visible = false;
                }
                blink = !blink;
            }
        }
    }
}
