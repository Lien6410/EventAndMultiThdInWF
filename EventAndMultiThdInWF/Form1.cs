﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventAndMultiThdInWF
{
    public partial class Form1 : Form
    {
        public Puber puber;

        public Form1()
        {
            InitializeComponent();
            puber = new Puber();
            puber.OnEvent += TestFunc;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread myThd1 = new Thread(new ThreadStart(Work));
            myThd1.IsBackground = true;

            Thread myThd2 = new Thread(new ThreadStart(Work));
            myThd2.IsBackground = true;

            myThd1.Start();
            Thread.Sleep(5);
            myThd2.Start();
        }

        private void Work()
        {
            var temp = DateTime.Now.ToString("MM/dd hh:mm:ss:fff");
            var msg = $"[{temp} : ThdId = {Thread.CurrentThread.ManagedThreadId} : Trigger]";
            puber.Trigger(msg);
        }

        public void TestFunc(object sender, string msg)
        {
            TextBoxAdd(textBox1, msg);
        }

        public delegate void TextBoxAddCallBack(TextBox textBox, string msg);

        private void TextBoxAdd(TextBox textBox, string msg)
        {
            if (textBox.InvokeRequired)
            {
                TextBoxAddCallBack callBack = new TextBoxAddCallBack(TextBoxAdd);
                this.Invoke(callBack, new object[] { textBox, Thread.CurrentThread.ManagedThreadId + msg });
                
            }
            else
            {
                var temp = DateTime.Now.ToString("MM/dd hh:mm:ss:fff");
                textBox.Text += $"[{temp} {textBox.InvokeRequired} : ThdId = {Thread.CurrentThread.ManagedThreadId} : Handler] [{msg}]" + Environment.NewLine;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TextBoxAdd(textBox1, Thread.CurrentThread.ManagedThreadId.ToString());
        }
    }
}
