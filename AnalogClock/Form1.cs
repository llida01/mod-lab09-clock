using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AnalogClock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            GraphicsState gs;

            int w = this.Width;
            int h = this.Height;

            DateTime dt = DateTime.Now;
            g.TranslateTransform(w / 2, h / 2);

            Pen p1 = new Pen(Color.SteelBlue, 3);

            g.DrawEllipse(p1, -100, -100, 200, 200);
            Brush b = new SolidBrush(Color.Azure);
            g.FillEllipse(b, -100, -100, 200, 200);
            gs = g.Save();
            Draws(g, gs, p1);

            Pen p2 = new Pen(Color.DarkBlue, 2);
            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            g.DrawLine(p2, 0, h / 20, 0, (float)(-h / 5));
            g.Restore(gs);

            Pen p3 = new Pen(Color.MediumBlue, 3);
            gs = g.Save();
            g.RotateTransform(6 * dt.Minute + dt.Second / 10);
            g.DrawLine(p3, 0, 0, 0, (float)(-h / 6));
            g.Restore(gs);

            Pen p4 = new Pen(Color.MidnightBlue, 4);
            gs = g.Save();
            g.RotateTransform(6 * dt.Hour + dt.Minute / 5);
            g.DrawLine(p4, 0, 0, 0, (float)(-h / 8));
            g.Restore(gs);
        }

        static void Draws(Graphics g, GraphicsState gs, Pen p)
        {
            Font f = new Font("Times New Roman", 12);
            Point center = new Point(0, 0);
            for (int i = 0; i < 12; i++)
            {
                gs = g.Save();
                g.RotateTransform(30 * i + 45);
                g.DrawLine(p, -63, -63, -71, -71);
                g.Restore(gs);
            }
            for (int i = 1; i <= 12; ++i)
            {
                float a = i * 30;
                float x = center.X + (float)(75 * Math.Cos((a - 90) * Math.PI / 180));
                float y = center.Y + (float)(75 * Math.Sin((a - 90) * Math.PI / 180));

                g.DrawString(i.ToString(), f, Brushes.SteelBlue, x, y, new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                });
            }
            for (int i = 0; i < 60; i++)
            {
                gs = g.Save();
                g.RotateTransform(6 * i + 45);
                g.DrawLine(p, -67, -67, -71, -71);
                g.Restore(gs);
            }
        }
    }
}
