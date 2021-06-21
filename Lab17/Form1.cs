using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int n = 10000;
        private void btStart_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 6; i++)
            {
                chart1.Series[i].Points.Clear();
            }
            ModelPPP P1 = new ModelPPP((double)lam1.Value, n);
            ModelPPP P2 = new ModelPPP((double)lam2.Value, n);
            while (P1.t > P1.Time)
            {
                P1.ExpRV();
                chart1.Series[0].Points.AddXY(P1.Time, P1.lambda);
                chart1.Series[3].Points.AddXY(P1.Time, P1.lambda);
                chart1.Series[2].Points.AddXY(P1.Time, P1.lambda + P2.lambda);
                chart1.Series[5].Points.AddXY(P1.Time, P1.lambda + P2.lambda);
            }
            while (P2.t > P2.Time)
            {
                P2.ExpRV();
                chart1.Series[1].Points.AddXY(P2.Time, P2.lambda);
                chart1.Series[4].Points.AddXY(P2.Time, P2.lambda);
                chart1.Series[2].Points.AddXY(P2.Time, P1.lambda + P2.lambda);
                chart1.Series[5].Points.AddXY(P2.Time, P1.lambda + P2.lambda);
            }

            ModelPPP P3 = new ModelPPP((double)(lam1.Value + lam2.Value), n + n);
            int max1 = P1.Statistics();
            int max2 = P2.Statistics();
            int max3 = P3.Statistics();
            int size;
            if (max1 < max2)
            {
                size = max2 + 1;
            }
            else
            {
                size = max1 + 1;
            }
            double[] Freq = new double[size];
            double[] Freq1 = new double[max3 + 1];
            for (int i = 0; i < P1.N; i++)
            {
                Freq[P1.ArrayCountPoints[i]]++;
            }
            for (int i = 0; i < P2.N; i++)
            {
                Freq[P2.ArrayCountPoints[i]]++;
            }
            for (int i = 0; i < size; i++)
            {
                Freq[i] = Freq[i] / P1.N * 2;
            }
            for (int i = 0; i < P3.N; i++)
            {
                Freq1[P3.ArrayCountPoints[i]]++;
            }
            for (int i = 0; i < max3 + 1; i++)
            {
                Freq1[i] = Freq1[i] / P3.N;
            }
            chart2.Series[0].Points.Clear();
            for (int i = 1; i < size; i++)
            {
                chart2.Series[0].Points.AddXY(i, Freq[i]);
            }
            chart2.Series[1].Points.Clear();
            for (int i = 1; i < max3 + 1; i++)
            {
                chart2.Series[1].Points.AddXY(i, Freq1[i]);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ModelPPP P1 = new ModelPPP((double)lam1.Value, n);
            ModelPPP P2 = new ModelPPP((double)lam2.Value, n);
            while (P1.t > P1.Time)
            {
                P1.ExpRV();
                chart1.Series[0].Points.AddXY(P1.Time, P2.lambda);
                chart1.Series[3].Points.AddXY(P1.Time, P2.lambda);
            }
        }
    }
}
