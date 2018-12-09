using System.Windows.Forms;

namespace WinFormsHW2_3
{
    public partial class Form2 : Form
    {
        Matrix matrix;
        public Form2(ref Matrix matrix)
        {
            InitializeComponent();
            this.matrix = matrix;
            dataGridView1.ColumnCount = dataGridView2.ColumnCount = matrix.Col;
            dataGridView1.RowCount = dataGridView2.RowCount = matrix.Row;
            for (int i = 0; i < matrix.Row; i++)
            {
                for (int j = 0; j < matrix.Col; j++)
                {
                    dataGridView1[j, i].Value = matrix[j, i];
                    dataGridView1.Columns[j].Width = dataGridView2.Columns[j].Width = 95;
                }
            }
            matrix.ChangeNegCountPos();
            for (int i = 0; i < matrix.Row; i++)
            {
                for (int j = 0; j < matrix.Col; j++)
                {
                    dataGridView2[j, i].Value = matrix[j, i];
                }
            }
        }
    }
}
