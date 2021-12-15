using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fil_TECH_COMPETITION
{
    public partial class Form_graphics : Form
    {
        public Form_graphics()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graphics.Image = null;
            draw_axis(-graphics.Width, graphics.Width, -graphics.Height, graphics.Height, "linear");
        }

        public double trans_coorY(double y)
        {
            double result = 0;

            result = graphics.Height / 2 - y;

            return result;
        }

        public double trans_coorX(double x)
        {
            double result = 0;

            result = x + graphics.Width / 2;

            return result;
        }
        public double get_mult_x(double x1, double x2)
        {
            double result = 0;
            if (Math.Abs(x1) > Math.Abs(x2))
            {
                result = Math.Round(Math.Abs((x1 * 2) + 25) / graphics.Width * 1.25, 2);
            }
            else
            {
                result = Math.Round((Math.Abs(x2 * 2) + 25) / graphics.Width * 1.25, 2);
            }
            return result;
        }

        public double get_mult_y(double y1, double y2)
        {
            double result = 0;
            if (Math.Abs(y1) > Math.Abs(y2))
            {
                result = Math.Round(Math.Abs((y1 * 2) + 25) / graphics.Width * 1.25, 2);
            }
            else
            {
                result = Math.Round(Math.Abs((y2 * 2) + 25) / graphics.Width * 1.25, 2);
            }
            return result;
        }

        public void draw_axis(double x1, double x2, double y1, double y2, string type)
        {
            int gap = 25;
            Graphics gr = graphics.CreateGraphics();
            Point[] yax = new Point[2];
            Font myFont = new Font("Arial", 8);
            Font XY_font = new Font("Arial", 12);
            Point[] xax = new Point[2];
            Pen pen_axis = new Pen(Color.Gray, 1f);
            Pen pen_background = new Pen(this.BackColor, graphics.Width);
            gr.DrawRectangle(pen_background, 0, 0, graphics.Width, graphics.Height);
            StringFormat stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;


            yax[0] = new Point(graphics.Width / 2, graphics.Height);
            yax[1] = new Point(graphics.Width / 2, 0);

            xax[0] = new Point(0, graphics.Height / 2);
            xax[1] = new Point(graphics.Width, graphics.Height / 2);
            //draw axises
            gr.DrawLines(pen_axis, yax);
            gr.DrawLines(pen_axis, xax);
            int[] num_nums = new int[graphics.Width];

            int[] y_gap = new int[graphics.Height + 1], x_gap = new int[graphics.Width + 1];
            for(int i = - graphics.Width / 2 + gap; i < 0; i += gap)
            {
                int num_nums_int = 0;
                int i1 = i;
                for(int j = 0; j < 50; j++)
                {
                    if(Math.Abs(i1 * get_mult_y(y1, y2)) / 10 >= 10)
                    {
                        num_nums_int++;
                    }
                    else
                    {
                        num_nums[i + graphics.Height / 2] = num_nums_int * 10;
                        break;
                    }
                    i1 = i1 / 10;
                }
            }

            double mult_x = 0;
            double mult_y = 0;
            if (type == "linear")
            {
                if (get_mult_x(x1, x2) > get_mult_y(y1, y2))
                {
                    mult_x = get_mult_x(x1, x2);
                    mult_y = get_mult_x(x1, x2);
                }
                else
                {
                    mult_x = get_mult_y(y1, y2);
                    mult_y = get_mult_y(y1, y2);
                }
            }
            else if(type == "pow" || type == "tr_f")
            {
                /*
                if (get_mult_x(x1, x2) > get_mult_y(y1, y2))
                {
                    mult_x = get_mult_x(x1, x2);
                    mult_y = get_mult_x(x1, x2);
                }
                else
                {
                    mult_x = get_mult_y(y1, y2);
                    mult_y = get_mult_y(y1, y2);
                }*/
                mult_x = get_mult_x(x1, x2);
                mult_y = get_mult_y(y1, y2);
            }
            //draw markup
            for (int i = -graphics.Width / 2 + gap; i < graphics.Width / 2; i += gap)
            {
                //x
                if (i > 0)
                {
                    gr.DrawString("" + Math.Round(i * mult_x), myFont, Brushes.Black, new Point(Convert.ToInt32(i + graphics.Width / 2 - 8 /*- y_gap[i + graphics.Width / 2]*/), graphics.Height / 2 + 10), stringFormat);
                }
                else if (i < 0)
                {
                    gr.DrawString("" + Math.Round(i * mult_x), myFont, Brushes.Black, new Point(Convert.ToInt32(i + graphics.Width / 2 - 8 /*- y_gap[i + graphics.Width / 2]*/), graphics.Height / 2 - 35 /*- x_gap[i + graphics.Width]*/), stringFormat);
                }
                gr.DrawLine(pen_axis, new Point(i + graphics.Width / 2, graphics.Height / 2 + 10), new Point(i + graphics.Width / 2, graphics.Height / 2 - 10));
            }
            for (int i = -graphics.Height / 2 + gap; i < graphics.Height / 2; i += gap)
            {
                //y
                if (i < 0)
                {
                    gr.DrawString("" + Math.Round(i * mult_y), myFont, Brushes.Black, new Point(graphics.Width / 2 - 34 - y_gap[i + graphics.Width / 2] - num_nums[i + graphics.Width / 2], graphics.Height / 2 - i - 8));
                }
                else if (i > 0)
                {
                    gr.DrawString("" + Math.Round(i * mult_y), myFont, Brushes.Black, new Point(graphics.Width / 2 + 8 /*+ y_gap[i + graphics.Width / 2]*/, graphics.Height / 2 - i - 8));
                }
                gr.DrawLine(pen_axis, new Point(graphics.Width / 2 - 10, graphics.Height / 2 - i), new Point(graphics.Width / 2 + 10, graphics.Height / 2 - i));
            }

            //draw y-arrow
            gr.DrawLine(pen_axis, new Point(graphics.Width / 2 - 15, 15), new Point(graphics.Width / 2, 0));
            gr.DrawLine(pen_axis, new Point(graphics.Width / 2 + 15, 15), new Point(graphics.Width / 2, 0));
            gr.DrawString("y", XY_font, Brushes.Red, new Point(graphics.Width / 2 - 28, 0));

            //draw x-arrow
            gr.DrawLine(pen_axis, new Point(graphics.Width - 15, graphics.Height / 2 + 15), new Point(graphics.Width, graphics.Height / 2));
            gr.DrawLine(pen_axis, new Point(graphics.Width - 15, graphics.Height / 2 - 15), new Point(graphics.Width, graphics.Height / 2));
            gr.DrawString("x", XY_font, Brushes.Red, new Point(graphics.Width - 28, graphics.Height / 2 - 35));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label4.Hide();
                label5.Hide();
                label6.Hide();
                label7.Hide();
                label8.Hide();
                txtBox_square.Hide();
                txtBox_power.Hide();
                txtBox_xMult.Hide();
                txtBox_zn1.Hide();
                label9.Hide();
                label11.Hide();
                label12.Hide();
                label14.Hide();
                comboBox2.Hide();
                txt_degree.Hide();
                tr_mult.Hide();
                label10.Hide();

                label1.Show();
                label2.Show();
                label3.Show();
                txtBox_x.Show();
                txtBox_zn.Show();

                label1.Text = "y = ";
            }
            if(comboBox1.SelectedIndex == 1)
            {
                label1.Hide();
                label2.Hide();
                label3.Hide();
                txtBox_x.Hide();
                txtBox_zn.Hide();
                label9.Hide();
                label11.Hide();
                label12.Hide();
                label14.Hide();
                comboBox2.Hide();
                txt_degree.Hide();
                tr_mult.Hide();
                label10.Hide();

                label4.Show();
                label5.Show();
                label6.Show();
                label7.Show();
                label8.Show();
                txtBox_square.Show();
                txtBox_power.Show();
                txtBox_xMult.Show();
                txtBox_zn1.Show();
            }
            if(comboBox1.SelectedIndex == 2)
            {
                label4.Hide();
                label5.Hide();
                label6.Hide();
                label7.Hide();
                label8.Hide();
                txtBox_square.Hide();
                txtBox_power.Hide();
                txtBox_xMult.Hide();
                txtBox_zn1.Hide();

                label1.Show();
                label2.Show();
                label3.Show();
                txtBox_x.Show();
                txtBox_zn.Show();
                label1.Text = "+";

                label9.Show();
                label10.Show();
                label11.Show();
                label12.Show();
                label14.Show();
                comboBox2.Show();
                txt_degree.Show();
                tr_mult.Show();
            }
        }

        private void Form_graphics_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Linear");
            comboBox1.Items.Add("With a power");
            comboBox1.Items.Add("Trigonometric functions");

            comboBox2.Items.Add("cos");
            comboBox2.Items.Add("sin");

            label1.Hide();
            label2.Hide();
            label3.Hide();
            txtBox_x.Hide();
            txtBox_zn.Hide();

            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            txtBox_square.Hide();
            txtBox_power.Hide();
            txtBox_xMult.Hide();
            txtBox_zn1.Hide();

            label9.Hide();
            label10.Hide();
            label11.Hide();
            label12.Hide();
            label14.Hide();
            comboBox2.Hide();
            txt_degree.Hide();
            tr_mult.Hide();

            //draw_axis(-graphics.Width, graphics.Width, -graphics.Height, graphics.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            draw_axis(-graphics.Width, graphics.Width, -graphics.Height, graphics.Height, "linear");
            Graphics gr = graphics.CreateGraphics();
            //draw graphics
            Color color_graph = Color.Blue;
            Pen pen = new Pen(color_graph, 3f);

            //gr.DrawLine(pen, new Point(Convert.ToInt32(trans_coorX(0)), Convert.ToInt32(trans_coorY(0))), new Point(Convert.ToInt32(trans_coorX(100)), Convert.ToInt32(trans_coorY(100))));
            if (comboBox1.SelectedIndex == 0)
            {
                string type = "linear";
                int num_dots = graphics.Width;
                if (txtBox_x.Text != "" && txtBox_zn.Text != "")
                {
                    double x_mult = Convert.ToDouble(txtBox_x.Text), b = Convert.ToDouble(txtBox_zn.Text), maxx = -10000, maxy = -10000, minx = 10000, miny = 1000;
                    if (b != 0)
                    {
                        num_dots += Convert.ToInt32(Math.Abs(b * x_mult));
                    }
                    if (num_dots % 2 != 0)
                    {
                        num_dots++;
                    }
                    /*
                    double y_1 = graphics.Height / 2 - (x_mult * -graphics.Width / 2 + b);
                    double y_2 = graphics.Height / 2 - (x_mult * graphics.Width / 2 + b);
                    double x_1 = 0;
                    double x_2 = graphics.Width;
                    double test_mult = 0;
                    listBox1.Items.Add("y1: " + y_1 + " x1: " + x_1 + " y_2: " + y_2 + " x2: " + x_2);
                    listBox1.Items.Add("Mult_x: " + get_mult_x(x_1, x_2) + " mult_y: " + get_mult_y(y_1, y_2));
                    if (get_mult_x(x_1, x_2) > get_mult_y(y_1, y_2))
                    {
                        test_mult = get_mult_x(x_1, x_2);
                    }
                    else
                    {
                        test_mult = get_mult_y(y_1, y_2);
                    }
                    num_dots *= Convert.ToInt32(test_mult);
                    listBox1.Items.Add(num_dots);
                    */
                    double[] x = new double[num_dots];
                    double[] y = new double[num_dots];
                    Point[] points = new Point[num_dots];

                    for (int i = -num_dots / 2; i < num_dots / 2; i++)
                    {
                        x[i + num_dots / 2] = i;
                        y[i + num_dots / 2] = i * x_mult + b;

                        if (minx > Math.Abs(x[i + num_dots / 2]) + graphics.Width / 2)
                        {
                            minx = x[i + num_dots / 2] + graphics.Width / 2;
                        }
                        if (maxx < Math.Abs(x[i + num_dots / 2]) + graphics.Width / 2)
                        {
                            maxx = x[i + num_dots / 2] + graphics.Width / 2;
                        }
                        if (miny > Math.Abs(graphics.Height / 2 - y[i + num_dots / 2]))
                        {
                            miny = y[i + num_dots / 2];
                        }
                        if (maxy < Math.Abs(graphics.Height / 2 - y[i + num_dots / 2]))
                        {
                            maxy = y[i + num_dots / 2];
                        }
                        /*
                        if (i >= -50 && i <= 50)
                        {
                            listBox1.Items.Add("X: " + x[i + num_dots / 2] + " Y: " + (y[i + num_dots / 2]));
                        }*/
                        if (Math.Abs(i) <= 50)
                        {
                            listBox1.Items.Add("X: " + (x[i + num_dots / 2]) + "                                        Y: " + (y[i + num_dots / 2]));
                        }   
                    }

                    double mult_x = get_mult_x(minx, maxx);
                    double mult_y = get_mult_y(miny, maxy);
                    double mult = 0;
                    /*
                    listBox1.Items.Add("Min_x: " + minx + " Max_x: " + maxx);
                    listBox1.Items.Add("Min_y: " + miny + " Max_y: " + maxy);
                    listBox1.Items.Add("Mult_x: " + mult_x + " Mult_y: " + mult_y);
                    */
                    if (mult_x > mult_y)
                    {
                        mult = mult_x;
                    }
                    else
                    {
                        mult = mult_y;
                    }
                    draw_axis(minx, maxx, miny, maxy, type);

                    for (int i = 0; i < num_dots; i++)
                    {
                        points[i] = new Point(Convert.ToInt32((x[i]) /* mult*/ + graphics.Width / 2 /* mult + graphics.Width / 4*/), Convert.ToInt32(graphics.Height / 2 - y[i] + b - b / mult));
                       // listBox1.Items.Add(points[i]);
                        //listBox1.Items.Add(i);
                    }

                    gr.DrawLines(pen, points);
                }
                else
                {
                    MessageBox.Show("You didn't fill one or more of the text boxes, please try again!");
                }
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                string type = "pow";
                if (txtBox_square.Text != "" && txtBox_power.Text != "" && txtBox_xMult.Text != "" && txtBox_zn1.Text != "")
                {
                    double x_in_pow = Convert.ToDouble(txtBox_square.Text);
                    double x_pow = Convert.ToDouble(txtBox_power.Text);
                    double x_mult = Convert.ToDouble(txtBox_xMult.Text);
                    double b = Convert.ToDouble(txtBox_zn1.Text);
                    double miny = 0, maxy = 0, minx = 0, maxx = 0;
                    int num_dots = graphics.Width * 2;
                    if (x_pow >= 2 && x_in_pow != 0)
                    {
                        num_dots = 30;
                    }
                    if (b != 0)
                    {
                        num_dots += Convert.ToInt32(Math.Abs(b * x_mult));
                    }
                    if (num_dots % 2 == 0)
                    {
                        num_dots++;
                    }
                    double[] x = new double[num_dots];
                    double[] y = new double[num_dots];
                    Point[] points = new Point[num_dots];

                    for (int i = -num_dots / 2; i <= num_dots / 2; i++)
                    {
                        x[i + num_dots / 2] = i + graphics.Width / 2;
                        if (i >= 1 || x_pow % 2 == 0)
                        {
                            y[i + num_dots / 2] = x_in_pow * Math.Pow(Math.Abs(x[i + num_dots / 2]) - graphics.Width / 2, x_pow) + /*x[i + num_dots / 2]*/ i * x_mult + b /*- graphics.Height / 2*/;
                        }
                        else if (Math.Abs(x_pow) >= 1)
                        {
                            y[i + num_dots / 2] = -x_in_pow * Math.Pow(Math.Abs(x[i + num_dots / 2] - graphics.Width / 2), x_pow) + /*x[i + num_dots / 2]*/ i * x_mult + b;
                        }

                        if (minx > Math.Abs(x[i + num_dots / 2] - graphics.Width))
                        {
                            minx = Math.Abs(x[i + num_dots / 2] - graphics.Width / 2);
                        }
                        else if (maxx < Math.Abs(x[i + num_dots / 2] - graphics.Width))
                        {
                            maxx = Math.Abs(x[i + num_dots / 2] - graphics.Width / 2);
                        }
                        if (miny > y[i + num_dots / 2])
                        {
                            miny = y[i + num_dots / 2];
                        }
                        else if (maxy < y[i + num_dots / 2])
                        {
                            maxy = y[i + num_dots / 2];
                        }
                        if (Math.Abs(i) <= 50)
                        {
                            listBox1.Items.Add("X: " + Math.Round((x[i + num_dots / 2] - graphics.Width / 2), 2) + "                                         Y: " + Math.Round(y[i + num_dots / 2], 2));
                        }
                    }
                    double mult_x = get_mult_x(minx, maxx);
                    double mult_y = get_mult_y(miny, maxy);
                    double mult = 0;
                    if (mult_x > mult_y)
                    {
                        mult = mult_x;
                    }
                    else
                    {
                        mult = mult_y;
                    }
                    /*
                    listBox1.Items.Add("Min_x: " + minx + " Max_x: " + maxx);
                    listBox1.Items.Add("Min_y: " + miny + " Max_y: " + maxy);
                    listBox1.Items.Add(mult);
                    */
                    draw_axis(minx, maxx, miny, maxy, type);

                    for (int i = 0; i < num_dots; i++)
                    {
                        if (Math.Abs(x_pow) >= 1 || i == 0)
                        {
                            points[i] = new Point(Convert.ToInt32((x[i] - graphics.Width / 2) / mult_x + graphics.Width / 2), Convert.ToInt32(graphics.Height / 2 - y[i] + b - b / mult_y));
                            //points[i] = new Point(Convert.ToInt32((x[i] - graphics.Width / 2) / mult + graphics.Width / 2), Convert.ToInt32(graphics.Height / 2 - y[i] / mult));
                        }
                        else
                        {
                            if (i < num_dots / 2)
                            {
                                points[i] = new Point(Convert.ToInt32((x[num_dots / 2])/* - graphics.Width / 2*/), Convert.ToInt32(graphics.Height / 2 - y[i] + b - b / mult));
                                //points[i] = new Point(Convert.ToInt32((x[num_dots / 2] - graphics.Width / 2) / mult + graphics.Width / 2), Convert.ToInt32(graphics.Height / 2 - y[12] / mult));
                            }
                            else
                            {
                                points[i] = new Point(Convert.ToInt32((x[i]) /*- graphics.Width / 2*/), Convert.ToInt32(graphics.Height / 2 - y[i] + b - b / mult));
                                // points[i] = new Point(Convert.ToInt32((x[i] - graphics.Width / 2) / mult + graphics.Width / 2), Convert.ToInt32(graphics.Height / 2 - y[i] / mult));
                            }
                        }
                    }

                    gr.DrawLines(pen, points);
                }
                else
                {
                    MessageBox.Show("You didn't fill one or more of the text boxes, please try again!");
                }
            }
            else if(comboBox1.SelectedIndex == 2)
            {
                string type = "tr_f";
                int Is_Sinus = comboBox2.SelectedIndex;
                if (tr_mult.Text != "" && txtBox_x.Text != "" && txtBox_zn.Text != "" && txt_degree.Text != "")
                {
                    double tr_fun_mult = Convert.ToDouble(tr_mult.Text);
                    //bool isX = false;
                    double x_mult = Convert.ToDouble(txtBox_x.Text), b = Convert.ToDouble(txtBox_zn.Text), degree_mult = Convert.ToDouble(txt_degree.Text);
                    double minx = 10000, maxx = -10000, miny = 10000, maxy = -100000;
                   /* if (txt_degree.Text == "x" || txt_degree.Text == "X")
                    {
                        isX = true;
                    }
                    else
                    {
                        degree = Convert.ToDouble(txt_degree.Text);
                        isX = false;
                    }*/
                    int num_dots = 40;
                    if (b != 0)
                    {
                        num_dots += Convert.ToInt32(Math.Abs(b * x_mult));
                    }
                    if (num_dots % 2 != 0)
                    {
                        num_dots++;
                    }
                    double[] x = new double[num_dots];
                    double[] y = new double[num_dots];
                    Point[] points = new Point[num_dots];

                    for (int i = -num_dots / 2; i < num_dots / 2; i++)
                    {
                        if (Is_Sinus == 0)
                        {
                            x[i + num_dots / 2] = i + graphics.Width / 2;
                            //if (isX == false)
                            //{
                                y[i + num_dots / 2] = tr_fun_mult * Math.Round(Math.Cos(degree_mult * i), 2) + i * x_mult + b;
                            /*}
                            else
                            {
                                y[i + num_dots / 2] = tr_fun_mult * Math.Round(Math.Cos(i), 2) + i * x_mult + b;
                                //listBox1.Items.Add(Math.Cos(x[i + num_dots / 2]));
                            }*/
                        }
                        else if (Is_Sinus == 1)
                        {
                            // y[i] = x_in_pow * Math.Pow( x[i], x_pow) + x[i] * x_mult + b
                            x[i + num_dots / 2] = i + graphics.Width / 2;
                           // if (isX == false)
                            //{
                                y[i + num_dots / 2] = tr_fun_mult * Math.Round(Math.Sin(degree_mult* i), 2) + i * x_mult + b;
                            //}
                            /*else
                            {
                                y[i + num_dots / 2] = tr_fun_mult * Math.Round(Math.Sin(i), 2) + i * x_mult + b;
                            }*/
                        }

                        if (minx > Math.Abs(x[i + num_dots / 2] - graphics.Width))
                        {
                            minx = x[i + num_dots / 2] - graphics.Width / 2;
                        }
                        else if (maxx < Math.Abs(x[i + num_dots / 2] - graphics.Width))
                        {
                            maxx = x[i + num_dots / 2] - graphics.Width / 2;
                        }
                        if (miny > y[i + num_dots / 2])
                        {
                            miny = y[i + num_dots / 2];
                        }
                        else if (maxy < y[i + num_dots / 2])
                        {
                            maxy = y[i + num_dots / 2];
                        }
                        if(Math.Abs(i) <= 50)
                        listBox1.Items.Add("X: " + Math.Round((x[i + num_dots / 2] - graphics.Width / 2), 2) + "                                         Y: " + Math.Round(y[i + num_dots / 2], 2));

                    }

                    double mult_x = get_mult_x(minx, maxx);
                    double mult_y = get_mult_y(miny, maxy);
                    double mult = 0;
                    if (mult_x < mult_y)
                    {
                        mult = mult_x;
                    }
                    else
                    {
                        mult = mult_y;
                    }
                    /*
                    listBox1.Items.Add("Min_x: " + minx + " Max_x: " + maxx);
                    listBox1.Items.Add("Min_y: " + miny + " Max_y: " + maxy);
                    listBox1.Items.Add("Mult_x: " + mult_x + " Mult_y: " + mult_y);
                    */
                    draw_axis(minx, maxx, miny, maxy, type);

                    for (int i = 0; i < num_dots; i++)
                    {
                        points[i] = new Point(Convert.ToInt32((x[i] - graphics.Width / 2) / mult_x + graphics.Width / 2), Convert.ToInt32(graphics.Height / 2 - y[i] / mult_y));
                        //points[i] = new Point(Convert.ToInt32((x[i]) / mult + graphics.Width / 2/* mult + graphics.Width / 4*/), Convert.ToInt32(graphics.Height / 2 - y[i] / mult)
                        //listBox1.Items.Add(points[i]);
                    }
                    //draw_axis(-graphics.Width, graphics.Width, -graphics.Height, graphics.Height);
                    gr.DrawLines(pen, points);
                }
                else
                {
                    MessageBox.Show("You didn't fill one or more of the text boxes, please try again!");
                }
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            /*
            label1.Hide();
            label2.Hide();
            label3.Hide();
            textBox1.Hide();
            textBox2.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            textBox6.Hide();
            */
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
