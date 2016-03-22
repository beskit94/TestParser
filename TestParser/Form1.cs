using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Add(chart1.Series.Count.ToString());
            chart1.Series[chart1.Series.Count - 1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[chart1.Series.Count-1].Points.Clear();
            string str = textBox1.Text;
            RPN rpn = new RPN(str);
            AlgTree at = new AlgTree();
            at.root = at.alg(rpn.istack);
            //double dd = at.alg2(at.root, 3); //проверка вычислений
            for (double d = -60; d <= 60; d += 0.1)
            {  
                try
                {
                    chart1.Series[chart1.Series.Count-1].Points.AddXY(d, at.alg2(at.root, d));
                }
                catch (DivideByZeroException ex)
                {
                    MessageBox.Show(ex.Message);
                    
                }
            }
            
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            chart1.ChartAreas[0].AxisX.Maximum = hScrollBar1.Value/2;
            chart1.ChartAreas[0].AxisX.Minimum = hScrollBar1.Value / -2;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            chart1.ChartAreas[0].AxisY.Maximum = vScrollBar1.Value / 2;
            chart1.ChartAreas[0].AxisY.Minimum = vScrollBar1.Value / -2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series.Add(chart1.Series.Count.ToString());
            chart1.Series[chart1.Series.Count - 1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(chart1.Series.Count != 0)
            chart1.Series.RemoveAt(chart1.Series.Count - 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = chart1.Series.Count; i > 0; i--)
            {
                chart1.Series.RemoveAt(i-1);
            }
        }
    }
}
