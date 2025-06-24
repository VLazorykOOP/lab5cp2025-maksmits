using System;
using System.Drawing;
using System.Windows.Forms;

namespace lp5
{
    public partial class Form1 : Form
    {
        PointF p1 = new PointF(100, 300);
        PointF p2 = new PointF(150, 100);
        PointF p3 = new PointF(250, 100);
        PointF p4 = new PointF(300, 300);
        int kochOrder = 3;

        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawBezier(g);
            DrawKochTriangle(g, kochOrder);
        }

        private void DrawBezier(Graphics g)
        {
            Pen pen = new Pen(Color.Blue, 2);
            for (float t = 0; t <= 1; t += 0.01f)
            {
                float x = (float)(Math.Pow(1 - t, 3) * p1.X +
                                  3 * Math.Pow(1 - t, 2) * t * p2.X +
                                  3 * (1 - t) * Math.Pow(t, 2) * p3.X +
                                  Math.Pow(t, 3) * p4.X);
                float y = (float)(Math.Pow(1 - t, 3) * p1.Y +
                                  3 * Math.Pow(1 - t, 2) * t * p2.Y +
                                  3 * (1 - t) * Math.Pow(t, 2) * p3.Y +
                                  Math.Pow(t, 3) * p4.Y);
                g.FillEllipse(Brushes.Red, x, y, 2, 2);
            }
        }

        private void DrawKoch(Graphics g, PointF a, PointF b, int order)
        {
            if (order == 0)
            {
                g.DrawLine(Pens.Green, a, b);
                return;
            }

            PointF p1 = new PointF((2 * a.X + b.X) / 3, (2 * a.Y + b.Y) / 3);
            PointF p2 = new PointF((a.X + 2 * b.X) / 3, (a.Y + 2 * b.Y) / 3);

            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            PointF peak = new PointF(
                (float)(p1.X + dx / 2 - Math.Sqrt(3) * dy / 2),
                (float)(p1.Y + dy / 2 + Math.Sqrt(3) * dx / 2)
            );

            DrawKoch(g, a, p1, order - 1);
            DrawKoch(g, p1, peak, order - 1);
            DrawKoch(g, peak, p2, order - 1);
            DrawKoch(g, p2, b, order - 1);
        }

        private void DrawKochTriangle(Graphics g, int order)
        {
            PointF A = new PointF(400, 300);
            PointF B = new PointF(500, 100);
            PointF C = new PointF(600, 300);
            DrawKoch(g, A, B, order);
            DrawKoch(g, B, C, order);
            DrawKoch(g, C, A, order);
        }
    }
}