using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsHW2_2
{
    public partial class Form1 : Form
    {
        List<Rabotnik> rabotniks = new List<Rabotnik>();
        BindingSource source = new BindingSource();

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.InitialDirectory = Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName;
            openFileDialog1.Filter = "Файлы в формате csv|*.csv";
            openFileDialog1.FileName = "";
            saveFileDialog1.InitialDirectory = Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName;
            saveFileDialog1.Filter = "Файл в формате csv|*.csv";
            saveFileDialog1.FileName = "";
            source.DataSource = rabotniks;
            timer1.Start();
            toolStripStatusLabel2.Text = DateTime.Now.ToString();
            numericUpDown2.Visible = false;
            label2.Visible = false;
        }
        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            source.Clear();
            source.Add(new Rabotnik("", "", new decimal[6], new DateTime()));
            dataGridView1.DataSource = source;
            dataGridView1.Font = new Font("Arial", 10);
            dataGridView1.Visible = true;
            сохранитьToolStripMenuItem.Visible = true;
            обработкаToolStripMenuItem.Visible = true;
            диаграммаToolStripMenuItem.Visible = true;
            label2.Visible = true;
            numericUpDown2.Visible = true;
            numericUpDown2.Value = source.Count;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(openFileDialog1.FileName))
                    {
                        source.Clear();
                        while (!reader.EndOfStream)
                        {
                            string[] temp = reader.ReadLine().Split(';');
                            decimal[] salary = new decimal[6];
                            for (int i = 2, j = 0; i < 8; i++, j++)
                            {
                                salary[j] = Decimal.Parse(temp[i]);
                            }
                            source.Add(new Rabotnik(temp[0], temp[1], salary, DateTime.Parse(temp[8])));
                        }
                    }
                    dataGridView1.DataSource = source;
                    dataGridView1.Visible = true;
                    сохранитьToolStripMenuItem.Visible = true;
                    обработкаToolStripMenuItem.Visible = true;
                    диаграммаToolStripMenuItem.Visible = true;
                    numericUpDown2.Visible = true;
                    label2.Visible = true;
                    numericUpDown2.Value = dataGridView1.RowCount;
                }
                catch
                {
                    MessageBox.Show("Could not read file");
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                {
                    foreach (Rabotnik item in rabotniks)
                    {
                        writer.WriteLine(item.Surname + ";" + item.CompanyName + ";" +
                            item.SalaryForHalfYear+";"+ item.Birthday.ToShortDateString());
                    }
                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Wrong input data by birthday on line " + (e.RowIndex + 1));
            dataGridView1[e.ColumnIndex, e.RowIndex].Value = "";
        }

        private void средняяЗарплатаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(ref rabotniks);
            form2.ShowDialog();
        }

        private void самыйМолодойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime minBirthday = rabotniks.Select(x=>x.Birthday).ToList().Max();
            int minBirthdayRow = rabotniks.FindIndex(x=>x.Birthday==minBirthday);
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1[i, minBirthdayRow].Style.BackColor = Color.Red;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(numericUpDown2.Value);
            if (n > source.Count)
            {
                for (int i = source.Count; i < n; i++)
                {
                    source.Add(new Rabotnik("", "", new decimal[6], new DateTime()));
                }
                dataGridView1.DataSource = source;
            }
            else if (n < source.Count)
            {
                for (int i = source.Count; i > n; i--)
                {
                    source.Remove(source[i-1]);
                }
                dataGridView1.DataSource = source;
            }
        }
    }
}
