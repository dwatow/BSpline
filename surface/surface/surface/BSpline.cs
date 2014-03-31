using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;

namespace Nurbs
{
    /*
    class surface1
    {
    public double _N(int i, int k, double U) //blending function 基底函數
        {
            if (k != 0)
            {
                double L_UP = U - t[i];
                double L_DOWN = t[i + k] - t[i];

                double L = 0.0;
                if (L_DOWN > 0 && L_UP > 0)
                    L = L_UP / L_DOWN * _N(i, k - 1, U);

                double R_UP = t[i + 1 + k] - U;
                double R_DOWN = t[i + 1 + k] - t[i + 1];

                double R = 0.0;
                if (R_DOWN > 0 && R_UP > 0)
                    R = R_UP / R_DOWN * _N(i + 1, k - 1, U);

                return L + R;
            }
            else
            {
                if ((t[i] <= U) && (U < t[i + 1]))
                    return 1.0000;
                else
                    return 0.0000;
            }
        }
    private List<List<Point>> ctrlPt = new List<List<Point>>();
    private void setTotalCtrlPoint()
        {
            t.SetTotalCtrlPoint(ctrlPt.Count());
        }
    //public void AddCtrlPoint(Point pt)
    //    {
    //        ctrlPt.Add(pt);
    //        setTotalCtrlPoint();
    //    }
    //public void AddCtrlPoint(int x, int y)
    //    {
    //        Point pt = new Point(x, y);
    //        ctrlPt.Add(pt);
    //        setTotalCtrlPoint();
    //    }
    public void AddCtrlPoint(List<List<Point>> vCPt)
        {
            ctrlPt = vCPt;
            setTotalCtrlPoint();
        }

    private
	    KnotVector t = new KnotVector();         //knot vector
    public
	    bool SetDegree(int degree)
        {
            return t.SetDegree(degree);
        }
    public Point Answer(double U)
        {
            Point Ans = new Point();
            //double thisN;

            //int count = 0;


            //foreach (Point it in ctrlPt)
            //{
            //    thisN = _N(indexPoint, U);



            //    Ans.X += (int)((double)it.X * xN);
            //    Ans.Y += (int)((double)it.Y * xN);
            //    count++;
            //}
	        return Ans;
        }
    public double Umax() { return t.GetMax(); }
    public double Umin() { return t.GetMin(); }
        //Point begin() { return ctrlPt.First(); }
        //Point end() { return ctrlPt.Last(); }
    }*/
}
