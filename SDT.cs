using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDT
{
    public class SDT
    {
        /// <summary>
        /// 压缩点集
        /// </summary>
        /// <param name="lstOrgPoints">原始数据</param>
        /// <param name="dThresholdValue">允许的偏差</param>
        /// <returns>压缩后的点集</returns>
        public static List<Point> CompactPoints(List<Point> lstOrgPoints,double dThresholdValue)
        {
            List<Point> lstRes = new List<Point>();
            double upLineSlope = double.MinValue;         //上斜率
            double downLineSlope = double.MaxValue;       //下斜率
            if (lstOrgPoints.Count<1)
            {
                return lstRes;
            }
            Point pPre = lstOrgPoints[0];
            lstRes.Add(pPre);
            for(int i=0;i< lstOrgPoints.Count;i++)
            {
                if(i<1)
                {
                    continue;
                }
                Point pCur = lstOrgPoints[i];
                double curSlope = GetLineSlope(pPre, pCur, 0);  //根据偏差获取斜率
                upLineSlope = Math.Max(GetLineSlope(pPre, pCur, -dThresholdValue), upLineSlope);
                downLineSlope = Math.Min(GetLineSlope(pPre, pCur, dThresholdValue), downLineSlope);
                if (upLineSlope >= downLineSlope|| i ==lstOrgPoints.Count-1)   //两条射线不会相交，保存此点
                {
                    lstRes.Add(pCur);
                    pPre = pCur;
                    upLineSlope = double.MinValue; ;
                    downLineSlope = double.MaxValue;   //重置上下斜率
                }
            }

            return lstRes;
        }
        private static double  GetLineSlope(Point p1,Point p2, double dThresholdValue)  //根据偏差获取斜率
        {
            double slope = 0;
            try
            {
                slope = (p2.Y - p1.Y+ dThresholdValue) / (p2.X - p1.X);
            }
            catch (Exception)
            {
            }
            return slope;
        }
       
    }
    public class Point
    {
        public double X;
        public double Y;
        public Point(double x,double y)
        {
            X = x;
            Y = y;
        }
        //public double X
        //{
        //    get { return X; }
        //    set { X = value; }
        //}
        //public double Y
        //{
        //    get { return Y; }
        //    set { Y = value; }
        //}
    }

}
