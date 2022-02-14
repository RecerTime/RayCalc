using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayCalc
{
    public partial class Form1 : Form
    {
        double surfaceAngle = 0;
        double surfaceLength;
        Point surfaceOrigin;
        Point surfaceTarget;

        double ray1Angle = 45;
        Point ray1Target;

        const double rayLength = 200;
        Point rayOrigin;

        double ray2Angle;
        Point ray2Target;

        Bitmap bitmap;
        Graphics g;

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(panel1.Width, panel1.Height);
            g = Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            surfaceAngle = AngleCorrection(surfaceAngle);
            ray1Angle = AngleCorrection(ray1Angle);
            DrawSurface();
            DrawRay();
             
            label1.Text = surfaceAngle.ToString();
            label2.Text = ray1Angle.ToString();
            label3.Text = ray2Angle.ToString();

            panel1.Invalidate();
        }
        void DrawSurface()
        {
            surfaceLength = panel1.Height/2;

            surfaceOrigin = new Point(panel1.Width / 2, panel1.Height / 2);
            surfaceTarget = new Point(  (int)(surfaceLength * Math.Cos(surfaceAngle * Math.PI / 180)) + surfaceOrigin.X,
                                        (int)(surfaceLength * Math.Sin(surfaceAngle * Math.PI / 180) + surfaceOrigin.Y));            
        }
        void DrawRay()
        {
            rayOrigin = new Point(  (int)((surfaceOrigin.X + surfaceTarget.X) / 2), (int)((surfaceOrigin.Y + surfaceTarget.Y) / 2));
            ray1Target = new Point( (int)(rayLength * Math.Cos(ray1Angle * Math.PI / 180)) + rayOrigin.X,
                                    (int)(rayLength * Math.Sin(ray1Angle * Math.PI / 180) + rayOrigin.Y));

            ray2Angle = AngleCorrection(surfaceAngle + (180 - (ray1Angle - surfaceAngle)));
            ray2Target = new Point((int)(rayLength * Math.Cos(ray2Angle * Math.PI / 180)) + rayOrigin.X,
                                    (int)(rayLength * Math.Sin(ray2Angle * Math.PI / 180) + rayOrigin.Y));
        }

        double AngleCorrection(double x)
        {
            while (x < 0)
                x += 360;
            while (x >= 360)
                x -= 360;
            return x;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Blue);
            pen.Width = 2;
            g.DrawLine(pen, surfaceOrigin, surfaceTarget);
            g.DrawRectangle(pen, new Rectangle(surfaceOrigin.X - 1, surfaceOrigin.Y - 1, 2, 2));

            pen.Color = Color.Red;
            g.DrawLine(pen, rayOrigin, ray1Target);
            pen.Color = Color.DeepPink;
            g.DrawLine(pen, rayOrigin, ray2Target);

            panel1.BackColor = Color.Gray;
            panel1.BackgroundImage = bitmap;
        }
    }
}
