using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace Nurbs
{
    class basisFun
    {
        //基底函數的階數由節點向量決定
        private KnotVector m_t;// = new KnotVector();

        #region 建構子 寫入 節點向量
        public basisFun(KnotVector t)  //set knotVector, degree
        {
            knotVector = t;
        }
        #endregion

        #region 節點向量 唯讀
        public KnotVector knotVector
        {
            get { return m_t; }
            private set
            { m_t = value; }
        }
        #endregion

        #region 使用基底函數的function N(indexknotVector, inputU)
        public double n(int i, double u)
        {
            return N(i, m_t.LvDegree, u);
        }
        #endregion

        #region 數學定義
        private double N(int i, int k, double U) //blending function 基底函數
        {
            if (k != 0)
            {
                double L_UP = U - m_t[i];
                double L_DOWN = m_t[i + k] - m_t[i];

                double L = 0.0;
                if (L_DOWN > 0 && L_UP > 0)
                    L = L_UP / L_DOWN * N(i, k - 1, U);

                double R_UP = m_t[i + 1 + k] - U;
                double R_DOWN = m_t[i + 1 + k] - m_t[i + 1];

                double R = 0.0;
                if (R_DOWN > 0 && R_UP > 0)
                    R = R_UP / R_DOWN * N(i + 1, k - 1, U);

                return L + R;
            }
            else
            {
                if ((m_t[i] <= U) && (U < m_t[i + 1]))
                    return 1.0000;
                else
                    return 0.0000;
            }
        }
        #endregion
    }
}

namespace Nurbs
{
    class KnotVector
    {
        private List<double> m_knots = new List<double>();

        #region 數學定義
        private void creatVector()
        {
            if ((m_lvDegree != 0) &&   //如果有值，就創造一組
                (m_totalCtrlPt != 0))  //如果有值，就創造一組
            {
                m_knots.Clear();
                foreach (int iKnots in m_knots)
                {
                    if (iKnots <= m_lvDegree)
                        m_knots.Add(0);  //T.min = 0
                    else if (m_lvDegree < iKnots && iKnots < m_totalCtrlPt)
                        m_knots.Add((double)((double)iKnots - (double)m_lvDegree) / (double)((double)m_totalCtrlPt - (double)m_lvDegree)); //i-k / n+1-k(for Normalize)
                    else if (m_totalCtrlPt <= iKnots)
                        m_knots.Add(1.0);  //T.max = 1
                    else
                        Debug.Assert(false);

                }
            }
        }
        #endregion

        #region 建構子，初始化屬性
        public KnotVector()
        {
            m_lvDegree = 0;
            m_totalCtrlPt = 0;
        }
        #endregion

        #region 控制點總數 i/o
        private int m_totalCtrlPt;
        public int TotalCtrlPoint
        {
            private get { return m_totalCtrlPt; }
            set
            {
                m_totalCtrlPt = value;  // math define: ctrl point  = n + 1
                creatVector();          // 輸入ctrl pint 時檢查是否可以創造 knot vector
            }
        }
        #endregion

        #region 階數, 次方 i/o
        private int m_lvDegree;
        public int LvDegree
        {
            get { return m_lvDegree; }
            set
            {
                Debug.Assert(value >= 2);  //math define k >= 2
                m_lvDegree = value;
                creatVector();      //輸入ctrl pint 時檢查是否可以創造 knot vector
            }
        }
        #endregion

        #region 節點向量大小
        public long Size
        {
            get { return TotalCtrlPoint + LvDegree + 1; }
        }
        #endregion

        #region 索引值 operator[]
        public double this[int index]
        {
            private set { }
            get
            {
                if (index < Size)
                    return m_knots[index];
                else
                    Debug.Assert(false);
                return 0;
            }
        }
        #endregion
    }
}

namespace Nurbs
{

    class surface
    {
        #region 平面的控制點
        private List<List<Point>> m_ctrlPoint = new List<List<Point>>(); //平面的控制點
        public int totalXCtrlPt
        {
            private set { }
            get { return m_ctrlPoint.Count(); }
        }
        public int totalYCtrlPt
        {
            private set { }
            get { return m_ctrlPoint[0].Count(); }
        }
        public List<List<Point>> CtrlPt
        {
            set
            {
                m_ctrlPoint = value;

                //設定控制點，同時設定 節點向量的...控制點總數
                m_xt.TotalCtrlPoint = totalXCtrlPt;
                m_yt.TotalCtrlPoint = totalYCtrlPt;

                m_xN = new basisFun(m_xt);
                m_yN = new basisFun(m_yt);
            }
            get { return m_ctrlPoint; }
        }
        #endregion

        #region 節點向量
        private KnotVector m_xt;// = new KnotVector();
        private KnotVector m_yt;// = new KnotVector();

        public int Degree
        {
            set
            {
                m_xt.LvDegree = value;
                m_yt.LvDegree = value;
            }
            get
            {
                return m_xt.LvDegree;
            }
        }
        
        public KnotVector xKnotVector
        {
            set
            {
                m_xt = value;
            }
            private get
            {
                return m_xt;
            }
        }
        public KnotVector yKnotVector
        {
            set
            {
                m_yt = value;
            }
            private get
            {
                return m_yt;
            }
        }
        #endregion

        #region 基底函數
        private basisFun m_xN;// = new basisFun(xKnotVector);
        private basisFun m_yN;
        #endregion

        //之後要做分母唷！
        int m_xWidth;
        int m_yHeight;
        public void setRect(int w, int h)
        {
            m_xWidth  = w;
            m_yHeight = h;
        }
        

        #region 數學定義
        public Point Ans(double u)
        {
            Point isrtPoint = new Point(0, 0);
            for (int i = 0; i < m_ctrlPoint.Count(); ++i)
                for (int j = 0; i < m_ctrlPoint[0].Count(); ++j)
                {
                    isrtPoint.X += (int)(m_ctrlPoint[i][j].X * m_xN.n(i, u) * m_yN.n(j, u));
                    isrtPoint.Y += (int)(m_ctrlPoint[i][j].Y * m_xN.n(i, u) * m_yN.n(j, u));
                }
            return isrtPoint;
        }
        #endregion
    }
}
