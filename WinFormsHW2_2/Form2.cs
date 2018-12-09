using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsHW2_2
{
    public partial class Form2 : Form
    {
        List<string> company = new List<string>();
        BindingSource source = new BindingSource();
        List<Rabotnik> rabotniks;
        List<Rabotnik> rabotniksbyCompany = new List<Rabotnik>();

        bool checkTextChanged = false;
        public Form2(ref List<Rabotnik> rabotniks)
        {
            InitializeComponent();
            dataGridView1.Visible = false;
            this.rabotniks = rabotniks;
            dataGridView1.Font = new Font("Arial", 10);
            company = rabotniks.Select(x => x.CompanyName).Distinct().ToList();
            toolStripComboBox1.ComboBox.DataSource = company;
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (checkTextChanged)
            {
                var rabotniksbyCompany = rabotniks.Where(x => x.CompanyName == toolStripComboBox1.ComboBox.Text).
                    Select(x => new { x.Surname, x.CompanyName, SalaryAverage = x.salaryAverage().ToString("f2"), x.Birthday }).ToList();
                source.DataSource = rabotniksbyCompany;
                dataGridView1.DataSource = source;
                dataGridView1.Visible = true;
            }
            else
                checkTextChanged = true;
        }
    }
}
