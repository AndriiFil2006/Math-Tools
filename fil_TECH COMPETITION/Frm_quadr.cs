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
    public partial class Frm_quadr : Form
    {
        public Frm_quadr()
        {
            InitializeComponent();
        }

        int n0 = 0, n1 = 0, n2 = 0, n3 = 0;

        public double discriminant(double a, double b, double c)
        {
            double result = 0;
            result = Math.Pow(b, 2) - 4 * a * c;
            return result;
        }

        private void textBox3_Click(object sender, EventArgs e)
        { 
            if(n2 == 0)
             {
                textBox3.Text = "";
                n2++;
             }
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            if (n3 == 0)
            {
                textBox4.Text = "";
                n3++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Answers");
            listBox1.Items.Add("");
        }

        private void Frm_quadr_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("Answers");
            listBox1.Items.Add("");

            comboBox1.Items.Add("+");
            comboBox1.Items.Add("-");

            comboBox2.Items.Add("+");
            comboBox2.Items.Add("-");

            //comboBox1.Select = 0;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (n1 == 0)
            {
                textBox2.Text = "";
                n1++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "")
            {
                double a = Convert.ToDouble(textBox1.Text);
                double b = Convert.ToDouble(textBox2.Text);
                double c = Convert.ToDouble(textBox3.Text);
                double c_min = Convert.ToDouble(textBox4.Text);
                double res1 = 0;
                double res2 = 0;

                if (comboBox1.SelectedIndex == 1)
                {
                    b = -b;
                }
                if (comboBox2.SelectedIndex == 1)
                {
                    c = -c;
                }

                c -= c_min;
                if (b == 0 && c == 0)
                {
                    listBox1.Items.Add("there's only 1 solution: 0");
                }
                else if (b == 0 && c != 0)
                {
                    if (c < 0)
                    {
                        res1 = Math.Round(Math.Sqrt(-c), 2);
                        listBox1.Items.Add("x₁ = " + res1 + " or √" + (-c));
                        listBox1.Items.Add("x₂ = " + (-res1) + " or -√" + (-c));
                        listBox1.Items.Add("");
                    }
                    else
                    {
                        listBox1.Items.Add("In this case< there's no clear answer");
                        listBox1.Items.Add("i - imaginary number that equal √-1");
                        res1 = Math.Round(Math.Sqrt(c), 2);
                        listBox1.Items.Add("x₁ = i * " + res1 + " or √" + (-c));
                        listBox1.Items.Add("x₂ = i * (" + (-res1) + ") or -√" + (-c));
                        listBox1.Items.Add("");
                    }
                }
                else if (c == 0)
                {
                    res1 = 0;
                    res2 = -b;

                    listBox1.Items.Add("x₁ = " + res1);
                    listBox1.Items.Add("x₂ = " + res2);
                    listBox1.Items.Add("");
                }
                else
                {
                    if (discriminant(a, b, c) > 0)
                    {
                        res1 = Math.Round((-b + Math.Sqrt(discriminant(a, b, c))) / (2 * a), 2);
                        res2 = Math.Round((-b - Math.Sqrt(discriminant(a, b, c))) / (2 * a), 2);

                        listBox1.Items.Add("x₁ = " + res1 + " or (" + (-b) + " + √" + Math.Round((discriminant(a, b, c)), 2) + ") / " + 2 * a);
                        listBox1.Items.Add("x₂ = " + res2 + " or (" + (-b) + " + -√" + Math.Round((discriminant(a, b, c)), 2) + ") / " + 2 * a);
                        listBox1.Items.Add("");
                    }
                    else if (discriminant(a, b, c) == 0)
                    {
                        res1 = -b / 2 * a;
                        listBox1.Items.Add("there's only 1 solution: x = " + res1);
                        listBox1.Items.Add("");
                    }
                    else
                    {
                        listBox1.Items.Add("In this case< there's no clear answer");
                        listBox1.Items.Add("i - imaginary number that equal √-1");

                        res1 = Math.Round((-b + Math.Sqrt(-discriminant(a, b, c))) / (2 * a), 2);
                        res2 = Math.Round((-b - Math.Sqrt(-discriminant(a, b, c))) / (2 * a), 2);

                        listBox1.Items.Add("x₁ = i * " + res1 + " or (" + (-b) + " + i√" + Math.Round((discriminant(a, b, c)), 2) + ") / " + 2 * a);
                        listBox1.Items.Add("x₂ = i * " + res2 + " or (" + (-b) + " + i√" + Math.Round((-discriminant(a, b, c)), 2) + ") / " + 2 * a);
                        listBox1.Items.Add("");
                    }
                }
            }
            else
            {
                MessageBox.Show("You didn't fill one or more of the text boxes, please try again!");
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if(n0 == 0)
            {
                textBox1.Text = "";
                n0++;
            }
        }
    }
}
