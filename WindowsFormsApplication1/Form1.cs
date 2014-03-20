using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nurbs;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        List<Point> m_sample = new List<Point>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            AddPoint(Cursor.Position.X, Cursor.Position.Y);
        }

        private void AddPoint(int x, int y)
        {
            Point pt = new Point(x, y);
	        m_sample.Add(pt);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Blue, 1);
            Point prePt = new Point();
            prePt = m_sample.FirstOrDefault();

            Graphics graph = CreateGraphics();
            foreach (Point pt in m_sample)
            {
                graph.DrawLine(pen, prePt, pt);
                prePt = pt;
            }

            graph.DrawLine(pen, 12, 129, 42, 112);

            //const int degree = 2;
            //BSpline curve = new BSpline();
            //curve.SetDegree(degree);

            //Graphics graph = CreateGraphics();
            //Pen pen = new Pen(Color.Blue, 2);

            //if (m_sample.Count() == degree)
            //{
            //    graph.DrawLine(pen, m_sample[0], m_sample[m_sample.Count()-1]);
            //}
            //else if (m_sample.Count() > degree)
            //{
            //    curve.AddCtrlPoint(m_sample);

            //    //normal U
            //    double DomainRange = (double)(curve.Umax()-curve.Umin());  //從tmax到tmin當作是u的定義域
            //    //double totalCoordLadder = (double)(100);
            //    double DomainStep = (DomainRange/6000);  //分成這麼多區間畫出曲線

            //    Point displayCtrlPt = new Point(1, 1);

            //    int ptCounter = 0;

            //    //graph.DrawLine(pen, m_sample[0]);
            //    Point prePt = m_sample[0];
            //    while (displayCtrlPt.IsEmpty)
            //    {
            //        ptCounter++;
            //        displayCtrlPt = curve.Answer(ptCounter * DomainStep);

            //        if (displayCtrlPt.IsEmpty)
            //            break;
            //        graph.DrawLine(pen, prePt, displayCtrlPt);
            //        prePt = displayCtrlPt;
            //    }
            //    //graph.DrawLine(pen, m_sample[m_sample.Count - 1]);

	        //}
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            cursor.Text = String.Format("{0}, {1}", Cursor.Position.X, Cursor.Position.Y);
        }
    }
}
