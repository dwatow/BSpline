using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Nurbs
{
    class KnotVector
    {
        private
            List<double> m_knots = new List<double>();
        private
            void creatVector()
            {
                if (//(m_knots.empty()) &&   //如果是空的，就創造一組
                    (m_lvDegree != 0) &&   //如果有值，就創造一組
                    (m_totalCtrlPt != 0))  //如果有值，就創造一組
                {
                    m_knots.Clear();
                    //		for (int iKnots = 0; iKnots <= m_totalCtrlPt + m_lvDegree; ++iKnots) //T.size = n+1+k
                    for (int iKnots = 0; iKnots <= GetSize(); ++iKnots) //T.size = n+1+k
                    {
                        if (iKnots <= m_lvDegree)
                            m_knots.Add(0);  //T.min = 0
                        else if (m_lvDegree < iKnots && iKnots < m_totalCtrlPt)
                            //m_knots.push_back(iKnots-m_lvDegree); //i-k 
                            m_knots.Add((double)((double)iKnots - (double)m_lvDegree) / (double)((double)m_totalCtrlPt - (double)m_lvDegree)); //i-k / n+1-k(for Normalize)
                        else if (m_totalCtrlPt <= iKnots)
                            //m_knots.push_back(m_totalCtrlPt-m_lvDegree);  //T.max = n+1-k 
                            m_knots.Add(1.0);  //T.max = 1 (for Normalize)
                        else
                            Debug.Assert(false);
                            
                    }
                }
            }


        public KnotVector()
            {
                m_lvDegree = 0;
                m_totalCtrlPt = 0;
            }
        public long GetSize() { return m_totalCtrlPt + GetDegree() + 1; }
        public long GetMax() { return 1; }
        public long GetMin() { return 0; }
            
        public double this[int index]
            {
                get
                {
                    if (index < GetSize())
                        return m_knots[index];
                    else
                        Debug.Assert(false);
                    return 0;
                }
            }

        private
	        int m_totalCtrlPt;          //控制點
        // 	int n() const;              //控制點 = n+1
        public
            void SetTotalCtrlPoint(int totalPt)
            {
                m_totalCtrlPt = totalPt;  // math define: ctrl point  = n + 1
                creatVector();            // 輸入ctrl pint 時檢查是否可以創造 knot vector
            }

        private
	        int m_lvDegree;              //階數, 次方
        public bool SetDegree(int degree)
            {
                Debug.Assert(degree >= 2);  //math define k >= 2
                if (degree < 2)
                    return false;
                else
                {
                    m_lvDegree = degree;
                    creatVector();      //輸入ctrl pint 時檢查是否可以創造 knot vector
                    return true;
                }
                // 	creatVector();      //輸入ctrl pint 時檢查是否可以創造 knot vector
            }
        public int GetDegree()
            {
                return m_lvDegree;
            }
    }
}
