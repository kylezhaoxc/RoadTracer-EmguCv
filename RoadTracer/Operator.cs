﻿using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace RoadTracer
{
    public class Operator
    {
        public static int Img_Width=640;
        public static int Img_Height = 480;
        public static int Roi_Height = 280;
        private Image<Bgr, byte> temp;
        public  void FindMiddleByPath(string url)
        {
            Image<Bgr, byte> img = new Image<Bgr, byte>(url);
            List<LineSegment2D> positiveLines = new List<LineSegment2D>();
            List<LineSegment2D> negativeLines = new List<LineSegment2D>();
            LineHelper helper = new LineHelper(Img_Height,Img_Width,Roi_Height);

            //store all the possible lines
            List<LineSegment2D> lines = helper.ExtractContour(img,out temp);

            //divide the lines into two parts.
            foreach (LineSegment2D line in lines)
            {
                float theta = line.Direction.Y / line.Direction.X;
                if (theta > 0) { positiveLines.Add(line); }
                else if (theta < 0) { negativeLines.Add(line); }
            }

            //find the average line of two groups above
            if (positiveLines.Count == 0) throw new TextException("No negative Line, Try turn right!");
            if (negativeLines.Count==0) throw new TextException("No positive  Line,Try turn left!");
            LineSegment2DF posline = helper.AvrageLine(positiveLines);
            LineSegment2DF negline = helper.AvrageLine(negativeLines);
            
            //draw two average lines.
            temp.Draw(posline, new Bgr(0, 255, 0), 5);
            temp.Draw(negline, new Bgr(0, 255, 0), 5);

            //Find and draw the vanishing point
            PointF crosspt = helper.GetIntersection(ref posline, ref negline);
            if (crosspt.Y > 140) throw new TextException("Near Corner");
            temp.Draw(new CircleF(crosspt, 3), new Bgr(0, 0, 255), 5);

            //Find the bisector
            float i = 0;
            for (i = 0; i < 640; i++)
            {
                PointF pt = new PointF(i, Roi_Height);
                if (Math.Abs(helper.GetDist(pt, posline) - helper.GetDist(pt, negline)) < 1)
                    break;
            }
            LineSegment2DF middleline = new LineSegment2DF(crosspt, new PointF(i, 280));

            #region test bisector
            //double angle1, angle2;
            //angle1 = middleline.GetExteriorAngleDegree(posline);
            //angle2 = middleline.GetExteriorAngleDegree(negline);
            //double angle3 = posline.GetExteriorAngleDegree(negline);
            #endregion
            temp.Draw(middleline, new Bgr(0, 0, 255), 5);
            temp.Save("D:\\temp\\lines.jpg");
        }
        public void FindMiddleByImg(Image<Bgr, byte> img)
        {
            List<LineSegment2D> positiveLines = new List<LineSegment2D>();
            List<LineSegment2D> negativeLines = new List<LineSegment2D>();
            LineHelper helper = new LineHelper(Img_Height, Img_Width, Roi_Height);

            //store all the possible lines
            List<LineSegment2D> lines = helper.ExtractContour(img, out temp);

            //divide the lines into two parts.
            foreach (LineSegment2D line in lines)
            {
                float theta = line.Direction.Y / line.Direction.X;
                if (theta > 0) { positiveLines.Add(line); }
                else if (theta < 0) { negativeLines.Add(line); }
            }

            //find the average line of two groups above
            if (positiveLines.Count == 0) throw new TextException("No negative Line");
            if (negativeLines.Count == 0) throw new TextException("No positive Line");
            LineSegment2DF posline = helper.AvrageLine(positiveLines);
            LineSegment2DF negline = helper.AvrageLine(negativeLines);

            //draw two average lines.
            //temp.Draw(posline, new Bgr(0, 255, 0), 5);
            //temp.Draw(negline, new Bgr(0, 255, 0), 5);

            //Find and draw the vanishing point
            PointF crosspt = helper.GetIntersection(ref posline, ref negline);
            if (crosspt.Y > 140) throw new TextException("Near Corner");
            //temp.Draw(new CircleF(crosspt, 3), new Bgr(0, 0, 255), 5);

            //Find the bisector
            float i = 0;
            for (i = 0; i < 640; i++)
            {
                PointF pt = new PointF(i, Roi_Height);
                if (Math.Abs(helper.GetDist(pt, posline) - helper.GetDist(pt, negline)) < 1)
                    break;
            }
            LineSegment2DF middleline = new LineSegment2DF(crosspt, new PointF(i, 280));

            #region test bisector
            //double angle1, angle2;
            //angle1 = middleline.GetExteriorAngleDegree(posline);
            //angle2 = middleline.GetExteriorAngleDegree(negline);
            //double angle3 = posline.GetExteriorAngleDegree(negline);
            #endregion
            temp.Draw(middleline, new Bgr(0, 0, 255), 5);
            //temp.Save("D:\\temp\\lines.jpg");
        }
        public Image<Bgr, byte> GetRoadImg()
        {
            return temp;
        }

    }
}