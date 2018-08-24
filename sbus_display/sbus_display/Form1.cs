using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace sbus_display
{
    public partial class Form1 : Form
    {
        int[] channels = new int[12];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Blue, 1);

            int base_x = 100;
            int base_y = 100;
            int base_w = 20;
            int base_f = 40;
            int base_h = 200;

            for(int i=1;i<13;i++)
            {
                g.DrawRectangle(p, base_x + base_f * (i-1), base_y, base_w, base_h);
            }           

            Pen p2 = new Pen(Color.Green, base_w);

            for(int i = 0; i < 12; i++)
            {
                int x = base_x + base_w/2 + base_f * i;

                g.DrawLine(p2, x, base_y + base_h,x, base_y + base_h - channels[i]/10);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxCom.Items.AddRange( SerialPort.GetPortNames());
        }

        private void buttonConn_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen==false)
            {
                serialPort1.PortName = comboBoxCom.SelectedItem.ToString();
                serialPort1.BaudRate = 115200;
                serialPort1.Open();

                buttonConn.Text = "已连接";
            }
            else
            {
                serialPort1.Close();
                buttonConn.Text = "连接";
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string str = serialPort1.ReadLine();
            Console.Write(str);

            string[] arr = str.Split(new char[] { ' ' });
            for(int i=0;i<12;i++)
            {
                channels[i] = int.Parse(arr[i]);
            }

            Invalidate();
        }
    }
}
