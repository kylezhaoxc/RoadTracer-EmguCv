using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace RoadTracer
{
    public class LineHelper
    {
        private  LineSegment2D[][] lines = null;
        public  int Img_Height { get; private set; }
        public  int Roi_Height { get; private set; }
        public  int Img_Width { get; private set; }

        public LineHelper(int i_h, int i_w, int r_h)
        {
            Img_Height = i_h;
            Img_Width = i_w;
            Roi_Height = r_h;
        }
        public  LineSegment2DF AvrageLine(List<LineSegment2D> lines)
        {
            float avg_k = 0, avg_c = 0;
            PointF p1 = new PointF(0,0), p2 = new PointF(0,0);
            foreach (LineSegment2D line in lines)
            {
                float k = line.Direction.Y / line.Direction.X;
                float c = line.P1.Y - k * line.P1.X;
                avg_k += k;avg_c += c;
            }
            avg_k /= lines.Count;avg_c /= lines.Count;
            p1.X = 0;p1.Y = avg_c;
            p2.X =640;p2.Y = 640*avg_k+avg_c;
            LineSegment2DF avg = new LineSegment2DF(p1,p2);
            return avg;
            //float tan_theta=0;
            //float c = 0;
            //foreach (LineSegment2D line in lines)
            //{
            //    tan_theta += line.Direction.Y / line.Direction.X;

            //}
            //tan_theta /= lines.Count;
            //return avg;
        }
        public  PointF GetIntersection(ref LineSegment2DF posline, ref LineSegment2DF negline)
        {
            float kp = posline.Direction.Y / posline.Direction.X;
            float cp = posline.P1.Y - kp * posline.P1.X;
            float kn = negline.Direction.Y / negline.Direction.X;
            float cn = negline.P1.Y - kn * negline.P1.X;
            float x = (cp - cn) / (kn - kp);
            PointF crosspt = new PointF(x, kp * x + cp);
            return crosspt;
        }

        public  List<LineSegment2D> ExtractContour(Image<Bgr, byte> img,out Image<Bgr, byte> temp )
        {
            List<LineSegment2D> ListOfLines = new List<LineSegment2D>();
            Image<Gray, byte> gray = new Image<Gray, byte>(img.ToBitmap());
            gray.ThresholdBinary(new Gray(149), new Gray(255));
            gray._Dilate(1);
            gray._Erode(1);
            img.ROI = new Rectangle(new Point(0, Img_Height - Roi_Height), new Size(Img_Width, Roi_Height));
            gray.ROI = new Rectangle(new Point(0, Img_Height - Roi_Height), new Size(Img_Width, Roi_Height));
            Image<Gray, byte> canny = gray.Canny(150, 50);
            //canny.Save("D:\\temp\\canny.jpg");
            Image<Bgr, byte> lineimage = img;
            lines = canny.HoughLines(1, 2, 3, Math.PI / 180, 150, 80, 400);
            foreach (LineSegment2D line in lines[0])
            {
                float theta = line.Direction.Y / line.Direction.X;
                float c = line.P1.Y - theta * line.P1.X;
                if (Math.Abs(theta) > 0.1 && Math.Abs(theta) < 0.9&&c<300)
                {
                    lineimage.Draw(line, new Bgr(0, 0, 255), 1);
                    ListOfLines.Add(line);
                }
            }
            temp = lineimage;
            //lineimage.Save("D:\\temp\\HoughLines.jpg");
            return ListOfLines;

        }
        public  double GetDist(PointF pt, LineSegment2DF line)
        {
            double result = 0;
            double k = line.Direction.Y / line.Direction.X;
            double c = line.P1.Y - k * line.P1.X;
            result = Math.Abs(k * pt.X - pt.Y + c) / Math.Sqrt(1 + k * k);
            return result;
        }
    }
}
