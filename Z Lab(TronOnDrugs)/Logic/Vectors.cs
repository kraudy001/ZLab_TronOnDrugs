﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Z_Lab_TronOnDrugs_.Logic
{
    public class Vectors
    {
        private double displayWidth;
        private double displayHeight;
        private double lineWidth;

        public Point StartPoint;
        public Point EndPoint;
        public string sorce;

        #region Constructors
        public Vectors(int StartX, int StartY, int EndX, int EndY)
        {
            StartPoint = new Point(StartX, StartY);
            EndPoint = new Point(EndX, EndY);
        }
        public Vectors(Point StartPoint, Point EndPoint)
        {
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
        }
        public Vectors(double displayWidth, double displayHeight, double lineWidth)
        {
            this.displayWidth = displayWidth;
            this.displayHeight = displayHeight;
            this.lineWidth = lineWidth;
        }
        #endregion

        public bool VectorsIntersect(Vectors vektor)
        {
            if (this.StartPoint == vektor.StartPoint)
            {
                return true;
            }
            if(this.StartPoint == vektor.EndPoint)
            {
                return true;
            }
            if(this.EndPoint == vektor.EndPoint)
            {
                return true;
            }
            if(this.EndPoint == vektor.StartPoint)
            {
                return true;
            }

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

        public double DistanceBetweenEndPoints()
        {
            return Math.Sqrt(Math.Pow(EndPoint.X - StartPoint.X, 2) + Math.Pow(EndPoint.Y - StartPoint.Y, 2));
        }

        public Size LineSize
        {
            get
            {
                return new Size(5, 5); //preferably same numbers
            }
        }

        public Geometry Lines
        {
            get
            {
                GeometryGroup lines = new GeometryGroup();
                //walls
                lines.Children.Add(new RectangleGeometry(new Rect(0, 0, displayWidth, lineWidth)));
                lines.Children.Add(new RectangleGeometry(new Rect(0, 0, lineWidth, displayHeight)));
                lines.Children.Add(new RectangleGeometry(new Rect(displayWidth - lineWidth, 0, lineWidth, displayHeight)));
                lines.Children.Add(new RectangleGeometry(new Rect(0, displayHeight - lineWidth, displayWidth, lineWidth)));
                //lines
                lines.Children.Add(new RectangleGeometry(new Rect(StartPoint,LineSize)));
                return lines;
            }
        }
        public Geometry Barriers
        {
            get
            {
                GeometryGroup barriers = new GeometryGroup();
                barriers.Children.Add(new RectangleGeometry(new Rect(150, 150, displayWidth / 4, lineWidth)));
                barriers.Children.Add(new RectangleGeometry(new Rect(300, 500, displayWidth / 6, lineWidth)));
                barriers.Children.Add(new RectangleGeometry(new Rect(1600, 600, displayWidth / 10, lineWidth)));
                barriers.Children.Add(new RectangleGeometry(new Rect(1000, 70, lineWidth, displayHeight / 8)));
                barriers.Children.Add(new RectangleGeometry(new Rect(1200, 400, lineWidth, displayHeight / 10)));
                barriers.Children.Add(new RectangleGeometry(new Rect(800, 600, lineWidth, displayHeight / 5)));
                return barriers;
            }
        }
    }
}
