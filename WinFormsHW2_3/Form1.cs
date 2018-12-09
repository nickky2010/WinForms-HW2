using System;
using System.Windows.Forms;

namespace WinFormsHW2_3
{
    public partial class Form1 : Form
    {
        Matrix matrix;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 1;
            dataGridView1.Columns[0].Width = 95;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = Convert.ToInt32(numericUpDown1.Value);
            dataGridView1.Columns[dataGridView1.ColumnCount - 1].Width = 95;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.RowCount = Convert.ToInt32(numericUpDown2.Value);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CheckMatrix();
                if (checkBox1.Checked)
                {
                    Form2 form2 = new Form2(ref matrix);
                    form2.ShowDialog();
                }
                else
                {
                    matrix.ChangeNegCountPos();
                    for (int i = 0; i < matrix.Row; i++)
                    {
                        for (int j = 0; j < matrix.Col; j++)
                        {
                            dataGridView1[j, i].Value = matrix[j, i];
                        }
                    }
                }
            }
            catch
            {
                const string message = "Values are not numbers!";
                const string caption = "Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void CreatMatrix()
        {
            matrix = new Matrix(dataGridView1.ColumnCount, dataGridView1.RowCount);
            for (int i = 0; i < matrix.Col; i++)
            {
                for (int j = 0; j < matrix.Row; j++)
                {
                    matrix[i, j] = Convert.ToInt32(dataGridView1[i, j].Value);
                }
            }
        }
        private void CheckMatrix()
        {
            if (matrix == null)
                CreatMatrix();
            else
            {
                if (matrix.Col == dataGridView1.ColumnCount &&
                    matrix.Row == dataGridView1.RowCount)
                {
                    for (int i = 0; i < matrix.Col; i++)
                    {
                        for (int j = 0; j < matrix.Row; j++)
                        {
                            if (matrix[i, j] != Convert.ToInt32(dataGridView1[i, j].Value))
                            {
                                CreatMatrix();
                                j = matrix.Row;
                                i = matrix.Col;
                            }
                        }
                    }
                }
                else
                    CreatMatrix();
            }
        }
    }
}
