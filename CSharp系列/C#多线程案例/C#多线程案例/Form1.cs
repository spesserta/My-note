using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace C_多线程案例
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //点击单线程做菜后其他按钮都无法点击，都在等待这个
            Thread.Sleep(3000);
            MessageBox.Show("素菜做好了");
            Thread.Sleep(5000);
            MessageBox.Show("荤菜做好了");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Thread创建一个新线程
            Thread t = new Thread(() =>
            {
                //在新线程里面运行做菜，点击按钮后其他按钮不会被锁住
                Thread.Sleep(3000);
                MessageBox.Show("素菜做好了");
                Thread.Sleep(5000);
                MessageBox.Show("荤菜做好了");
            }
                );
            t.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Task创建一个新线程（更推荐这个）
            Task.Run(() =>
            {
                //在新线程里面运行做菜，点击按钮后其他按钮不会被锁住
                Thread.Sleep(3000);
                MessageBox.Show("素菜做好了");
                Thread.Sleep(5000);
                MessageBox.Show("荤菜做好了");
            }
                );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //同时运行素菜和肉菜
            Task.Run(() =>
            {
                Thread.Sleep(3000);
                MessageBox.Show("素菜做好了");
                
            });
            Task.Run(() =>
            {
                Thread.Sleep(5000);
                MessageBox.Show("荤菜做好了");
            });
        }

        //async表示异步，异步线程和UI线程分开来了

        private async void button4_Click_1(object sender, EventArgs e)
        {
            //创建线程链表
            List<Task> ts = new List<Task>();
            //往线程链表里塞线程
            ts.Add(Task.Run(() =>
            {
                Thread.Sleep(3000);
                MessageBox.Show("素菜做好了");
            }));
            ts.Add(Task.Run(() =>
            {
                Thread.Sleep(5000);
                MessageBox.Show("荤菜做好了");
            }));
            //当所有链表里的线程执行完就执行这个
            Task.WhenAll(ts).ContinueWith(t =>
            {
                MessageBox.Show("开饭了");
            });
        }
    }
}
