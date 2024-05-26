using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotLightDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.FormClosing += Form1_FormClosing;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.spotLightControl1.StopAnimation();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.spotLightControl1.StartAnimation();
        }
    }
}
