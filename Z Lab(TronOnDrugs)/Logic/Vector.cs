using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Z_Lab_TronOnDrugs_.Logic
{
    public class Vector
    {
        public Point StartPoint;
        public Point EndPoint;
        public string sorce;

        public Vector(int StartX, int StartY, int EndX, int EndY)
        {
            StartPoint = new Point(StartX, StartY);
            EndPoint = new Point(EndX, EndY);
        }
        public Vector(Point StartPoint, Point EndPoint)
        {
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
        }


        public bool VectorsIntersect(Vector vektor)
        {
            double s;               //s(x2 - x1) - t(x4 - x3) = x3 - x1     (1)
            double t;               //s(y2 - y1) - t(y4 - y3) = y3 - y1     (2)

            double s1 = this.EndPoint.X - this.StartPoint.X;
            double s2 = this.EndPoint.Y - this.StartPoint.Y;
            double t1 = -(vektor.EndPoint.X - vektor.StartPoint.X);
            double t2 = -(vektor.EndPoint.Y - vektor.StartPoint.Y);
            double result1 = vektor.StartPoint.X - this.StartPoint.X;
            double result2 = vektor.StartPoint.Y - this.StartPoint.Y;


            t1 = t1 / s1;
            result1 = result1 / s1;     // s re rendezünk
            s1 = 1;

            t2 = t2 / s2;
            result2 = result2 / s2;     // s re rendezünk
            s2 = 1;

            t2 = t2 - t1;
            result2 = result2 - result1;  // 1. egyenletből kivonjuk a másodikat

            t = result2 / t2;

            t1 = t1 * t;

            if (t1 < 0)
            {
                s = result1 + (-1 * t1);
            }
            else if (t1 > 0)
            {
                s = result1 - t1;
            }
            else
            {
                s = result1;
            }
            return (s >= 0 && s <= 1) && (t >= 0 && t <= 1);

        }



    }
}
