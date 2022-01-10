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
    public partial class Probability : Form
    {
        public Probability()
        {
            InitializeComponent();
        }
        int n = 5;
        int m = 5;

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "Please, input your data \n in table.";
            dataGridView1.RowCount = n + 2;
            dataGridView1.ColumnCount = m + 2;
            dataGridView1.Rows[0].Cells[0].ReadOnly = true;
            // dataGridView1.AutoResizeColumns();
            dataGridView1.Rows[0].Cells[0].Style.BackColor = Color.Gray;
            for(int i = 1; i < dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
            }
            for (int i = 1; i < dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Rows[0].Cells[i].Style.BackColor = Color.Yellow;
            }
            dataGridView1.Rows[dataGridView1.RowCount- 1].Cells[0].Value = "Sum";
            dataGridView1.Rows[0].Cells[dataGridView1.ColumnCount - 1].Value = "Sum";
            //put example in table
            dataGridView1.Rows[1].Cells[0].Value = "Dogs";
            dataGridView1.Rows[2].Cells[0].Value = "Cats";
            dataGridView1.Rows[0].Cells[1].Value = "Red toy";
            dataGridView1.Rows[0].Cells[2].Value = "Blue toy";
            dataGridView1.Rows[0].Cells[3].Value = "Yellow toy";

            Random rand = new Random();
            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < m; j++)
                {
                    if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != "" && Convert.ToString(dataGridView1.Rows[0].Cells[j].Value) != "")
                    {
                        dataGridView1.Rows[i].Cells[j].Value = rand.Next(0, 25);
                    }
                }
            }

        }

        public double ProbAnd(int a, int b)
        {
            if(a < comboBox1.Items.Count / 2 && b < comboBox3.Items.Count / 2 || a > comboBox1.Items.Count / 2 && b > comboBox3.Items.Count / 2)
            {
                return 0;
                //label4.Text = "Probability of that action is equel to 0";
            }
            else
            {
                if(b < comboBox3.Items.Count / 2)
                {
                    return Math.Round(Convert.ToDouble(dataGridView1.Rows[b + 1].Cells[a - comboBox1.Items.Count / 2 + 1].Value) / Convert.ToDouble(dataGridView1.Rows[n + 1].Cells[m + 1].Value) * 100, 2);
                    //label4.Text = "Probability of that action is equel to: " + Math.Round(Convert.ToDouble(dataGridView1.Rows[b + 1].Cells[a - comboBox1.Items.Count / 2 + 1].Value) / Convert.ToDouble(dataGridView1.Rows[n + 1].Cells[m + 1].Value) * 100, 2) + "%";
                }
                else
                {
                    return Math.Round(Convert.ToDouble(dataGridView1.Rows[a + 1].Cells[b - comboBox3.Items.Count / 2 + 1].Value) / Convert.ToDouble(dataGridView1.Rows[n + 1].Cells[m + 1].Value) * 100, 2);
                    //label4.Text = "Probability of that action is equel to: " + Math.Round(Convert.ToDouble(dataGridView1.Rows[a + 1].Cells[b - comboBox3.Items.Count / 2 + 1].Value) / Convert.ToDouble(dataGridView1.Rows[n + 1].Cells[m + 1].Value) * 100, 2) + "%";
                }
            }
        }

        public double probability_0(int a)
        {
            double num = 0;
            if(a < comboBox1.Items.Count / 2)
            {
                num = Convert.ToDouble(dataGridView1.Rows[a + 1].Cells[m + 1].Value);
            }
            else
                num = Convert.ToDouble(dataGridView1.Rows[n + 1].Cells[a + 1 - comboBox1.Items.Count / 2].Value);
            //label4.Text = "" + Convert.ToDouble(dataGridView1.Rows[n + 1].Cells[m + 1].Value);
            double result = Math.Round(num / Convert.ToDouble(dataGridView1.Rows[n + 1].Cells[m + 1].Value) * 100, 2);
            return result;
           // label4.Text = "Probability of that action is equel to: " + Math.Round(num / Convert.ToDouble(dataGridView1.Rows[n + 1].Cells[m + 1].Value) * 100, 2) + "%";
        }

        public double probability_or(int a, int b)
        {
            double res = 0;
            if (a < comboBox1.Items.Count / 2 && b < comboBox3.Items.Count / 2 || a > comboBox1.Items.Count / 2 && b > comboBox3.Items.Count / 2)
            {
                res = probability_0(a) + probability_0(b);
            }
            else
            {
               res = probability_0(a) + probability_0(b) - ProbAnd(a, b);
            }
            return res;
        }

        public double probability_conditional(int a, int b)
        {
            double result = 0;
            result = Math.Round(ProbAnd(a,b) / probability_0(b) * 100, 2);
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // comboBox3.Visible = true;
            int[,] values = new int[n, m];
            for (int i = 1; i < n; i++)
            {
                for(int j = 1; j < n; j++)
                {
                    values[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                }
            }

            int sumAll = 0;

            for(int i = 1; i < m - 2; i++)
            {
                int sumy = 0;
                int sumx = 0;
                for(int j = 1; j < n - 2; j++)
                {
                    sumy += values[j, i];
                    sumx += values[i, j];
                    sumAll += values[i,j];
                }
                dataGridView1.Rows[n + 1].Cells[i].Value = sumy;
                dataGridView1.Rows[i].Cells[m + 1].Value = sumx;
            }

            dataGridView1.Rows[n + 1].Cells[0].Value = "Sum";
            dataGridView1.Rows[0].Cells[m + 1].Value = "Sum";
           // listBox1.Items.Add("n " + (n + 1) + " m " + (m + 1));
            dataGridView1.Rows[n + 1].Cells[m + 1].Value = sumAll;
            if (comboBox2.SelectedIndex == 0)
            {
                double result = probability_0(comboBox1.SelectedIndex);
                listBox1.Items.Add("Probability of that action is equel to: " + result + "%");
            }
            if (comboBox2.SelectedIndex == 1)
            {
                double result = ProbAnd(comboBox1.SelectedIndex, comboBox3.SelectedIndex);
                listBox1.Items.Add("Probability of that action is equel to: " + result + "%");
            }
            if(comboBox2.SelectedIndex == 2)
            {
                double result = probability_or(comboBox1.SelectedIndex, comboBox3.SelectedIndex);
                listBox1.Items.Add("Probability of that action is equel to: " + result + "%");
            }
            if(comboBox2.SelectedIndex == 3)
            {
                double result1 = probability_conditional(comboBox1.SelectedIndex, comboBox3.SelectedIndex);
                listBox1.Items.Add("Probability of that action is equel to: " + result1 + "%");
            }

            //dataGridView1.AutoResizeColumns();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            n++;
            dataGridView1.RowCount = n + 2;
            for (int i = 0; i <= m + 1; i++)
            {
                dataGridView1.Rows[n].Cells[i].Value = "";
            }
            dataGridView1.Rows[n].Cells[0].Style.BackColor = Color.Yellow;
            dataGridView1.Rows[n + 1].Cells[0].Value = "Sum";
            //dataGridView1.AutoResizeColumns();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m++;
            dataGridView1.ColumnCount = m + 2;
            for (int i = 0; i <= n + 1; i++)
            {
                dataGridView1.Rows[i].Cells[m].Value = "";
            }
            dataGridView1.Rows[0].Cells[m].Style.BackColor = Color.Yellow;
            dataGridView1.Rows[0].Cells[m + 1].Value = "Sum";
            //dataGridView1.AutoResizeColumns();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            for (int i = 1; i < n; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != "")
                {
                    comboBox1.Items.Add(Convert.ToString(dataGridView1.Rows[i].Cells[0].Value));
                    //comboBox3.Items.Add(Convert.ToString(dataGridView1.Rows[i].Cells[0].Value));
                }
            }
            for (int j = 1; j < m; j++)
            {
                if (Convert.ToString(dataGridView1.Rows[0].Cells[j].Value) != "")
                {
                    comboBox1.Items.Add(Convert.ToString(dataGridView1.Rows[0].Cells[j].Value));
                    //comboBox3.Items.Add(Convert.ToString(dataGridView1.Rows[0].Cells[j].Value));
                }
            }
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            for (int i = 1; i < n; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != "")
                {
                    //comboBox1.Items.Add(Convert.ToString(dataGridView1.Rows[i].Cells[0].Value));
                    comboBox3.Items.Add(Convert.ToString(dataGridView1.Rows[i].Cells[0].Value));
                }
            }
            for (int j = 1; j < m; j++)
            {
                if (Convert.ToString(dataGridView1.Rows[0].Cells[j].Value) != "")
                {
                   // comboBox1.Items.Add(Convert.ToString(dataGridView1.Rows[0].Cells[j].Value));
                    comboBox3.Items.Add(Convert.ToString(dataGridView1.Rows[0].Cells[j].Value));
                }
            }

        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("");
            comboBox2.Items.Add("and");
            comboBox2.Items.Add("or");
            comboBox2.Items.Add("|");
            if (comboBox2.SelectedIndex == 0)
            {
                comboBox3.Visible = false;
            }
            else
            {
                comboBox3.Visible = true;
            }
        }

        private void bt_rand_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            for(int i = 1; i < n; i++)
            {
                for(int j = 1; j < m; j++)
                {
                    if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != "" && Convert.ToString(dataGridView1.Rows[0].Cells[j].Value) != "")
                    {
                        dataGridView1.Rows[i].Cells[j].Value = rand.Next(0, 25);
                    }
                }
            }
        }

        private void bt_clear_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = "";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
