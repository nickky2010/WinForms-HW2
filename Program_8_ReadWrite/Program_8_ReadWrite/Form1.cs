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

namespace Program_8_ReadWrite
{
    public partial class Form1 : Form
    {
        List<Dog> dogs = new List<Dog>();
        BindingSource source = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabel2.Text = DateTime.Now.ToString();
            timer1.Start();
            dogs.Add(new Dog());
            dogs.Add(new Dog());
            source.DataSource = dogs;
            dataGridView1.Font = new Font("Arial",10);
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].HeaderText = "Кличка";
            dataGridView1.Columns[1].HeaderText = "Владелец";
            dataGridView1.Columns[2].HeaderText = "Размер в холке";
            openFileDialog1.InitialDirectory = Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName;
            openFileDialog1.Filter = "Файлы в формате csv|*.csv";
            openFileDialog1.FileName = "";
            saveFileDialog1.InitialDirectory = Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName;
        }

        private void новыйToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            поискToolStripMenuItem.Enabled = true;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(openFileDialog1.FileName))
                {
                    source.Clear();
                    while (!reader.EndOfStream)
                    {
                        try
                        {
                            string[] temp = reader.ReadLine().Split(';');
                            source.Add(new Dog() { Name = temp[0], Owner = temp[1], Height = Double.Parse(temp[2]) });
                        }
                        catch
                        {
                            MessageBox.Show("Не удалось прочитать файл");
                        }
                    }
                }
                panel1.Visible = true;
                dataGridView1.DataSource = source;
                dataGridView1.Columns[0].HeaderText = "Кличка";
                dataGridView1.Columns[1].HeaderText = "Владелец";
                dataGridView1.Columns[2].HeaderText = "Размер в холке";
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using(StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                {
                    foreach (Dog item in dogs)
                    {
                        writer.WriteLine(item.Name + ";" + item.Owner + ";" + item.Height);
                    }
                }
            }
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = dogs.Count(d=>d.Height>25).ToString();
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString();
        }
        //добавить собаку
        private void button1_Click(object sender, EventArgs e)
        {
            source.AddNew();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message+" ошибка в строке "+(e.RowIndex+1));
            if(e.ColumnIndex == 2)
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].Value = 0;
            }
            else
                dataGridView1[e.ColumnIndex, e.RowIndex].Value = "";
        }
    }
}
