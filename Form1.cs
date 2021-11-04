using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Lab7_2
{
    public partial class Form1 : Form
    {
        static Bitmap bmp = new Bitmap(800, 800);

        Graphics g = Graphics.FromImage(bmp);

        Pen myPen = new Pen(Color.Black);
        List<Line> lines;
        Polyhedron poly = new Polyhedron();

        int size = 70;
        Edge generatrix;
        Point3 moving_point = new Point3(0, 0, 0);
        Point3 moving_point_line = new Point3(0, 0, 0);
        Point3 centr;
        string Path = "points.txt";

        public Form1()
        {
            InitializeComponent();
            //lines = Hex(size);
            centr = new Point3(pictureBox1.Width / 2, pictureBox1.Height / 2, 0);
            poly = Tetr(size);
            var edges = poly.edges;
            foreach (var ed in edges)
                g.DrawPolygon(myPen, Position2d(ed));
            pictureBox1.Image = bmp;
        }
        public class Point3
        {
            public double X;
            public double Y;
            public double Z;
            public int ID;

            public Point3() { X = 0; Y = 0; Z = 0; ID = 0; }

            public Point3(double x, double y, double z)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }
            public Point3(double x, double y, double z, int id)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
                this.ID = id;
            }
        }

        public class Line
        {
            public Point3 p1;
            public Point3 p2;
            public int ID;

            public Line()
            {
                p1 = new Point3();
                p2 = new Point3();
            }

            public Line(Point3 p1, Point3 p2)
            {
                this.p1 = p1;
                this.p2 = p2;
            }

        }

        public class Edge
        {
            public List<Point3> points;

            public Edge()
            {
                this.points = new List<Point3> { };
            }
            public Edge(List<Point3> p)
            {
                this.points = p;
            }
        }

        public class Polyhedron
        {
            public List<Edge> edges;

            public Polyhedron()
            {
                this.edges = new List<Edge> { };
            }
            public Polyhedron(List<Edge> e)
            {
                this.edges = e;
            }
        }

        public Polyhedron Hex(int size)
        {
            var hc = size / 2;
            Polyhedron p = new Polyhedron();
            Edge e = new Edge();
            // 1-2-3-4
            e.points = new List<Point3> {
                new Point3(-hc, hc, -hc), // 1
                new Point3(hc, hc, -hc), // 2
                new Point3(hc, -hc, -hc), // 3
                new Point3(-hc, -hc, -hc) // 4
            };
            p.edges.Add(e);
            e = new Edge();

            // 1-2-6-5
            e.points = new List<Point3> {
                new Point3(-hc, hc, -hc), // 1
                new Point3(hc, hc, -hc), // 2
                new Point3(hc, hc, hc), // 6 
                new Point3(-hc, hc, hc) // 5
            };
            p.edges.Add(e);
            e = new Edge();

            // 5-6-7-8
            e.points = new List<Point3> {
                new Point3(-hc, hc, hc), // 5
                new Point3(hc, hc, hc), // 6 
                new Point3(hc, -hc, hc), // 7
                new Point3(-hc, -hc, hc) // 8
            };
            p.edges.Add(e);
            e = new Edge();

            // 6-2-3-7
            e.points = new List<Point3> {
                new Point3(hc, hc, hc), // 6 
                new Point3(hc, hc, -hc), // 2
                new Point3(hc, -hc, -hc), // 3
                new Point3(hc, -hc, hc) // 7
            };
            p.edges.Add(e);
            e = new Edge();

            // 5-1-4-8
            e.points = new List<Point3> {
                new Point3(-hc, hc, hc), // 5
                new Point3(-hc, hc, -hc), // 1
                new Point3(-hc, -hc, -hc), // 4
                new Point3(-hc, -hc, hc) // 8
            };
            p.edges.Add(e);
            e = new Edge();

            // 4-3-7-8
            e.points = new List<Point3> {
                new Point3(-hc, -hc, -hc), // 4
                new Point3(hc, -hc, -hc), // 3
                new Point3(hc, -hc, hc), // 7
                new Point3(-hc, -hc, hc) // 8
            };
            p.edges.Add(e);
            e = new Edge();

            return p;

        }

        public Polyhedron Tetr(int size)
        {
            //var tetr_centr = size / 2;
            //return new List<Line>
            //{
            //    new Line(new Point3(-tetr_centr, tetr_centr, -tetr_centr), new Point3(-tetr_centr, -tetr_centr, tetr_centr)), //1->2
            //    new Line(new Point3(-tetr_centr, tetr_centr, -tetr_centr), new Point3(-tetr_centr, tetr_centr, tetr_centr)), //1->4
            //    new Line(new Point3(-tetr_centr, tetr_centr, -tetr_centr), new Point3(tetr_centr, tetr_centr, tetr_centr)), //1->3
            //    new Line(new Point3(-tetr_centr, -tetr_centr, tetr_centr), new Point3(-tetr_centr, tetr_centr, tetr_centr)), //2->4
            //    new Line(new Point3(-tetr_centr, -tetr_centr, tetr_centr), new Point3(tetr_centr, tetr_centr, tetr_centr)), //2->3
            //    new Line(new Point3(tetr_centr, tetr_centr, tetr_centr), new Point3(-tetr_centr, tetr_centr, tetr_centr)) //3->4
            //};

            var hc = size / 2;
            Polyhedron p = new Polyhedron();
            Edge e = new Edge();
            // 1-2-3
            e.points = new List<Point3> {
                new Point3(-hc, hc, -hc), // 1
                new Point3(-hc, -hc, hc), // 2
                new Point3(-hc, hc, hc), // 3
            };
            p.edges.Add(e);
            e = new Edge();

            // 1-4-2
            e.points = new List<Point3> {
                new Point3(-hc, hc, -hc), // 1
                new Point3(hc, hc, hc), // 4
                new Point3(-hc, -hc, hc), // 2 
            };
            p.edges.Add(e);
            e = new Edge();

            // 1-3-4
            e.points = new List<Point3> {
                new Point3(-hc, hc, -hc), // 1
                new Point3(-hc, hc, hc), // 3
                new Point3(hc, hc, hc), // 4
            };
            p.edges.Add(e);
            e = new Edge();

            // 3-2-4
            e.points = new List<Point3> {
                new Point3(-hc, hc, hc), // 3 
                new Point3(-hc, -hc, hc), // 2
                new Point3(hc, hc, hc), // 4
            };
            p.edges.Add(e);

            return p;
        }

        public Polyhedron Oct(int size)
        {
            var hc = size / 2;
            Polyhedron p = new Polyhedron();
            Edge e = new Edge();
            // 1-2-3-4
            //e.points = new List<Point3> {
            //    new Point3(hc, 0, -hc), // 1
            //    new Point3(-hc, 0, -hc), // 2
            //    new Point3(-hc, 0, hc), // 3
            //    new Point3(hc, 0, hc) // 4
            //};
            //p.edges.Add(e);
            //e = new Edge();

            // 2-5-1
            e.points = new List<Point3> {
                new Point3(-hc, 0, -hc), // 2
                new Point3(0, hc, 0), // 5
                new Point3(hc, 0, -hc), // 1 
            };
            p.edges.Add(e);
            e = new Edge();

            // 2-5-3
            e.points = new List<Point3> {
                new Point3(-hc, 0, -hc), // 2
                new Point3(0, hc, 0), // 5
                new Point3(-hc, 0, hc), // 3
            };
            p.edges.Add(e);
            e = new Edge();

            // 3-5-4
            e.points = new List<Point3> {
                new Point3(-hc, 0, hc), // 3 
                new Point3(0, hc, 0), // 5
                new Point3(hc, 0, hc), // 4
            };
            p.edges.Add(e);
            e = new Edge();

            // 1-5-4
            e.points = new List<Point3> {
                new Point3(hc, 0, -hc), // 1
                new Point3(0, hc, 0), // 5
                new Point3(hc, 0, hc), // 4
            };
            p.edges.Add(e);
            e = new Edge();
            ////////
            // 2-6-1
            e.points = new List<Point3> {
                new Point3(-hc, 0, -hc), // 2
                new Point3(0, -hc, 0), // 6
                new Point3(hc, 0, -hc), // 1 
            };
            p.edges.Add(e);
            e = new Edge();

            // 2-6-3
            e.points = new List<Point3> {
                new Point3(-hc, 0, -hc), // 2
                new Point3(0, -hc, 0), // 6
                new Point3(-hc, 0, hc), // 3
            };
            p.edges.Add(e);
            e = new Edge();

            // 3-6-4
            e.points = new List<Point3> {
                new Point3(-hc, 0, hc), // 3 
                new Point3(0, -hc, 0), // 6
                new Point3(hc, 0, hc), // 4
            };
            p.edges.Add(e);
            e = new Edge();

            // 1-6-4
            e.points = new List<Point3> {
                new Point3(hc, 0, -hc), // 1
                new Point3(0, -hc, 0), // 6
                new Point3(hc, 0, hc), // 4
            };
            p.edges.Add(e);
            e = new Edge();

            return p;
        }


        Point Position2d(Point3 p)
        {
            return new Point((int)p.X + (int)centr.X, (int)p.Y + (int)centr.Y);
        }

        Point[] Position2d(Edge e)
        {
            List<Point> p2D = new List<Point> { };
            foreach (var p3 in e.points)
            {
                p2D.Add(new Point((int)p3.X + (int)centr.X, (int)p3.Y + (int)centr.Y));
            }
            return p2D.ToArray();
        }

        Point[] Position2dnocent(Edge e)
        {
            List<Point> p2D = new List<Point> { };
            foreach (var p3 in e.points)
            {
                p2D.Add(new Point((int)p3.X, (int)p3.Y));
            }
            return p2D.ToArray();
        }

        public static double[,] MultiplyMatrix(double[,] m1, double[,] m2)
        {
            double[,] m = new double[1, 4];

            for (int i = 0; i < 4; i++)
            {
                var temp = 0.0;
                for (int j = 0; j < 4; j++)
                {
                    temp += m1[0, j] * m2[j, i];
                }
                m[0, i] = temp;
            }
            return m;
        }



        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                g.Clear(Color.White);
                poly = Tetr(size);
                var edges = poly.edges;
                foreach (var ed in edges)
                    g.DrawPolygon(myPen, Position2d(ed));
                pictureBox1.Image = bmp;
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                g.Clear(Color.White);
                poly = Hex(size);
                var edges = poly.edges;
                foreach (var ed in edges)
                    g.DrawPolygon(myPen, Position2d(ed));
                pictureBox1.Image = bmp;
            }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
            {
                g.Clear(Color.White);
                poly = Oct(size);
                var edges = poly.edges;
                foreach (var ed in edges)
                    g.DrawPolygon(myPen, Position2d(ed));
                pictureBox1.Image = bmp;
            }
        }



        // clear
        private void button7_Click(object sender, EventArgs e)
        {
            generatrix = new Edge();
            poly = new Polyhedron();
            moving_point = new Point3(0, 0, 0);
            moving_point_line = new Point3(0, 0, 0);
            //lines = Hex(size);
            g.Clear(Color.White);
            pictureBox1.Image = bmp;
        }

        // move
        private void button1_Click(object sender, EventArgs e)
        {
            var posx = double.Parse(textBox1.Text);
            var posy = double.Parse(textBox2.Text);
            var posz = double.Parse(textBox3.Text);


            g.Clear(Color.White);
            moving_point.X += posx;
            moving_point.Y -= posy;
            moving_point.Z += posz;
            List<Edge> newEdges = new List<Edge>();
            foreach (var edge in poly.edges)
            {
                Edge newPoints = new Edge();
                foreach (var point in edge.points)
                {
                    double[,] m = new double[1, 4];
                    m[0, 0] = point.X;
                    m[0, 1] = point.Y;
                    m[0, 2] = point.Z;
                    m[0, 3] = 1;

                    double[,] matr = new double[4, 4]
                {   { 1, 0, 0, 0},
                    { 0, 1, 0, 0 },
                    {0, 0, 1, 0 },
                    { posx, -posy, posz, 1 } };

                    var final_matrix = MultiplyMatrix(m, matr);

                    newPoints.points.Add(new Point3(final_matrix[0, 0], final_matrix[0, 1], final_matrix[0, 2]));
                }
                newEdges.Add(newPoints);

            }
            poly.edges = newEdges;
            DrawPol();
            pictureBox1.Image = bmp;
        }

        // rotate
        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            List<Edge> newEdges = new List<Edge>();
            foreach (var edge in poly.edges)
            {
                Edge newPoints = new Edge();
                foreach (var point in edge.points)
                {
                    double[,] m = new double[1, 4];
                    m[0, 0] = point.X - moving_point.X;
                    m[0, 1] = point.Y - moving_point.Y;
                    m[0, 2] = point.Z - moving_point.Z;
                    m[0, 3] = 1;

                    var angle = double.Parse(textBox6.Text) * Math.PI / 180;
                    double[,] matrx = new double[4, 4]
                {   { Math.Cos(angle), 0, Math.Sin(angle), 0},
                    { 0, 1, 0, 0 },
                    {-Math.Sin(angle), 0, Math.Cos(angle), 0 },
                    { 0, 0, 0, 1 } };

                    angle = double.Parse(textBox5.Text) * Math.PI / 180;
                    double[,] matry = new double[4, 4]
                    {  { 1, 0, 0, 0 },
                    { 0, Math.Cos(angle), -Math.Sin(angle), 0},
                    {0, Math.Sin(angle), Math.Cos(angle), 0 },
                    { 0, 0, 0, 1 } };

                    angle = double.Parse(textBox4.Text) * Math.PI / 180;
                    double[,] matrz = new double[4, 4]
                    {  { Math.Cos(angle), -Math.Sin(angle), 0, 0},
                    { Math.Sin(angle), Math.Cos(angle), 0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, 1 } };

                    var final_matrix = MultiplyMatrix(m, matrx);
                    final_matrix = MultiplyMatrix(final_matrix, matry);
                    final_matrix = MultiplyMatrix(final_matrix, matrz);

                    newPoints.points.Add(new Point3(final_matrix[0, 0] + moving_point.X, final_matrix[0, 1] + moving_point.Y, final_matrix[0, 2] + moving_point.Z));
                }
                newEdges.Add(newPoints);
            }
            poly.edges = newEdges;
            DrawPol();
            pictureBox1.Image = bmp;
        }

        // scale
        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            List<Edge> newEdges = new List<Edge>();
            var posx = double.Parse(textBox9.Text);
            var posy = double.Parse(textBox8.Text);
            var posz = double.Parse(textBox7.Text);
            foreach (var edge in poly.edges)
            {
                Edge newPoints = new Edge();
                foreach (var point in edge.points)
                {
                    double[,] m = new double[1, 4];
                    m[0, 0] = point.X - moving_point.X;
                    m[0, 1] = point.Y - moving_point.Y;
                    m[0, 2] = point.Z - moving_point.Z;
                    m[0, 3] = 1;

                    double[,] matr = new double[4, 4]
                {   { posx, 0, 0, 0 },
                    { 0, posy, 0, 0 },
                    { 0, 0, posz, 0 },
                    { 0, 0, 0, 1 } };

                    var final_matrix = MultiplyMatrix(m, matr);

                    newPoints.points.Add(new Point3(final_matrix[0, 0] + moving_point.X, final_matrix[0, 1] + moving_point.Y, final_matrix[0, 2] + moving_point.Z));
                }
                newEdges.Add(newPoints);

            }
            poly.edges = newEdges;
            DrawPol();

        }

        public void DrawAll()
        {
            foreach (var line in lines)
            {
                g.DrawLine(myPen, Position2d(line.p1), Position2d(line.p2));
            }
            pictureBox1.Image = bmp;
        }
        public void DrawPol()
        {
            g.Clear(Color.White);
            foreach (var edge in poly.edges)
                g.DrawPolygon(myPen, Position2d(edge));
            pictureBox1.Image = bmp;
        }

        public void DrawPolNoCent()
        {
            g.Clear(Color.White);
            foreach (var edge in poly.edges)
                g.DrawPolygon(myPen, Position2dnocent(edge));
            pictureBox1.Image = bmp;
        }


        private void button8_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(Path))
            {
                foreach (var edge in poly.edges)
                {
                    foreach (var point in edge.points)
                    {
                        writer.Write("" + point.X + " " + point.Y + " " + point.Z + ";");
                    }
                    writer.Write("\n");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var readText = File.ReadAllLines(Path);
            List<Edge> Edges = new List<Edge>();
            foreach (var line in readText)
            {
                Edge points = new Edge();
                var pointsline = line.Split(';');
                foreach (var point in pointsline)
                {
                    var pointXYZ = point.Split();
                    if (pointXYZ.Length > 1)
                        points.points.Add(new Point3(double.Parse(pointXYZ[0]), double.Parse(pointXYZ[1]), double.Parse(pointXYZ[2])));
                }
                Edges.Add(points);
            }
            poly.edges = Edges;

            DrawPol();

        }

        public void RotationFigure(List<Point3> generatrix, char axis, int count_splitting)
        {
            double angle_rotate = 360 / count_splitting;

            while (count_splitting != 0)
            {
                List<Point3> newpoints = new List<Point3>();
                foreach (var p in generatrix)
                {
                    double[,] m = new double[1, 4];
                    m[0, 0] = p.X;
                    m[0, 1] = p.Y;
                    m[0, 2] = p.Z;
                    m[0, 3] = 1;
                    if (axis == 'x')
                    {
                        double[,] matrx = new double[4, 4]
                            {   { Math.Cos(angle_rotate), 0, Math.Sin(angle_rotate), 0},
                                { 0, 1, 0, 0 },
                                {-Math.Sin(angle_rotate), 0, Math.Cos(angle_rotate), 0 },
                                { 0, 0, 0, 1 } };
                        m = MultiplyMatrix(m, matrx);
                    }
                    if (axis == 'y')
                    {
                        double[,] matry = new double[4, 4]
                        {  { 1, 0, 0, 0 },
                        { 0, Math.Cos(angle_rotate), -Math.Sin(angle_rotate), 0},
                        {0, Math.Sin(angle_rotate), Math.Cos(angle_rotate), 0 },
                        { 0, 0, 0, 1 } };
                        m = MultiplyMatrix(m, matry);
                    }
                    if (axis == 'z')
                    {
                        double[,] matrz = new double[4, 4]
                        {  { Math.Cos(angle_rotate), -Math.Sin(angle_rotate), 0, 0},
                            { Math.Sin(angle_rotate), Math.Cos(angle_rotate), 0, 0 },
                            { 0, 0, 1, 0 },
                            { 0, 0, 0, 1 } };
                        m = MultiplyMatrix(m, matrz);
                    }
                    newpoints.Add(new Point3(m[0, 0], m[0, 1], m[0, 2]));

                }
                generatrix = newpoints;
                var gen = new List<Point>();
                foreach (var x in generatrix)
                    gen.Add(new Point((int)x.X, (int)x.Y));
                g.DrawPolygon(myPen, gen.ToArray());
                pictureBox1.Image = bmp;
                count_splitting--;
            }
        }

        bool mouse_Down = false;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_Down = true;
            if (checkBox1.Checked)
            {
                
                generatrix.points.Add(new Point3(e.X - pictureBox1.Width/2, e.Y - pictureBox1.Height / 2, 0));
                bmp.SetPixel(e.X, e.Y, Color.Black);
            }
            pictureBox1.Image = bmp;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_Down = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //var gen = new List<Point>();
            poly.edges.Add(generatrix);
            //DrawPolNoCent();
            DrawPol();
            //foreach(var x in generatrix.points)
            //    gen.Add(new Point((int)x.X, (int)x.Y));
            //g.DrawPolygon(myPen, gen.ToArray());
            //pictureBox1.Image = bmp;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //var axis = 'y';
            //RotationFigure(generatrix.points, axis, 5);
            rotate(40,comboBox1.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            poly = new Polyhedron();
        }

        void rotate(int count,string axiis)
        {
            var angle = 360 / count * Math.PI / 180;
            var edges = new List<Edge>();

            while (count != 0)
            {
                List<Edge> newEdges = new List<Edge>();
                //foreach (var edge in poly.edges)
                {
                    Edge newPoints = new Edge();
                    foreach (var point in poly.edges.Last().points)
                    {
                        double[,] m = new double[1, 4];
                        m[0, 0] = point.X - moving_point.X;
                        m[0, 1] = point.Y - moving_point.Y;
                        m[0, 2] = point.Z - moving_point.Z;
                        m[0, 3] = 1;
                        double[,] matrx = new double[4,4];
                        //var angle = double.Parse(textBox6.Text) * Math.PI / 180;
                        if (axiis == "x")
                        {
                            matrx = new double[4, 4]
                            {   { Math.Cos(angle), 0, Math.Sin(angle), 0},
                            { 0, 1, 0, 0 },
                            {-Math.Sin(angle), 0, Math.Cos(angle), 0 },
                            { 0, 0, 0, 1 } };
                        }
                        if (axiis == "y")
                        {
                            matrx = new double[4, 4]
                            {  { 1, 0, 0, 0 },
                            { 0, Math.Cos(angle), -Math.Sin(angle), 0},
                            {0, Math.Sin(angle), Math.Cos(angle), 0 },
                            { 0, 0, 0, 1 } };
                        }
                        if (axiis == "z")
                        {
                            matrx = new double[4, 4]
                            {  { Math.Cos(angle), -Math.Sin(angle), 0, 0},
                            { Math.Sin(angle), Math.Cos(angle), 0, 0 },
                            { 0, 0, 1, 0 },
                            { 0, 0, 0, 1 } };
                        }
                        var final_matrix = MultiplyMatrix(m, matrx);

                        newPoints.points.Add(new Point3(final_matrix[0, 0] + moving_point.X, final_matrix[0, 1] + moving_point.Y, final_matrix[0, 2] + moving_point.Z));
                        var edg = new Edge();

                        edg.points.Add(new Point3(final_matrix[0, 0] + moving_point.X, final_matrix[0, 1] + moving_point.Y, final_matrix[0, 2] + moving_point.Z));
                        edg.points.Add(new Point3(point.X, point.Y, point.Z));
                        edges.Add(edg);

                    }
                    newEdges.Add(newPoints);
                }
                foreach(var x in edges)
                poly.edges.Add(x);
                foreach (var x in newEdges)
                poly.edges.Add(x);
                //DrawPol();
                foreach (var edge in poly.edges)
                    g.DrawPolygon(myPen, Position2d(edge));
                count--;
            }
            pictureBox1.Image = bmp;
        }
    }
}
